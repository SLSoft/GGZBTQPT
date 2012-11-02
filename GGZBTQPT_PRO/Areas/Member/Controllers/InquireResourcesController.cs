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
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();
            ViewData["Industry"] = new SelectList(Industry, "ID", "Name");

            var financials = db.T_XM_Financing.ToList();
            return View(financials); 
        } 

        /// <summary>
        /// 找到的项目内容
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InquiredFinancials(FormCollection collection)
        {
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();
            ViewData["Industry"] = new SelectList(Industry, "ID", "Name");
            string keys = "";
            string select_itemtype = "";
            string select_industry = "";
            string select_Financial = "";
            if(collection["keys"].ToString().Trim() != "")
                keys = collection["keys"].ToString();
            if (collection["cbItemType"] != null)
                select_itemtype = collection["cbItemType"];
            if (collection["cbIndustry"] != null)
                select_industry = collection["cbIndustry"];
            if (collection["cbFinancial"] != null)
                select_Financial = collection["cbFinancial"];
            string order = "ID";
            System.Data.SqlClient.SqlParameter[] selparms = new System.Data.SqlClient.SqlParameter[5];
            selparms[0] = new System.Data.SqlClient.SqlParameter("@keys",keys);
            selparms[1] = new System.Data.SqlClient.SqlParameter("@ItemType", select_itemtype);
            selparms[2] = new System.Data.SqlClient.SqlParameter("@Industry", select_industry);
            selparms[3] = new System.Data.SqlClient.SqlParameter("@FinancSum", select_Financial);
            selparms[4] = new System.Data.SqlClient.SqlParameter("@Order", order);
            var financials = (from p in db.T_XM_Financing.SqlQuery("exec dbo.P_GetRZXMByCondition @keys,@ItemType,@Industry,@FinancSum,@Order", selparms) select p).ToList();
            return View("Financials",financials); 
        } 

        /// <summary>
        /// 找资金
        /// </summary>
        /// <returns></returns>
        public ActionResult Investments(FormCollection collection)
        {
            List<T_PTF_DicDetail> TeamworkType = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM06")).ToList();
            ViewData["TeamworkType"] = new SelectList(TeamworkType, "ID", "Name");
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();
            ViewData["Industry"] = new SelectList(Industry, "ID", "Name");
            var investments = db.T_XM_Investment.ToList();
            //if (Request.IsAjaxRequest())
            //{
            //    string keys = "";
            //    string select_TeamworkType = "";
            //    string select_industry = "";
            //    string select_Investment = "";
            //    if (collection["keys"].ToString().Trim() != "")
            //        keys = collection["keys"].ToString();
            //    if (collection["cbTeamworkType"] != null)
            //        select_TeamworkType = collection["cbTeamworkType"];
            //    if (collection["cbIndustry"] != null)
            //        select_industry = collection["cbIndustry"];
            //    if (collection["cbFinancial"] != null)
            //        select_Investment = collection["cbFinancial"];
            //    string order = "ID";
            //    System.Data.SqlClient.SqlParameter[] selparms = new System.Data.SqlClient.SqlParameter[5];
            //    selparms[0] = new System.Data.SqlClient.SqlParameter("@keys", keys);
            //    selparms[1] = new System.Data.SqlClient.SqlParameter("@TeamworkType", select_TeamworkType);
            //    selparms[2] = new System.Data.SqlClient.SqlParameter("@Industry", select_industry);
            //    selparms[3] = new System.Data.SqlClient.SqlParameter("@FinancSum", select_Investment);
            //    selparms[4] = new System.Data.SqlClient.SqlParameter("@Order", order);
            //    investments = (from p in db.T_XM_Investment.SqlQuery("exec dbo.P_GetTZXMByCondition @keys,@TeamworkType,@Industry,@FinancSum,@Order", selparms) select p).ToList();
            //}
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
