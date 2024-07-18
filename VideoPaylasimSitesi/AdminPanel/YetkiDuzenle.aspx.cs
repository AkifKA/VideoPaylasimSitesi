using System;
using System.Data.SqlClient;
using System.Web.UI;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.AdminPanel
{
    public partial class YetkiDuzenle : System.Web.UI.Page
    {
        Baglanti baglanti = new Baglanti();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Sayfa ilk yüklendiğinde yönetici bilgilerini getir
                if (Request.QueryString["YoneticiID"] != null)
                {
                    int YoneticiID = Convert.ToInt32(Request.QueryString["YoneticiID"]);
                    YoneticiBilgiGetirVeDoldur(YoneticiID);
                }
            }
        }

        private void YoneticiBilgiGetirVeDoldur(int YoneticiID)
        {
            // Veritabanından yönetici yetki bilgilerini getir ve TextBox'lara doldur
            string sorgu = "SELECT YoneticiAdi, YoneticiSoyadi, YoneticiRol FROM Yoneticiler WHERE YoneticiID = @YoneticiID";
            SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
            komut.Parameters.AddWithValue("@YoneticiID", YoneticiID);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                TxtYoneticiAdi.Text = dr["YoneticiAdi"].ToString();
                TxtYoneticiSoyadi.Text = dr["YoneticiSoyadi"].ToString();
                TxtYoneticiYetki.Text = dr["YoneticiRol"].ToString();
            }
            dr.Close();
        }

        protected void BtnYetkiDuzenle_Click1(object sender, EventArgs e)
        {
            try
            {
                // Yönetici güncelleme işlemleri
                int YoneticiID = Convert.ToInt32(Request.QueryString["YoneticiID"]);
                string yeniYoneticiYetki = DrpYoneticiYeniYetki.SelectedValue;

                if (string.IsNullOrEmpty(yeniYoneticiYetki))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", "alert('Lütfen yeni yetki seçiniz.');", true);
                    return;
                }

                string sorgu = "UPDATE Yoneticiler SET YoneticiRol = @YoneticiRol WHERE YoneticiID = @YoneticiID";
                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                komut.Parameters.AddWithValue("@YoneticiRol", yeniYoneticiYetki);
                komut.Parameters.AddWithValue("@YoneticiID", YoneticiID);
                komut.ExecuteNonQuery();

                // Başarılı güncelleme mesajı ve yönlendirme
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlertAndRedirect",
             "alert('Yönetici Yetkisi Başarıyla Güncellendi.'); window.location.href = 'Yoneticiler.aspx';", true);

                baglanti.baglan().Close();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }
    }
}
