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
            var t_zc_menu = db.T_ZC_Menu.Include(t => t.System).Where(p => p.IsValid == true).ToList();

            ViewBag.systems = t_zc_system;
            return View(t_zc_menu);
        }

        public ActionResult Create(int system_id)
        {
            ViewBag.SystemID = new SelectList(db.T_ZC_System.Where(p => p.IsValid == true), "ID", "Name", system_id);
            ViewBag.ParentID = new SelectList(db.T_ZC_Menu.Where(p => p.IsValid == true && p.SystemID == system_id), "ID", "Name");

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

            ViewBag.SystemID = new SelectList(db.T_ZC_System.Where(p => p.IsValid == true), "ID", "Name",t_zc_menu.SystemID);
            ViewBag.ParentID = new SelectList(db.T_ZC_Menu.Where(p => p.IsValid == true), "ID", "Name", t_zc_menu.ParentID);
            return Json(new { });
        }

        public ActionResult Edit(int id, int system_id)
        {
            T_ZC_Menu t_zc_menu = db.T_ZC_Menu.Find(id);

            ViewBag.SystemID = new SelectList(db.T_ZC_System.Where(p => p.IsValid == true), "ID", "Name", system_id);
            ViewBag.ParentID = new SelectList(db.T_ZC_Menu.Where(p => p.IsValid == true && p.SystemID == system_id), "ID", "Name", t_zc_menu.ParentID);
            return View(t_zc_menu);
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
                        return ReturnJson(true, "操作成功", "", "menuInfoBox", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
            }

            ViewBag.SystemID = new SelectList(db.T_ZC_System.Where(p => p.IsValid == true), "ID", "Name", t_zc_menu.SystemID);
            ViewBag.ParentID = new SelectList(db.T_ZC_Menu.Where(p => p.IsValid == true), "ID", "Name", t_zc_menu.ParentID); 
            return Json(new { });
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
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

        public PartialViewResult MenuInfo(int id, int pageNum = 1, int numPerPage = 15)
        {

            IList<GGZBTQPT_PRO.Models.T_ZC_Menu> list = db.T_ZC_Menu.Include("System")
                                                                    .Where(m => m.SystemID == id && m.IsValid == true)
                                                                    .OrderBy(s => s.ID)
                                                                    .Skip(numPerPage * (pageNum - 1))
                                                                    .Take(numPerPage).ToList(); 
            ViewBag.recordCount = db.T_ZC_Menu.Where(m => m.SystemID == id && m.IsValid == true).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.ID = id;
            ViewBag.SystemID = id;
            return PartialView(list);
        }


        //
        //helper
        public int ConvertMenuCode(T_ZC_Menu menu)
        {
            if(menu.ParentID.ToString() != "")
            {
                return menu.ParentID;
            }  
            return 0; 
        }


    }
}