using System;
using System.Data.SqlClient;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.AdminPanel
{
    public partial class Sil : System.Web.UI.Page
    {
        Baglanti baglanti = new Baglanti();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && Request.QueryString["sorgu"] != null && Request.QueryString["type"] != null)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    string sorgu = Request.QueryString["sorgu"];
                    string type = Request.QueryString["type"];
                    Delete(id, sorgu, type);
                }
                else
                {
                    ErrorMessage.Visible = true;
                    ErrorMessage.Text = "Hata: Eksik parametreler.";
                }
            }
        }

        private void Delete(int id, string sorgu, string type)
        {
            try
            {
                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                komut.Parameters.AddWithValue("@ID", id);
                komut.ExecuteNonQuery();
                baglanti.baglan().Close();

                SuccessMessage.Visible = true;
                SuccessMessage.Text = "Başarıyla silindi. 3 saniye içinde yönlendirileceksiniz.";

                string redirectUrl = "";

                if (type == "video")
                    redirectUrl = "Videolar.aspx";
                else if (type == "altkategori")
                    redirectUrl = "AltKategoriler.aspx";
                else if (type == "kategori")
                    redirectUrl = "Kategoriler.aspx";
                else if (type == "yorum")
                    redirectUrl = "Yorumlar.aspx";
                else
                    redirectUrl = "Yoneticiler.aspx";

                string script = $"startCountdown('{redirectUrl}', 3000);";
                ClientScript.RegisterStartupScript(this.GetType(), "redirect", script, true);
            }
            catch (Exception ex)
            {
                ErrorMessage.Visible = true;
                ErrorMessage.Text = "Hata: " + ex.Message;

                string script = "document.getElementById('countdown').style.display = 'none';";
                ClientScript.RegisterStartupScript(this.GetType(), "hideCountdown", script, true);
            }
        }
    }
}
