using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.ViewModels;

namespace GGZBTQPT_PRO.Controllers
{ 
    public class ZC_UserController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /ZC_User/

        public ViewResult Index()
        {
            var t_zc_user = db.T_ZC_User.Include(t => t.Department);
            return View(t_zc_user.ToList());
        }

        //
        // GET: /ZC_User/Details/5

        public ViewResult Details(int id)
        {
            T_ZC_User t_zc_user = db.T_ZC_User.Find(id);
            return View(t_zc_user);
        }

        //
        // GET: /ZC_User/Create

        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.T_ZC_Department, "ID", "Name");
            return View();
        } 

        //
        // POST: /ZC_User/Create

        [HttpPost]
        public ActionResult Create(T_ZC_User t_zc_user)
        {
            if (ModelState.IsValid)
            {
                db.T_ZC_User.Add(t_zc_user);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.DepartmentID = new SelectList(db.T_ZC_Department, "ID", "Name", t_zc_user.DepartmentID);
            return View(t_zc_user);
        }
        
        //
        // GET: /ZC_User/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_ZC_User t_zc_user = db.T_ZC_User.Find(id);
            ViewBag.DepartmentID = new SelectList(db.T_ZC_Department, "ID", "Name", t_zc_user.DepartmentID);
            return View(t_zc_user);
        }

        //
        // POST: /ZC_User/Edit/5

        [HttpPost]
        public ActionResult Edit(T_ZC_User t_zc_user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_zc_user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.T_ZC_Department, "ID", "Name", t_zc_user.DepartmentID);
            return View(t_zc_user);
        }

        //
        // GET: /ZC_User/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_ZC_User t_zc_user = db.T_ZC_User.Find(id);
            return View(t_zc_user);
        }

        //
        // POST: /ZC_User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            T_ZC_User t_zc_user = db.T_ZC_User.Find(id);
            db.T_ZC_User.Remove(t_zc_user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public PartialViewResult DepartmentLinks()
        {
            var links = db.T_ZC_Department.ToList();
            return PartialView(links);
        }

        public PartialViewResult UserInfo(int department_id)
        {
            var users = db.T_ZC_User.Include("Department").Where(m => m.DepartmentID == department_id);
            return PartialView(users);
        }

        public PartialViewResult SelectUsers()
        { 
            var depart_user = new VM_SelectUser();
            depart_user.Departments = db.T_ZC_Department.ToList();
            depart_user.Users = db.T_ZC_User.Include("Department").ToList();
            return PartialView(depart_user);
        }
    }
}