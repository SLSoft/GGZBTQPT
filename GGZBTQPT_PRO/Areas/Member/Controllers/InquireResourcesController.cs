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
    public class InquireResourcesController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();


        /// <summary>
        /// 找项目
        /// </summary>
        /// <returns></returns>
        public ActionResult Financials()
        {
           var financials = db.T_XM_Financing.ToList();
           return View(financials); 
        } 

        /// <summary>
        /// 找到的项目内容
        /// </summary>
        /// <returns></returns>
        public ActionResult InquiredFinancials()
        {
           return View(); 
        } 

        /// <summary>
        /// 找资金
        /// </summary>
        /// <returns></returns>
        public ActionResult Investments()
        {
           var investments = db.T_XM_Investment.ToList();
           return View(investments); 
        }


        /// <summary>
        /// 找服务
        /// </summary>
        /// <returns></returns>
        public ActionResult Products()
        {
           return View(); 
        }

        //Helper
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
