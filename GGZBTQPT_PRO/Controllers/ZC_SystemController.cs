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
    public class ZC_SystemController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNum">当前页码</param>
        /// <param name="numPerPage">每页显示多少条</param>
        /// <param name="keywords">搜索关键字</param>
        /// <returns></returns>
        public ActionResult Index( string keywords, int pageNum = 1, int numPerPage = 5)
        { 
            keywords = keywords == null ? "" : keywords;

            IList<GGZBTQPT_PRO.Models.T_ZC_System> list = db.T_ZC_System.Where(p => p.Name.Contains(keywords)).Where(p => p.IsValid == true)
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum -1))
                                                            .Take(numPerPage).ToList(); 

            ViewBag.recordCount = db.T_ZC_System.Where(p => p.Name.Contains(keywords)).Where(p => p.IsValid == true).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;

            return View(list);
        } 

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
                    {
                        Logging((int)LogTypes.operate,"成功新增了一条系统数据:" + t_zc_system.Name);
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    }
                    else
                    {
                        Logging((int)LogTypes.warn,"新增系统失败:" + t_zc_system.Name);
                        return ReturnJson(false, "操作失败", "", "", false, "");
                    }
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