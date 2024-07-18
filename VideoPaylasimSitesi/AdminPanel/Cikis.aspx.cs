using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VideoPaylasimSitesi.AdminPanel
{
    public partial class Cikis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Tüm sessionları temizle
            Session.Clear();
         

            // Giriş sayfasına yönlendir
            Response.Redirect("Giris.aspx");
        }
    }
}