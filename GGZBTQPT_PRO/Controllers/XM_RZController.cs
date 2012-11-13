﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using System.IO;

namespace GGZBTQPT_PRO.Controllers
{
    public class XM_RZController : BaseController
    {
        //private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /XM_RZ/

        public ViewResult Index(int pageNum = 1, int numPerPage = 5)
        {
            var t_xm_financing = db.T_XM_Financing.Where(p => p.IsValid == true).OrderBy(s => s.ID)
                                                                    .Skip(numPerPage * (pageNum - 1))
                                                                    .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_XM_Financing.Where(c => c.IsValid == true).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
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
            var t_xm_financing = new T_XM_Financing();
            return View(t_xm_financing);
        } 

        //
        // POST: /XM_RZ/Create

        [HttpPost]
        public ActionResult Create(T_XM_Financing t_xm_financing, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                string checkedTransactionMode = (collection["TransactionMode"] + ",").Replace("false,", "");
                if (checkedTransactionMode.Length > 1)
                    checkedTransactionMode = checkedTransactionMode.Remove(checkedTransactionMode.Length - 1);
                t_xm_financing.TransactionMode = checkedTransactionMode;
                t_xm_financing.City = collection["ddlCity"];
                t_xm_financing.IsValid = true;
                t_xm_financing.OP = 0;
                t_xm_financing.CreateTime = DateTime.Now;
                t_xm_financing.UpdateTime = DateTime.Now;
                t_xm_financing.MemberID = 1;

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
            return View(t_xm_financing);
        }

        //
        // POST: /XM_RZ/Edit/5

        [HttpPost]
        public ActionResult Edit(T_XM_Financing t_xm_financing, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_xm_financing).State = EntityState.Modified;
                string checkedTransactionMode = (collection["TransactionMode"] + ",").Replace("false,", "");
                if (checkedTransactionMode.Length > 1)
                    checkedTransactionMode = checkedTransactionMode.Remove(checkedTransactionMode.Length - 1);
                t_xm_financing.TransactionMode = checkedTransactionMode;
                t_xm_financing.City = collection["ddlCity"];
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
                    return ReturnJson(true, "操作成功", "", "", true, "");
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

        public ActionResult RZCheckList()
        {
            return View(db.T_XM_Financing.Where(p => (p.IsValid == true && p.PublicStatus == "1")).ToList());
        }

        [HttpPost]
        public ActionResult RZCheckList(FormCollection collection)
        {
            string strPublicState = collection["PublicState"];
            ViewBag.State = strPublicState;
            return View(db.T_XM_Financing.Where(p => (p.IsValid == true && p.PublicStatus == strPublicState)).ToList());
        }

        public ActionResult RZCheck(int id, string state)
        {
            T_XM_Financing t_xm_financing = db.T_XM_Financing.Find(id);
            t_xm_financing.PublicStatus = state;
            db.SaveChanges();
            return RedirectToAction("RZCheckList");
        }

        //helper
        public FileContentResult ShowPic(int xm_id)
        {
            return File(db.T_XM_Financing.Find(xm_id).Pic, "image/jpeg");
        }
    }
}