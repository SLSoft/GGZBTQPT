using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.Enums;

namespace GGZBTQPT_PRO.Controllers
{
    public class ZC_DepartmentController : BaseController
    {
        public ActionResult Index(string keywords, int pageNum = 1, int numPerPage = 15)
        { 
            keywords = keywords == null ? "" : keywords;

            IList<GGZBTQPT_PRO.Models.T_ZC_Department> list = db.T_ZC_Department.Where(p => p.Name.Contains(keywords)).Where(p => p.IsValid == true && p.UseLevel == (int)UseLevel.System)
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_ZC_Department.Where(p => p.Name.Contains(keywords)).Where(p => p.IsValid == true).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;

            return View(list);
        }

        public ActionResult Create()
        {
            ViewBag.ParentID = new SelectList(db.T_ZC_Department.Where(p => p.IsValid == true), "ID", "Name"); 
            return View();
        } 

        [HttpPost]
        public ActionResult Create(T_ZC_Department t_zc_department)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    t_zc_department.CreatedAt = DateTime.Now;
                    t_zc_department.UpdatedAt = DateTime.Now;
                    t_zc_department.UseLevel = (int)UseLevel.System;
                    t_zc_department.IsValid = true;
                    db.T_ZC_Department.Add(t_zc_department);
                    int result = db.SaveChanges();
                    if (result > 0)
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
            }
            ViewBag.ParentID = new SelectList(db.T_ZC_Department.Where(p => p.IsValid == true), "ID", "Name", t_zc_department.ParentID);
            return Json(new { });
        }
        
        public ActionResult Edit(int id)
        {
            T_ZC_Department t_zc_department = db.T_ZC_Department.Find(id);
            ViewBag.ParentID = new SelectList(db.T_ZC_Department.Where(p => p.IsValid == true), "ID", "Name", t_zc_department.ParentID); 
            return View(t_zc_department);
        }

        [HttpPost]
        public ActionResult Edit(T_ZC_Department t_zc_department)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    t_zc_department.UpdatedAt = DateTime.Now;
                    t_zc_department.UseLevel = (int)UseLevel.System;
                    t_zc_department.IsValid = true;
                    db.Entry(t_zc_department).State = EntityState.Modified;
                    int result = db.SaveChanges();
                    if (result >= 0)
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
            }
            ViewBag.ParentID = new SelectList(db.T_ZC_Department.Where(p => p.IsValid == true), "ID", "Name", t_zc_department.ParentID);
            return Json(new { });
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {   
            if (Request.IsAjaxRequest())
            {
                T_ZC_Department t_zc_department = db.T_ZC_Department.Find(id);
                t_zc_department.IsValid = false;
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
    }
}