using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VideoPaylasimSitesi.AdminPanel
{
    public partial class Admin : System.Web.UI.MasterPage
    {

        public string yoneticiResUrl { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            string yoneticiAd = Session["yoneticiAd"] as String;
            string yoneticiSoyad = Session["yoneticiSoyad"] as String;
            string yoneticiMail = Session["yoneticiMail"] as String;
         
            string yoneticiYetki = Session["yoneticiYetki"] as String;

            lblYöneticiAdi.Text = yoneticiAd + " " +yoneticiSoyad;

            if (Session["yoneticiResUrl"] != null && !string.IsNullOrEmpty(Session["yoneticiResUrl"].ToString()))
            {
                yoneticiResUrl = Session["yoneticiResUrl"] as String;
            }
            else
            {
                // Girişin yapılıp yapılmadığını kontrol et
          
                

                 
                yoneticiResUrl = "Resimler/varsayilan.png";
            }


        }
    }
}