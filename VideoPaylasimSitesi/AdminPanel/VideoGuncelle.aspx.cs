using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.AdminPanel
{
    public partial class VideoGuncelle : System.Web.UI.Page
    {
        Baglanti baglanti = new Baglanti();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["VideoID"] != null)
                {
                    int VideoID = Convert.ToInt32(Request.QueryString["VideoID"]);
                    AltKategoriGetir();
                    VideoGetirVeDoldur(VideoID);
                }
            }
        }

        private void VideoGetirVeDoldur(int VideoID)
        {
            // Veritabanından video bilgilerini getir ve TextBox'lara doldur
            string sorgu = "SELECT * FROM Videolar WHERE VideoID = @VideoID";
            using (SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan()))
            {
                komut.Parameters.AddWithValue("@VideoID", VideoID);
                using (SqlDataReader dr = komut.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        TxtVideoBaslik.Text = dr["VideoBaslik"].ToString();
                        TxtVideoAciklama.Text = dr["VideoAciklama"].ToString();
                        TxtVideoUrl.Text = dr["VideoUrl"].ToString();
                        TxtVideoFrameUrl.Text = dr["VideoFrameUrl"].ToString();
                        DrpVideoKategoriID.SelectedValue = dr["AltKategoriID"].ToString();
                    }
                }
            }
        }

        protected void AltKategoriGetir()
        {
            try
            {
                string sorgu = "SELECT AltKatID, AltKatAd FROM AltKategoriler ORDER BY AltKatAd";

                using (SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan()))
                {
                    using (SqlDataReader dr = komut.ExecuteReader())
                    {
                        DrpVideoKategoriID.DataSource = dr;
                        DrpVideoKategoriID.DataTextField = "AltKatAd";
                        DrpVideoKategoriID.DataValueField = "AltKatID";
                        DrpVideoKategoriID.DataBind();
                    }
                }
                DrpVideoKategoriID.Items.Insert(0, new ListItem("-- Seçiniz --", ""));
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }

        protected void BtnVideoGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                // Video güncelleme işlemleri
                int VideoID = Convert.ToInt32(Request.QueryString["VideoID"]);

                // Girişlerin doğruluğunu kontrol et
                if (VideoID == 0 || string.IsNullOrWhiteSpace(TxtVideoBaslik.Text) ||
                    string.IsNullOrWhiteSpace(TxtVideoAciklama.Text) ||
                    string.IsNullOrWhiteSpace(TxtVideoUrl.Text) ||
                    string.IsNullOrWhiteSpace(TxtVideoFrameUrl.Text) ||
                    string.IsNullOrWhiteSpace(DrpVideoKategoriID.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", $"alert('Tüm alanları doldurduğunuzdan emin olun.');", true);
                    return;
                }

                string yeniVideoBaslik = TxtVideoBaslik.Text;
                string yeniVideoAciklama = TxtVideoAciklama.Text;
                string yeniVideoUrl = TxtVideoUrl.Text;
                string yeniVideoFrameUrl = TxtVideoFrameUrl.Text;
                int yeniVideoKategoriID = Convert.ToInt32(DrpVideoKategoriID.SelectedValue);

                string sorgu = "UPDATE Videolar SET VideoBaslik = @VideoBaslik, VideoAciklama = @VideoAciklama, VideoUrl = @VideoUrl, VideoFrameUrl = @VideoFrameUrl, AltKategoriID = @AltKategoriID WHERE VideoID = @VideoID";

                using (SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan()))
                {
                    komut.Parameters.AddWithValue("@VideoBaslik", yeniVideoBaslik);
                    komut.Parameters.AddWithValue("@VideoAciklama", yeniVideoAciklama);
                    komut.Parameters.AddWithValue("@VideoUrl", yeniVideoUrl);
                    komut.Parameters.AddWithValue("@VideoFrameUrl", yeniVideoFrameUrl);
                    komut.Parameters.AddWithValue("@AltKategoriID", yeniVideoKategoriID);
                    komut.Parameters.AddWithValue("@VideoID", VideoID);

                    int affectedRows = komut.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        // Başarılı güncelleme mesajı ve yönlendirme
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlertAndRedirect",
                    "alert('Video Başarıyla Güncellendi.'); window.location.href = 'Videolar.aspx';", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", $"alert('Güncelleme başarısız. Lütfen tekrar deneyin.');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", $"alert('Hata: {ex.Message}');", true);
            }
        }
    }
}
