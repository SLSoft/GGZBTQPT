﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.Util;
using GGZBTQPT_PRO.Enums;
using GGZBTQPT_PRO.ViewModels;

using System.Net;namespace GGZBTQPT_PRO.Controllers
{
    public class XM_JYController : BaseController
    {

        //
        // GET: /XM_JY/

        public ViewResult Index(string title, string accept, string give, int pageNum = 1, int numPerPage = 15)
        {
            title = title == null ? "" : title;
            accept = accept == null ? "" : accept;
            give = give == null ? "" : give;
            var t_xm_transaction = db.T_XM_Transaction.Where(p => (p.IsValid == true && p.TranTitle.Contains(title) && p.AcceptMember.Contains(accept) && p.GiveMember.Contains(give))).OrderBy(s => s.TranTime)
                                                                    .Skip(numPerPage * (pageNum - 1))
                                                                    .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_XM_Transaction.Where(p => (p.IsValid == true && p.TranTitle.Contains(title) && p.AcceptMember.Contains(accept) && p.GiveMember.Contains(give))).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.title = title;
            ViewBag.accept = accept;
            ViewBag.give = give;
            return View(t_xm_transaction);
        }

        //
        // GET: /XM_JY/Details/5

        public ViewResult Details(int id)
        {
            T_XM_Transaction t_xm_transaction = db.T_XM_Transaction.Find(id);
            return View(t_xm_transaction);
        }

        //
        // GET: /XM_JY/Create

        public ActionResult Create(int id = 0,int itype=0)
        {
            var t_xm_tran = new T_XM_Transaction();
            t_xm_tran.ItemID = id;
            t_xm_tran.TranTitle = GetItemName(id,itype);
            ViewBag.itype = itype;
            return View(t_xm_tran);
        }

        private string GetItemName(int id,int itype)
        {
            string result = "";
            if (itype == 0)
            {
                var fin = db.T_XM_Financing.Find(id);
                if (fin != null)
                    result = fin.ItemName;
            }
            else
            {
                var inv = db.T_XM_Investment.Find(id);
                if (inv != null)
                    result = inv.ItemName;
            }
            return result;
        }
        //
        // POST: /XM_JY/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(T_XM_Transaction t_xm_transaction, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                int itype = 0;
                if (fc["itype"]!=null||fc["itype"]!="")
                    itype = Convert.ToInt32(fc["itype"]);
                t_xm_transaction.TranTime = Convert.ToDateTime(fc["trantime"]);
                t_xm_transaction.TranContent = fc["trancontent"];
                t_xm_transaction.IsValid = true;
                t_xm_transaction.OP = 0;
                t_xm_transaction.CreateTime = DateTime.Now;
                t_xm_transaction.UpdateTime = DateTime.Now;
                db.T_XM_Transaction.Add(t_xm_transaction);

                //添加到案例库
                BusinessService.TransformatFromXM(t_xm_transaction.ItemID, t_xm_transaction.TranTitle, itype, t_xm_transaction.TranContent);

                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
        }
        
        //
        // GET: /XM_JY/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_XM_Transaction t_xm_transaction = db.T_XM_Transaction.Find(id);
            return View(t_xm_transaction);
        }

        //
        // POST: /XM_JY/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(T_XM_Transaction t_xm_transaction, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_xm_transaction).State = EntityState.Modified;
                t_xm_transaction.TranTime = Convert.ToDateTime(fc["trantime"]);
                t_xm_transaction.TranContent = fc["trancontent"];
                t_xm_transaction.UpdateTime = DateTime.Now;
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            if (Request.IsAjaxRequest())
            {
                T_XM_Transaction t_xm_transaction = db.T_XM_Transaction.Find(id);
                t_xm_transaction.IsValid = false;
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

        //交易统计
        public ActionResult JYReport()
        {
            var xmjy = db.T_XM_Transaction.Where(p => p.IsValid == true).ToList();
            ViewBag.JYCount = xmjy.Count;//项目交易总量
            ViewBag.JYSum = xmjy.Sum(p => p.Amount);//项目交易总额
            return View();
        }

        public ActionResult DataReportbyMonth()
        {
            var xmjy = db.T_XM_Transaction.Where(p => p.IsValid == true).ToList();
            ViewBag.JYCount = xmjy.Count;//项目交易总量
            ViewBag.JYSum = xmjy.Sum(p => p.Amount);//项目交易总额

            var up = db.T_XM_Transaction.Where(p => (p.IsValid == true && p.TranTime.Year == DateTime.Now.Year)).GroupBy(g => g.TranTime.Month)
                                    .Select(s => new { um = s.Sum(m => m.Amount), cnt = s.Count(), type = (int)s.Key });
            var list = from u in db.T_XM_Alendar
                       join p in up on u.ID equals p.type into gj
                       from x in gj.DefaultIfEmpty()
                       orderby u.ID
                       select new VM_JYReport
                       {
                           TypeName = u.month,
                           Count = x.type == null ? 0 : x.cnt,
                           Sum = x.type == null ? 0 : x.um,
                       };
            return PartialView(list.ToList());
        }


        public ActionResult ChartReportbyMonth()
        {
            var up = db.T_XM_Transaction.Where(p => (p.IsValid == true && p.TranTime.Year == DateTime.Now.Year)).GroupBy(g => g.TranTime.Month)
                                    .Select(s => new { cnt = s.Count(), type = (int)s.Key });
            var list = from u in db.T_XM_Alendar
                       join p in up on u.ID equals p.type into gj
                       from x in gj.DefaultIfEmpty()
                       orderby u.ID
                       select new VM_CPReport
                       {
                           TypeName = u.month,
                           Count = x.type == null ? 0 : x.cnt,
                       };
            Dictionary<String, int> dic = new Dictionary<string, int>();
            foreach (VM_CPReport vmx in list.ToList())
            {
                dic.Add(vmx.TypeName, vmx.Count);
            }

            return Json(new { statData = dic }, JsonRequestBehavior.AllowGet);
        }
    }
}