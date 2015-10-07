using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DailyReportOracleDB.Dal;

namespace DailyReport.Controllers
{
    public class TestController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /GridController1/

        public ActionResult Index()
        {
            return View(db.DR_MODELPROGRESS_LINE.ToList());
        }

        //
        // GET: /GridController1/Details/5

        public ActionResult Details(string id = null)
        {
            DR_MODELPROGRESS_LINE dr_modelprogress_line = db.DR_MODELPROGRESS_LINE.Find(id);
            if (dr_modelprogress_line == null)
            {
                return HttpNotFound();
            }
            return View(dr_modelprogress_line);
        }

        //
        // GET: /GridController1/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /GridController1/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DR_MODELPROGRESS_LINE dr_modelprogress_line)
        {
            if (ModelState.IsValid)
            {
                db.DR_MODELPROGRESS_LINE.Add(dr_modelprogress_line);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dr_modelprogress_line);
        }

        //
        // GET: /GridController1/Edit/5

        public ActionResult Edit(string id = null)
        {
            DR_MODELPROGRESS_LINE dr_modelprogress_line = db.DR_MODELPROGRESS_LINE.Find(id);
            if (dr_modelprogress_line == null)
            {
                return HttpNotFound();
            }
            return View(dr_modelprogress_line);
        }

        //
        // POST: /GridController1/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DR_MODELPROGRESS_LINE dr_modelprogress_line)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dr_modelprogress_line).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dr_modelprogress_line);
        }

        //
        // GET: /GridController1/Delete/5

        public ActionResult Delete(string id = null)
        {
            DR_MODELPROGRESS_LINE dr_modelprogress_line = db.DR_MODELPROGRESS_LINE.Find(id);
            if (dr_modelprogress_line == null)
            {
                return HttpNotFound();
            }
            return View(dr_modelprogress_line);
        }

        //
        // POST: /GridController1/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DR_MODELPROGRESS_LINE dr_modelprogress_line = db.DR_MODELPROGRESS_LINE.Find(id);
            db.DR_MODELPROGRESS_LINE.Remove(dr_modelprogress_line);
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