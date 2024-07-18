using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.UserInterface
{
    public partial class index : Page
    {
        Baglanti baglanti = new Baglanti();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VideoListele();
                AltKategoriListele();
                KategoriListele();
            }
        }

        private void VideoListele()
        {
            try
            {
                string sorgu = "SELECT v.VideoID, v.VideoBaslik, v.VideoAciklama, v.VideoUrl, a.AltKatAd FROM Videolar v  LEFT JOIN AltKategoriler a ON v.AltKategoriID=a.AltKatID";
              
                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                Repeater1.DataSource = dt;
                Repeater1.DataBind();

                Repeater2.DataSource = dt;
                Repeater2.DataBind();


                IndicatorRepeater.DataSource = dt;
                IndicatorRepeater.DataBind();

                baglanti.baglan().Close();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }

        private void AltKategoriListele()
        {
            try
            {
                string sorgu = "SELECT ak.AltKatID, ak.AltKatAd, ak.AltKatAciklama, ak.AltKatResUrl, k.KatAd FROM AltKategoriler ak LEFT JOIN Kategoriler k ON ak.KategoriID = k.KatID";

                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                Repeater3.DataSource = dt;
                Repeater3.DataBind();

                baglanti.baglan().Close();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }


        private void KategoriListele()
        {
            try
            {
                string sorgu = "SELECT * FROM Kategoriler";
                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                Repeater4.DataSource = dt;
                Repeater4.DataBind();

                baglanti.baglan().Close();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }
    }
}
