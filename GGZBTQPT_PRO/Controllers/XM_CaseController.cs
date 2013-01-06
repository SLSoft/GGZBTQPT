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
using GGZBTQPT_PRO.Util;
using GGZBTQPT_PRO.ViewModels;

namespace GGZBTQPT_PRO.Controllers
{
    public class XM_CaseController : BaseController
    { 
        public ActionResult CasesFromUser(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            IList<GGZBTQPT_PRO.Models.T_XM_Case> list = db.T_XM_Case.Where(p => p.Name.Contains(keywords)).Where(p => p.IsValid == true && p.GenerateType == (int)GenerateTypes.FromUser)
                                                            .OrderByDescending(c => c.CreatedAt)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_XM_Case.Where(p => p.Name.Contains(keywords)).Where(p => p.IsValid == true && p.GenerateType == (int)GenerateTypes.FromUser).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;

            return View(list);
        }

        public ActionResult CasesFromMember(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            IList<GGZBTQPT_PRO.Models.T_XM_Case> list = db.T_XM_Case.Where(p => p.Name.Contains(keywords)).Where(p => p.IsValid == true && p.GenerateType == (int)GenerateTypes.FromMember)
                                                            .OrderByDescending(c => c.CreatedAt)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_XM_Case.Where(p => p.Name.Contains(keywords)).Where(p => p.IsValid == true && p.GenerateType == (int)GenerateTypes.FromMember).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;

            return View(list);
        }

        public ActionResult CasesFromTransaction(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            IList<GGZBTQPT_PRO.Models.T_XM_Case> list = db.T_XM_Case.Where(p => p.Name.Contains(keywords)).Where(p => p.IsValid == true && p.GenerateType == (int)GenerateTypes.FromSystem)
                                                            .OrderByDescending(c => c.CreatedAt)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_XM_Case.Where(p => p.Name.Contains(keywords)).Where(p => p.IsValid == true && p.GenerateType == (int)GenerateTypes.FromSystem).Count();
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
                    t_xm_case.GenerateType = (int)GenerateTypes.FromUser;
                    t_xm_case.IsPublished = false;
                    t_xm_case.GenerateFromID = CurrentUser().ID;
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
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    }
                    else
                    {
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

        public ActionResult Edit(int id, int generate_type)
        {
            T_XM_Case t_xm_case = db.T_XM_Case.Find(id);
            ViewBag.GenerateType = generate_type;
            return View(t_xm_case);
        }

        [HttpPost, ActionName("Edit")]
        public JsonResult Edit(T_XM_Case t_xm_case, int generate_type)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    t_xm_case.UpdatedAt = DateTime.Now;
                    t_xm_case.IsValid = true;
                    t_xm_case.GenerateType = generate_type;
                    BusinessService.TransformatFromXM(1,"xx",1,"xxx");
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
        
        public ActionResult Analysis(int xm_id, int xm_type)
        {
            AnalysisResult result = new AnalysisResult();
            switch(xm_type)
            {
                case (int)CaseTypes.Financing:
                    result = GetFinancingAnalysisResult(xm_id, result); 
                    break;
                case (int)CaseTypes.Investment:
                    result = GetInvestmentAnalysisResult(xm_id, result);
                    break;
            }   
            
            return PartialView(result);  
        }


        #region//外网相关
        public ActionResult PublishCaseToCMS(int id)
        {
            var xm_case = db.T_XM_Case.Find(id);
            object[] args = new object[17];

            args[0] = "admin";
            args[1] = "!?gg2012";
            args[2] = xm_case.Name;
            args[3] = xm_case.Name;
            args[4] = "#000";
            args[5] = 0;
            args[6] = "";
            args[7] = xm_case.Analysis;
            args[8] = xm_case.Name;
            args[9] = 0;
            args[10] = "";
            args[11] = "光谷资本大厦";
            args[12] = "";
            args[13] = 1052;
            args[14] = "光谷资本大厦";
            args[15] = 1;
            args[16] = 0; 

            object result = WebServiceProxy.InvokeWebService("http://www.ovcstf.com/SL_CMS_PBWS/SL_CMS_AddArticleService.asmx",
                                                            "AddArticle",
                                                            args);

            if ((int)result > 0)
            {
                xm_case.IsPublished = true;
                db.Entry(xm_case).State = EntityState.Modified;
                db.SaveChanges();
                return ReturnJson(true, "成功发布案例成功", "", "", false, "");
            }
            else
            {
                return ReturnJson(false, "案例发布失败", "", "", false, "");
            }
        }
        #endregion
 
        #region//private method
        private AnalysisResult GetFinancingAnalysisResult(int finacial_id, AnalysisResult result)
        {
            //to-do:
            //添加成交项目的判断
            var finacial = db.T_XM_Financing.Find(finacial_id); 
            var finacial_list = db.T_XM_Financing
                                  .Where(f => f.ItemType == finacial.ItemType && f.Industry == finacial.Industry && f.IsValid == true)
                                  .ToList();

            result.Turnover = (Decimal)finacial_list.Average(m => m.TotalInvestment);
            result.ReturnRatio = (Double)finacial_list.Average(m => m.ReturnRatio);
            result.Num = finacial_list.Count();
            result.TurnoverRatio = finacial_list.Count() % db.T_XM_Financing.Where(f => f.IsValid == true).Count(); 
            
            return result;
        }


        private AnalysisResult GetInvestmentAnalysisResult(int investment_id, AnalysisResult result)
        {
            var investment = db.T_XM_Investment.Find(investment_id);
            var investment_list = db.T_XM_Investment
                                    .Where(i => i.AimIndustry.IndexOf((String)investment.AimIndustry) >= 0 && i.TeamworkType.IndexOf((String)investment.TeamworkType) >= 0)
                                    .ToList();

            result.Turnover = (Decimal)investment_list.Average(m => m.Investment);
            result.ReturnRatio = (Double)investment_list.Average(m => m.ReturnRatio);
            result.Num = investment_list.Count();
            result.TurnoverRatio = investment_list.Count() % db.T_XM_Financing.Where(f => f.IsValid == true).Count(); 

            return result;

        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }

}
