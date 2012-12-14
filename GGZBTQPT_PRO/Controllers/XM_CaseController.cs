using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.Enums;
using System.IO;

namespace GGZBTQPT_PRO.Controllers
{
    public class XM_CaseController : BaseController
    {
        public ActionResult Index(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            IList<GGZBTQPT_PRO.Models.T_XM_Case> list = db.T_XM_Case.Where(p => p.Name.Contains(keywords)).Where(p => p.IsValid == true)
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_XM_Case.Where(p => p.Name.Contains(keywords)).Where(p => p.IsValid == true).Count();
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
        public ActionResult Create(T_XM_Case t_xm_case)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    t_xm_case.CreatedAt = DateTime.Now;
                    t_xm_case.UpdatedAt = DateTime.Now;
                    t_xm_case.IsValid = true;
                    if(Session["CaseFile"] != null && Session["CaseFile"].ToString() != "")
                    {
                        Stream stream = (Stream)Session["CaseFile"];
                        //存入文件
                        if (stream.Length > 0)
                        {
                            t_xm_case.File = new byte[stream.Length];
                            stream.Read(t_xm_case.File, 0, t_xm_case.File.Length);
                        } 
                    }
                    
                    db.T_XM_Case.Add(t_xm_case);
                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        Logging((int)LogTypes.operate, "成功新增了一条系统数据:" + t_xm_case.Name);
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    }
                    else
                    {
                        Logging((int)LogTypes.warn, "新增系统失败:" + t_xm_case.Name);
                        return ReturnJson(false, "操作失败", "", "", false, "");
                    }
                }
            }
            return Json(new { });
        }

        [HttpPost]
        public ActionResult TemporariedFile()
        {
            try
            {
                Stream stream = Request.Files.Count > 0
                                        ? Request.Files[0].InputStream
                                        : Request.InputStream; 

                Session["CaseFile"] = stream;
                return Json(new { success = "上传成功!" });
            }
            catch
            {
                return Json(new { error = "上传失败!" }); 
            }
        }

        public ActionResult DownLoadFile(int id)
        {
            byte[] file;
            if (db.T_XM_Case.Find(id).File != null)
                file = db.T_XM_Case.Find(id).File;
            else
                file = new byte[1];
            return File(file, "application/msword");
        }

        public ActionResult Edit(int id)
        {
            T_XM_Case t_xm_case = db.T_XM_Case.Find(id);
            return View(t_xm_case);
        }

        [HttpPost, ActionName("Edit")]
        public JsonResult Edit(T_XM_Case t_xm_case)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    t_xm_case.UpdatedAt = DateTime.Now;
                    t_xm_case.IsValid = true;
                    if (Session["CaseFile"] != null && Session["CaseFile"].ToString() != "")
                    {
                        Stream stream = (Stream)Session["CaseFile"];
                        //存入文件
                        if (stream.Length > 0)
                        {
                            t_xm_case.File = new byte[stream.Length];
                            stream.Read(t_xm_case.File, 0, t_xm_case.File.Length);
                        }
                    }
                    db.Entry(t_xm_case).State = EntityState.Modified;
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
                T_XM_Case t_xm_case = db.T_XM_Case.Find(id);
                t_xm_case.IsValid = false;
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
