using EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EventManagement.Controllers
{
    public class StudentsController : Controller
    {
        //--------------------STATUS CONVENTIONS-------------------
        //---------------------------------------------------------

        //NOTE : -2 IS USED FOR THE CATCH THE EXCEPTION
       
        //---------------------------------------------------------
        //---------------------------------------------------------

        //Connection with the database
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString);

       //-------------------------------------------------------------------------------------------------
       //-------------------------------------------------------------------------------------------------     
   
        //display main page of website
        public ActionResult Index()
        {
          
            return View();
        }

        //display the signup page
        public ActionResult Signup()
        {
            return View();
        }

        //display the signin page
        public ActionResult Signin()
        {
            return View();
        }

        //display the profile page of student
        public ActionResult Profile()
        {
            return View();
        }

        //display all student registered event with delete button
        public ActionResult DispRegEvt()
        {
            return View();
        }

        //event registeration page with dropdown of events schduled by admin
        public ActionResult RegEvt()
        {
            return View();
        }

        //action method for slider timeline jquery for connectivity with iframe
        public ActionResult Timeline()
        {
            return View();
        }

      
        //-------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------- 


        // method for signup the student 
         public int RegisterStudent(Students stu)
        {

            try
            {
              
                    con.Open();
                    SqlCommand cmd = new SqlCommand("insert into tbstudents values(@name,@fname,@email,@pwd)", con);
                    cmd.Parameters.Add(new SqlParameter("@name", stu.stuname));
                    cmd.Parameters.Add(new SqlParameter("@fname", stu.stufname));
                    cmd.Parameters.Add(new SqlParameter("@email", stu.stuemail));
                    cmd.Parameters.Add(new SqlParameter("@pwd", stu.stupwd));
                    int i = cmd.ExecuteNonQuery();
                    return i;
                
              
            }

            catch (Exception ex)
            {
                return -2;
            }
        }

        // method for finding the student profile 
         public JsonResult FindStudent(int id)
        {
            try
            {
                Students stu = new Students();
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tbstudents where stuid=@id", con);
                cmd.Parameters.Add(new SqlParameter("@id", id));

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    stu.stuid = Convert.ToInt32(rdr["stuid"].ToString());
                    stu.stuname = rdr["stuname"].ToString();
                    stu.stufname = rdr["stufname"].ToString();
                    stu.stuemail = rdr["stuemail"].ToString();
                    stu.stupwd = rdr["stupwd"].ToString();
                }


                return Json(stu, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Json(-2, JsonRequestBehavior.AllowGet);
            }
 
        }

        // method for login the student 
         public Int32 stu_login_check(Students stu)
         {
             try
             {
                
                     con.Open();
                     SqlCommand cmd = new SqlCommand("Sp_Stulogin_check", con);
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.Parameters.Add("@eml", SqlDbType.VarChar, 50).Value = stu.stuemail;
                     cmd.Parameters.Add("@pwd", SqlDbType.VarChar, 50).Value = stu.stupwd;
                     cmd.Parameters.Add("@cod", SqlDbType.Int);
                     cmd.Parameters["@cod"].Direction = ParameterDirection.Output;
                     cmd.ExecuteNonQuery();
                     Int32 a = Convert.ToInt32(cmd.Parameters["@cod"].Value);
                     cmd.Dispose();
                     con.Close();
                     return a;
                
             }

             catch (Exception ex)
             {
                 return -2;
             }
         }

        // method for register a student to a event
         public int StuRegEvt(StuRegEvt stureg)
         {
             try
             {
                
                     con.Open();
                     SqlCommand cmd = new SqlCommand("insert into tbregisterations values(@stuid,@evtid)", con);
                     cmd.Parameters.Add(new SqlParameter("@stuid", stureg.regstuid));
                     cmd.Parameters.Add(new SqlParameter("@evtid", stureg.regevtid));
                     int i = cmd.ExecuteNonQuery();
                     return i;
                
             }

             catch (Exception ex)
             {
                 return -2;
             }
         }

        // method for delete from particular event
         public int Deregister(int stuid, int evtid)
         {
             try
             {
                
                     con.Open();
                     SqlCommand cmd = new SqlCommand("delete from tbregisterations where regstuid=@stuid and regevtid=@evtid", con);
                     cmd.Parameters.Add(new SqlParameter("@stuid", stuid));
                     cmd.Parameters.Add(new SqlParameter("@evtid", evtid));
                     int i = cmd.ExecuteNonQuery();
                     return i;
                
             }
             catch(Exception ex){
                 return -2;
             }
         }

        // method for registered all event display
         public JsonResult DispStuRegs(int id)
         {
             try
             {
                 List<Registerations> lstregs = new List<Registerations>();
                 con.Open();
                 SqlCommand cmd = new SqlCommand("select Distinct stuname, stufname, stuemail, etitle, eid  from tbregisterations inner join tbstudents on  tbregisterations.regstuid=tbstudents.stuid inner join  tbevents on tbregisterations.regevtid=tbevents.eid where tbstudents.stuid=@stuid", con);
                 cmd.Parameters.Add(new SqlParameter("@stuid", id));
                 SqlDataReader rdr = cmd.ExecuteReader();
                 while (rdr.Read())
                 {
                     Registerations regs = new Registerations();
                     regs.stuname = rdr["stuname"].ToString();
                     regs.stufname = rdr["stufname"].ToString();
                     regs.stuemail = rdr["stuemail"].ToString();
                     regs.etitle = rdr["etitle"].ToString();
                     regs.eid = int.Parse(rdr["eid"].ToString());
                     lstregs.Add(regs);
                 }
                 return Json(lstregs, JsonRequestBehavior.AllowGet);
             }

             catch (Exception ex)
             {
                 return Json(-2,JsonRequestBehavior.AllowGet);
             }


         }

        

    }
}
