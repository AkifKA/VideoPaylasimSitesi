using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using VideoPaylasimSitesi.Class;
using System.Data;

namespace VideoPaylasimSitesi.AdminPanel
{
    public partial class index : System.Web.UI.Page
    {
        Baglanti baglanti = new Baglanti();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VideoListele();
                AltKategoriListele();
                KategoriListele();
                ToplamVideoSayisi();
                ToplamKategoriSayisi();
                ToplamAKategoriSayisi();


                // Session'dan yönetici adı bilgisini alın
                string yoneticiAd = Session["yoneticiAd"] as string;
                string yoneticiSoyad = Session["yoneticiSoyad"] as string;
                string yoneticiYetki = Session["yoneticiYetki"] as string;
                string yoneticiMail = Session["yoneticiMail"] as string;
                string yoneticiID = Session["yoneticiID"] as string;

                lblYoneticiAd.Text = yoneticiAd + " " + yoneticiSoyad;
                LblYetki.Text = yoneticiYetki;

            }


        }

        private void VideoListele()
        {
            try
            {
        
                string sorgu = "SELECT v.VideoID, v.VideoBaslik, v.VideoAciklama, v.VideoUrl, v.VideoFrameUrl, a.AltKatAd FROM Videolar v LEFT JOIN AltKategoriler a ON v.AltKategoriID=a.AltKatID";
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

        private void AltKategoriListele()
        {
            try
            {
                string sorgu = "SELECT a.AltKatID, a.AltKatAd, a.AltKatAciklama, a.AltKatResUrl, k.KatAd \r\n                               FROM AltKategoriler a \r\n                               LEFT JOIN Kategoriler k ON k.KatID = a.KategoriID";

                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                SqlDataReader dr = komut.ExecuteReader();
                Repeater2.DataSource = dr;
                Repeater2.DataBind();
                dr.Close();
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
                SqlDataReader dr = komut.ExecuteReader();
                Repeater3.DataSource = dr;
                Repeater3.DataBind();
                dr.Close();
                baglanti.baglan().Close();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }

        private void ToplamVideoSayisi()
        {
            try
            {
                string sorgu = "SELECT COUNT(*) FROM Videolar";
                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                int toplamVideoSayisi = (int)komut.ExecuteScalar();
                LblToplamVideo.Text = toplamVideoSayisi.ToString();
                baglanti.baglan().Close();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }

        private void ToplamKategoriSayisi()
        {
            try
            {
                string sorgu = "SELECT COUNT(*) ToplamKategori FROM Kategoriler";
                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                int toplamKategoriSayisi = (int)komut.ExecuteScalar();


                // Veritabanından verileri çek
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Repeater'ın veri kaynağını ayarla ve bağla
                sayac.DataSource = dt;
                sayac.DataBind();
                baglanti.baglan().Close();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }

        private void ToplamAKategoriSayisi()
        {
            try
            {
                string sorgu = "SELECT COUNT(*) FROM AltKategoriler";
                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                int toplamAKategoriSayisi = (int)komut.ExecuteScalar();
                LblToplamAKategori.Text = toplamAKategoriSayisi.ToString();
                baglanti.baglan().Close();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }


    }
}