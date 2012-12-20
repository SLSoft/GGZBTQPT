using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;

namespace GGZBTQPT_PRO.Controllers
{ 
    public class JG_LinkmanController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /JG_Linkman/

        public ViewResult Index()
        {
            var t_jg_linkman = db.T_JG_Linkman.Include(t => t.Agency);
            return View(t_jg_linkman.ToList());
        }

        //
        // GET: /JG_Linkman/Details/5

        public ViewResult Details(int id)
        {
            T_JG_Linkman t_jg_linkman = db.T_JG_Linkman.Find(id);
            return View(t_jg_linkman);
        }

        //
        // GET: /JG_Linkman/Create

        public ActionResult Create()
        {
            ViewBag.AgencyID = new SelectList(db.T_JG_Agency, "ID", "AgencyName");
            return View();
        } 

        //
        // POST: /JG_Linkman/Create

        [HttpPost]
        public ActionResult Create(T_JG_Linkman t_jg_linkman)
        {
            if (ModelState.IsValid)
            {
                db.T_JG_Linkman.Add(t_jg_linkman);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.AgencyID = new SelectList(db.T_JG_Agency, "ID", "AgencyName", t_jg_linkman.AgencyID);
            return View(t_jg_linkman);
        }
        
        //
        // GET: /JG_Linkman/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_JG_Linkman t_jg_linkman = db.T_JG_Linkman.Find(id);
            ViewBag.AgencyID = new SelectList(db.T_JG_Agency, "ID", "AgencyName", t_jg_linkman.AgencyID);
            return View(t_jg_linkman);
        }

        //
        // POST: /JG_Linkman/Edit/5

        [HttpPost]
        public ActionResult Edit(T_JG_Linkman t_jg_linkman)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_jg_linkman).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgencyID = new SelectList(db.T_JG_Agency, "ID", "AgencyName", t_jg_linkman.AgencyID);
            return View(t_jg_linkman);
        }

        //
        // GET: /JG_Linkman/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_JG_Linkman t_jg_linkman = db.T_JG_Linkman.Find(id);
            return View(t_jg_linkman);
        }

        //
        // POST: /JG_Linkman/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            T_JG_Linkman t_jg_linkman = db.T_JG_Linkman.Find(id);
            db.T_JG_Linkman.Remove(t_jg_linkman);
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