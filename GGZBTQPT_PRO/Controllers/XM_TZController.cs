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
    public class XM_TZController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /XM_TZ/

        public ViewResult Index()
        {
            return View(db.T_XM_Investment.ToList());
        }

        //
        // GET: /XM_TZ/Details/5

        public ViewResult Details(int id)
        {
            T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);
            return View(t_xm_investment);
        }

        //
        // GET: /XM_TZ/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /XM_TZ/Create

        [HttpPost]
        public ActionResult Create(T_XM_Investment t_xm_investment)
        {
            if (ModelState.IsValid)
            {
                db.T_XM_Investment.Add(t_xm_investment);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(t_xm_investment);
        }
        
        //
        // GET: /XM_TZ/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);
            return View(t_xm_investment);
        }

        //
        // POST: /XM_TZ/Edit/5

        [HttpPost]
        public ActionResult Edit(T_XM_Investment t_xm_investment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_xm_investment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_xm_investment);
        }

        //
        // GET: /XM_TZ/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);
            return View(t_xm_investment);
        }

        //
        // POST: /XM_TZ/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);
            db.T_XM_Investment.Remove(t_xm_investment);
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