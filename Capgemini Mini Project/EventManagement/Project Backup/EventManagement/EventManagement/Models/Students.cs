using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Models
{
    //Property classes for student
    public class Students
    {
        public int stuid { get; set; }
        public string stuname { get; set; }
        public string stufname { get; set; }
        public string stuemail { get; set; }
        public string stupwd { get; set; }
    }
    //Property classes for student Event Registeration
    public class StuRegEvt
    {
        public int regid { get; set; }
        public int regstuid { get; set; }
        public int regevtid { get; set; }
    }
    //Property classes for Events
    public class Events
    {
        public int eid { get; set; }
        public string etime { get; set; }
        public string etitle { get; set; }
        public string edesc { get; set; }
    }
    //Property classes for student
    public class Registerations
    {
        public string stuname { get; set; }
        public string stufname { get; set; }
        public string stuemail { get; set; }
        public string etitle { get; set; }
        public int eid { get; set; }
    }
    //proprty classes for admin login
    public class AdminPrp
    {
        public int admid { get; set; }
        public string admemail { get; set; }
        public string admpwd { get; set; }
    }

    public class dashboard
    {
        public int TotalStudents { get; set; }
        public int TotalEvents { get; set; }
        public int TotalParticipations { get; set; }
        public int TotalNotParticipations { get; set; }
    }
}