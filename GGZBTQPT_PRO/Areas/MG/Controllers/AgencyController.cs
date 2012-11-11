using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;

namespace GGZBTQPT_PRO.Areas.MG.Controllers
{ 
    public class AgencyController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();
 
 
        public void BindAgencyType(object select = null)
        {
            List<T_PTF_DicDetail> AgencyType = db.T_PTF_DicDetail.Where(p => (p.DicType == "JG01" && p.IsValid == "1")).ToList();

            ViewData["AgencyType"] = new SelectList(AgencyType, "ID", "Name", select);
        }
        public void BindArea(object select = null)
        {
            List<T_PTF_DicTreeDetail> Area = db.T_PTF_DicTreeDetail.Where(p => (p.DicType == "34" && p.Depth == 1)).ToList();

            ViewData["Province"] = new SelectList(Area, "ID", "Name", select);
        }

        public ViewResult Details(int id)
        {
            T_JG_Agency t_jg_agency = db.T_JG_Agency.Find(id);
            return View(t_jg_agency);
        }

        
        //
        // GET: /JG_Agency/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_JG_Agency t_jg_agency = db.T_JG_Agency.Find(id);
            BindArea(t_jg_agency.Province);
            BindAgencyType(t_jg_agency.AgencyType);

            return PartialView(t_jg_agency);
        }

        //
        // POST: /JG_Agency/Edit/5

        [HttpPost]
        public ActionResult Edit(T_JG_Agency t_jg_agency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_jg_agency).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { statusCode = "200", message = "信息保存成功！", type = "success" });
            }
            return Json(new { statusCode = "200", message = "信息保存失败！", type = "error" }); 
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}