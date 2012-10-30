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
    public class ZC_UserController : BaseController
    {
        public ViewResult Index()
        {
            var t_zc_user = db.T_ZC_User.Include(t => t.Department);
            return View(t_zc_user.Where(p => p.IsValid == true).ToList());
        }

        public ViewResult Details(int id)
        {
            T_ZC_User t_zc_user = db.T_ZC_User.Find(id);
            return View(t_zc_user);
        }

        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.T_ZC_Department, "ID", "Name");
            return View();
        } 

        [HttpPost]
        public ActionResult Create(T_ZC_User t_zc_user)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    t_zc_user.CreatedAt = DateTime.Now;
                    t_zc_user.UpdatedAt = DateTime.Now;
                    t_zc_user.IsValid = true;
                    db.T_ZC_User.Add(t_zc_user);
                    int result = db.SaveChanges();
                    if (result > 0)
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
            }
            ViewBag.DepartmentID = new SelectList(db.T_ZC_Department, "ID", "Name", t_zc_user.DepartmentID);
            return Json(new { });
        }
        
        public ActionResult Edit(int id)
        {
            T_ZC_User t_zc_user = db.T_ZC_User.Find(id);
            ViewBag.DepartmentID = new SelectList(db.T_ZC_Department, "ID", "Name", t_zc_user.DepartmentID);
            return View(t_zc_user);
        }

        [HttpPost]
        public ActionResult Edit(T_ZC_User t_zc_user)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    t_zc_user.UpdatedAt = DateTime.Now;
                    t_zc_user.IsValid = true;
                    db.Entry(t_zc_user).State = EntityState.Modified;
                    int result = db.SaveChanges();
                    if (result >= 0)
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
            }
            ViewBag.DepartmentID = new SelectList(db.T_ZC_Department, "ID", "Name", t_zc_user.DepartmentID);
            return Json(new { });
        }

        //public ActionResult Delete(int id)
        //{
        //    T_ZC_User t_zc_user = db.T_ZC_User.Find(id);
        //    return View(t_zc_user);
        //}

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            //T_ZC_User t_zc_user = db.T_ZC_User.Find(id);
            //db.T_ZC_User.Remove(t_zc_user);
            //db.SaveChanges();
            //return RedirectToAction("Index");
            if (Request.IsAjaxRequest())
            {
                T_ZC_User t_zc_user = db.T_ZC_User.Find(id);
                t_zc_user.IsValid = false;
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", false, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //public PartialViewResult UserInfo(int department_id)
        //{
        //    var users = db.T_ZC_User.Include("Department").Where(m => m.DepartmentID == department_id);
        //    return PartialView(users.Where(p => p.IsValid == true));
        //}

        public PartialViewResult UserInfo(int? pageNum, int? numPerPage, int id)
        {
            int pageIndex = pageNum.HasValue ? pageNum.Value - 1 : 0;
            int pageSize = numPerPage.HasValue && numPerPage.Value > 0 ? numPerPage.Value : 1;
            //IList<GGZBTQPT_PRO.Models.T_ZC_User> list = db.T_ZC_User.Include(t => t.Department).Where(p => p.IsValid == true).ToList();
            IList<GGZBTQPT_PRO.Models.T_ZC_User> list = db.T_ZC_User.Include("Department").Where(m => m.DepartmentID == id).Where(p => p.IsValid == true).ToList();
            ViewBag.recordCount = list.Count();
            list = list.OrderBy(i => i.ID).Skip(pageSize * pageIndex).Take(pageSize).ToList();
            ViewBag.numPerPage = pageSize;
            ViewBag.pageNum = pageIndex + 1;
            ViewBag.ID = id;
            return PartialView(list);
        }

        public PartialViewResult DepartmentLinks()
        {
            var links = db.T_ZC_Department.Where(p => p.IsValid == true).ToList();
            return PartialView(links);
        }

        public PartialViewResult SelectUsers()
        { 
            var depart_user = new VM_SelectUser();
            depart_user.Departments = db.T_ZC_Department.Where(p => p.IsValid == true).ToList();
            depart_user.Users = db.T_ZC_User.Where(p => p.IsValid == true).Include("Department").ToList();
            return PartialView(depart_user);
        }
    }
}