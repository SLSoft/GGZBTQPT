using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;

namespace GGZBTQPT_PRO.Controllers
{
    public class JG_AgencyController : BaseController
    {
        //private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /JG_Agency/

        public ViewResult Index(int pageNum = 1, int numPerPage = 5)
        {
            var t_jg_agency = db.T_JG_Agency.Where(c => c.IsValid == true).OrderBy(s => s.ID)
                                                                    .Skip(numPerPage * (pageNum - 1))
                                                                    .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_JG_Agency.Where(c => c.IsValid == true).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            return View(t_jg_agency);
        }

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

        //
        // GET: /JG_Agency/Details/5

        public ViewResult Details(int id)
        {
            T_JG_Agency t_jg_agency = db.T_JG_Agency.Find(id);
            return View(t_jg_agency);
        }

        //
        // GET: /JG_Agency/Create

        public ActionResult Create()
        {
            BindArea();
            BindAgencyType();
            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName");
            var t_jg_agency = new T_JG_Agency();
            return View(t_jg_agency);
        } 

        //
        // POST: /JG_Agency/Create

        [HttpPost]
        public ActionResult Create(T_JG_Agency t_jg_agency)
        {
            if (ModelState.IsValid)
            {
                t_jg_agency.MemberID = Convert.ToInt32(Session["MemberID"] == null ? 0 : Session["MemberID"]);
                t_jg_agency.IsValid = true;
                t_jg_agency.OP = 0;
                t_jg_agency.CreateTime = DateTime.Now;
                t_jg_agency.UpdateTime = DateTime.Now;

                HttpPostedFileBase file = Request.Files[0];
                //存入文件
                if (file.ContentLength > 0)
                {
                    t_jg_agency.Pic = new byte[Request.Files[0].InputStream.Length];
                    Request.Files[0].InputStream.Read(t_jg_agency.Pic, 0, t_jg_agency.Pic.Length);
                }

                db.T_JG_Agency.Add(t_jg_agency);
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }

            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName", t_jg_agency.MemberID);
            return Json(new { });
        }
        
        //
        // GET: /JG_Agency/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_JG_Agency t_jg_agency = db.T_JG_Agency.Find(id);
            BindArea(t_jg_agency.Province);
            BindAgencyType(t_jg_agency.AgencyType);
            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName", t_jg_agency.MemberID);
            return View(t_jg_agency);
        }

        //
        // POST: /JG_Agency/Edit/5

        [HttpPost]
        public ActionResult Edit(T_JG_Agency t_jg_agency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_jg_agency).State = EntityState.Modified;

                if (t_jg_agency.RegTime == null)
                    t_jg_agency.RegTime = DateTime.MaxValue;
                t_jg_agency.UpdateTime = DateTime.Now;
                HttpPostedFileBase file = Request.Files[0];
                //存入文件
                if (file.ContentLength > 0)
                {
                    t_jg_agency.Pic = new byte[Request.Files[0].InputStream.Length];
                    Request.Files[0].InputStream.Read(t_jg_agency.Pic, 0, t_jg_agency.Pic.Length);
                }

                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }

            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName", t_jg_agency.MemberID);
            return Json(new { });
        }

        //
        // GET: /JG_Agency/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_JG_Agency t_jg_agency = db.T_JG_Agency.Find(id);
            return View(t_jg_agency);
        }

        //
        // POST: /JG_Agency/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            if (Request.IsAjaxRequest())
            {
                T_JG_Agency t_jg_agency = db.T_JG_Agency.Find(id);
                t_jg_agency.IsValid = false;
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
        }

        //helper
        public FileContentResult ShowPic(int Agency_id)
        {
            return File(db.T_JG_Agency.Find(Agency_id).Pic, "image/jpeg");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //企业查询功能
        public ActionResult AgencyQuery(FormCollection collection, int pageNum = 1, int numPerPage = 5)
        {
            BindAgencyType();
            string agencyname = collection["agencyname"] == null ? "" : collection["agencyname"];
            int agencytype = collection["AgencyType"] == null || collection["AgencyType"] == "" ? 0 : Convert.ToInt32(collection["AgencyType"]);


            var t_jg_agency = db.T_JG_Agency.Where(c => c.IsValid == true && c.AgencyName.Contains(agencyname));
            if (agencytype != 0)
            {
                t_jg_agency = t_jg_agency.Where(c => c.AgencyType == agencytype);
            }

            IList<GGZBTQPT_PRO.Models.T_JG_Agency> list = t_jg_agency.OrderByDescending(s => s.CreateTime)
                                                        .Skip(numPerPage * (pageNum - 1))
                                                        .Take(numPerPage).ToList();
            ViewBag.recordCount = t_jg_agency.Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;

            return PartialView(list);
        }
    }
}