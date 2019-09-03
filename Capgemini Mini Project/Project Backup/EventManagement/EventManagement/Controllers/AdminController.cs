using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;

namespace EventManagement.Controllers
{
    public class AdminController : Controller
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);

 //-------------------------------------------------------------------------------------------------
 //-------------------------------------------------------------------------------------------------     
   
        //display the main page after the login of admin
        public ActionResult Index()
        {
            return View();
        }

        //display admin login page
        public ActionResult Admlogin()
        {
            return View();
        }

        //display event add page of admin
        public ActionResult Admevents()
        {
            return View();
        }

        //display all registered events of students
        public ActionResult Admregisterations()
        {
            return View();
        }

        //display The view for fast scanner
        public ActionResult AdmFastScanner()
        {
            return View();
        }

        //Display the data in view of fast scanner
        public ActionResult DispFastScanner()
        {
            return View();
        }

//-------------------------------------------------------------------------------------------------
//-------------------------------------------------------------------------------------------------     
    // METHODS RELATED TO EVENT 
        //api method for adding the events
        public int addevent(Events evt)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into tbevents values(@time,@title,@desc)", con);
            cmd.Parameters.Add(new SqlParameter("@time", evt.etime));
            cmd.Parameters.Add(new SqlParameter("@title", evt.etitle));
            cmd.Parameters.Add(new SqlParameter("@desc", evt.edesc));
            int i = cmd.ExecuteNonQuery();
            return i;
            
        }

        //api method for deleting the events
        public int delevent(int id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from tbevents where eid=@id", con);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            int i = cmd.ExecuteNonQuery();
            return i;

        }

        //api method for updating event
        public int updevent(Events evt)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update tbevents set etime=@time, etitle=@title, edesc=@desc where eid=@id", con);
            cmd.Parameters.Add(new SqlParameter("@id", evt.eid));
            cmd.Parameters.Add(new SqlParameter("@time", evt.etime));
            cmd.Parameters.Add(new SqlParameter("@title", evt.etitle));
            cmd.Parameters.Add(new SqlParameter("@desc", evt.edesc));
            int i = cmd.ExecuteNonQuery();
            return i;
        }

        //api method for display all event
        public JsonResult dispallevents()
        {
            try
            {
                List<Events> lstevent = new List<Events>();

                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tbevents", con);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Events evt = new Events();
                    evt.eid = Convert.ToInt32(rdr["eid"].ToString());
                    evt.etime = rdr["etime"].ToString();
                    evt.etitle = rdr["etitle"].ToString();
                    evt.edesc = rdr["edesc"].ToString();
                    lstevent.Add(evt);

                }


                return Json(lstevent, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(-2, JsonRequestBehavior.AllowGet);
            }
        }

        //api method for find event
        public JsonResult findevent(int id)
        {
            Events evt = new Events();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from tbevents where eid=@id", con);
            cmd.Parameters.Add(new SqlParameter("@id", id));

            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                evt.eid = Convert.ToInt32(rdr["eid"].ToString());
                evt.etime = rdr["etime"].ToString();
                evt.etitle = rdr["etitle"].ToString();
                evt.edesc = rdr["edesc"].ToString();
               

            }


            return Json(evt, JsonRequestBehavior.AllowGet);

 
        }

        //api method to display all registerations
        public JsonResult dispregisterations()
        {
            List<Registerations> lstregs = new List<Registerations>();
            
            con.Open();
            SqlCommand cmd = new SqlCommand("select stuname, stufname, stuemail, etitle from tbregisterations inner join tbstudents on  tbregisterations.regstuid=tbstudents.stuid inner join  tbevents on tbregisterations.regevtid=tbevents.eid",con);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Registerations regs = new Registerations();
                regs.stuname = rdr["stuname"].ToString();
                regs.stufname = rdr["stufname"].ToString();
                regs.stuemail = rdr["stuemail"].ToString();
                regs.etitle = rdr["etitle"].ToString();
                lstregs.Add(regs);
 
            }
            

            return Json(lstregs,JsonRequestBehavior.AllowGet);
 

            
        }

        //api method for admin login
        public Int32 adm_login_check(AdminPrp adm)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Sp_Admlogin_check", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@eml", SqlDbType.VarChar, 20).Value = adm.admemail;
            cmd.Parameters.Add("@pwd", SqlDbType.VarChar, 20).Value = adm.admpwd;
            cmd.Parameters.Add("@cod", SqlDbType.Int);
            cmd.Parameters["@cod"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            Int32 a = Convert.ToInt32(cmd.Parameters["@cod"].Value);
            cmd.Dispose();
            con.Close();
            return a;

        }

        public JsonResult GetDashboard()
        {
            dashboard dash=new dashboard();
            con.Open();
            //-------------------TOTAL STUDENTS----------------------------
            SqlCommand cmd = new SqlCommand("select count(*) from tbstudents", con);
            dash.TotalStudents = Convert.ToInt32(cmd.ExecuteScalar());
            //-------------------TOTAL EVENTS------------------------------
            cmd = new SqlCommand("select count(*) from tbevents", con);
            dash.TotalEvents = Convert.ToInt32(cmd.ExecuteScalar());
            //-------------------TOTAL PARTICIPATIONS------------------------------
            cmd = new SqlCommand("select COUNT(DISTINCT stuname) from tbstudents inner join tbregisterations on tbregisterations.regstuid=tbstudents.stuid inner join tbevents on tbregisterations.regevtid=tbevents.eid", con);
            dash.TotalParticipations = Convert.ToInt32(cmd.ExecuteScalar());
            //-------------------TOTAL NOT PARTICIPATIONS------------------------------

            dash.TotalNotParticipations =Math.Abs(dash.TotalStudents - dash.TotalParticipations);
            //-------------------Returing the data to the user------------------------------
            return Json(dash, JsonRequestBehavior.AllowGet);
 
        }


    }
}
