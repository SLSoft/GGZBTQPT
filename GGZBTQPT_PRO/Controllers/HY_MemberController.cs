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
    public class HY_MemberController : BaseController
    { 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNum">当前页码</param>
        /// <param name="numPerPage">每页显示多少条</param>
        /// <param name="keywords">搜索关键字</param>
        /// <returns></returns>
        public ActionResult Index(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            IList<GGZBTQPT_PRO.Models.T_HY_Member> list = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords)).Where(p => p.IsValid == true)
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords)).Where(p => p.IsValid == true).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;

            return View(list);
        } 

        public ActionResult UnVerified(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            IList<GGZBTQPT_PRO.Models.T_HY_Member> list = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords)).Where(p => p.IsValid == true && p.IsVerified == false)
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords)).Where(p => p.IsValid == true && p.IsVerified == false).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;

            return View(list);
        }

        public ActionResult HasVerified(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            IList<GGZBTQPT_PRO.Models.T_HY_Member> list = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords)).Where(p => p.IsValid == true && p.IsVerified == true)
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords)).Where(p => p.IsValid == true && p.IsVerified == true).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;

            return View(list);
        } 

        
        //
        // GET: /T_HY_Member/Details/5

        public ViewResult Details(int id)
        {
            T_HY_Member t_hy_member = db.T_HY_Member.Find(id);
            return View(t_hy_member);
        }

        //
        // GET: /T_HY_Member/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /T_HY_Member/Create

        [HttpPost]
        public ActionResult Create(T_HY_Member t_hy_member)
        {
            if (ModelState.IsValid)
            {
                db.T_HY_Member.Add(t_hy_member);
                db.SaveChanges();
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, ""); 
            }

            return View(t_hy_member);
        }
        
        //
        // GET: /T_HY_Member/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_HY_Member t_hy_member = db.T_HY_Member.Find(id);
            return View(t_hy_member);
        }

        //
        // POST: /T_HY_Member/Edit/5

        [HttpPost]
        public ActionResult Edit(T_HY_Member t_hy_member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_hy_member).State = EntityState.Modified;
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, ""); 
            }
            return View(t_hy_member);
        }

        //
        // GET: /T_HY_Member/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_HY_Member t_hy_member = db.T_HY_Member.Find(id);
            return View(t_hy_member);
        }

        //
        // POST: /T_HY_Member/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            T_HY_Member t_hy_member = db.T_HY_Member.Find(id);
            db.T_HY_Member.Remove(t_hy_member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Verify(int id)
        {
            T_HY_Member t_hy_member = db.T_HY_Member.Find(id);
            t_hy_member.IsVerified = true;
            db.Entry(t_hy_member).State = EntityState.Modified;

            if (db.SaveChanges() > 0)
                return ReturnJson(true, "审核成功", "", "UnVerified", false, "");
            else
                return ReturnJson(false, "审核失败", "", "", false, ""); 

        }

        [HttpPost]
        public ActionResult UnVerify(int id)
        {
            T_HY_Member t_hy_member = db.T_HY_Member.Find(id);
            t_hy_member.IsVerified = false;
            db.Entry(t_hy_member).State = EntityState.Modified;

            if (db.SaveChanges() > 0)
                return ReturnJson(true, "撤销审核成功", "", "UnVerified", false, "");
            else
                return ReturnJson(false, "撤销审核失败", "", "", false, "");

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}