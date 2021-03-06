﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using System.IO;
using GGZBTQPT_PRO.Enums;

namespace GGZBTQPT_PRO.Areas.MG.Controllers
{ 
    public class AgencyController : BaseController
    {
 
 
        public void BindAgencyType(object select = null)
        {
            List<T_PTF_DicDetail> AgencyType = db.T_PTF_DicDetail.Where(p => (p.DicType == "JG01" && p.IsValid == "1")).ToList();

            ViewData["AgencyType"] = new SelectList(AgencyType, "ID", "Name", select);
        }
        public void BindArea(object select = null)
        {
            List<T_PTF_DicTreeDetail> Area = db.T_PTF_DicTreeDetail.Where(p => (p.DicType == "34" && p.Depth == 1)).ToList();

            ViewData["Province"] = new SelectList(Area, "Code", "Name", select);
        }

        public ViewResult Details(int id)
        {
            T_JG_Agency t_jg_agency = db.T_JG_Agency.Find(id);
            return View(t_jg_agency);
        }

        
        //

 
        public ActionResult Edit(int id)
        {
            T_JG_Agency t_jg_agency = db.T_JG_Agency.Find(id);
            BindArea(t_jg_agency.Province);
            BindAgencyType(t_jg_agency.AgencyType);
            if (t_jg_agency.RegTime > DateTime.Now)
            {
                t_jg_agency.RegTime = DateTime.Now;
            }
            return PartialView(t_jg_agency);
        }

        //
        // POST: /JG_Agency/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(T_JG_Agency t_jg_agency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_jg_agency).State = EntityState.Modified;
                t_jg_agency.Services = t_jg_agency.Services == null ? "" : t_jg_agency.Services;
                t_jg_agency.Remark = t_jg_agency.Remark == null ? "" : t_jg_agency.Remark;
                db.SaveChanges();

                Logging("更新了机构详细信息", (int)OperateTypes.Edit, (int)GenerateSystem.Personal);
                return Json(new { statusCode = "200", message = "信息保存成功！", type = "success" });
            }
            return Json(new { statusCode = "200", message = "信息保存失败！", type = "error" }); 
        }

        public ActionResult UploadLogo(string qqfile, int agency_id)
        {
            var agency = db.T_JG_Agency.Find(agency_id);
            db.Entry(agency).State = EntityState.Modified;

            try
            { 
                Stream stream = Request.Files.Count > 0
                    ? Request.Files[0].InputStream
                    : Request.InputStream;
                //存入文件
                if (stream.Length > 0)
                {
                    agency.Pic = new byte[stream.Length];
                    stream.Read(agency.Pic, 0, agency.Pic.Length);
                }
                db.SaveChanges();
                return Json(new { success = "true", message = "上传成功", logo = Convert.ToBase64String(agency.Pic) }, "text/x-json");
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, "text/x-json");
            }
        }

        public FileContentResult ShowLogo(int agency_id)
        {
            byte[] pic;
            if (db.T_JG_Agency.Find(agency_id).Pic != null)
                pic = db.T_JG_Agency.Find(agency_id).Pic;
            else
                pic = new byte[1];
            return File(pic, "image/jpeg");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}