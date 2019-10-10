using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UASAPI.Models
{
    public class Stream
    {
        public int sid { get; set; }
        public string sname { get; set; }
    }

    public class Applicant {
        public int appid { get; set; }
        public string appfname { get; set; }
        public string appfcontact { get; set; }
        public string appname { get; set; }
        public string appemail { get; set; }
        public string apppwd { get; set; }
        public string appcontact { get; set; }
        public string appaddress { get; set; }
        public string appgender { get; set; }
        public DateTime appDOB { get; set; }
        
        public int yop_10 { get; set; }
        public string board_10 { get; set; }
        public int percent_10 { get; set; }

        public int yop_12 { get; set; }
        public string board_12 { get; set; }
        public int percent_12 { get; set; }

        public int yop_grad { get; set; }
        public string univ_grad { get; set; }
        public int percent_grad { get; set; }


    
    }

    public class Admin {
        public int admid { get; set; }
        public string admname { get; set; }
        public string admemail { get; set; }
        public string admpwd { get; set; }
    
    }

    public class AdminAppDet { 
   
        public string appfname { get; set; }
        public string appfcontact { get; set; }
        public string appname { get; set; }
        public string appemail { get; set; }
        public string appcontact { get; set; }
        public string appaddress { get; set; }
        public string appgender { get; set; }
        public string appDOB { get; set; }
        public string sname { get; set; }
        public string dname { get; set; }
        public string cname { get; set; }
    
    }

    public class Admission
    {
        public int aid { get; set; }
        public int aappid { get; set; }
        public int asid { get; set; }
        public int adid { get; set; }
        public int acid { get; set; }
        public int asts { get; set; }
    }

    public class ClsException {
        public DateTime ExceptionTiming { get; set; }
        public string ExceptionSummary { get; set; }
        public string ExceptionStackTrace { get; set; }
    }


}