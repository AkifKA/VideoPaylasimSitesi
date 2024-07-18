using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using VideoPaylasimSitesi.Class;

namespace VideoPaylasimSitesi.UserInterface
{
    public partial class User : System.Web.UI.MasterPage
    {
        public string KullaniciResUrl { get; set; }
        Baglanti baglanti = new Baglanti();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserInfo"] != null)
                {
                    KullaniciResUrl = Request.Cookies["UserInfo"]["KullaniciResUrl"];
                    KategoriListele();
                    AltKategoriListele();
                    LoadVideoCategories();
                }
            }
        }
        protected string GetActiveClass(string page)
        {
            string currentPage = Request.Url.Segments[Request.Url.Segments.Length - 1];
            return currentPage.Equals(page, StringComparison.OrdinalIgnoreCase) ? "active" : "";
        }

        private void KategoriListele()
        {
            try
            {
                string sorgu = "SELECT KatID, KatAd FROM Kategoriler ORDER BY KatAd";

                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptCategories.DataSource = dt;
                rptCategories.DataBind();
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
                string sorgu = "SELECT AltKatID, AltKatAd FROM AltKategoriler ORDER BY AltKatAd";

                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptAltKat.DataSource = dt;
                rptAltKat.DataBind();

                baglanti.baglan().Close();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }

        private void LoadVideoCategories()
        {
            try
            {
                string sorgu = @"
                    SELECT DISTINCT
                        k.KatID, 
                        k.KatAd
                    FROM 
                        Kategoriler k
                    LEFT JOIN 
                        AltKategoriler ak ON k.KatID = ak.KategoriID
                    ORDER BY 
                        k.KatAd;";

                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptVideoCategories.DataSource = dt;
                rptVideoCategories.DataBind();
                baglanti.baglan().Close();
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }

        private void VideoListele(int altKatID, Repeater rptVideos, Literal litVideoNoVideos)
        {
            try
            {
                string sorgu = "SELECT VideoID, VideoBaslik FROM Videolar WHERE AltKategoriID = @AltKategoriID ORDER BY VideoBaslik";

                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                komut.Parameters.AddWithValue("@AltKategoriID", altKatID);

                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptVideos.DataSource = dt;
                rptVideos.DataBind();

                if (dt.Rows.Count == 0)
                {
                    litVideoNoVideos.Text = "<span class='dropdown-item text-muted'>Video yok</span>";
                }
            }
            catch (Exception ex)
            {
                Response.Write("Hata: " + ex.Message);
            }
        }

        protected void rptCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptSubCategories = (Repeater)e.Item.FindControl("rptSubCategories");
                DataRowView rowView = (DataRowView)e.Item.DataItem;

                int katID = Convert.ToInt32(rowView["KatID"]);
                string sorgu = "SELECT AltKatID, AltKatAd FROM AltKategoriler WHERE KategoriID = @KategoriID ORDER BY AltKatAd";

                SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                komut.Parameters.AddWithValue("@KategoriID", katID);

                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                rptSubCategories.DataSource = dt;
                rptSubCategories.DataBind();

                if (dt.Rows.Count == 0)
                {
                    Literal litNoSubCategory = (Literal)e.Item.FindControl("litNoSubCategory");
                    if (litNoSubCategory != null)
                    {
                        litNoSubCategory.Text = "<span class='dropdown-item text-muted'>Alt kategori yok</span>";
                    }
                }
            }
        }

        protected void rptSubCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptVideos = (Repeater)e.Item.FindControl("rptVideos");
                Literal litVideoNoVideos = (Literal)e.Item.FindControl("litVideoNoVideos");
                if (rptVideos != null && litVideoNoVideos != null)
                {
                    DataRowView rowView = (DataRowView)e.Item.DataItem;
                    int altKatID = Convert.ToInt32(rowView["AltKatID"]);
                    VideoListele(altKatID, rptVideos, litVideoNoVideos);
                }
            }
        }

        protected void rptVideoCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptVideoSubCategories = (Repeater)e.Item.FindControl("rptVideoSubCategories");
                if (rptVideoSubCategories != null)
                {
                    DataRowView rowView = (DataRowView)e.Item.DataItem;

                    int katID = Convert.ToInt32(rowView["KatID"]);
                    string sorgu = "SELECT AltKatID, AltKatAd FROM AltKategoriler WHERE KategoriID = @KategoriID ORDER BY AltKatAd";

                    SqlCommand komut = new SqlCommand(sorgu, baglanti.baglan());
                    komut.Parameters.AddWithValue("@KategoriID", katID);

                    SqlDataAdapter da = new SqlDataAdapter(komut);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    rptVideoSubCategories.DataSource = dt;
                    rptVideoSubCategories.DataBind();

                    if (dt.Rows.Count == 0)
                    {
                        Literal litVideoNoSubCategory = (Literal)e.Item.FindControl("litVideoNoSubCategory");
                        if (litVideoNoSubCategory != null)
                        {
                            litVideoNoSubCategory.Text = "<span class='dropdown-item text-muted'>Alt kategori yok</span>";
                        }
                    }
                }
            }
        }

        protected void rptVideoSubCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptVideos = (Repeater)e.Item.FindControl("rptVideos");
                Literal litVideoNoVideos = (Literal)e.Item.FindControl("litVideoNoVideos");
                if (rptVideos != null && litVideoNoVideos != null)
                {
                    DataRowView rowView = (DataRowView)e.Item.DataItem;
                    int altKatID = Convert.ToInt32(rowView["AltKatID"]);
                    VideoListele(altKatID, rptVideos, litVideoNoVideos);
                }
            }
        }
    }
}
