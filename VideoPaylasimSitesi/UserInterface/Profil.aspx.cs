using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.UserInterface
{
    public partial class Profil : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Kullanıcı bilgilerini yükle
                LoadUserInfo();
            }
        }

        private void LoadUserInfo()
        {
            int userId = GetUserIdFromCookie();
            if (userId == 0)
            {
                // Kullanıcı ID'si bulunamadı, giriş sayfasına yönlendir
                Response.Redirect("Giris.aspx");
                return;
            }

            Baglanti baglanti = new Baglanti();
            using (SqlConnection conn = baglanti.baglan())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT KullaniciAdi, KullaniciSoyadi, KullaniciTakmaAd, KullaniciEmail, KullaniciResUrl FROM Kullanicilar WHERE KullaniciID = @KullaniciID", conn))
                {
                    cmd.Parameters.AddWithValue("@KullaniciID", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            litKullaniciAdi.Text = reader["KullaniciAdi"].ToString();
                            litKullaniciSoyadi.Text = reader["KullaniciSoyadi"].ToString();
                            litKullaniciTakmaAd.Text = reader["KullaniciTakmaAd"].ToString();
                            litKullaniciEmail.Text = reader["KullaniciEmail"].ToString();
                            imgProfilePicture.ImageUrl = reader["KullaniciResUrl"].ToString();

                            txtAdi.Text = litKullaniciAdi.Text;
                            txtSoyadi.Text = litKullaniciSoyadi.Text;
                            txtTakmaAd.Text = litKullaniciTakmaAd.Text;
                            txtEmail.Text = litKullaniciEmail.Text;
                        }
                    }
                }
            }
        }



        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int userId = GetUserIdFromCookie();
            if (userId == 0)
            {
                // Kullanıcı ID'si bulunamadı, giriş sayfasına yönlendir
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
                using (SqlCommand cmd = new SqlCommand("UPDATE Kullanicilar SET KullaniciAdi = @Adi, KullaniciSoyadi = @Soyadi, KullaniciTakmaAd = @TakmaAd, KullaniciEmail = @Email, KullaniciResUrl = @ResUrl WHERE KullaniciID = @KullaniciID", conn))
                {
                    cmd.Parameters.AddWithValue("@Adi", newAdi);
                    cmd.Parameters.AddWithValue("@Soyadi", newSoyadi);
                    cmd.Parameters.AddWithValue("@TakmaAd", newTakmaAd);
                    cmd.Parameters.AddWithValue("@Email", newEmail);
                    cmd.Parameters.AddWithValue("@ResUrl", newResUrl);
                    cmd.Parameters.AddWithValue("@KullaniciID", userId);

                    cmd.ExecuteNonQuery();
                }
            }

            // Güncellenen bilgileri tekrar yükle
            LoadUserInfo();

            // Bilgiler güncellendikten sonra mesaj göster ve yönlendir
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Kullanıcı bilgileri güncellendi. Ayarların aktif olması için tekrar giriş yapınız.'); window.location='Giris.aspx';", true);
        }

        private int GetUserIdFromCookie()
        {
            HttpCookie userCookie = Request.Cookies["UserInfo"];
            if (userCookie != null && userCookie.Values["kullaniciID"] != null)
            {
                int userId;
                if (int.TryParse(userCookie.Values["kullaniciID"], out userId))
                {
                    return userId;
                }
            }
            return 0;
        }
    }
}
