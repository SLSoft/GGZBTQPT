﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using Webdiyer.WebControls.Mvc;

namespace GGZBTQPT_PRO.Areas.MG.Controllers
{
    public class PublishedController : BaseController
    {

        //
        // GET: /Member/Publish/

        public ActionResult Index()
        {
            
            if(CurrentMember() != null)
            {
                var member = db.T_HY_Member.Find(CurrentMember().ID);
                return View(member);
            }
            else
            {
                return RedirectToAction("Login", "Member");
            }
        }

        /// <summary>
        /// 用户所发布的项目
        /// </summary>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public ActionResult PublishedFinancials(int member_id, int id = 1)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Login", "Member");
            }
            try
            {
                PagedList<T_XM_Financing> finacials = db.T_XM_Financing.OrderByDescending(f => f.CreateTime).Where(f => f.MemberID == member.ID).ToPagedList(id, 5);
                return PartialView(finacials);
            }
            catch
            {
                return PartialView();
            } 

        }

        /// <summary>
        /// 用户所发布的投资意向
        /// </summary>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public ActionResult PublishedInvestments(int member_id, int id = 1)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Login", "Member");
            }
            try
            {
                PagedList<T_XM_Investment> investments = db.T_XM_Investment.OrderByDescending(f => f.CreateTime).Where(f => f.MemberID == member.ID).ToPagedList(id, 5);
                return PartialView(investments);
            }
            catch
            {
                return PartialView();
            } 
        }

        /// <summary>
        /// 用户所发布的金融产品
        /// </summary>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public ActionResult PublishedProducts(int member_id, int id = 1 )
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Login", "Member");
            }
            try
            {
                PagedList<T_JG_Product> products = db.T_JG_Product.OrderByDescending(p => p.CreateTime).Where( p => p.MemberID == member.ID).ToPagedList(id, 5);
                return PartialView(products);
            }
            catch
            {
                return PartialView();
            } 
        } 

    }
}