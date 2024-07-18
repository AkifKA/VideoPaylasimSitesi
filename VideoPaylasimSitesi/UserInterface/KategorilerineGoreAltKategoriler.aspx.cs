using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.UserInterface
{
    public partial class KategorilerineGoreAltKategoriler : System.Web.UI.Page
    {
        Baglanti baglanti = new Baglanti();
        public int AltKategoriRows { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                KatBaslikGetir();
                AltKategoriListele();
            }
        }

        private void KatBaslikGetir()
        {
            try
            {
                int katID = Convert.ToInt32(Request.QueryString["KatID"]);
                string sorgu = "SELECT k.KatAciklama FROM Kategoriler k LEFT JOIN AltKategoriler ak ON ak.AltKatID=k.KatID WHERE k.KatID = @katID";

                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                komut.Parameters.AddWithValue("@katID", katID);

                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptAltKatBaslik.DataSource = dt;
                rptAltKatBaslik.DataBind();
                baglanti.baglan().Close();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }

        private void AltKategoriListele()
        {
            try
            {
                int katID = Convert.ToInt32(Request.QueryString["KatID"]);

                string sorgu = "SELECT * FROM AltKategoriler WHERE KategoriID = @katID";

                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                komut.Parameters.AddWithValue("@katID", katID);

                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptAltKat.DataSource = dt;
                rptAltKat.DataBind();

                AltKategoriRows = dt.Rows.Count;
                baglanti.baglan().Close();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }



    }
}