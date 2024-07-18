using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VideoPaylasimSitesi.UserInterface
{
    public partial class Cikis : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Tüm cookieleri temizle
            HttpCookieCollection cookies = Request.Cookies;
            foreach (string cookieName in cookies.AllKeys)
            {
                HttpCookie cookie = cookies[cookieName];
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }


            // Giriş sayfasına yönlendir
            Response.Redirect("index.aspx");
        }
    }
}