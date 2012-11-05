﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
namespace GGZBTQPT_PRO.Areas.Member.Controllers
{
    public class InvestmentController : BaseController
    {
        //
        // GET: /Member/Investment/

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
            List<T_PTF_DicTreeDetail> Area = db.T_PTF_DicTreeDetail.Where(p => (p.DicType == "34" && p.Depth == 1)).ToList();

            ViewData["Province"] = new SelectList(Area, "Code", "Name", select);
        }

        public JsonResult GetCity(string ParentCode)
        {
            List<T_PTF_DicTreeDetail> City = db.T_PTF_DicTreeDetail.Where(p => (p.DicType == "34" && p.ParentCode == ParentCode)).ToList();

            return Json(City, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /XM_TZ/Create

        public ActionResult Create(string notice_type)
        {
            if(notice_type == "success")
            {
                ViewData["notice"] = "投资项目发布成功，可进入我的发布中查阅！";
            }

            BindArea();
            BindIndustry();
            BindTeamworkType();
            return View();
        }

        //
        // POST: /XM_TZ/Create

        [HttpPost]
        public ActionResult Create(T_XM_Investment t_xm_investment, FormCollection collection)
        {
            
            if (ModelState.IsValid)
            {
                t_xm_investment.City = Int32.Parse(collection["ddlCity"]);
                t_xm_investment.IsValid = true;
                t_xm_investment.OP = 0;
                t_xm_investment.CreateTime = DateTime.Now;
                t_xm_investment.UpdateTime = DateTime.Now;
                t_xm_investment.MemberID = CurrentMember().ID;
                db.T_XM_Investment.Add(t_xm_investment);
                db.SaveChanges(); 
                return RedirectToAction("Create", new { notice_type = "success" });
            }
            ViewData["error"] = "投资项目发布失败!请检查输入信息或联系我们!";
            return View(t_xm_investment);
        }

        //
        // GET: /XM_TZ/Edit/5

        public ActionResult Edit(int id, string notice_type)
        {
            if (notice_type == "success")
            {
                ViewData["notice"] = "投资项目更新成功，可进入我的发布中查阅！";
            }
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

                db.Entry(t_xm_investment).State = EntityState.Modified;
                t_xm_investment.AimIndustry = checkedIndustry;
                t_xm_investment.AjmArea = checkedProvince;
                t_xm_investment.City = Int32.Parse(collection["ddlCity"]);
                t_xm_investment.UpdateTime = DateTime.Now;
                db.SaveChanges();

                return RedirectToAction("Edit", new { notice_type = "success" });
            }
            ViewData["error"] = "投资项目更新失败!请检查输入信息或联系我们!";
            return View(t_xm_investment);
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
            T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);
            db.T_XM_Investment.Remove(t_xm_investment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}