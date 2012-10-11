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
    public class QY_CorpController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /QY_Corp/

        public ViewResult Index()
        {
            return View(db.T_QY_CorpInfo.ToList());
        }

        //
        // GET: /QY_Corp/Details/5

        public ViewResult Details(int id)
        {
            T_QY_Corp t_qy_corp = db.T_QY_CorpInfo.Find(id);
            return View(t_qy_corp);
        }

        //
        // GET: /QY_Corp/Create

        public ActionResult Create()
        {
            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName");
            return View();
        } 

        //
        // POST: /QY_Corp/Create

        [HttpPost]
        public ActionResult Create(T_QY_Corp t_qy_corp)
        {
            if (ModelState.IsValid)
            {
                db.T_QY_CorpInfo.Add(t_qy_corp);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName", t_qy_corp.MemberID);
            return View(t_qy_corp);
        }
        
        //
        // GET: /QY_Corp/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_QY_Corp t_qy_corp = db.T_QY_CorpInfo.Find(id);
            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName", t_qy_corp.MemberID);
            return View(t_qy_corp);
        }

        //
        // POST: /QY_Corp/Edit/5

        [HttpPost]
        public ActionResult Edit(T_QY_Corp t_qy_corp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_qy_corp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName", t_qy_corp.MemberID);
            return View(t_qy_corp);
        }

        //
        // GET: /QY_Corp/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_QY_Corp t_qy_corp = db.T_QY_CorpInfo.Find(id);
            return View(t_qy_corp);
        }

        //
        // POST: /QY_Corp/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            T_QY_Corp t_qy_corp = db.T_QY_CorpInfo.Find(id);
            db.T_QY_CorpInfo.Remove(t_qy_corp);
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