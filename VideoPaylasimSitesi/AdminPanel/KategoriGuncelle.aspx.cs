using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.AdminPanel
{
    public partial class KategoriGuncelle : System.Web.UI.Page
    {
        Baglanti baglanti = new Baglanti();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Sayfa ilk yüklendiğinde kategori bilgilerini getir
                if (Request.QueryString["KatID"] != null)
                {
                    int katID = Convert.ToInt32(Request.QueryString["KatID"]);
                    KategoriGetirVeDoldur(katID);
                }
            }
        }

        private void KategoriGetirVeDoldur(int katID)
        {
            // Veritabanından kategori bilgilerini getir ve TextBox'lara doldur
            string sorgu = "SELECT KatAd, KatAciklama, KatResUrl FROM Kategoriler WHERE KatID = @KatID";
            SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
            komut.Parameters.AddWithValue("@KatID", katID);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                TxtKAd.Text = dr["KatAd"].ToString();
                TxtKAciklama.Text = dr["KatAciklama"].ToString();
                TxtKResimUrl.Text = dr["KatResUrl"].ToString();
            }
            dr.Close();

        }

        protected void BtnKategoriGuncelle_Click(object sender, EventArgs e)
        {
            // Kategori güncelleme işlemleri
            int katID = Convert.ToInt32(Request.QueryString["KatID"]);
            string yeniKategoriAdi = TxtKAd.Text;
            string yeniKategoriAciklama = TxtKAciklama.Text;
            string yeniKategoriResUrl = TxtKResimUrl.Text;

            string sorgu = "UPDATE Kategoriler SET KatAd = @KatAd, KatAciklama = @KatAciklama, KatResUrl = @KatResUrl WHERE KatID = @KatID";
            SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());

            komut.Parameters.AddWithValue("@KatAd", yeniKategoriAdi);
            komut.Parameters.AddWithValue("@KatAciklama", yeniKategoriAciklama);
            komut.Parameters.AddWithValue("@KatResUrl", yeniKategoriResUrl);
            komut.Parameters.AddWithValue("@KatID", katID);
            komut.ExecuteNonQuery();
            baglanti.baglan().Close();

            // Başarılı güncelleme mesajı ve yönlendirme
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlertAndRedirect",
         "alert('Kategori Başarıyla Güncellendi.'); window.location.href = 'Kategoriler.aspx';", true);
        }
    }
}