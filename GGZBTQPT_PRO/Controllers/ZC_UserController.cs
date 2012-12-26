using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.ViewModels;
using GGZBTQPT_PRO.Enums;

namespace GGZBTQPT_PRO.Controllers
{
    public class ZC_UserController : BaseController
    {
        public ViewResult Index()
        {
            var t_zc_user = db.T_ZC_User.Include(t => t.Department).Where(p => p.IsValid == true && p.UseLevel == (int)UseLevel.System).ToList();
            return View(t_zc_user);
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
                    t_zc_user.UseLevel = (int)UseLevel.System;
                    t_zc_user.IsValid = true;
                    db.T_ZC_User.Add(t_zc_user);
                    int result = db.SaveChanges();
                    if (result > 0)
                        return ReturnJson(true, "操作成功", "", "userInfoBox", true, "");
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
                    t_zc_user.UseLevel = (int)UseLevel.System;
                    t_zc_user.IsValid = true;
                    db.Entry(t_zc_user).State = EntityState.Modified;
                    int result = db.SaveChanges();
                    if (result >= 0)
                        return ReturnJson(true, "操作成功", "", "userInfoBox", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
            }
            ViewBag.DepartmentID = new SelectList(db.T_ZC_Department, "ID", "Name", t_zc_user.DepartmentID);
            return Json(new { });
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {  
            if (Request.IsAjaxRequest())
            {
                T_ZC_User t_zc_user = db.T_ZC_User.Find(id);
                t_zc_user.IsValid = false;
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "userInfoBox", false, "");
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

        public PartialViewResult UserInfo(int id, int pageNum = 1, int numPerPage = 15)
        { 
            IList<GGZBTQPT_PRO.Models.T_ZC_User> list = db.T_ZC_User.Include("Department")
                                                        .Where(m => m.DepartmentID == id).Where(p => p.IsValid == true)
                                                        .OrderBy(s => s.ID)
                                                        .Skip(numPerPage * (pageNum - 1))
                                                        .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_ZC_User.Where(m => m.DepartmentID == id && m.IsValid == true).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.ID = id;
            ViewBag.DepartmentID = id;
            return PartialView(list);
        }

        public PartialViewResult DepartmentLinks()
        {
            var links = db.T_ZC_Department.Where(p => p.IsValid == true && p.UseLevel == (int)UseLevel.System).ToList();
            return PartialView(links);
        }

        public PartialViewResult SelectUsers()
        { 
            var depart_user = new VM_SelectUser();
            depart_user.Departments = db.T_ZC_Department.Where(p => p.IsValid == true && p.UseLevel == (int)UseLevel.System).ToList();
            depart_user.Users = db.T_ZC_User.Where(p => p.IsValid == true && p.UseLevel == (int)UseLevel.System).Include("Department").ToList();
            return PartialView(depart_user);
        }

    }
}