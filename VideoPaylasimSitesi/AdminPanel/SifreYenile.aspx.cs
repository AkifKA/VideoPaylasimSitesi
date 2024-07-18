using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.AdminPanel
{
    public partial class SifreYenile : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Kullanıcı oturum kontrolü yap
                int userId = GetYoneticiIdFromSession();
                if (userId == 0)
                {
                    // Kullanıcı ID'si bulunamadı, giriş sayfasına yönlendir
                    Response.Redirect("Giris.aspx");
                }
            }
        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            int userId = GetYoneticiIdFromSession();
            if (userId == 0)
            {
                // Kullanıcı ID'si bulunamadı, giriş sayfasına yönlendir
                Response.Redirect("Giris.aspx");
                return;
            }

            string currentPassword = txtCurrentPassword.Text;
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
                using (SqlCommand cmd = new SqlCommand("SELECT YoneticiSifre FROM Yoneticiler WHERE YoneticiID = @YoneticiID", conn))
                {
                    cmd.Parameters.AddWithValue("@YoneticiID", userId);
                    string currentDbPassword = cmd.ExecuteScalar()?.ToString();

                    if (currentDbPassword != currentPassword)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Mevcut şifre yanlış.');", true);
                        return;
                    }
                }

                using (SqlCommand cmd = new SqlCommand("UPDATE Yoneticiler SET YoneticiSifre = @YeniSifre WHERE YoneticiID = @YoneticiID", conn))
                {
                    cmd.Parameters.AddWithValue("@YeniSifre", newPassword);
                    cmd.Parameters.AddWithValue("@YoneticiID", userId);
                    cmd.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Şifre başarıyla güncellendi.'); window.location='Giris.aspx';", true);
                }
            }
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
