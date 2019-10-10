using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UASAPI.Models;

namespace UASAPI.Controllers
{
    public class defaultController : Controller
    {
       
        [HttpPost]
        public void Add(Stream stream)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);
            con.Close();
            SqlCommand cmd = new SqlCommand("insert into tbstream values(@sname)",con);
            cmd.Parameters.Add(new SqlParameter("@sname", stream.sname));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }  

    }
}
