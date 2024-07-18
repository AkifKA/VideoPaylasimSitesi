using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.AdminPanel
{
    public partial class AltKategoriGuncelle : System.Web.UI.Page
    {
        Baglanti baglanti = new Baglanti();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UstKategoriGetir();
                if (Request.QueryString["AltKatID"] != null)
                {
                    int AltKatID = Convert.ToInt32(Request.QueryString["AltKatID"]);
                    AltKategoriGetirVeDoldur(AltKatID);
                }
            }
        }

        private void AltKategoriGetirVeDoldur(int AltKatID)
        {
            string sorgu = "SELECT * FROM AltKategoriler WHERE AltKatID = @AltKatID";
            SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
            komut.Parameters.AddWithValue("@AltKatID", AltKatID);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                TxtAltKAd.Text = dr["AltKatAd"].ToString();
                TxtAltKAciklama.Text = dr["AltKatAciklama"].ToString();
                TxtAltKResimUrl.Text = dr["AltKatResUrl"].ToString();
                if (DrpUstKategori.Items.FindByValue(dr["KategoriID"].ToString()) != null)
                {
                    DrpUstKategori.SelectedValue = dr["KategoriID"].ToString();
                }
            }
            dr.Close();
        }

        protected void UstKategoriGetir()
        {
            try
            {
                string sorgu = "SELECT KatID, KatAd FROM Kategoriler";
                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                SqlDataReader dr = komut.ExecuteReader();
                DrpUstKategori.DataSource = dr;
                DrpUstKategori.DataTextField = "KatAd";
                DrpUstKategori.DataValueField = "KatID";
                DrpUstKategori.DataBind();
                dr.Close();
                baglanti.baglan().Close();

                // Seçiniz seçeneğini ekleyin
                DrpUstKategori.Items.Insert(0, new ListItem("-- Seçiniz --", ""));
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }

        protected void BtnKategoriGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                int AltKatID = Convert.ToInt32(Request.QueryString["AltKatID"]);
                string yeniAltKategoriAdi = TxtAltKAd.Text;
                string yeniAltKategoriAciklama = TxtAltKAciklama.Text;
                string yeniAltKategoriResUrl = TxtAltKResimUrl.Text;
                string yeniUstKategoriID = DrpUstKategori.SelectedValue;

                if (string.IsNullOrWhiteSpace(yeniAltKategoriAdi) ||
                    string.IsNullOrWhiteSpace(yeniAltKategoriAciklama) ||
                    string.IsNullOrWhiteSpace(yeniAltKategoriResUrl) ||
                    string.IsNullOrWhiteSpace(yeniUstKategoriID))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", $"alert('Tüm alanları doldurduğunuzdan emin olun.');", true);
                    return;
                }

                string sorgu = "UPDATE AltKategoriler SET AltKatAd = @AltKatAd, AltKatAciklama = @AltKatAciklama, AltKatResUrl = @AltKatResUrl, KategoriID = @KategoriID WHERE AltKatID = @AltKatID";
                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                komut.Parameters.AddWithValue("@AltKatAd", yeniAltKategoriAdi);
                komut.Parameters.AddWithValue("@AltKatAciklama", yeniAltKategoriAciklama);
                komut.Parameters.AddWithValue("@AltKatResUrl", yeniAltKategoriResUrl);
                komut.Parameters.AddWithValue("@KategoriID", yeniUstKategoriID);
                komut.Parameters.AddWithValue("@AltKatID", AltKatID);
                komut.ExecuteNonQuery();

                // Başarılı güncelleme mesajı ve yönlendirme
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlertAndRedirect",
                    "alert('Alt Kategori Başarıyla Güncellendi.'); window.location.href = 'AltKategoriler.aspx';", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", $"alert('Hata: {ex.Message}');", true);
            }
        }
    }
}
