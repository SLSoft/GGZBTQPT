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
    public class ZC_SystemController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /ZC_System/

        public ViewResult Index()
        {
            return View(db.T_ZC_System.ToList());
        }

        //
        // GET: /ZC_System/Details/5

        public ViewResult Details(int id)
        {
            T_ZC_System t_zc_system = db.T_ZC_System.Find(id);
            return View(t_zc_system);
        }

        //
        // GET: /ZC_System/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ZC_System/Create

        [HttpPost]
        public ActionResult Create(T_ZC_System t_zc_system)
        {
            if (ModelState.IsValid)
            {
                t_zc_system.CreatedAt = DateTime.Now;
                t_zc_system.UpdatedAt = DateTime.Now;
                db.T_ZC_System.Add(t_zc_system);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(t_zc_system);
        }
        
        //
        // GET: /ZC_System/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_ZC_System t_zc_system = db.T_ZC_System.Find(id);
            return View(t_zc_system);
        }

        //
        // POST: /ZC_System/Edit/5

        [HttpPost]
        public ActionResult Edit(T_ZC_System t_zc_system)
        {
            if (ModelState.IsValid)
            {
                t_zc_system.UpdatedAt = DateTime.Now;
                db.Entry(t_zc_system).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_zc_system);
        }

        //
        // GET: /ZC_System/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_ZC_System t_zc_system = db.T_ZC_System.Find(id);
            return View(t_zc_system);
        }

        //
        // POST: /ZC_System/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            T_ZC_System t_zc_system = db.T_ZC_System.Find(id);
            db.T_ZC_System.Remove(t_zc_system);
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