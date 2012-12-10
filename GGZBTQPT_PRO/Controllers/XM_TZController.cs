﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using System.IO;
using System.Data.Objects.SqlClient;

namespace GGZBTQPT_PRO.Controllers
{
    public class XM_TZController : BaseController
    {
        //private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /XM_TZ/

        public ViewResult Index(int pageNum = 1, int numPerPage = 5)
        {
            var t_xm_investment = db.T_XM_Investment.Where(p => p.IsValid == true).OrderBy(s => s.ID)
                                                                    .Skip(numPerPage * (pageNum - 1))
                                                                    .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_XM_Investment.Where(c => c.IsValid == true).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            return View(t_xm_investment);
        }

        //
        // GET: /XM_TZ/Details/5

        public ViewResult Details(int id)
        {
            T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);
            return View(t_xm_investment);
        }
        public void BindIndustry(object select = null)
        {
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();

            ViewData["Industry"] = new SelectList(Industry, "ID", "Name", select);
        }
        public void BindTeamworkType(object select = null)
        {
            List<T_PTF_DicDetail> TeamworkType = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM06")).ToList();

            ViewData["TeamworkType"] = new SelectList(TeamworkType, "ID", "Name", select);
        }
        public void BindArea(object select = null)
        {
            List<T_PTF_DicTreeDetail> Area = db.T_PTF_DicTreeDetail.Where(p => (p.DicType == "34" && p.Depth == 1)).OrderBy(p => p.Taxis).ToList();

            ViewData["Province"] = new SelectList(Area, "Code", "Name", select);
        }

        public JsonResult GetCity(string ParentCode)
        {
            List<T_PTF_DicTreeDetail> City = db.T_PTF_DicTreeDetail.Where(p => (p.DicType == "34" && p.ParentCode == ParentCode)).ToList();

            return Json(City, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /XM_TZ/Create

        public ActionResult Create()
        {
            BindArea();
            BindIndustry();
            BindTeamworkType();
            var t_xm_investment = new T_XM_Investment();
            return View(t_xm_investment);
        } 

        //
        // POST: /XM_TZ/Create

        [HttpPost]
        public ActionResult Create(T_XM_Investment t_xm_investment, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                string checkedIndustry = (collection["cbIndustry"] + ",").Replace("false,", "");
                if (checkedIndustry.Length > 1)
                    checkedIndustry = checkedIndustry.Remove(checkedIndustry.Length - 1);
                string checkedProvince = (collection["cbProvince"] + ",").Replace("false,", "");
                if (checkedProvince.Length > 1)
                    checkedProvince = checkedProvince.Remove(checkedProvince.Length - 1);
                string checkedcbTeamWorkType = (collection["cbTeamWorkType"] + ",").Replace("false,", "");
                if (checkedcbTeamWorkType.Length > 1)
                    checkedcbTeamWorkType = checkedcbTeamWorkType.Remove(checkedcbTeamWorkType.Length - 1);
                t_xm_investment.AimIndustry = checkedIndustry;
                t_xm_investment.AjmArea = checkedProvince;
                t_xm_investment.TeamworkType = checkedcbTeamWorkType;
                t_xm_investment.City = collection["ddlCity"];
                t_xm_investment.IsValid = true;
                t_xm_investment.OP = 0;
                t_xm_investment.CreateTime = DateTime.Now;
                t_xm_investment.UpdateTime = DateTime.Now;

                HttpPostedFileBase file = Request.Files[0];
                //存入文件
                if (file.ContentLength > 0)
                {
                    t_xm_investment.Pic = new byte[Request.Files[0].InputStream.Length];
                    Request.Files[0].InputStream.Read(t_xm_investment.Pic, 0, t_xm_investment.Pic.Length);
                }

                db.T_XM_Investment.Add(t_xm_investment);
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
        }
        
        //
        // GET: /XM_TZ/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);
            BindArea(t_xm_investment.Province);
            BindIndustry(t_xm_investment.Industry);
            BindTeamworkType(t_xm_investment.TeamworkType);
            return View(t_xm_investment);
        }

        //
        // POST: /XM_TZ/Edit/5

        [HttpPost]
        public ActionResult Edit(T_XM_Investment t_xm_investment, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                string checkedIndustry = (collection["cbIndustry"] + ",").Replace("false,", "");
                if (checkedIndustry.Length > 1)
                    checkedIndustry = checkedIndustry.Remove(checkedIndustry.Length - 1);
                string checkedProvince = (collection["cbProvince"] + ",").Replace("false,", "");
                if (checkedProvince.Length > 1)
                    checkedProvince = checkedProvince.Remove(checkedProvince.Length - 1);
                string checkedcbTeamWorkType = (collection["cbTeamWorkType"] + ",").Replace("false,", "");
                if (checkedcbTeamWorkType.Length > 1)
                    checkedcbTeamWorkType = checkedcbTeamWorkType.Remove(checkedcbTeamWorkType.Length - 1);
                db.Entry(t_xm_investment).State = EntityState.Modified;
                t_xm_investment.AimIndustry = checkedIndustry;
                t_xm_investment.AjmArea = checkedProvince;
                t_xm_investment.TeamworkType = checkedcbTeamWorkType;
                t_xm_investment.City = collection["ddlCity"];
                t_xm_investment.UpdateTime = DateTime.Now;

                HttpPostedFileBase file = Request.Files[0];
                //存入文件
                if (file.ContentLength > 0)
                {
                    t_xm_investment.Pic = new byte[Request.Files[0].InputStream.Length];
                    Request.Files[0].InputStream.Read(t_xm_investment.Pic, 0, t_xm_investment.Pic.Length);
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
        // GET: /XM_TZ/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);
            return View(t_xm_investment);
        }

        //
        // POST: /XM_TZ/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.IsAjaxRequest())
            {
                T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);
                t_xm_investment.IsValid = false;
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
        }

        //helper
        public FileContentResult ShowPic(int xm_id)
        {
            byte[] pic;
            if (db.T_XM_Investment.Find(xm_id).Pic != null)
                pic = db.T_XM_Investment.Find(xm_id).Pic;
            else
                pic = new byte[1];
            return File(pic, "image/jpeg");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //待审核意向
        public ActionResult TZCheckList(int pageNum = 1, int numPerPage = 15)
        {
            IList<GGZBTQPT_PRO.Models.T_XM_Investment> list = db.T_XM_Investment.Where(p => (p.IsValid == true && p.PublicStatus == "1")).ToList()
                                                            .OrderByDescending(s => s.SubmitTime)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_XM_Investment.Where(p => (p.IsValid == true && p.PublicStatus == "1")).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;

            return View(list);
        }

        //已审核意向
        public ActionResult TZCheckList_Pass(int pageNum = 1, int numPerPage = 15)
        {
            IList<GGZBTQPT_PRO.Models.T_XM_Investment> list = db.T_XM_Investment.Where(p => (p.IsValid == true && p.PublicStatus == "2")).ToList()
                                                            .OrderByDescending(s => s.SubmitTime)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_XM_Investment.Where(p => (p.IsValid == true && p.PublicStatus == "1")).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;

            return View(list);
        }

        //意向审核
        public ActionResult TZCheck(int id)
        {
            T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);
            t_xm_investment.PublicStatus = "2";
            t_xm_investment.PublicTime = DateTime.Now;
            t_xm_investment.UpdateTime = DateTime.Now;

            if (db.SaveChanges() > 0)
                return ReturnJson(true, "审核成功", "", "TZCheckList", false, "");
            else
                return ReturnJson(false, "审核失败", "", "", false, "");
        }

        //意向撤销审核
        public ActionResult UnTZCheck(int id)
        {
            T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);
            t_xm_investment.PublicStatus = "1";
            t_xm_investment.PublicTime = DateTime.Now;
            t_xm_investment.UpdateTime = DateTime.Now;

            if (db.SaveChanges() > 0)
                return ReturnJson(true, "撤销审核成功", "", "TZCheckList_Pass", false, "");
            else
                return ReturnJson(false, "撤销审核失败", "", "", false, "");
        }

        //项目分析--投资意向列表
        public ActionResult TZMatch(int pageNum = 1, int numPerPage = 5)
        {
            IList<GGZBTQPT_PRO.Models.T_XM_Investment> list = db.T_XM_Investment.Where(p => (p.IsValid == true && p.PublicStatus == "2")).ToList()
                                                            .OrderByDescending(s => s.SubmitTime)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_XM_Investment.Where(p => (p.IsValid == true && p.PublicStatus == "2")).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;

            return View(list);
        }

        //根据投资意向匹配对应的项目
        public ActionResult TZMatchResult(int id, int pageNum = 1, int numPerPage = 5)
        {
            T_XM_Investment inverst = db.T_XM_Investment.Find(id);
            decimal investment = inverst.Investment == null ? 0 : (decimal)inverst.Investment;
            string[] industry = inverst.AimIndustry.Split(',');
            int[] temp = new int[industry.Length];
            for (int i = 0; i < industry.Length; i++)
            {
                temp[i] = Convert.ToInt32(industry[i]);
            }
            ViewData["inverstName"] = inverst.ItemName;
            IList<GGZBTQPT_PRO.Models.T_XM_Financing> list = db.T_XM_Financing.Where(p => (p.IsValid == true && p.PublicStatus == "2" && (p.FinancSum < investment || p.TransferPrice < investment || p.Investment < investment || p.OtherItemFinancSum < investment) && temp.Contains((int)p.Industry))).ToList()
                                                        .OrderByDescending(s => s.SubmitTime)
                                                        .Skip(numPerPage * (pageNum - 1))
                                                        .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_XM_Financing.Where(p => (p.IsValid == true && p.PublicStatus == "2" && (p.FinancSum > investment || p.TransferPrice > investment || p.Investment > investment || p.OtherItemFinancSum > investment) && industry.Contains(SqlFunctions.StringConvert((double)p.Industry)))).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;

            return PartialView(list);
        }
    }
}