using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoPaylasimSitesi.Class;
using System.IO;

namespace VideoPaylasimSitesi.UserInterface
{
    public partial class Kayit : System.Web.UI.Page
    {
        Baglanti baglanti = new Baglanti();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnKayitYap_Click(object sender, EventArgs e)
        {
            string kullaniciAd = TxtAd.Text;
            string kullaniciSoyad = TxtSoyad.Text;
            string kullaniciTakmaAd = TxtTakmaAd.Text;
            string kullaniciSifre = TxtSifre.Text;
            string kullaniciMail = TxtMail.Text;
            string kullaniciResUrl = string.Empty;

            if (fileUpload.HasFile)
            {
                try
                {
                    // Dosya adını benzersiz yapmak için GUID ekleyin
                    string dosyaAdi = Guid.NewGuid().ToString() + "_" + Path.GetFileName(fileUpload.FileName);
                    // Dosya yükleme yolu
                    string yuklemeYolu = Server.MapPath("ResimYuklemeYeri/") + dosyaAdi;
                    // Dosyayı sunucuya yükleyin
                    fileUpload.SaveAs(yuklemeYolu);
                    // Veritabanına kaydedilecek dosya yolu
                    kullaniciResUrl = "ResimYuklemeYeri/" + dosyaAdi;
                }
                catch (Exception ex)
                {
                    LblResHataMesaji.Text = "Dosya yükleme hatası: " + ex.Message;
                    return;
                }
            }
            string sorgu = "INSERT INTO Kullanicilar (KullaniciAdi, KullaniciSoyadi, KullaniciTakmaAd, KullaniciEMail, KullaniciSifre, KullaniciResUrl) VALUES (@KullaniciAd, @KullaniciSoyad, @KullaniciTakmaAd, @KullaniciMail, @KullaniciSifre, @KullaniciResUrl)";
            SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
            komut.Parameters.AddWithValue("@KullaniciAd", kullaniciAd);
            komut.Parameters.AddWithValue("@KullaniciSoyad", kullaniciSoyad);
            komut.Parameters.AddWithValue("@KullaniciTakmaAd", kullaniciTakmaAd);
            komut.Parameters.AddWithValue("@KullaniciSifre", kullaniciSifre);
            komut.Parameters.AddWithValue("@KullaniciMail", kullaniciMail);
            komut.Parameters.AddWithValue("@KullaniciResUrl", kullaniciResUrl);


            try
            {
                komut.ExecuteNonQuery();
                Response.Redirect("Giris.aspx");
            }
            catch (Exception ex)
            {
                LblHataMesaji.Text = "Hata: " + ex.Message;
            }


        }
    }
}