using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UASAPI.Models;

namespace UASAPI.Controllers
{
    public class StreamController : ApiController
    {

        // API METHOD TO ADD THE STREAM 
        [HttpPost]
        public void Add(Stream stream)
        {
            SqlConnection con = new SqlConnection("server=(localdb)\\v11.0; database=dbUAS; integrated security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into tbstream values(@sname)", con);
            cmd.Parameters.Add(new SqlParameter("@sname", stream.sname));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        //API METHOD TO GET ALL THE STREAMS 
        [HttpGet]      
        public List<Stream> GetAll()
        {
            SqlDataReader reader = null;
            SqlConnection con = new SqlConnection("server=(localdb)\\v11.0; database=dbUAS; integrated security=true;");
            SqlCommand cmd = new SqlCommand("select * from tbstream",con);
            con.Open();
            reader = cmd.ExecuteReader();
            Stream stream = null;
            List<Stream> lststrm = new List<Stream>();
            while (reader.Read())
            {
                stream = new Stream();
                stream.sid = Convert.ToInt32(reader[0].ToString());
                stream.sname = reader[1].ToString();
                lststrm.Add(stream);
            }
            return lststrm;
            
        }  

        //API METHOD TO DELETE THE STREAM
   

        //API METHOD TO UPDATE THE STREAM
        [HttpPost]
        public void Update(Stream stream)
        {
            SqlConnection con = new SqlConnection("server=(localdb)\\v11.0; database=dbUAS; integrated security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("update tbstream set sname=@sname where sid=@sid", con);
            cmd.Parameters.Add(new SqlParameter("@sid", stream.sid));
            cmd.Parameters.Add(new SqlParameter("@sname", stream.sname));
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }


        //api method for login the Applicant 
        [HttpPost]
        public Int32 AppLogin(Applicant applicant)
        {
           
                SqlConnection con = new SqlConnection("server=(localdb)\\v11.0; database=dbUAS; integrated security=true");
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_AppLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@eml", SqlDbType.VarChar, 50).Value = applicant.appemail;
                cmd.Parameters.Add("@pwd", SqlDbType.VarChar, 50).Value = applicant.apppwd;
                cmd.Parameters.Add("@cod", SqlDbType.Int);
                cmd.Parameters["@cod"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                Int32 result = Convert.ToInt32(cmd.Parameters["@cod"].Value);
                cmd.Dispose();
                con.Close();
                return result;     
           
        }

        //api method for login the Admin 
        [HttpPost]
        public Int32 AdmLogin(Admin admin)
        {

            SqlConnection con = new SqlConnection("server=(localdb)\\v11.0; database=dbUAS; integrated security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_AdmLogin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@eml", SqlDbType.VarChar, 50).Value = admin.admemail;
            cmd.Parameters.Add("@pwd", SqlDbType.VarChar, 50).Value = admin.admpwd;
            cmd.Parameters.Add("@cod", SqlDbType.Int);
            cmd.Parameters["@cod"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            Int32 result = Convert.ToInt32(cmd.Parameters["@cod"].Value);
            cmd.Dispose();
            con.Close();
            return result;

        }




        public List<Applicant> GetProfile(int id)
        {
            SqlDataReader reader = null;
            SqlConnection con = new SqlConnection("server=(localdb)\\v11.0; database=dbUAS; integrated security=true;");
            SqlCommand cmd = new SqlCommand("select appfname,appfcontact,appname,appemail,appcontact,appaddress,appgender,appDOB,yop_10,board_10,percent_10,yop_12,board_12,percent_12,yop_grad,univ_grad,percent_grad from tbapplicant left join tbeducation on tbapplicant.appid=tbeducation.eappid where tbapplicant.appid=@id", con);
            cmd.Parameters.Add(new SqlParameter("@id", id));
            con.Open();
            reader = cmd.ExecuteReader();
            Applicant app = null;
            List<Applicant> lstapp = new List<Applicant>();
            while (reader.Read())
            {
                app = new Applicant();
                app.appfname = reader[0].ToString();
                app.appfcontact = reader[1].ToString();
                app.appname = reader[2].ToString();
                app.appemail = reader[3].ToString();
                app.appcontact = reader[4].ToString();
                app.appaddress = reader[5].ToString();
                app.appgender = reader[6].ToString();
                app.appDOB =Convert.ToDateTime( reader[7].ToString());

                app.yop_10 = int.Parse(reader[8].ToString());
                app.board_10 = reader[9].ToString();
                app.percent_10 = int.Parse(reader[10].ToString());

                app.yop_12 = int.Parse(reader[11].ToString());
                app.board_12 = reader[12].ToString();
                app.percent_12 = int.Parse(reader[13].ToString());

                app.yop_grad = int.Parse(reader[14].ToString());
                app.univ_grad = reader[15].ToString();
                app.percent_grad = int.Parse(reader[16].ToString());
                lstapp.Add(app);
            }
            return lstapp;

        }









        //api method for get all applications for admin
        public List<AdminAppDet> GetAllApplicants()
        {
            SqlDataReader reader = null;
            SqlConnection con = new SqlConnection("server=(localdb)\\v11.0; database=dbUAS; integrated security=true;");
            SqlCommand cmd = new SqlCommand("select appfname,appfcontact,appname,appemail,appcontact,appaddress,appgender,appDOB,sname,dname,cname from tbadmission inner join tbapplicant on tbadmission.aappid=tbapplicant.appid inner join tbstream on tbadmission.asid=tbstream.sid inner join  tbdepartment on tbadmission.adid=tbdepartment.did inner join  tbcourse on tbadmission.acid=tbcourse.cid", con);
            con.Open();
            reader = cmd.ExecuteReader();
            AdminAppDet app = null;
            List<AdminAppDet> lstapp = new List<AdminAppDet>();
            while (reader.Read())
            {
                app = new AdminAppDet();
                app.appfname = reader[0].ToString();
                app.appfcontact = reader[1].ToString();
                app.appname = reader[2].ToString();
                app.appemail = reader[3].ToString();
                app.appcontact = reader[4].ToString();
                app.appaddress = reader[5].ToString();
                app.appgender = reader[6].ToString();
                app.appDOB = reader[7].ToString();
                app.sname = reader[8].ToString();
                app.dname = reader[9].ToString();
                app.cname = reader[10].ToString();
                lstapp.Add(app);
            }
            return lstapp;
        
        }

        //api method for get all applications for Applicant



        //API METHOD TO UPDATE THE STATUS 
        [HttpPost]
        public bool UpdateSts(Admission admission)
        {
            SqlConnection conn = new SqlConnection("server=(localdb)\\v11.0; database=dbUAS; integrated security=true;");
            conn.Open();
            SqlCommand cmd = new SqlCommand("update tbadmission set asts=@asts where aid=@aid", conn);
            cmd.Parameters.Add(new SqlParameter("@aid", admission.aid));
            cmd.Parameters.Add(new SqlParameter("@asts", admission.asts));
            int result = cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            if (result > 0) { return true; }
            else { return false; }
        }

    }
}
