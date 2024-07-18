using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.Admin
{
    public partial class Kategoriler : System.Web.UI.Page
    {
        Baglanti baglanti = new Baglanti();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                KategoriListele();

            }
        }

        private void KategoriListele()
        {
            try
            {
                string sorgu = "SELECT * FROM Kategoriler";

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



        protected void BtnKategoriEkle_Click(object sender, EventArgs e)
        {
            try
            {
                string kategoriAdi = TxtKAd.Text;
                string kategoriAciklama = TxtKAciklama.Text;
                string kategoriResUrl = TxtKResimUrl.Text;
                string sorgu = "INSERT INTO Kategoriler (KatAd, KatAciklama, KatResUrl) " +
                               "VALUES (@KatAd, @KatAciklama, @KatResUrl)";

                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                komut.Parameters.AddWithValue("@KatAd", kategoriAdi);
                komut.Parameters.AddWithValue("@KatAciklama", kategoriAciklama);
                komut.Parameters.AddWithValue("@KatResUrl", kategoriResUrl);
                komut.ExecuteNonQuery();

                baglanti.baglan().Close();

                // Başarılı güncelleme mesajı ve yönlendirme
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlertAndRedirect",
             "alert('Kategori Başarıyla Eklendi.'); window.location.href = 'Kategoriler.aspx';", true);

                // Verileri yenile
                KategoriListele();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }


    }
}