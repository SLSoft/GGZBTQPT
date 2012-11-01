using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;

namespace GGZBTQPT_PRO.Areas.Member.Controllers
{ 
    public class JG_AgencyController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /JG_Agency/

        public ViewResult Index()
        {
            return View(db.T_JG_Agency.ToList());
        }

        public void BindAgencyType(object select = null)
        {
            List<T_PTF_DicDetail> AgencyType = db.T_PTF_DicDetail.Where(p => (p.DicType == "JG01" && p.IsValid == "1")).ToList();

            ViewData["AgencyType"] = new SelectList(AgencyType, "ID", "Name", select);
        }
        public void BindArea(object select = null)
        {
            List<T_PTF_DicTreeDetail> Area = db.T_PTF_DicTreeDetail.Where(p => (p.DicType == "34" && p.Depth == 1)).ToList();

            ViewData["Province"] = new SelectList(Area, "ID", "Name", select);
        }

        //
        // GET: /JG_Agency/Details/5

        public ViewResult Details(int id)
        {
            T_JG_Agency t_jg_agency = db.T_JG_Agency.Find(id);
            return View(t_jg_agency);
        }

        //
        // GET: /JG_Agency/Create

        public ActionResult Create()
        {
            BindArea();
            BindAgencyType();
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
                t_jg_agency.IsValid = true;
                t_jg_agency.OP = 0;
                t_jg_agency.CreateTime = DateTime.Now;
                t_jg_agency.UpdateTime = DateTime.Now;
                db.T_JG_Agency.Add(t_jg_agency);
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
            T_JG_Agency t_jg_agency = db.T_JG_Agency.Find(id);
            BindArea(t_jg_agency.Province);
            BindAgencyType(t_jg_agency.AgencyType);
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
            T_JG_Agency t_jg_agency = db.T_JG_Agency.Find(id);
            return View(t_jg_agency);
        }

        //
        // POST: /JG_Agency/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            T_JG_Agency t_jg_agency = db.T_JG_Agency.Find(id);
            db.T_JG_Agency.Remove(t_jg_agency);
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