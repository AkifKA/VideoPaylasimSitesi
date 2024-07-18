using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.UserInterface
{
    public partial class Giris : System.Web.UI.Page
    {
        Baglanti baglanti = new Baglanti();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnGirisYap_Click(object sender, EventArgs e)
        {
            try
            {
                string kullaniciTakmaAd = TxtTakmaAd.Text;
                string kullaniciSifre = TxtSifre.Text;
                string sorgu = "SELECT * FROM Kullanicilar WHERE KullaniciTakmaAd=@kullaniciTakmaAd AND KullaniciSifre=@kullaniciSifre";
                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                komut.Parameters.AddWithValue("@kullaniciTakmaAd", kullaniciTakmaAd);
                komut.Parameters.AddWithValue("@kullaniciSifre", kullaniciSifre);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    // Cookie oluşturuluyor
               
                    HttpCookie userCookie = new HttpCookie("UserInfo");
                    userCookie.Values["kullaniciID"] = dr["KullaniciID"].ToString();
                    userCookie.Values["kullaniciAd"] = Server.UrlEncode(dr["KullaniciAdi"].ToString());
                    userCookie.Values["kullaniciSoyad"] = Server.UrlEncode(dr["KullaniciSoyadi"].ToString());
                    userCookie.Values["kullaniciTakmaAd"] = dr["KullaniciTakmaAd"].ToString();
                    userCookie.Values["kullaniciSifre"] = dr["KullaniciSifre"].ToString();
                    userCookie.Values["kullaniciMail"] = dr["KullaniciEmail"].ToString();
                    userCookie.Values["kullaniciResUrl"] = Server.UrlEncode(dr["KullaniciResUrl"].ToString());

                    // Cookie'nin süresi ayarlanıyor
                    userCookie.Expires = DateTime.Now.AddDays(7); // Cookie 7 gün boyunca geçerli olacak

                    // Cookie tarayıcıya ekleniyor
                    Response.Cookies.Add(userCookie);

           

                    // Index sayfasına yönlendiriliyor
                    Response.Redirect("index.aspx");
                }
                else
                {
                    LblHataMesaji.Text = "Takma ad veya şifre hatalı!";
                }
            }
            catch (Exception ex)
            {

                Response.Write("Hata: " + ex.Message);
            }
        }
    }
}


