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
    public class ZC_DepartmentController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /ZC_Department/

        public ViewResult Index()
        {
            return View(db.T_ZC_Department.ToList());
        }

        //
        // GET: /ZC_Department/Details/5

        public ViewResult Details(int id)
        {
            T_ZC_Department t_zc_department = db.T_ZC_Department.Find(id);
            return View(t_zc_department);
        }

        //
        // GET: /ZC_Department/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ZC_Department/Create

        [HttpPost]
        public ActionResult Create(T_ZC_Department t_zc_department)
        {
            if (ModelState.IsValid)
            {
                db.T_ZC_Department.Add(t_zc_department);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(t_zc_department);
        }
        
        //
        // GET: /ZC_Department/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_ZC_Department t_zc_department = db.T_ZC_Department.Find(id);
            return View(t_zc_department);
        }

        //
        // POST: /ZC_Department/Edit/5

        [HttpPost]
        public ActionResult Edit(T_ZC_Department t_zc_department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_zc_department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_zc_department);
        }

        //
        // GET: /ZC_Department/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_ZC_Department t_zc_department = db.T_ZC_Department.Find(id);
            return View(t_zc_department);
        }

        //
        // POST: /ZC_Department/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            T_ZC_Department t_zc_department = db.T_ZC_Department.Find(id);
            db.T_ZC_Department.Remove(t_zc_department);
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