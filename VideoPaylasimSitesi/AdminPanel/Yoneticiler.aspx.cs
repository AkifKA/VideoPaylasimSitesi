using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.AdminPanel
{
    public partial class Yoneticiler : System.Web.UI.Page
    {
        Baglanti baglanti = new Baglanti();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                YoneticiListele();
            }
        }

        private void YoneticiListele()
        {
            try
            {
                string sorgu = "SELECT * FROM Yoneticiler";
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

        protected void FileUpload1_Changed(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                // Dosya adını ve yolu belirleyin
                string filename = Path.GetFileName(FileUpload1.FileName);
                string folderPath = Server.MapPath("~/AdminPanel/ResimYuklemeYeri/");
                string savePath = Path.Combine(folderPath, filename);

                // Dosyayı kaydedin
                FileUpload1.SaveAs(savePath);

                // Dosya yolunu al ve TextBox'a yazdır
                string yoneticiResUrl = "~/AdminPanel/ResimYuklemeYeri/" + filename;
                TxtYoneticiResUrl.Text = yoneticiResUrl;
            }
            else
            {
                // Debug mesajı
                Response.Write("Dosya yüklenmedi.");
            }
        }

        protected void BtnYoneticiEkle_Click(object sender, EventArgs e)
        {
            try
            {
                // Yönetici ekleme işlemi
                string yoneticiAdi = TxtYoneticiAdi.Text.Trim();
                string yoneticiSoyadi = TxtYoneticiSoyadi.Text.Trim();
                string yoneticiTakmaAd = TxtYoneticiTakmaAd.Text.Trim();
                string yoneticiEmail = TxtYoneticiEmail.Text.Trim();
                string yoneticiSifre = TxtYoneticiSifre.Text.Trim();
                string yoneticiRol = DdlYoneticiRol.SelectedValue; // DropdownList'ten seçilen rol
                string yoneticiResUrl = TxtYoneticiResUrl.Text.Trim(); // TextBox'tan alınan dosya yolu

                string sorgu = "INSERT INTO Yoneticiler (YoneticiAdi, YoneticiSoyadi, YoneticiTakmaAd, YoneticiEmail, YoneticiSifre, YoneticiRol, YoneticiResUrl) " +
                               "VALUES (@YoneticiAdi, @YoneticiSoyadi, @YoneticiTakmaAd, @YoneticiEmail, @YoneticiSifre, @YoneticiRol, @YoneticiResUrl)";
                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                komut.Parameters.AddWithValue("@YoneticiAdi", yoneticiAdi);
                komut.Parameters.AddWithValue("@YoneticiSoyadi", yoneticiSoyadi);
                komut.Parameters.AddWithValue("@YoneticiTakmaAd", yoneticiTakmaAd);
                komut.Parameters.AddWithValue("@YoneticiEmail", yoneticiEmail);
                komut.Parameters.AddWithValue("@YoneticiSifre", yoneticiSifre);
                komut.Parameters.AddWithValue("@YoneticiRol", yoneticiRol);
                komut.Parameters.AddWithValue("@YoneticiResUrl", yoneticiResUrl);
                komut.ExecuteNonQuery();
                baglanti.baglan().Close();

                // Başarılı ekleme mesajı ve yönlendirme
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlertAndRedirect",
                    "alert('Yönetici Başarıyla Eklendi.'); window.location.href = 'Yoneticiler.aspx';", true);
            }
            catch (Exception ex)
            {
                // Hata mesajı gösterebilirsiniz
                Response.Write("Hata: " + ex.Message);
            }
        }

        protected void BtnTemizle_Click(object sender, EventArgs e)
        {
            // Form alanlarını temizleyin
            TxtYoneticiAdi.Text = "";
            TxtYoneticiSoyadi.Text = "";
            TxtYoneticiTakmaAd.Text = "";
            TxtYoneticiEmail.Text = "";
            TxtYoneticiSifre.Text = "";
            DdlYoneticiRol.SelectedIndex = 0; // DropdownList'i sıfırlayın
            TxtYoneticiResUrl.Text = "";
            FileUpload1.Attributes.Clear(); // FileUpload'ı sıfırlayın
        }
    }
}
