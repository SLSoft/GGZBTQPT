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
    public class ZC_MenuController : BaseController
    {
        public ViewResult Index()
        {
            var t_zc_system = db.T_ZC_System.Where(p => p.IsValid == true).ToList();
            var t_zc_menu = db.T_ZC_Menu.Include(t => t.System);
            ViewBag.systems = t_zc_system;
            return View(t_zc_menu.Where(p => p.IsValid == true).ToList());
        }

        public ActionResult Create()
        {
            ViewBag.SystemID = new SelectList(db.T_ZC_System.Where(p => p.IsValid == true), "ID", "Name");
            ViewBag.ParentID = new SelectList(db.T_ZC_Menu.Where(p => p.IsValid == true), "ID", "Name");
            return View();
        } 

        [HttpPost]
        public ActionResult Create(T_ZC_Menu t_zc_menu)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    t_zc_menu.CreatedAt = DateTime.Now;
                    t_zc_menu.UpdatedAt = DateTime.Now;
                    t_zc_menu.IsValid = true;
                    db.T_ZC_Menu.Add(t_zc_menu);
                    int result = db.SaveChanges();
                    if (result > 0)
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
            }
            ViewBag.SystemID = new SelectList(db.T_ZC_System.Where(p => p.IsValid == true), "ID", "Name", t_zc_menu.SystemID);
            ViewBag.ParentID = new SelectList(db.T_ZC_Menu.Where(p => p.IsValid == true), "ID", "Name", t_zc_menu.ParentID);
            return Json(new { });
        }
        
        public ActionResult Edit(int id)
        {
            T_ZC_Menu t_zc_menu = db.T_ZC_Menu.Find(id);
            ViewBag.SystemID = new SelectList(db.T_ZC_System.Where(p => p.IsValid == true), "ID", "Name", t_zc_menu.SystemID);
            ViewBag.ParentID = new SelectList(db.T_ZC_Menu.Where(p => p.IsValid == true), "ID", "Name", t_zc_menu.ParentID);
            return View("Edit", t_zc_menu);
        }

        [HttpPost]
        public ActionResult Edit(T_ZC_Menu t_zc_menu)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    t_zc_menu.UpdatedAt = DateTime.Now;
                    t_zc_menu.IsValid = true;
                    db.Entry(t_zc_menu).State = EntityState.Modified;
                    int result = db.SaveChanges();
                    if (result >= 0)
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
            }
            ViewBag.SystemID = new SelectList(db.T_ZC_System.Where(p => p.IsValid == true), "ID", "Name", t_zc_menu.SystemID);
            ViewBag.ParentID = new SelectList(db.T_ZC_Menu.Where(p => p.IsValid == true), "ID", "Name", t_zc_menu.ParentID);
            //return PartialView("Edit",t_zc_menu);
            return Json(new { });
        }

        //public ActionResult Delete(int id)
        //{
        //    T_ZC_Menu t_zc_menu = db.T_ZC_Menu.Find(id);
        //    return View(t_zc_menu);
        //}

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            //T_ZC_Menu t_zc_menu = db.T_ZC_Menu.Find(id);
            //db.T_ZC_Menu.Remove(t_zc_menu);
            //db.SaveChanges();
            //return Json(new { statusCode = "200", message = "操作成功", navTabId = "", rel = "", callbackType = "dialogAjaxDone", forwardUrl = "" });
            if (Request.IsAjaxRequest())
            {
                T_ZC_Menu t_zc_menu = db.T_ZC_Menu.Find(id);
                t_zc_menu.IsValid = false;
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


        public PartialViewResult SystemLinks()
        {
            var links = db.T_ZC_System.Where(p => p.IsValid == true).ToList();
            return PartialView(links);
        }

        public PartialViewResult MenuInfo(int? pageNum, int? numPerPage, int id)
        {
            int pageIndex = pageNum.HasValue ? pageNum.Value - 1 : 0;
            int pageSize = numPerPage.HasValue && numPerPage.Value > 0 ? numPerPage.Value : 1;
            //IList<GGZBTQPT_PRO.Models.T_ZC_Menu> list = db.T_ZC_Menu.Include(t => t.System).Where(p => p.IsValid == true).ToList();
            IList<GGZBTQPT_PRO.Models.T_ZC_Menu> list = db.T_ZC_Menu.Include("System").Where(m => m.SystemID == id).Where(p => p.IsValid == true).ToList();
            ViewBag.recordCount = list.Count();
            list = list.OrderBy(i => i.ID).Skip(pageSize * pageIndex).Take(pageSize).ToList();
            ViewBag.numPerPage = pageSize;
            ViewBag.pageNum = pageIndex + 1;
            ViewBag.ID = id;
            return PartialView(list);
        }

        //public PartialViewResult MenuInfo(int system_id)
        //{
        //    var menus = db.T_ZC_Menu.Where(p => p.IsValid == true).Include("System").Where(m => m.SystemID == system_id);
        //    return PartialView(menus);
        //}
    }
}