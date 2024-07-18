using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using VideoPaylasimSitesi.Class;
using System.Data;
using System.Windows.Forms;

namespace VideoPaylasimSitesi.UserInterface
{
    public partial class KategorilerineGoreVideolar : System.Web.UI.Page
    {
        Baglanti baglanti = new Baglanti();
        public int VideoRows { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VideoBaslikGetir();
                VideoListele();
            }
        }

        private void VideoBaslikGetir()
        {
            try
            {

                int altKatID = Convert.ToInt32(Request.QueryString["AltKatID"]);
                string sorgu = "SELECT AltKatAciklama FROM AltKategoriler WHERE AltKatID=@AltKatID";
                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                komut.Parameters.AddWithValue("@AltKatID", altKatID);

                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptBaslik.DataSource = dt;
                rptBaslik.DataBind();

           
                baglanti.baglan().Close();


            }
            catch (Exception ex)
            {

                Response.Write("Hata: " + ex.Message);
            }
        }

        private void VideoListele()
        {

            try
            {
                int altKatID = Convert.ToInt32(Request.QueryString["AltKatID"]);
                string sorgu = "SELECT v.VideoID, v.VideoBaslik, v.VideoAciklama, v.VideoUrl, a.AltKatAd, a.AltKatAciklama FROM Videolar v INNER JOIN AltKategoriler a ON v.AltKategoriID = a.AltKatID WHERE v.AltKategoriID = @AltKategoriID";

                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                komut.Parameters.AddWithValue("@AltKategoriID", altKatID);

                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptVideo.DataSource = dt;
                rptVideo.DataBind();

                VideoRows = dt.Rows.Count;
                baglanti.baglan().Close();

            }
            catch (Exception ex)
            {

                Response.Write("Hata: " + ex.Message);

            }
        }



    }
}