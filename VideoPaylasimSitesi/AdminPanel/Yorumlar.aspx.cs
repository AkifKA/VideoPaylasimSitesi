using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.AdminPanel
{
    public partial class Yorumlar : System.Web.UI.Page
    {
        Baglanti baglanti = new Baglanti();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                YorumlariListele();
            }
        }

        private void YorumlariListele()
        {
            try
            {
                string sorgu = @"
                    SELECT y.YorumID, y.YorumMetni, y.YorumTarihi, 
                           k.KullaniciTakmaAd, 
                           v.VideoBaslik,
                           y.YorumDurum
                    FROM Yorumlar y
                    LEFT JOIN Kullanicilar k ON y.KullaniciID = k.KullaniciID
                    LEFT JOIN Videolar v ON y.VideoID = v.VideoID
                    ORDER BY y.YorumTarihi DESC";

                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                SqlDataReader dr = komut.ExecuteReader();
                rptYorumlar.DataSource = dr;
                rptYorumlar.DataBind();
                dr.Close();

                // Toplam yorum sayısını al ve Label'a yaz
                string toplamYorumSorgu = "SELECT COUNT(*) FROM Yorumlar";
                SqlCommand toplamYorumKomut = new SqlCommand(toplamYorumSorgu, baglanti.baglan());
                int toplamYorumSayisi = (int)toplamYorumKomut.ExecuteScalar();
                lblToplamYorum.Text = toplamYorumSayisi.ToString();

                baglanti.baglan().Close();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }

        protected void rptYorumlar_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Onayla")
            {
                int yorumID = Convert.ToInt32(e.CommandArgument);
                bool isApproved = YorumOnayla(yorumID);

                if (isApproved)
                {
                    ShowSuccessMessage("Yorum başarıyla onaylandı.");
                }
                else
                {
                    ShowErrorMessage("Yorum onaylanırken bir hata oluştu.");
                }

                YorumlariListele();
            }
        }

        protected void rptYorumlar_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var yorumDurum = (bool)DataBinder.Eval(e.Item.DataItem, "YorumDurum");
                var btnOnayla = (Button)e.Item.FindControl("btnOnayla");

                if (yorumDurum)
                {
                    btnOnayla.Visible = false;
                }
            }
        }

        private bool YorumOnayla(int yorumID)
        {
            try
            {
                using (SqlConnection conn = baglanti.baglan())
                {
                    string sorgu = "UPDATE Yorumlar SET YorumDurum = 1 WHERE YorumID = @YorumID";
                    SqlCommand cmd = new SqlCommand(sorgu, conn);
                    cmd.Parameters.AddWithValue("@YorumID", yorumID);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Hata mesajını Response'a yazmak yerine ShowErrorMessage metodunu kullan
                ShowErrorMessage("Hata: " + ex.Message);
                return false;
            }
        }
        public string GetUserProfileImageUrl()
        {
            HttpCookie userCookie = Request.Cookies["UserInfo"];
            if (userCookie != null && userCookie["kullaniciResUrl"] != null)
            {
                return userCookie["kullaniciResUrl"];
            }
            return "Resimler/varsayilan.jpg"; // Varsayılan resim yolu
        }

        private void ShowSuccessMessage(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('{message}');", true);
        }

        private void ShowErrorMessage(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('{message}');", true);
        }
    }
}
