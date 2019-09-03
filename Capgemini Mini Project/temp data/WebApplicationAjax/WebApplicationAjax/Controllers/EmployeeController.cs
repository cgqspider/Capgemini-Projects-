using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationAjax.Models;

namespace WebApplicationAjax.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeDBContext db = new EmployeeDBContext();

        //
        // GET: /Employee/

        public ActionResult Index()
        {
            return View(db.EmpInfoes.ToList());
        }

        //
        // GET: /Employee/Details/5

        public ActionResult Details(int id = 0)
        {
            EmpInfo empinfo = db.EmpInfoes.Find(id);
            if (empinfo == null)
            {
                return HttpNotFound();
            }
            return View(empinfo);
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create(EmpInfo empinfo)
        {
            if (ModelState.IsValid)
            {
                db.EmpInfoes.Add(empinfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(empinfo);
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(int id = 0)
        {
            EmpInfo empinfo = db.EmpInfoes.Find(id);
            if (empinfo == null)
            {
                return HttpNotFound();
            }
            return View(empinfo);
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        public ActionResult Edit(EmpInfo empinfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empinfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empinfo);
        }

        //
        // GET: /Employee/Delete/5

        public ActionResult Delete(int id = 0)
        {
            EmpInfo empinfo = db.EmpInfoes.Find(id);
            if (empinfo == null)
            {
                return HttpNotFound();
            }
            return View(empinfo);
        }

        //
        // POST: /Employee/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            EmpInfo empinfo = db.EmpInfoes.Find(id);
            db.EmpInfoes.Remove(empinfo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}