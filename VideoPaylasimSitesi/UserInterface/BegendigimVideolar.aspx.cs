using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.UserInterface
{
    public partial class BegendigimVideolar : System.Web.UI.Page
    {
        Baglanti baglanti = new Baglanti();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBegendigimVideolar();
            }
        }

        private void LoadBegendigimVideolar()
        {
            HttpCookie userCookie = Request.Cookies["UserInfo"];
            if (userCookie != null)
            {
                int kullaniciID;
                if (int.TryParse(userCookie.Values["kullaniciID"], out kullaniciID))
                {
                    string sorgu = @"
                    SELECT v.VideoID, v.VideoBaslik, v.VideoAciklama, v.VideoUrl
                    FROM Videolar v
                    INNER JOIN Begeniler b ON v.VideoID = b.VideoID
                    WHERE b.KullaniciID = @KullaniciID AND b.BegeniDurumu = 1";

                    using (SqlConnection conn = baglanti.baglan())
                    {
                        SqlCommand cmd = new SqlCommand(sorgu, conn);
                        cmd.Parameters.AddWithValue("@KullaniciID", kullaniciID);
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        Repeater2.DataSource = dt;
                        Repeater2.DataBind();
                    }
                }
            }
        }
    }
}
