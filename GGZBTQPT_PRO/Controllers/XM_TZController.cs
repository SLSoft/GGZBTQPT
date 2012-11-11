using System;
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
    public class XM_TZController : BaseController
    {
        //private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /XM_TZ/

        public ViewResult Index()
        {
            return View(db.T_XM_Investment.Where(p => p.IsValid == true).ToList());
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
                Stream stream = Request.Files.Count > 0
                                        ? Request.Files[0].InputStream
                                        : Request.InputStream;
                //存入文件
                if (stream.Length > 0)
                {
                    t_xm_investment.Pic = new byte[stream.Length];
                    //stream.Read(t_qy_corp.Logo, 0, t_qy_corp.Logo.Length);
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
                Stream stream = Request.Files.Count > 0
                                        ? Request.Files[0].InputStream
                                        : Request.InputStream;
                //存入文件
                if (stream.Length > 0)
                {
                    t_xm_investment.Pic = new byte[stream.Length];
                    //stream.Read(t_qy_corp.Logo, 0, t_qy_corp.Logo.Length);
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}