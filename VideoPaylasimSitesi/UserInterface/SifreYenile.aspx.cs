using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.UserInterface
{
    public partial class SifreYenile : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Kullanıcı oturum kontrolü yap
                int userId = GetUserIdFromCookie();
                if (userId == 0)
                {
                    // Kullanıcı ID'si bulunamadı, giriş sayfasına yönlendir
                    Response.Redirect("Giris.aspx");
                }
            }
        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            int userId = GetUserIdFromCookie();
            if (userId == 0)
            {
                // Kullanıcı ID'si bulunamadı, giriş sayfasına yönlendir
                Response.Redirect("Giris.aspx");
                return;
            }

            string oldPassword = txtOldPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmNewPassword = txtConfirmNewPassword.Text;

            if (newPassword != confirmNewPassword)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Yeni şifreler uyuşmuyor.');", true);
                return;
            }

            Baglanti baglanti = new Baglanti();
            using (SqlConnection conn = baglanti.baglan())
            {
                using (SqlCommand cmd = new SqlCommand("SELECT KullaniciSifre FROM Kullanicilar WHERE KullaniciID = @KullaniciID", conn))
                {
                    cmd.Parameters.AddWithValue("@KullaniciID", userId);
                    string currentPassword = cmd.ExecuteScalar()?.ToString();

                    if (currentPassword != oldPassword)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Eski şifre yanlış.');", true);
                        return;
                    }
                }

                using (SqlCommand cmd = new SqlCommand("UPDATE Kullanicilar SET KullaniciSifre = @YeniSifre WHERE KullaniciID = @KullaniciID", conn))
                {
                    cmd.Parameters.AddWithValue("@YeniSifre", newPassword);
                    cmd.Parameters.AddWithValue("@KullaniciID", userId);
                    cmd.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Şifre başarıyla güncellendi. Ayarların uygulanması için lütfen tekrar giriş yapınız.'); window.location='Giris.aspx';", true);
                }
            }
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
