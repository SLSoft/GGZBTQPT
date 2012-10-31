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
    public class ZC_SystemController : BaseController
    {
        public ActionResult Index(int? pageNum, int? numPerPage, string keywords)
        {
            int pageIndex = pageNum.HasValue ? pageNum.Value - 1 : 0;
            int pageSize = numPerPage.HasValue && numPerPage.Value > 0 ? numPerPage.Value : 1;
            keywords = keywords == null ? "" : keywords;
            IList<GGZBTQPT_PRO.Models.T_ZC_System> list = db.T_ZC_System.Where(p => p.Name.Contains(keywords)).Where(p => p.IsValid == true).ToList();
            //ViewBag.recordCount = db.T_ZC_System.Where(p => p.IsValid == true).Count();
            ViewBag.recordCount = list.Count();
            list = list.OrderBy(i => i.ID).Skip(pageSize * pageIndex).Take(pageSize).ToList();
            ViewBag.numPerPage = pageSize;
            ViewBag.pageNum = pageIndex + 1;
            ViewBag.keywords = keywords;
            return View(list);
        }

        //public ViewResult Index(FormCollection form)
        //{
        //    keyString = form["keyword"];
        //    //int p = Int32.Parse(form["pageNum"]);
        //    //int size = Int32.Parse(form["numPerPage"]);  
        //    int p = 1;
        //    int size = 5;  
        //    //var List = from ds in db.T_ZC_System orderby ds.ID ascending
        //    //           where keyString == null || ds.Name.Contains(keyString) || ds.Name.Contains(keyString) select ds;
        //    //return View(List.Skip((PageNum - 1) * NumPerPage).Take(NumPerPage));
        //    var List = db.T_ZC_System.Select(n => n).OrderBy(n => n.ID).Skip((p - 1) * size).Take(size);  
        //    TotalCount = List.Count();
        //    ViewData["List"] = List;
        //    ViewData["recordCount"] = form["recordCount"];
        //    ViewData["pageNum"] = p;
        //    ViewData["numPerPage"] = size;
        //    return View();              
        //    //return View(db.T_ZC_System.Where(p => p.IsValid == true).ToList());
        //}

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public ActionResult Create(T_ZC_System t_zc_system)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    t_zc_system.CreatedAt = DateTime.Now;
                    t_zc_system.UpdatedAt = DateTime.Now;
                    t_zc_system.IsValid = true;
                    db.T_ZC_System.Add(t_zc_system);
                    int result = db.SaveChanges();
                    if (result > 0)
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
            }
            return Json(new { });
        }
        
        public ActionResult Edit(int id)
        {
            T_ZC_System t_zc_system = db.T_ZC_System.Find(id);
            return View(t_zc_system);
        }

        [HttpPost, ActionName("Edit")]
        public JsonResult Edit(T_ZC_System t_zc_system)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    t_zc_system.UpdatedAt = DateTime.Now;
                    t_zc_system.IsValid = true;
                    db.Entry(t_zc_system).State = EntityState.Modified;
                    int result = db.SaveChanges();
                    if (result >= 0)
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
            }
            return Json("");
        }

        //public ActionResult Delete(int id)
        //{
        //    T_ZC_System t_zc_system = db.T_ZC_System.Find(id);
        //    return View(t_zc_system);
        //}

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            if (Request.IsAjaxRequest())
            {
                T_ZC_System t_zc_system = db.T_ZC_System.Find(id);
                t_zc_system.IsValid = false;
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