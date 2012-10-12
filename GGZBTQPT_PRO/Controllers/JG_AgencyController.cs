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
    public class JG_AgencyController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /JG_Agency/

        public ViewResult Index()
        {
            return View(db.T_JG_AgencyInfo.ToList());
        }

        //
        // GET: /JG_Agency/Details/5

        public ViewResult Details(int id)
        {
            T_JG_Agency t_jg_agency = db.T_JG_AgencyInfo.Find(id);
            return View(t_jg_agency);
        }

        //
        // GET: /JG_Agency/Create

        public ActionResult Create()
        {
            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName");
            return View();
        } 

        //
        // POST: /JG_Agency/Create

        [HttpPost]
        public ActionResult Create(T_JG_Agency t_jg_agency)
        {
            if (ModelState.IsValid)
            {
                db.T_JG_AgencyInfo.Add(t_jg_agency);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName", t_jg_agency.MemberID);
            return View(t_jg_agency);
        }
        
        //
        // GET: /JG_Agency/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_JG_Agency t_jg_agency = db.T_JG_AgencyInfo.Find(id);
            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName", t_jg_agency.MemberID);
            return View(t_jg_agency);
        }

        //
        // POST: /JG_Agency/Edit/5

        [HttpPost]
        public ActionResult Edit(T_JG_Agency t_jg_agency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_jg_agency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName", t_jg_agency.MemberID);
            return View(t_jg_agency);
        }

        //
        // GET: /JG_Agency/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_JG_Agency t_jg_agency = db.T_JG_AgencyInfo.Find(id);
            return View(t_jg_agency);
        }

        //
        // POST: /JG_Agency/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            T_JG_Agency t_jg_agency = db.T_JG_AgencyInfo.Find(id);
            db.T_JG_AgencyInfo.Remove(t_jg_agency);
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