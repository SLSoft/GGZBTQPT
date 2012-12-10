using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.Enums;
using GGZBTQPT_PRO.ViewModels;
using Webdiyer.WebControls.Mvc;

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

        /// <summary>
        /// 会员查询
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="type"></param>
        /// <param name="pageNum"></param>
        /// <param name="numPerPage"></param>
        /// <returns></returns>
        public ActionResult Query(string keywords, int type=1, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            var types = from MemberTypes mtype in Enum.GetValues(typeof(MemberTypes))
                        select new { ID = (int)mtype, Name = mtype.ToString() };
            ViewData["Types"] = new SelectList(types, "ID", "Name");

            IList<VM_MemberStat> list = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords)).Where(p => p.IsValid == true).Where(p => p.Type == type)
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage)
                                                            .Select(w => new VM_MemberStat { FinancingCount = w.Financials.Count, InvestmentCount=w.Investments.Count,Member=w })
                                                            .ToList();
            ViewBag.recordCount = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords)).Where(p => p.IsValid == true).Where(p => p.Type == type).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            ViewBag.type = type;

            return View(list);
        }

        /// <summary>
        /// 查询会员详细信息(如：发布项目、投资意向、金融产品)
        /// </summary>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public PartialViewResult QueryDetails(int member_id, int type = 1, int pageNum = 1, int numPerPage = 5)
        {
            var member_Details = new VM_SelectMember();

            member_Details.Financings = db.T_XM_Financing.OrderByDescending(f => f.CreateTime).Where(f => f.MemberID == member_id).Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_XM_Financing.OrderByDescending(f => f.CreateTime).Where(f => f.MemberID == member_id).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;

            member_Details.Investments = db.T_XM_Investment.OrderByDescending(f => f.CreateTime).Where(f => f.MemberID == member_id).ToList();
            member_Details.Products = db.T_JG_Product.OrderByDescending(p => p.CreateTime).Where(p => p.MemberID == member_id).ToList();

            return PartialView(member_Details);

            //switch (type)
            //{
            //    case 1:
            //        return QueryXM(member_id,pageNum,numPerPage);
            //    default:
            //        member_Details.Investments = db.T_XM_Investment.OrderByDescending(f => f.CreateTime).Where(f => f.MemberID == member_id).Skip(numPerPage * (pageNum - 1))
            //                                                .Take(numPerPage).ToList();
            //        ViewBag.recordCount2 = db.T_XM_Investment.OrderByDescending(f => f.CreateTime).Where(f => f.MemberID == member_id).Count();
            //        ViewBag.numPerPage2 = numPerPage;
            //        ViewBag.pageNum2 = pageNum;     
            //        return PartialView(member_Details);
            //}
        }

        public PartialViewResult QueryXM(int member_id, int pageNum, int numPerPage)
        {
            var member_Details = new VM_SelectMember();

            member_Details.Financings = db.T_XM_Financing.OrderByDescending(f => f.CreateTime).Where(f => f.MemberID == member_id).Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_XM_Financing.OrderByDescending(f => f.CreateTime).Where(f => f.MemberID == member_id).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;

            return PartialView(member_Details);
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}