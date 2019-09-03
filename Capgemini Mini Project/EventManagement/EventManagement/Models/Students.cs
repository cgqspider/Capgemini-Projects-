using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace EventManagement.Models
{
    // Property classes for student
    public class Students
    {

        public int stuid { get; set; }
        [Required]
        public string stuname { get; set; }
        [Required]
        public string stufname { get; set; }
        [Required]
        public string stuemail { get; set; }
        [Required]
        public string stupwd { get; set; }
    }

    // Property classes for student Event Registeration
    public class StuRegEvt
    {
        public int regid { get; set; }
        [Required]
        public int regstuid { get; set; }
        [Required]
        public int regevtid { get; set; }
    }

    // Property classes for Events
    public class Events
    {
        public int eid { get; set; }
        [Required]
        public string etime { get; set; }
        [Required]
        public string etitle { get; set; }
        [Required]
        public string edesc { get; set; }
    }

    // Property classes for student
    public class Registerations
    {
        [Required]
        public string stuname { get; set; }
        [Required]
        public string stufname { get; set; }
        [Required]
        public string stuemail { get; set; }
        [Required]
        public string etitle { get; set; }
        [Required]
        public int eid { get; set; }
    }

    // proprty classes for admin login
    public class AdminPrp
    {

        public int admid { get; set; }
        [Required]
        public string admemail { get; set; }
        [Required]
        public string admpwd { get; set; }
    }

    // Property classes for the dashboard
    public class dashboard
    {
        public int TotalStudents { get; set; }
        public int TotalEvents { get; set; }
        public int TotalParticipations { get; set; }
        public int TotalNotParticipations { get; set; }
    }

}