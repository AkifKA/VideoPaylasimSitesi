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
    public partial class Videolar : System.Web.UI.Page
    {
        Baglanti baglanti = new Baglanti();
        public int ToplamBegeniSayisi { get; set;}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VideoListele();
                AltKategoriGetir();
                BegeniListele();
                VideoBegeniSayilariListele();
            }
        }

        private void VideoListele()
        {
            try
            {
                string sorgu = @"
                    SELECT v.VideoID, v.VideoBaslik, v.VideoAciklama, v.VideoUrl, v.VideoFrameUrl, ak.AltKatAd, k.KatAd
                    FROM Videolar v
                    LEFT JOIN AltKategoriler ak ON v.AltKategoriID = ak.AltKatID
                    LEFT JOIN Kategoriler k ON ak.KategoriID = k.KatID";
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

        private void BegeniListele()
        {
            try
            {
                string sorgu = @"
                    SELECT k.KullaniciTakmaAd, v.VideoBaslik, b.BegeniDurumu
                    FROM Begeniler b
                    LEFT JOIN Kullanicilar k ON b.KullaniciID = k.KullaniciID
                    LEFT JOIN Videolar v ON b.VideoID = v.VideoID";
                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                SqlDataReader dr = komut.ExecuteReader();
                rptBegeniler.DataSource = dr;
                rptBegeniler.DataBind();
                dr.Close();
                baglanti.baglan().Close();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }

        private void VideoBegeniSayilariListele()
        {
            try
            {
                string sorgu = @"
                    SELECT v.VideoID, v.VideoBaslik, ak.AltKatAd, k.KatAd,
                           (SELECT COUNT(*) FROM Begeniler WHERE VideoID = v.VideoID AND BegeniDurumu = 1) AS BegeniSayisi
                    FROM Videolar v
                    LEFT JOIN AltKategoriler ak ON v.AltKategoriID = ak.AltKatID
                    LEFT JOIN Kategoriler k ON ak.KategoriID = k.KatID
                    ORDER BY BegeniSayisi DESC";
                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                SqlDataReader dr = komut.ExecuteReader();
                rptVideoBegeniSayilari.DataSource = dr;
                rptVideoBegeniSayilari.DataBind();
                dr.Close();
                // Toplam beğeni sayısını hesapla
                string toplamBegeniSorgu = "SELECT COUNT(*) FROM Begeniler WHERE BegeniDurumu = 1";
                SqlCommand toplamBegeniKomut = new SqlCommand(toplamBegeniSorgu, baglanti.baglan());
                ToplamBegeniSayisi = (int)toplamBegeniKomut.ExecuteScalar();
             
                baglanti.baglan().Close();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }



        protected void BtnVideoEkle_Click(object sender, EventArgs e)
        {
            try
            {
                string videoBaslik = TxtVideoBaslik.Text;
                string videoAciklama = TxtVideoAciklama.Text;
                string videoUrl = TxtVideoUrl.Text;
                string videoFrameUrl = TxtVideoFrameUrl.Text;
                string videoKategoriID = DrpVideoKategoriAdi.SelectedValue;

                string sorgu = "INSERT INTO Videolar (VideoBaslik, VideoAciklama, VideoUrl, VideoFrameUrl, AltKategoriID) " +
                               "VALUES (@VideoBaslik, @VideoAciklama, @VideoUrl, @VideoFrameUrl, @AltKategoriID)";

                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                komut.Parameters.AddWithValue("@VideoBaslik", videoBaslik);
                komut.Parameters.AddWithValue("@VideoAciklama", videoAciklama);
                komut.Parameters.AddWithValue("@VideoUrl", videoUrl);
                komut.Parameters.AddWithValue("@VideoFrameUrl", videoFrameUrl);
                komut.Parameters.AddWithValue("@AltKategoriID", videoKategoriID);
                komut.ExecuteNonQuery();
                baglanti.baglan().Close();
                // Verileri yenile
                VideoListele();
                BegeniListele();
                VideoBegeniSayilariListele();

                // Başarılı ekleme mesajı ve yönlendirme
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlertAndRedirect",
             "alert('Video Başarıyla Eklendi.'); window.location.href = 'Videolar.aspx';", true);
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }

        protected void AltKategoriGetir()
        {
            try
            {
                string sorgu = "SELECT AltKatID, AltKatAd FROM AltKategoriler";

                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                SqlDataReader dr = komut.ExecuteReader();
                DrpVideoKategoriAdi.DataSource = dr;
                DrpVideoKategoriAdi.DataTextField = "AltKatAd";
                DrpVideoKategoriAdi.DataValueField = "AltKatID";
                DrpVideoKategoriAdi.DataBind();
                dr.Close();
                baglanti.baglan().Close();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }
    }
}
