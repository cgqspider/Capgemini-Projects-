using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UASAPI.Models;

namespace UASAPI.Controllers
{
    public class DepartmentController : Controller
    {
        //API for create the stream
        public JsonResult Add(tbdepartment department)
        {
            UASDBContext dbcontext = new UASDBContext();
            tbdepartment RetDepartment = dbcontext.tbdepartments.Add(department);
            dbcontext.SaveChanges();
            return Json(RetDepartment, JsonRequestBehavior.AllowGet);
        }

        ////API for update the stream
        //public JsonResult Update(tbdepartment department)
        //{
        //    UASDBContext dbcontext = new UASDBContext();
        //    tbdepartment RetDepartment = dbcontext.tbdepartments.ToList().Single(data => data.did == department.did);
        //    RetDepartment.dname = department.dname;
        //    RetDepartment.dsid = department.dsid;
        //    dbcontext.SaveChanges();
        //    return Json(RetDepartment, JsonRequestBehavior.AllowGet);
        //}

        ////API for delete the stream
        //public JsonResult Delete(int id)
        //{
        //    UASDBContext dbcontext = new UASDBContext();
        //    bool result = false;
        //    var department = dbcontext.tbdepartments.ToList().Single(data => data.did == id);
        //    if (department != null)
        //    {
        //        dbcontext.tbdepartments.Remove(department);
        //        dbcontext.SaveChanges();
        //        result = true;
        //    }

        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //// API for find the stream
        //public JsonResult Find(int id)
        //{
        //    UASDBContext dbcontext = new UASDBContext();
        //    return Json(dbcontext.tbdepartments.ToList().Single(data => data.did == id), JsonRequestBehavior.AllowGet);
        //}

        // API for display all streams
        public JsonResult GetAll()
        {
            UASDBContext dbcontext = new UASDBContext();
            List<tbdepartment> depDet =dbcontext.tbdepartments.ToList();
            return Json(depDet, JsonRequestBehavior.AllowGet);
        }

    }
}
