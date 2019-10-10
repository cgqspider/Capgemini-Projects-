using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UASAPI.Models;

namespace UASAPI.Controllers
{
    public class AdminController : Controller
    {
        UASDBContext dbcontext = new UASDBContext();
      
        public JsonResult Disptbadmin()
        {
            List<tbadmin> admin = dbcontext.tbadmins.ToList();
            return Json(admin,JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddApp(tbapplicant lap)
        {
            UASDBContext dbcontext2 = new UASDBContext();
            
            return Json(lap, JsonRequestBehavior.AllowGet);
        }




    }
}
