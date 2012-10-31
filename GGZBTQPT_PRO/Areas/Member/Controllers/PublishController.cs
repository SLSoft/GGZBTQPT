using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;

namespace GGZBTQPT_PRO.Areas.Member.Controllers
{
    public class PublishController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();
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
        public ActionResult PublishedFinancials(int member_id)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Login", "Member");
            }
            try
            {
                var finacials = db.T_XM_Financing.Where(f => f.MemberID == member.ID).ToList();
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
        public ActionResult PublishedInvestments(int member_id)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Login", "Member");
            }
            try
            {
                var finacials = db.T_XM_Investment.Where(f => f.MemberID == member.ID).ToList();
                return PartialView(finacials);
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
        public ActionResult PublishedProducts(int member_id)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Login", "Member");
            }
            try
            {
                var finacials = db.T_JG_Product.Where(f => f.MemberID == member.ID).ToList();
                return PartialView(finacials);
            }
            catch
            {
                return PartialView();
            } 
        }



        private T_HY_Member CurrentMember()
        {
            if (Session["MemberID"] != null && Session["MemberID"].ToString() != "")
            {
                var member = db.T_HY_Member.Find(Convert.ToInt32(Session["MemberID"].ToString()));
                return member;
            }
            return null;
        }
    }
}
