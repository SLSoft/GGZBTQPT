﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using System.IO;
using GGZBTQPT_PRO.ViewModels;

namespace GGZBTQPT_PRO.Controllers
{
    public class XM_RZController : BaseController
    {
        //private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /XM_RZ/

        public ViewResult Index(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;
            var t_xm_financing = db.T_XM_Financing.Where(p => (p.IsValid == true && p.ItemName.Contains(keywords))).OrderByDescending(p => p.CreateTime)
                                                                    .Skip(numPerPage * (pageNum - 1))
                                                                    .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_XM_Financing.Where(p => (p.IsValid == true && p.ItemName.Contains(keywords))).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            return View(t_xm_financing);
        }

        //
        // GET: /XM_RZ/Details/5

        public ViewResult Details(int id)
        {
            T_XM_Financing t_xm_financing = db.T_XM_Financing.Find(id);
            return View(t_xm_financing);
        }

        public void BindIndustry(object select = null)
        {
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();

            ViewData["Industry"] = new SelectList(Industry,"ID","Name",select);
        }

        public void BindArea(object select = null)
        {
            List<T_PTF_DicTreeDetail> Area = db.T_PTF_DicTreeDetail.Where(p => (p.DicType == "34" && p.Depth == 1)).ToList();

            ViewData["Province"] = new SelectList(Area, "Code", "Name", select);
        }
        public void BindFinancType(object select = null)
        {
            List<T_PTF_DicDetail> FinancType = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM02")).ToList();

            ViewData["FinancType"] = new SelectList(FinancType, "ID", "Name", select);
        }
        public void BindItemStage(object select = null)
        {
            List<T_PTF_DicDetail> ItemStage = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM04")).ToList();

            ViewData["ItemStage"] = new SelectList(ItemStage, "ID", "Name", select);
        }
        public JsonResult GetCity(string ParentCode)
        {
            List<T_PTF_DicTreeDetail> City = db.T_PTF_DicTreeDetail.Where(p => (p.DicType == "34" && p.ParentCode == ParentCode)).ToList();
            
            return Json(City, JsonRequestBehavior.AllowGet);
        }
        public void BindAssetsType(object select = null)
        {
            List<T_PTF_DicDetail> AssetsType = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM03")).ToList();

            ViewData["AssetsType"] = new SelectList(AssetsType, "ID", "Name", select);
        }
        public void BindTransactionMode(object select = null)
        {
            List<T_PTF_DicDetail> TransactionMode = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM05")).ToList();

            ViewData["TransactionMode"] = new SelectList(TransactionMode, "ID", "Name", select);
        }
        public void BindCooperationMode(object select = null)
        {
            List<T_PTF_DicDetail> CooperationMode = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM06")).ToList();

            ViewData["CooperationMode"] = new SelectList(CooperationMode, "ID", "Name", select);
        }
        
        //
        // GET: /XM_RZ/Create

        public ActionResult Create()
        {
            BindArea();
            BindIndustry();
            BindFinancType();
            BindItemStage();
            BindAssetsType();
            BindTransactionMode();
            BindCooperationMode();
            var t_xm_financing = new T_XM_Financing();
            return View(t_xm_financing);
        } 

        //
        // POST: /XM_RZ/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(T_XM_Financing t_xm_financing, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                string checkedTransactionMode = (collection["TransactionMode"] + ",").Replace("false,", "");
                if (checkedTransactionMode.Length > 1)
                    checkedTransactionMode = checkedTransactionMode.Remove(checkedTransactionMode.Length - 1);
                t_xm_financing.TransactionMode = checkedTransactionMode == "," ? "" : checkedTransactionMode;
                t_xm_financing.City = collection["ddlCity"];
                t_xm_financing.ItemContent = t_xm_financing.ItemContent == null ? "" : t_xm_financing.ItemContent;
                t_xm_financing.IsValid = true;
                t_xm_financing.OP = 0;
                t_xm_financing.CreateTime = DateTime.Now;
                t_xm_financing.SubmitTime = DateTime.Now;

                HttpPostedFileBase file = Request.Files[0];
                //存入文件
                if (file.ContentLength > 0)
                {
                    t_xm_financing.Pic = new byte[Request.Files[0].InputStream.Length];
                    Request.Files[0].InputStream.Read(t_xm_financing.Pic, 0, t_xm_financing.Pic.Length);
                }
                db.T_XM_Financing.Add(t_xm_financing);
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
        }
        
        //
        // GET: /XM_RZ/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_XM_Financing t_xm_financing = db.T_XM_Financing.Find(id);
            BindArea(t_xm_financing.Province);
            BindIndustry(t_xm_financing.Industry);
            BindFinancType(t_xm_financing.FinancType);
            BindItemStage(t_xm_financing.ItemStage);
            BindAssetsType(t_xm_financing.AssetsType);
            BindTransactionMode(t_xm_financing.TransactionMode);
            BindCooperationMode(t_xm_financing.CooperationMode);
            return View(t_xm_financing);
        }

        //
        // POST: /XM_RZ/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(T_XM_Financing t_xm_financing, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_xm_financing).State = EntityState.Modified;
                string checkedTransactionMode = (collection["TransactionMode"] + ",").Replace("false,", "");
                if (checkedTransactionMode.Length > 1)
                    checkedTransactionMode = checkedTransactionMode.Remove(checkedTransactionMode.Length - 1);
                t_xm_financing.TransactionMode = checkedTransactionMode == "," ? "" : checkedTransactionMode;
                t_xm_financing.City = collection["ddlCity"];
                t_xm_financing.ItemContent = t_xm_financing.ItemContent == null ? "" : t_xm_financing.ItemContent;
                t_xm_financing.UpdateTime = DateTime.Now;

                HttpPostedFileBase file = Request.Files[0];
                //存入文件
                if (file.ContentLength > 0)
                {
                    t_xm_financing.Pic = new byte[Request.Files[0].InputStream.Length];
                    Request.Files[0].InputStream.Read(t_xm_financing.Pic, 0, t_xm_financing.Pic.Length);
                }
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
        }

        //
        // GET: /XM_RZ/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_XM_Financing t_xm_financing = db.T_XM_Financing.Find(id);
            return View(t_xm_financing);
        }

        //
        // POST: /XM_RZ/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.IsAjaxRequest())
            {
                T_XM_Financing t_xm_financing = db.T_XM_Financing.Find(id);
                t_xm_financing.IsValid = false;
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

        //待审核项目一览
        public ActionResult RZCheckList(int pageNum = 1, int numPerPage = 15)
        {
            IList<GGZBTQPT_PRO.Models.T_XM_Financing> list = db.T_XM_Financing.Where(p => (p.IsValid == true && p.PublicStatus == "1")).ToList()
                                                            .OrderByDescending(s => s.SubmitTime)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();
            
            ViewBag.recordCount = db.T_XM_Financing.Where(p => (p.IsValid == true && p.PublicStatus == "1")).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;

            return View(list);
        }
        //审核通过项目一览
        public ActionResult RZCheckList_Pass(int pageNum = 1, int numPerPage = 15)
        {
            IList<GGZBTQPT_PRO.Models.T_XM_Financing> list = db.T_XM_Financing.Where(p => (p.IsValid == true && p.PublicStatus == "2")).ToList()
                                                            .OrderByDescending(s => s.SubmitTime)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_XM_Financing.Where(p => (p.IsValid == true && p.PublicStatus == "2")).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;

            return View(list);
        }

        /// <summary>
        /// 项目审核、撤销审核
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RZCheck(int id, string state)
        {
            T_XM_Financing t_xm_financing = db.T_XM_Financing.Find(id);
            t_xm_financing.PublicStatus = state;
            t_xm_financing.PublicTime = DateTime.Now;
            t_xm_financing.UpdateTime = DateTime.Now;

            if (db.SaveChanges() > 0)
                if(state == "2")
                    return ReturnJson(true, "审核成功", "","", false, "");
                else
                    return ReturnJson(true, "撤销审核成功", "", "", false, "");
            else
                if (state == "2")
                    return ReturnJson(false, "审核失败", "", "", false, "");
                else
                    return ReturnJson(false, "撤销审核失败", "", "", false, "");
        }

        //helper
        public FileContentResult ShowPic(int xm_id)
        {
            byte[] pic;
            if (db.T_XM_Financing.Find(xm_id).Pic != null)
                pic = db.T_XM_Financing.Find(xm_id).Pic;
            else
                pic = new byte[1];
            return File(pic, "image/jpeg");
        }

        //项目分析--项目列表
        public ActionResult RZMatch(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;
            IList<GGZBTQPT_PRO.Models.T_XM_Financing> list = db.T_XM_Financing.Where(p => (p.IsValid == true && p.ItemName.Contains(keywords) && p.PublicStatus == "2")).ToList()
                                                            .OrderByDescending(s => s.SubmitTime)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewData["recordCount"] = db.T_XM_Financing.Where(p => (p.IsValid == true && p.ItemName.Contains(keywords) && p.PublicStatus == "2")).Count();
            ViewData["numPerPage"] = numPerPage;
            ViewData["pageNum"] = pageNum;
            ViewBag.keywords = keywords;
            return View(list);
        }

        //根据项目匹配对应的资金
        public ActionResult RZMatchResult(int id, int pageNum = 1, int numPerPage = 15)
        {
            T_XM_Financing financing = db.T_XM_Financing.Find(id);
            decimal amount = Convert.ToDecimal(financing.Amount);
            string strindustry = financing.Industry.ToString();
            ViewData["itemname"] = financing.ItemName;
            IList<GGZBTQPT_PRO.Models.T_XM_Investment> list = db.T_XM_Investment.Where(p => (p.IsValid == true && p.PublicStatus == "2" && p.Investment > amount && p.AimIndustry.Contains(strindustry))).ToList()
                                                            .OrderByDescending(s => s.SubmitTime)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_XM_Investment.Where(p => (p.IsValid == true && p.PublicStatus == "2" && p.Investment == amount && p.AimIndustry.Contains(strindustry))).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;

            return PartialView(list);
        }

        //项目查询
        public ActionResult RZQuery(FormCollection collection, int pageNum = 1, int numPerPage = 15)
        {
            ViewBag.condition1 = collection["condition1"];
            ViewBag.condition2 = collection["condition2"];
            ViewBag.condition3 = collection["condition3"];
            ViewBag.condition4 = collection["condition4"];
            ViewBag.context = collection["context"];
            ViewBag.keys = collection["keys"];
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();
            ViewData["Industry"] = new SelectList(Industry, "ID", "Name");
            List<T_PTF_DicDetail> ItemStage = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM04")).ToList();
            ViewData["ItemStage"] = new SelectList(ItemStage, "ID", "Name");
            string keys = "";
            string select_itemtype = "";
            string select_industry = "";
            string select_Financial = "";
            string select_itemstage = "";
            if (ViewBag.keys != null && ViewBag.keys.ToString().Trim() != "")
                keys = ViewBag.keys.ToString();
            if (ViewBag.condition1 != null && ViewBag.condition1.ToString().Trim() != "")
                select_itemtype = ViewBag.condition1.ToString().Substring(1);
            if (ViewBag.condition2 != null && ViewBag.condition2.ToString().Trim() != "")
                select_industry = ViewBag.condition2.ToString().Substring(1);
            if (ViewBag.condition3 != null && ViewBag.condition3.ToString().Trim() != "")
            {
                string[] temp = ViewBag.condition3.ToString().Substring(1).Split(',');
                select_itemstage += " and (";
                foreach (string str in temp)
                {
                    select_itemstage += " ItemStage = '" + str + "' or";
                }
                select_itemstage = select_itemstage.Substring(0, select_itemstage.Length - 3);
                select_itemstage += ")";
            }
            if (ViewBag.condition4 != null && ViewBag.condition4.ToString().Trim() != "")
            {
                string[] temp = ViewBag.condition4.ToString().Substring(1).Split(',');
                select_Financial += " and (";
                foreach (string str in temp)
                {
                    select_Financial += " FinancSum " + str + " or";
                }
                select_Financial = select_Financial.Substring(0, select_Financial.Length - 3);
                select_Financial += ")";
            }
            string order = "ID";
            System.Data.SqlClient.SqlParameter[] selparms = new System.Data.SqlClient.SqlParameter[6];
            selparms[0] = new System.Data.SqlClient.SqlParameter("@keys", keys);
            selparms[1] = new System.Data.SqlClient.SqlParameter("@ItemType", select_itemtype);
            selparms[2] = new System.Data.SqlClient.SqlParameter("@Industry", select_industry);
            selparms[3] = new System.Data.SqlClient.SqlParameter("@ItemStage", select_itemstage);
            selparms[4] = new System.Data.SqlClient.SqlParameter("@FinancSum", select_Financial);
            selparms[5] = new System.Data.SqlClient.SqlParameter("@Order", order);
            System.Data.SqlClient.SqlParameter[] selparms_new = new System.Data.SqlClient.SqlParameter[selparms.Length];

            for (int i = 0, j = selparms.Length; i < j; i++)
            {
                selparms_new[i] = (System.Data.SqlClient.SqlParameter)((ICloneable)selparms[i]).Clone();
            }

            IList<T_XM_Financing> financials = (from p in db.T_XM_Financing.SqlQuery("exec dbo.P_GetRZXMByCondition @keys,@ItemType,@Industry,@ItemStage,@FinancSum,@Order", selparms) select p).ToList().OrderByDescending(s => s.SubmitTime)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();
            ViewBag.recordCount = (from p in db.T_XM_Financing.SqlQuery("exec dbo.P_GetRZXMByCondition @keys,@ItemType,@Industry,@ItemStage,@FinancSum,@Order", selparms_new) select p).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            if (financials.Count == 0)
                ViewBag.Message = "未找到符合要求的项目!";
            return View(financials);
        }


        //指定会员发布的项目
        public ActionResult MemberFinancingList(int memberid, int pageNum = 1, int numPerPage = 10)
        {
            var t_xm_financing = db.T_XM_Financing.Where(p => (p.IsValid == true && p.MemberID == memberid)).OrderBy(p => p.CreateTime);
            ViewBag.recordCount = db.T_XM_Financing.Where(p => (p.IsValid == true && p.MemberID == memberid)).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            return View(t_xm_financing);
        }

        //项目统计
        public ActionResult RZXMReport()
        {
            var rzxm = db.T_XM_Financing.Where(p => p.IsValid == true).ToList();
            ViewBag.RZXMCount = rzxm.Count;//登记项目总量          
            
            return View();
        }

        #region 项目按行业统计
        public ActionResult DataReportbyIndustry()
        {
            var rzxm = db.T_XM_Financing.Where(p => p.IsValid == true).ToList();
            ViewBag.RZXMCount = rzxm.Count;//登记项目总量

            var up = db.T_XM_Financing.Where(p => p.IsValid == true).GroupBy(g => g.Industry)
                                    .Select(s => new { cnt = s.Count(), type = (int)s.Key });


            var list = from u in db.T_PTF_DicDetail
                       where u.DicType == "XM01"
                       join p in up on u.ID equals p.type into gj
                       from x in gj.DefaultIfEmpty()
                       orderby u.ID
                       select new VM_XMReport
                       {
                           TypeName = u.Name,
                           Count = x.type == null ? 0 : x.cnt
                       };
            return PartialView(list.ToList());
        }

        public ActionResult ChartReportbyIndustry()
        {
            var up = db.T_XM_Financing.Where(p => p.IsValid == true).GroupBy(g => g.Industry)
                                    .Select(s => new { cnt = s.Count(), type = (int)s.Key });


            var list = from u in db.T_PTF_DicDetail
                       where u.DicType == "XM01"
                       join p in up on u.ID equals p.type into gj
                       from x in gj.DefaultIfEmpty()
                       orderby u.ID
                       select new VM_XMReport
                       {
                           TypeName = u.Name,
                           Count = x.type == null ? 0 : x.cnt
                       };

            Dictionary<String, int> dic = new Dictionary<string, int>();
            foreach (VM_XMReport vmx in list.ToList())
            {
                dic.Add(vmx.TypeName, vmx.Count);
            }

            return Json(new { statData = dic }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //查看项目增加点击量
        public ActionResult FinancingClicks(int id)
        {
            T_XM_Financing t_xm_financing = db.T_XM_Financing.Find(id);
            if (t_xm_financing != null)
            {
                t_xm_financing.Clicks = t_xm_financing.Clicks + 1;

                db.Entry(t_xm_financing).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}