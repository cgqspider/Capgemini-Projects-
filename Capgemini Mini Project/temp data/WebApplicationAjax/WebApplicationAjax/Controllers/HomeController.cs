using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationAjax.Models;

namespace WebApplicationAjax.Controllers
{
    public class HomeController : Controller
    {
        //action method for returning the first page
        public ActionResult Index()
        {
            return View();
        }

        //Action method to Update all the records
        public JsonResult Update(EmpInfo empinfo)
        {
            EmployeeDBContext objemp = new EmployeeDBContext();

            EmpInfo e = objemp.EmpInfoes.ToList().Single(data => data.Id == empinfo.Id);
            e.Name = empinfo.Name;
            e.Salary = empinfo.Salary;
            e.DeptId = empinfo.DeptId;
            e.Location = empinfo.Location;
            objemp.SaveChanges();
            return Json(e, JsonRequestBehavior.AllowGet);
        }


        //Action method to add all the records
        public JsonResult Add(EmpInfo empinfo)
        {
            EmployeeDBContext objemp = new EmployeeDBContext();
            EmpInfo e= objemp.EmpInfoes.Add(empinfo);
            objemp.SaveChanges();
            return Json(e, JsonRequestBehavior.AllowGet);
        }


        //Action method to get the all records
        public JsonResult GetAll()
        {
            EmployeeDBContext objemp = new EmployeeDBContext();
            return Json(objemp.EmpInfoes.ToList(), JsonRequestBehavior.AllowGet);
        }


        //Action method to find the all record
        public JsonResult Find(int id)
        {
            EmployeeDBContext objemp = new EmployeeDBContext();
            return Json(objemp.EmpInfoes.ToList().Single(data=>data.Id==id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            EmployeeDBContext objemp = new EmployeeDBContext();
            bool result=false;
           var emp=objemp.EmpInfoes.ToList().Single(data => data.Id == id);
           if (emp != null)
           {
               objemp.EmpInfoes.Remove(emp);
               objemp.SaveChanges();
               result = true;
           }

           return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}
