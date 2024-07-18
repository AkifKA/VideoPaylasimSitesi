using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.UserInterface
{
    public partial class VideoDetay : System.Web.UI.Page
    {
        Baglanti baglanti = new Baglanti();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["VideoID"] != null && Request.Cookies["UserInfo"] != null)
                {
                    int videoID = Convert.ToInt32(Request.QueryString["VideoID"]);
                    int kullaniciID = Convert.ToInt32(Request.Cookies["UserInfo"]["kullaniciID"]);

                    VideoDetayGetir(videoID);
                    YorumlariGetir(videoID);
                    GetUserFullName();
                    CheckLikeStatus(videoID, kullaniciID);
                }
            }
            else
            {
                if (Request.Form["__EVENTTARGET"] == "UpdatePanel1")
                {
                    int videoID = Convert.ToInt32(Request.QueryString["VideoID"]);
                    YorumlariGetir(videoID);
                }
            }
        }

        private void VideoDetayGetir(int videoID)
        {
            string sorgu = @"
        SELECT 
            v.VideoID, 
            v.VideoBaslik, 
            v.VideoAciklama, 
            v.VideoFrameUrl, 
            (SELECT COUNT(*) FROM Begeniler WHERE VideoID = v.VideoID AND BegeniDurumu = 1) AS LikeCount,
            (SELECT CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END FROM Begeniler WHERE VideoID = v.VideoID AND KullaniciID = @KullaniciID) AS IsLiked 
        FROM Videolar v
        WHERE v.VideoID = @VideoID";

            using (SqlConnection conn = baglanti.baglan())
            {
                SqlCommand komut = new SqlCommand(sorgu, conn);
                komut.Parameters.AddWithValue("@VideoID", videoID);
                komut.Parameters.AddWithValue("@KullaniciID", Convert.ToInt32(Request.Cookies["UserInfo"]["kullaniciID"]));
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                da.Fill(dt);
                rptVideo.DataSource = dt;
                rptVideo.DataBind();
            }
        }

        private void YorumlariGetir(int videoID)
        {
            string sorgu = @"
        SELECT 
            Yorumlar.YorumID, 
            Yorumlar.VideoID, 
            Yorumlar.KullaniciID, 
            Yorumlar.YorumMetni, 
            Yorumlar.YorumTarihi,
            Kullanicilar.KullaniciAdi, 
            Kullanicilar.KullaniciSoyadi, 
            Kullanicilar.KullaniciResUrl 
        FROM Yorumlar 
        INNER JOIN Kullanicilar 
            ON Yorumlar.KullaniciID = Kullanicilar.KullaniciID 
        WHERE Yorumlar.VideoID = @VideoID AND Yorumlar.YorumDurum = 1
        ORDER BY 
            CASE 
                WHEN Yorumlar.KullaniciID = @KullaniciID THEN Yorumlar.YorumTarihi END DESC,
            CASE 
                WHEN Yorumlar.KullaniciID != @KullaniciID THEN Yorumlar.YorumTarihi END ASC";

            using (SqlConnection conn = baglanti.baglan())
            {
                SqlCommand komut = new SqlCommand(sorgu, conn);
                komut.Parameters.AddWithValue("@VideoID", videoID);
                komut.Parameters.AddWithValue("@KullaniciID", Convert.ToInt32(Request.Cookies["UserInfo"]["kullaniciID"]));
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(komut);
                da.Fill(dt);
                rptYorumlar.DataSource = dt;
                rptYorumlar.DataBind();
            }
        }

        [WebMethod]
        public static string AddComment(int videoID, string yorumMetni)
        {
            try
            {
                HttpCookie userInfoCookie = HttpContext.Current.Request.Cookies["UserInfo"];
                if (userInfoCookie == null || !int.TryParse(userInfoCookie["kullaniciID"], out int kullaniciID))
                {
                    throw new Exception("Kullanıcı bilgisi bulunamadı.");
                }

                Baglanti baglanti = new Baglanti();
                using (SqlConnection conn = baglanti.baglan())
                {
                    string sorgu = "INSERT INTO Yorumlar (VideoID, KullaniciID, YorumMetni, YorumTarihi, YorumDurum) VALUES (@VideoID, @KullaniciID, @YorumMetni, @YorumTarihi, @YorumDurum)";
                    SqlCommand cmd = new SqlCommand(sorgu, conn);
                    cmd.Parameters.AddWithValue("@VideoID", videoID);
                    cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                    cmd.Parameters.AddWithValue("@YorumMetni", yorumMetni);
                    cmd.Parameters.AddWithValue("@YorumTarihi", DateTime.Now);
                    cmd.Parameters.AddWithValue("@YorumDurum", false); // Yönetici onayı bekliyor

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return JsonConvert.SerializeObject(new { success = true });
                    }
                    else
                    {
                        throw new Exception("Yorum eklenemedi.");
                    }
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { error = ex.Message });
            }
        }




        [WebMethod]
        public static string UpdateComment(int yorumID, string yorumMetni)
        {
            try
            {
                Baglanti baglanti = new Baglanti();
                using (SqlConnection conn = baglanti.baglan())
                {
                    string sorgu = "UPDATE Yorumlar SET YorumMetni = @YorumMetni WHERE YorumID = @YorumID";
                    SqlCommand cmd = new SqlCommand(sorgu, conn);
                    cmd.Parameters.AddWithValue("@YorumID", yorumID);
                    cmd.Parameters.AddWithValue("@YorumMetni", yorumMetni);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return JsonConvert.SerializeObject(new { success = true });
                    }
                    else
                    {
                        throw new Exception("Yorum güncellenemedi. YorumID: " + yorumID + ", YorumMetni: " + yorumMetni);
                    }
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { error = "Exception: " + ex.Message });
            }
        }

        protected void rptYorumlar_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Sil")
            {
                int yorumID = Convert.ToInt32(e.CommandArgument);
                YorumSil(yorumID);
                int videoID = Convert.ToInt32(Request.QueryString["VideoID"]);
                YorumlariGetir(videoID); // Yorumları güncelle
            }
        }

        private void YorumSil(int yorumID)
        {
            try
            {
                using (SqlConnection conn = baglanti.baglan())
                {
                    string sorgu = "DELETE FROM Yorumlar WHERE YorumID = @YorumID";
                    SqlCommand cmd = new SqlCommand(sorgu, conn);
                    cmd.Parameters.AddWithValue("@YorumID", yorumID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        ShowErrorMessage("Yorum başarıyla silindi.", false);
                    }
                    else
                    {
                        ShowErrorMessage("Yorum silinemedi.");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Hata: " + ex.Message);
            }
        }

        protected void rptYorumlar_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dataItem = (DataRowView)e.Item.DataItem;
                int yorumKullaniciID = Convert.ToInt32(dataItem["KullaniciID"]);
                int girisYapanKullaniciID = Convert.ToInt32(Request.Cookies["UserInfo"]["kullaniciID"]);

                LinkButton btnYorumDuzenle = (LinkButton)e.Item.FindControl("btnYorumDuzenle");
                LinkButton btnYorumSil = (LinkButton)e.Item.FindControl("btnYorumSil");

                if (yorumKullaniciID == girisYapanKullaniciID)
                {
                    btnYorumDuzenle.Visible = true;
                    btnYorumSil.Visible = true;
                    btnYorumDuzenle.OnClientClick = $"editComment({dataItem["YorumID"]}, '{dataItem["YorumMetni"]}'); return false;";
                }
            }
        }

        private string GetKullaniciYorumu(int videoID, int kullaniciID)
        {
            string yorumMetni = string.Empty;
            using (SqlConnection conn = baglanti.baglan())
            {
                string sorgu = "SELECT YorumMetni FROM Yorumlar WHERE VideoID = @VideoID AND KullaniciID = @KullaniciID YorumDurum=1";
                SqlCommand cmd = new SqlCommand(sorgu, conn);
                cmd.Parameters.AddWithValue("@VideoID", videoID);
                cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    yorumMetni = result.ToString();
                }
            }
            return yorumMetni;
        }

        public string GetLikeIconClass(bool isLiked)
        {
            return isLiked ? "fa-solid" : "fa-regular";
        }

        [WebMethod]
        public static string ToggleLike(int videoID)
        {
            try
            {
                int kullaniciID = 0;
                HttpCookie userInfoCookie = HttpContext.Current.Request.Cookies["UserInfo"];

                if (userInfoCookie != null && int.TryParse(userInfoCookie["kullaniciID"], out kullaniciID))
                {
                    Baglanti baglanti = new Baglanti();
                    using (SqlConnection conn = baglanti.baglan())
                    {
                        using (SqlCommand cmd = new SqlCommand("begeni_Toggle2", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                            cmd.Parameters.AddWithValue("@VideoID", videoID);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    int likeCount = reader.GetInt32(0);
                                    bool isLiked = reader.GetBoolean(1);

                                    return JsonConvert.SerializeObject(new { likeCount, isLiked });
                                }
                            }
                        }
                    }
                }

                return JsonConvert.SerializeObject(new { error = "Kullanıcı bilgisi bulunamadı." });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { error = ex.Message });
            }
        }

        private void CheckLikeStatus(int videoID, int kullaniciID)
        {
            Baglanti baglanti = new Baglanti();
            using (SqlConnection conn = baglanti.baglan())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT BegeniDurumu FROM Begeniler WHERE KullaniciID = @KullaniciID AND VideoID = @VideoID", conn))
                {
                    cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                    cmd.Parameters.AddWithValue("@VideoID", videoID);

                    object result = cmd.ExecuteScalar();
                    bool isLiked = result != null && Convert.ToBoolean(result);

                    ViewState["IsLiked_" + videoID] = isLiked;
                }
            }
        }

        protected string GetUserFullName()
        {
            string kullaniciAd = string.Empty;
            string kullaniciSoyad = string.Empty;
            HttpCookie userCookie = Request.Cookies["UserInfo"];
            if (userCookie != null)
            {
                kullaniciAd = Server.UrlDecode(userCookie["kullaniciAd"]);
                kullaniciSoyad = Server.UrlDecode(userCookie["kullaniciSoyad"]);
            }
            return kullaniciAd + " " + kullaniciSoyad;
        }

        protected string GetUserProfileImageUrl()
        {
            HttpCookie userCookie = Request.Cookies["UserInfo"];
            if (userCookie != null && userCookie["kullaniciResUrl"] != null)
            {
                return userCookie["kullaniciResUrl"];
            }
            return "varsayilan.jpg";
        }

        private void ShowErrorMessage(string message, bool isError = true)
        {
            var script = $@"
        <script type='text/javascript'>
            document.getElementById('{(isError ? "error-message" : "success-message")}').style.display = 'block';
            document.getElementById('{(isError ? "error-message" : "success-message")}').innerHTML = '{message}';
        </script>";
            ScriptManager.RegisterStartupScript(this, GetType(), "ShowErrorMessage", script, false);
        }
    }
}
