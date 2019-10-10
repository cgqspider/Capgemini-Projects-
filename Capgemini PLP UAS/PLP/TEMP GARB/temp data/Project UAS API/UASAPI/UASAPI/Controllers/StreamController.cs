using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UASAPI.Models;

namespace UASAPI.Controllers
{
    public class StreamController : Controller
    {
      //API for create the stream
        public JsonResult Add(tbstream stream)
        {
            UASDBContext dbcontext = new UASDBContext();
            tbstream RetStream = dbcontext.tbstreams.Add(stream);
            dbcontext.SaveChanges();
            return Json(RetStream, JsonRequestBehavior.AllowGet);
        }

        //API for update the stream
        public JsonResult Update(tbstream stream)
        {
            UASDBContext dbcontext = new UASDBContext();
            tbstream RetStream = dbcontext.tbstreams.ToList().Single(data => data.sid == stream.sid);
            RetStream.sname = stream.sname;
            dbcontext.SaveChanges();
            return Json(RetStream, JsonRequestBehavior.AllowGet);
        }

        //API for delete the stream
        public JsonResult Delete(int id)
        {
            UASDBContext dbcontext = new UASDBContext();
            bool result = false;
            var stream = dbcontext.tbstreams.ToList().Single(data => data.sid == id);
            if (stream != null)
            {
                dbcontext.tbstreams.Remove(stream);
                dbcontext.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // API for find the stream
        public JsonResult Find(int id)
        {
            UASDBContext dbcontext = new UASDBContext();
            return Json(dbcontext.tbstreams.ToList().Single(data => data.sid == id), JsonRequestBehavior.AllowGet);
        }

        // API for display all streams
        public JsonResult GetAll()
        {
            UASDBContext dbcontext = new UASDBContext();
            return Json(dbcontext.tbstreams.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}
