using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace VideoPaylasimSitesi.Class
{
    
    public class Baglanti
    {
        public SqlConnection baglan()
        {
            SqlConnection baglanti = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=VideoPaylasinSitesiDB;Integrated Security=True;TrustServerCertificate=True");
            baglanti.Open();
            return baglanti;
        }
    }
}