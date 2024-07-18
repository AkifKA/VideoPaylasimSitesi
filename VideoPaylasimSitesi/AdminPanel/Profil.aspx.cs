using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.AdminPanel
{
    public partial class Profil : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Yonetici bilgilerini yükle
                LoadUserInfo();
            }
        }

        private void LoadUserInfo()
        {
            int yoneticiId = GetYoneticiIdFromSession();
            if (yoneticiId == 0)
            {
                // Yonetici ID'si bulunamadı, giriş sayfasına yönlendir
                Response.Redirect("Giris.aspx");
                return;
            }

            Baglanti baglanti = new Baglanti();
            using (SqlConnection conn = baglanti.baglan())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT YoneticiAdi, YoneticiSoyadi, YoneticiTakmaAd, YoneticiEmail, YoneticiRol, YoneticiResUrl FROM Yoneticiler WHERE YoneticiID = @YoneticiID", conn))
                {
                    cmd.Parameters.AddWithValue("@YoneticiID", yoneticiId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            litYoneticiAdi.Text = reader["YoneticiAdi"].ToString();
                            litYoneticiSoyadi.Text = reader["YoneticiSoyadi"].ToString();
                            litYoneticiTakmaAd.Text = reader["YoneticiTakmaAd"].ToString();
                            litYoneticiEmail.Text = reader["YoneticiEmail"].ToString();
                            litYoneticiRol.Text = reader["YoneticiRol"].ToString();
                            imgProfilePicture.ImageUrl = reader["YoneticiResUrl"].ToString();

                            txtAdi.Text = litYoneticiAdi.Text;
                            txtSoyadi.Text = litYoneticiSoyadi.Text;
                            txtTakmaAd.Text = litYoneticiTakmaAd.Text;
                            txtEmail.Text = litYoneticiEmail.Text;
                        }
                    }
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int yoneticiId = GetYoneticiIdFromSession();
            if (yoneticiId == 0)
            {
                // Yonetici ID'si bulunamadı, giriş sayfasına yönlendir
                Response.Redirect("Giris.aspx");
                return;
            }

            string newAdi = txtAdi.Text;
            string newSoyadi = txtSoyadi.Text;
            string newTakmaAd = txtTakmaAd.Text;
            string newEmail = txtEmail.Text;
            string newResUrl = imgProfilePicture.ImageUrl; // Default olarak mevcut resim URL'sini kullan

            if (fileUpload.HasFile)
            {
                // Yeni profil resmi yüklendi
                string filePath = "ResimYuklemeYeri/" + fileUpload.FileName;
                fileUpload.SaveAs(Server.MapPath(filePath));
                newResUrl = filePath;
            }

            Baglanti baglanti = new Baglanti();
            using (SqlConnection conn = baglanti.baglan())
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE Yoneticiler SET YoneticiAdi = @Adi, YoneticiSoyadi = @Soyadi, YoneticiTakmaAd = @TakmaAd, YoneticiEmail = @Email, YoneticiResUrl = @ResUrl WHERE YoneticiID = @YoneticiID", conn))
                {
                    cmd.Parameters.AddWithValue("@Adi", newAdi);
                    cmd.Parameters.AddWithValue("@Soyadi", newSoyadi);
                    cmd.Parameters.AddWithValue("@TakmaAd", newTakmaAd);
                    cmd.Parameters.AddWithValue("@Email", newEmail);
                    cmd.Parameters.AddWithValue("@ResUrl", newResUrl);
                    cmd.Parameters.AddWithValue("@YoneticiID", yoneticiId);

                    cmd.ExecuteNonQuery();
                }
            }

            // Güncellenen bilgileri tekrar yükle
            LoadUserInfo();

            // Bilgiler güncellendikten sonra mesaj göster ve yönlendir
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Yönetici bilgileri güncellendi. Ayarların aktif olması için tekrar giriş yapınız.'); window.location='Cikis.aspx';", true);
        }

        private int GetYoneticiIdFromSession()
        {
            if (Session["yoneticiID"] != null)
            {
                int yoneticiId;
                if (int.TryParse(Session["yoneticiID"].ToString(), out yoneticiId))
                {
                    return yoneticiId;
                }
            }
            return 0;
        }
    }
}
