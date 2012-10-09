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
    public class ZC_RoleController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /ZC_Role/

        public ViewResult Index()
        {
            return View(db.T_ZC_Role.ToList());
        }

        //
        // GET: /ZC_Role/Details/5

        public ViewResult Details(int id)
        {
            T_ZC_Role t_zc_role = db.T_ZC_Role.Find(id);
            return View(t_zc_role);
        }

        //
        // GET: /ZC_Role/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ZC_Role/Create

        [HttpPost]
        public ActionResult Create(T_ZC_Role t_zc_role)
        {
            if (ModelState.IsValid)
            {
                db.T_ZC_Role.Add(t_zc_role);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(t_zc_role);
        }
        
        //
        // GET: /ZC_Role/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_ZC_Role t_zc_role = db.T_ZC_Role.Find(id);
            return View(t_zc_role);
        }

        //
        // POST: /ZC_Role/Edit/5

        [HttpPost]
        public ActionResult Edit(T_ZC_Role t_zc_role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_zc_role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_zc_role);
        }

        //
        // GET: /ZC_Role/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_ZC_Role t_zc_role = db.T_ZC_Role.Find(id);
            return View(t_zc_role);
        }

        //
        // POST: /ZC_Role/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            T_ZC_Role t_zc_role = db.T_ZC_Role.Find(id);
            db.T_ZC_Role.Remove(t_zc_role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// 返回当前角色下的所有用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult SelectUser(int id)
        {
            string selected_users = GenerateStringFromList(db.T_ZC_Role.Where( r => r.ID == id).First().Users.ToList());
            ViewBag.selected_users = selected_users;

            var select_user = new VM_SelectUser();
            select_user.Users = db.T_ZC_User.ToList();
            select_user.Departments = db.T_ZC_Department.ToList();

            return PartialView(select_user);
        }

        public string GenerateStringFromList(ICollection<T_ZC_User> users)
        {
            string select_users = "";
            foreach( T_ZC_User user in users)
            {
                select_users += user.ID + "|";
            }
            return select_users;
        }

    }
}