using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.AdminPanel
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
                string yoneticiTakmaAd = TxtTakmaAd.Text;
                string yoneticiSifre = TxtSifre.Text;
                string sorgu = "SELECT * FROM Yoneticiler WHERE YoneticiTakmaAd=@yoneticiTakmaAd AND YoneticiSifre=@yoneticiSifre";
                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                komut.Parameters.AddWithValue("@yoneticiTakmaAd", yoneticiTakmaAd);
                komut.Parameters.AddWithValue("@yoneticiSifre", yoneticiSifre);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    Session["yoneticiID"] = dr["YoneticiID"].ToString();
                    Session["yoneticiAd"] = dr["YoneticiAdi"].ToString();
                    Session["yoneticiSoyad"] = dr["YoneticiSoyadi"].ToString();
                    Session["yoneticiTakmaAd"] = dr["YoneticiTakmaAd"].ToString();
                    Session["yoneticiSifre"] = dr["YoneticiSifre"].ToString();
                    Session["yoneticiMail"] = dr["YoneticiEmail"].ToString();
                    Session["yoneticiResUrl"] = dr["YoneticiResUrl"].ToString();
                    Session["yoneticiYetki"] = dr["YoneticiRol"].ToString();
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