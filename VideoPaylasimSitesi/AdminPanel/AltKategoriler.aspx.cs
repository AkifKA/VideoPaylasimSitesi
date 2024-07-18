using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.AdminPanel
{
    public partial class AltKategoriler : System.Web.UI.Page
    {
        Baglanti baglanti = new Baglanti();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AltKategoriListele();
                UstKategoriGetir();
            }
        }

        private void AltKategoriListele()
        {
            try
            {
                string sorgu = @"
                    SELECT a.AltKatID, a.AltKatAd, a.AltKatAciklama, a.AltKatResUrl, k.KatAd 
                    FROM AltKategoriler a 
                    LEFT JOIN Kategoriler k ON k.KatID = a.KategoriID";

                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                SqlDataReader dr = komut.ExecuteReader();
                Repeater1.DataSource = dr;
                Repeater1.DataBind();
                dr.Close();
                baglanti.baglan().Close();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }

        protected void UstKategoriGetir()
        {
            try
            {
                string sorgu = "SELECT * FROM Kategoriler";

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

        protected void BtnAltKategoriEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(TxtAltKAd.Text) ||
                    string.IsNullOrWhiteSpace(TxtAltKAciklama.Text) ||
                    string.IsNullOrWhiteSpace(TxtAltKResimUrl.Text) ||
                    string.IsNullOrWhiteSpace(DrpUstKategori.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlert", $"alert('Tüm alanları doldurduğunuzdan emin olun.');", true);
                    return;
                }

                string altKategoriAdi = TxtAltKAd.Text;
                string altKategoriAciklama = TxtAltKAciklama.Text;
                string altKategoriResUrl = TxtAltKResimUrl.Text;
                string ustKategori = DrpUstKategori.SelectedValue;

                string sorgu = @"
                    INSERT INTO AltKategoriler (AltKatAd, AltKatAciklama, AltKatResUrl, KategoriID) 
                    VALUES (@AltKatAd, @AltKatAciklama, @AltKatResUrl, @KategoriID)";

                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                komut.Parameters.AddWithValue("@AltKatAd", altKategoriAdi);
                komut.Parameters.AddWithValue("@AltKatAciklama", altKategoriAciklama);
                komut.Parameters.AddWithValue("@AltKatResUrl", altKategoriResUrl);
                komut.Parameters.AddWithValue("@KategoriID", ustKategori);
                komut.ExecuteNonQuery();
                baglanti.baglan().Close();

                // Başarılı ekleme mesajı ve yönlendirme
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlertAndRedirect",
                 "alert('Alt Kategori Başarıyla Eklendi.'); window.location.href = 'AltKategoriler.aspx';", true);

                // Verileri yenile
                AltKategoriListele();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }
    }
}
