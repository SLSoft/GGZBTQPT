using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using Webdiyer.WebControls.Mvc;
using GGZBTQPT_PRO.Areas.MG.Filter;

namespace GGZBTQPT_PRO.Areas.MG.Controllers
{
    public class InquireResourcesController : BaseController
    {


        #region --------------------会员管理系统（找项目、找资金）--------------------------
        /// <summary>
        /// 找项目
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Financials(int id = 1)
        {
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();
            ViewData["Industry"] = new SelectList(Industry, "ID", "Name");
            List<T_PTF_DicDetail> ItemStage = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM04")).ToList();
            ViewData["ItemStage"] = new SelectList(ItemStage, "ID", "Name");
            ViewBag.current_page_id = id;
            //var financials = db.T_XM_Financing.OrderBy(p => p.CreateTime);
            PagedList<T_XM_Financing> financials = db.T_XM_Financing.Where(f=>(f.IsValid==true && f.PublicStatus=="2")).OrderByDescending(p => p.CreateTime).ToPagedList(id, 5);
            return View(financials); 
        } 

        /// <summary>
        /// 找到的项目内容
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Financials(FormCollection collection, int id = 1)
        {
            ViewBag.condition1 = collection["condition1"];
            ViewBag.condition2 = collection["condition2"];
            ViewBag.condition3 = collection["condition3"];
            ViewBag.condition4 = collection["condition4"];
            ViewBag.context = collection["context"];
            ViewBag.current_page_id = collection["current_page_id"];
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();
            ViewData["Industry"] = new SelectList(Industry, "ID", "Name");
            List<T_PTF_DicDetail> ItemStage = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM04")).ToList();
            ViewData["ItemStage"] = new SelectList(ItemStage, "ID", "Name");
            string keys = "";
            string select_itemtype = "";
            string select_industry = "";
            string select_Financial = "";
            string select_itemstage = "";
            if (collection["keys"] != null && collection["keys"].ToString().Trim() != "")
                keys = collection["keys"].ToString();
            if (collection["cbItemType"] != null)
                select_itemtype = collection["cbItemType"];
            if (collection["cbIndustry"] != null)
                select_industry = collection["cbIndustry"];
            if (collection["cbItemStage"] != null)
            {
                string[] temp = collection["cbItemStage"].Split(',');
                select_itemstage += " and (";
                foreach (string str in temp)
                {
                    select_itemstage += " ItemStage = '" + str + "' or";
                }
                select_itemstage = select_itemstage.Substring(0, select_itemstage.Length - 3);
                select_itemstage += ")";
            }
            if (collection["cbFinancial"] != null)
            {
                string[] temp = collection["cbFinancial"].Split(',');
                select_Financial += " and (";
                foreach (string str in temp)
                {
                    select_Financial += " FinancSum " + str + " or";
                }
                select_Financial = select_Financial.Substring(0, select_Financial.Length - 3);
                select_Financial += ")";
            }
            string order = "ID";
            System.Data.SqlClient.SqlParameter[] selparms = new System.Data.SqlClient.SqlParameter[6];
            selparms[0] = new System.Data.SqlClient.SqlParameter("@keys", keys);
            selparms[1] = new System.Data.SqlClient.SqlParameter("@ItemType", select_itemtype);
            selparms[2] = new System.Data.SqlClient.SqlParameter("@Industry", select_industry);
            selparms[3] = new System.Data.SqlClient.SqlParameter("@ItemStage", select_itemstage);
            selparms[4] = new System.Data.SqlClient.SqlParameter("@FinancSum", select_Financial);
            selparms[5] = new System.Data.SqlClient.SqlParameter("@Order", order);
            IList<T_XM_Financing> financials = (from p in db.T_XM_Financing.SqlQuery("exec dbo.P_GetRZXMByCondition @keys,@ItemType,@Industry,@ItemStage,@FinancSum,@Order", selparms) select p).ToList();
            PagedList<T_XM_Financing> paged_financials = new PagedList<T_XM_Financing>(financials, Convert.ToInt32(collection["current_page_id"]), 5);
            if (paged_financials.Count == 0)
                ViewBag.Message = "未找到符合要求的项目!";
            return View(paged_financials);
        }

        #region 作废
        [HttpPost]
        public ActionResult InquiredFinancials(FormCollection collection, string condition1, int id = 1)
        {
            ViewBag.condition1 = collection["condition1"];
            ViewBag.condition2 = collection["condition2"];
            ViewBag.condition3 = collection["condition3"];
            ViewBag.condition4 = collection["condition4"];
            ViewBag.context = collection["context"];
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();
            ViewData["Industry"] = new SelectList(Industry, "ID", "Name");
            List<T_PTF_DicDetail> ItemStage = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM04")).ToList();
            ViewData["ItemStage"] = new SelectList(ItemStage, "ID", "Name");
            string keys = "";
            string select_itemtype = "";
            string select_industry = "";
            string select_Financial = "";
            if (collection["keys"] != null && collection["keys"].ToString().Trim() != "")
                keys = collection["keys"].ToString();
            if (collection["cbItemType"] != null)
                select_itemtype = collection["cbItemType"];
            if (collection["cbIndustry"] != null)
                select_industry = collection["cbIndustry"];
            if (collection["cbFinancial"] != null)
            {
                string[] temp = collection["cbFinancial"].Split(',');
                select_Financial += " and (";
                foreach (string str in temp)
                {
                    select_Financial += " FinancSum " + str + " or";
                }
                select_Financial = select_Financial.Substring(0, select_Financial.Length - 3);
                select_Financial += ")";
            }
            string order = "ID";
            System.Data.SqlClient.SqlParameter[] selparms = new System.Data.SqlClient.SqlParameter[5];
            selparms[0] = new System.Data.SqlClient.SqlParameter("@keys", keys);
            selparms[1] = new System.Data.SqlClient.SqlParameter("@ItemType", select_itemtype);
            selparms[2] = new System.Data.SqlClient.SqlParameter("@Industry", select_industry);
            selparms[3] = new System.Data.SqlClient.SqlParameter("@FinancSum", select_Financial);
            selparms[4] = new System.Data.SqlClient.SqlParameter("@Order", order);
            IList<T_XM_Financing> financials = (from p in db.T_XM_Financing.SqlQuery("exec dbo.P_GetRZXMByCondition @keys,@ItemType,@Industry,@FinancSum,@Order", selparms) select p).ToList();
            PagedList<T_XM_Financing> paged_financials = new PagedList<T_XM_Financing>(financials, id, 5);

            ViewBag.Financials = paged_financials;
            if (paged_financials.Count == 0)
                ViewBag.Message = "未找到符合要求的项目!";
            return PartialView(paged_financials);
        }
        #endregion


        /// <summary>
        /// 找资金
        /// </summary>
        /// <returns></returns>
        public ActionResult Investments(int id = 1)
        {
            List<T_PTF_DicDetail> TeamworkType = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM06")).ToList();
            ViewData["TeamworkType"] = new SelectList(TeamworkType, "ID", "Name");
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();
            ViewData["Industry"] = new SelectList(Industry, "ID", "Name");
            List<T_PTF_DicDetail> InvestmentNature = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM07")).ToList();
            ViewData["InvestmentNature"] = new SelectList(InvestmentNature, "ID", "Name");
            List<T_PTF_DicDetail> InvestmentStage = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM04")).ToList();
            ViewData["InvestmentStage"] = new SelectList(InvestmentStage, "ID", "Name");
            ViewBag.current_page_id = id;
            var investments = db.T_XM_Investment.Where(f => (f.IsValid == true && f.PublicStatus == "2")).OrderByDescending(f => f.CreateTime).ToPagedList(id, 5);
            return View(investments);
        }
        [HttpPost]
        public ActionResult Investments(FormCollection collection, int id = 1)
        {
            ViewBag.condition1 = collection["condition1"];
            ViewBag.condition2 = collection["condition2"];
            ViewBag.condition3 = collection["condition3"];
            ViewBag.condition4 = collection["condition4"];
            ViewBag.condition5 = collection["condition5"];
            ViewBag.context = collection["context"];
            ViewBag.current_page_id = collection["current_page_id"];
            List<T_PTF_DicDetail> TeamworkType = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM06")).ToList();
            ViewData["TeamworkType"] = new SelectList(TeamworkType, "ID", "Name");
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();
            ViewData["Industry"] = new SelectList(Industry, "ID", "Name");
            List<T_PTF_DicDetail> InvestmentNature = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM07")).ToList();
            ViewData["InvestmentNature"] = new SelectList(InvestmentNature, "ID", "Name");
            List<T_PTF_DicDetail> InvestmentStage = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM04")).ToList();
            ViewData["InvestmentStage"] = new SelectList(InvestmentStage, "ID", "Name");
            
            string keys = "";
            string select_TeamworkType = "";
            string select_industry = "";
            string select_Investment = "";
            string select_InvestmentStage = "";
            string select_InvestmentNature = "";
            if (collection["keys"] != null && collection["keys"].ToString().Trim() != "")
                keys = collection["keys"].ToString();
            if (collection["cbInvestmentNature"] != null)
            {
                string[] temp = collection["cbInvestmentNature"].Split(',');
                select_InvestmentNature += " and (";
                foreach (string str in temp)
                {
                    select_InvestmentNature += " InvestmentNature = '" + str + "' or";
                }
                select_InvestmentNature = select_InvestmentNature.Substring(0, select_InvestmentNature.Length - 3);
                select_InvestmentNature += ")";
            }
            if (collection["cbInvestmentStage"] != null)
            {
                string[] temp = collection["cbInvestmentStage"].Split(',');
                select_InvestmentStage += " and (";
                foreach (string str in temp)
                {
                    select_InvestmentStage += " InvestmentStage like '%" + str + "%' or";
                }
                select_InvestmentStage = select_InvestmentStage.Substring(0, select_InvestmentStage.Length - 3);
                select_InvestmentStage += ")";
            }
            if (collection["cbTeamworkType"] != null)
            {
                string[] temp = collection["cbTeamworkType"].Split(',');
                select_TeamworkType += " and (";
                foreach (string str in temp)
                {
                    select_TeamworkType += " TeamworkType like '%" + str + "%' or";
                }
                select_TeamworkType = select_TeamworkType.Substring(0, select_TeamworkType.Length - 3);
                select_TeamworkType += ")";
            }
            if (collection["cbIndustry"] != null)
            {
                string[] temp = collection["cbIndustry"].Split(',');
                select_industry += " and (";
                foreach (string str in temp)
                {
                    select_industry += " AimIndustry like '%" + str + "%' or";
                }
                select_industry = select_industry.Substring(0, select_industry.Length - 3);
                select_industry += ")";
            }
            if (collection["cbFinancial"] != null)
            {
                string[] temp = collection["cbFinancial"].Split(',');
                select_Investment += " and (";
                foreach (string str in temp)
                {
                    select_Investment += " Investment " + str + " or";
                }
                select_Investment = select_Investment.Substring(0, select_Investment.Length - 3);
                select_Investment += ")";
            }
            string order = "ID";
            System.Data.SqlClient.SqlParameter[] selparms = new System.Data.SqlClient.SqlParameter[7];
            selparms[0] = new System.Data.SqlClient.SqlParameter("@keys", keys);
            selparms[1] = new System.Data.SqlClient.SqlParameter("@InvestmentNature", select_InvestmentNature);
            selparms[2] = new System.Data.SqlClient.SqlParameter("@TeamworkType", select_TeamworkType);
            selparms[3] = new System.Data.SqlClient.SqlParameter("@Industry", select_industry);
            selparms[4] = new System.Data.SqlClient.SqlParameter("@InvestmentStage", select_InvestmentStage);
            selparms[5] = new System.Data.SqlClient.SqlParameter("@FinancSum", select_Investment);
            selparms[6] = new System.Data.SqlClient.SqlParameter("@Order", order);

            IList<T_XM_Investment> investments = (from p in db.T_XM_Investment.SqlQuery("exec dbo.P_GetTZXMByCondition @keys,@InvestmentNature,@TeamworkType,@Industry,@InvestmentStage,@FinancSum,@Order", selparms) select p).ToList();
            PagedList<T_XM_Investment> paged_inverstments = new PagedList<T_XM_Investment>(investments, Convert.ToInt32(collection["current_page_id"]), 5);
            if (paged_inverstments.Count == 0)
                ViewBag.Message = "未找到符合要求的项目!";
            return View(paged_inverstments); 
        }


        /// <summary>
        /// 找服务
        /// </summary>
        /// <returns></returns>
        public ActionResult Products()
        {
           return View(); 
        }
 
        #endregion

        #region --------------------外网（找项目、找资金）--------------------------
        /// <summary>
        /// 找项目
        /// </summary>
        /// <returns></returns>
        public ActionResult zxm(int id = 1)
        {
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();
            ViewData["Industry"] = new SelectList(Industry, "ID", "Name");
            List<T_PTF_DicDetail> ItemStage = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM04")).ToList();
            ViewData["ItemStage"] = new SelectList(ItemStage, "ID", "Name");

            var financials = db.T_XM_Financing.Where(f=>(f.IsValid==true && f.PublicStatus=="2")).OrderByDescending(f => f.CreateTime).ToPagedList(id, 5);

            ViewBag.FavoredFinacials = FavoredItems(1);
            ViewBag.current_page_id = id;
            if (Session["MemberID"] != null && Session["MemberID"].ToString() != "")
            {
                ViewBag.CurrentMember = Session["MemberID"].ToString();
            }
            return View(financials);
        }

        /// <summary>
        /// 找到的项目内容
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult zxm(FormCollection collection, int id = 1)
        {
            ViewBag.condition1 = collection["condition1"];
            ViewBag.condition2 = collection["condition2"];
            ViewBag.condition3 = collection["condition3"];
            ViewBag.condition4 = collection["condition4"];
            ViewBag.context = collection["context"];
            ViewBag.current_page_id = collection["current_page_id"];
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();
            ViewData["Industry"] = new SelectList(Industry, "ID", "Name");
            List<T_PTF_DicDetail> ItemStage = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM04")).ToList();
            ViewData["ItemStage"] = new SelectList(ItemStage, "ID", "Name");
            string keys = "";
            string select_itemtype = "";
            string select_industry = "";
            string select_Financial = "";
            string select_itemstage = "";
            if (collection["keys"].ToString().Trim() != "")
                keys = collection["keys"].ToString();
            if (collection["cbItemType"] != null)
                select_itemtype = collection["cbItemType"];
            if (collection["cbIndustry"] != null)
                select_industry = collection["cbIndustry"];
            if (collection["cbItemStage"] != null)
            {
                string[] temp = collection["cbItemStage"].Split(',');
                select_itemstage += " and (";
                foreach (string str in temp)
                {
                    select_itemstage += " ItemStage = '" + str + "' or";
                }
                select_itemstage = select_itemstage.Substring(0, select_itemstage.Length - 3);
                select_itemstage += ")";
            }
            if (collection["cbFinancial"] != null)
            {
                string[] temp = collection["cbFinancial"].Split(',');
                select_Financial += " and (";
                foreach (string str in temp)
                {
                    select_Financial += " FinancSum " + str + " or";
                }
                select_Financial = select_Financial.Substring(0, select_Financial.Length - 3);
                select_Financial += ")";
            }
            string order = "CreateTime desc";
            System.Data.SqlClient.SqlParameter[] selparms = new System.Data.SqlClient.SqlParameter[6];
            selparms[0] = new System.Data.SqlClient.SqlParameter("@keys", keys);
            selparms[1] = new System.Data.SqlClient.SqlParameter("@ItemType", select_itemtype);
            selparms[2] = new System.Data.SqlClient.SqlParameter("@Industry", select_industry);
            selparms[3] = new System.Data.SqlClient.SqlParameter("@ItemStage", select_itemstage);
            selparms[4] = new System.Data.SqlClient.SqlParameter("@FinancSum", select_Financial);
            selparms[5] = new System.Data.SqlClient.SqlParameter("@Order", order);
            IList<T_XM_Financing> financials = (from p in db.T_XM_Financing.SqlQuery("exec dbo.P_GetRZXMByCondition @keys,@ItemType,@Industry,@ItemStage,@FinancSum,@Order", selparms) select p).ToList();
            PagedList<T_XM_Financing> paged_financials = new PagedList<T_XM_Financing>(financials, Convert.ToInt32(collection["current_page_id"]), 5);
            if (paged_financials.Count == 0)
                ViewBag.Message = "未找到符合要求的项目!";
            return View(paged_financials);
        }

        /// <summary>
        /// 找到资金
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult zzj(FormCollection collection,int id =1)
        {
            ViewBag.condition1 = collection["condition1"];
            ViewBag.condition2 = collection["condition2"];
            ViewBag.condition3 = collection["condition3"];
            ViewBag.condition4 = collection["condition4"];
            ViewBag.condition5 = collection["condition5"];
            ViewBag.context = collection["context"];
            ViewBag.current_page_id = collection["current_page_id"];
            List<T_PTF_DicDetail> TeamworkType = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM06")).ToList();
            ViewData["TeamworkType"] = new SelectList(TeamworkType, "ID", "Name");
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();
            ViewData["Industry"] = new SelectList(Industry, "ID", "Name");
            List<T_PTF_DicDetail> InvestmentNature = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM07")).ToList();
            ViewData["InvestmentNature"] = new SelectList(InvestmentNature, "ID", "Name");
            List<T_PTF_DicDetail> InvestmentStage = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM04")).ToList();
            ViewData["InvestmentStage"] = new SelectList(InvestmentStage, "ID", "Name");
            
            string keys = "";
            string select_TeamworkType = "";
            string select_industry = "";
            string select_Investment = "";
            string select_InvestmentStage = "";
            string select_InvestmentNature = "";
            if (collection["keys"].ToString().Trim() != "")
                keys = collection["keys"].ToString();
            if (collection["cbInvestmentNature"] != null)
            {
                string[] temp = collection["cbInvestmentNature"].Split(',');
                select_InvestmentNature += " and (";
                foreach (string str in temp)
                {
                    select_InvestmentNature += " InvestmentNature = '" + str + "' or";
                }
                select_InvestmentNature = select_InvestmentNature.Substring(0, select_InvestmentNature.Length - 3);
                select_InvestmentNature += ")";
            }
            if (collection["cbInvestmentStage"] != null)
            {
                string[] temp = collection["cbInvestmentStage"].Split(',');
                select_InvestmentStage += " and (";
                foreach (string str in temp)
                {
                    select_InvestmentStage += " InvestmentStage like '%" + str + "%' or";
                }
                select_InvestmentStage = select_InvestmentStage.Substring(0, select_InvestmentStage.Length - 3);
                select_InvestmentStage += ")";
            }
            if (collection["cbTeamworkType"] != null)
            {
                string[] temp = collection["cbTeamworkType"].Split(',');
                select_TeamworkType += " and (";
                foreach (string str in temp)
                {
                    select_TeamworkType += " TeamworkType like '%" + str + "%' or";
                }
                select_TeamworkType = select_TeamworkType.Substring(0, select_TeamworkType.Length - 3);
                select_TeamworkType += ")";
            }
            if (collection["cbIndustry"] != null)
            {
                string[] temp = collection["cbIndustry"].Split(',');
                select_industry += " and (";
                foreach (string str in temp)
                {
                    select_industry += " AimIndustry like '%" + str + "%' or";
                }
                select_industry = select_industry.Substring(0, select_industry.Length - 3);
                select_industry += ")";
            }
            if (collection["cbFinancial"] != null)
            {
                string[] temp = collection["cbFinancial"].Split(',');
                select_Investment += " and (";
                foreach (string str in temp)
                {
                    select_Investment += " Investment " + str + " or";
                }
                select_Investment = select_Investment.Substring(0, select_Investment.Length - 3);
                select_Investment += ")";
            }
            string order = "ID";
            System.Data.SqlClient.SqlParameter[] selparms = new System.Data.SqlClient.SqlParameter[7];
            selparms[0] = new System.Data.SqlClient.SqlParameter("@keys", keys);
            selparms[1] = new System.Data.SqlClient.SqlParameter("@InvestmentNature", select_InvestmentNature);
            selparms[2] = new System.Data.SqlClient.SqlParameter("@TeamworkType", select_TeamworkType);
            selparms[3] = new System.Data.SqlClient.SqlParameter("@Industry", select_industry);
            selparms[4] = new System.Data.SqlClient.SqlParameter("@InvestmentStage", select_InvestmentStage);
            selparms[5] = new System.Data.SqlClient.SqlParameter("@FinancSum", select_Investment);
            selparms[6] = new System.Data.SqlClient.SqlParameter("@Order", order);

            IList<T_XM_Investment> investments = (from p in db.T_XM_Investment.SqlQuery("exec dbo.P_GetTZXMByCondition @keys,@InvestmentNature,@TeamworkType,@Industry,@InvestmentStage,@FinancSum,@Order", selparms) select p).ToList();
            PagedList<T_XM_Investment> paged_inverstments = new PagedList<T_XM_Investment>(investments, Convert.ToInt32(collection["current_page_id"]), 5);
            if (paged_inverstments.Count == 0)
                ViewBag.Message = "未找到符合要求的项目!";
            return View(paged_inverstments);
        }

        /// <summary>
        /// 找资金
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public ActionResult zzj(int id = 1)
        {
            List<T_PTF_DicDetail> TeamworkType = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM06")).ToList();
            ViewData["TeamworkType"] = new SelectList(TeamworkType, "ID", "Name");
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();
            ViewData["Industry"] = new SelectList(Industry, "ID", "Name");
            List<T_PTF_DicDetail> InvestmentNature = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM07")).ToList();
            ViewData["InvestmentNature"] = new SelectList(InvestmentNature, "ID", "Name");
            List<T_PTF_DicDetail> InvestmentStage = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM04")).ToList();
            ViewData["InvestmentStage"] = new SelectList(InvestmentStage, "ID", "Name");
            ViewBag.current_page_id = id;
            var investments = db.T_XM_Investment.Where(f=>(f.IsValid == true && f.PublicStatus=="2")).OrderByDescending(f => f.CreateTime).ToPagedList(id, 5);
            return View(investments);
        }

        /// <summary>
        /// 找产品
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public ActionResult zcp(int id = 1)
        {
            List<T_JG_Agency> AgencyList = db.T_JG_Agency.Where(p => p.AgencyType == 2119).ToList();
            ViewData["AgencyList"] = new SelectList(AgencyList, "ID", "SubName");
            List<T_PTF_DicDetail> CustomerType = db.T_PTF_DicDetail.Where(p => (p.DicType == "JG02")).ToList();
            ViewData["CustomerType"] = new SelectList(CustomerType, "ID", "Name");
            var products = db.T_JG_Product.Where(p => (p.IsValid == true && p.PublicStatus == "2")).OrderByDescending(p => p.CreateTime).ToPagedList(id, 5);
            return View(products);
        }
        [HttpPost]
        public ActionResult zcp(FormCollection collection, int id = 1)
        {
            ViewBag.condition1 = collection["condition1"];
            ViewBag.condition2 = collection["condition2"];
            ViewBag.condition3 = collection["condition3"];
            ViewBag.condition4 = collection["condition4"];
            ViewBag.context = collection["context"];
            List<T_JG_Agency> AgencyList = db.T_JG_Agency.Where(p => p.AgencyType == 2119).ToList();
            ViewData["AgencyList"] = new SelectList(AgencyList, "ID", "SubName");
            List<T_PTF_DicDetail> CustomerType = db.T_PTF_DicDetail.Where(p => (p.DicType == "JG02")).ToList();
            ViewData["CustomerType"] = new SelectList(CustomerType, "ID", "Name");

            string keys = "";
            string select_CustomType = "";
            string select_Limit = "";
            string select_Agencys = "";
            string select_Amount = "";
            if (collection["keys"].ToString().Trim() != "")
                keys = collection["keys"].ToString();
            if (collection["cbCustomerType"] != null)
            {
                string[] temp = collection["cbCustomerType"].Split(',');
                select_CustomType += " and (";
                foreach (string str in temp)
                {
                    select_CustomType += " CustomerType like '%" + str + "%' or";
                }
                select_CustomType = select_CustomType.Substring(0, select_CustomType.Length - 3);
                select_CustomType += ")";
            }
            if (collection["cbLimit"] != null)
            {
                string[] temp = collection["cbLimit"].Split(',');
                select_Limit = temp[temp.Length - 1];
            }
            if (collection["cbAgency"] != null)
            {
                select_Agencys += collection["cbAgency"];
            }
            if (collection["cbFinancial"] != null)
            {
                string[] temp = collection["cbFinancial"].Split(',');
                select_Amount += " and (";
                foreach (string str in temp)
                {
                    select_Amount += " FinancingAmount " + str + " or";
                }
                select_Amount = select_Amount.Substring(0, select_Amount.Length - 3);
                select_Amount += ")";
            }
            string order = "ID";
            System.Data.SqlClient.SqlParameter[] selparms = new System.Data.SqlClient.SqlParameter[6];
            selparms[0] = new System.Data.SqlClient.SqlParameter("@keys", keys);
            selparms[1] = new System.Data.SqlClient.SqlParameter("@CustomType", select_CustomType);
            selparms[2] = new System.Data.SqlClient.SqlParameter("@Limit", select_Limit);
            selparms[3] = new System.Data.SqlClient.SqlParameter("@Agencys", select_Agencys);
            selparms[4] = new System.Data.SqlClient.SqlParameter("@Amount", select_Amount);
            selparms[5] = new System.Data.SqlClient.SqlParameter("@Order", order);
            IList<T_JG_Product> products = (from p in db.T_JG_Product.SqlQuery("exec dbo.P_GetCPByCondition @keys,@CustomType,@Limit,@Agencys,@Amount,@Order", selparms) select p).ToList();
            PagedList<T_JG_Product> paged_products = new PagedList<T_JG_Product>(products, id, 5);
            if (paged_products.Count == 0)
                ViewBag.Message = "未找到符合要求的产品!";
            return View(paged_products);
        }

        public void BindAgencyType(object select = null)
        {
            List<T_PTF_DicDetail> AgencyType = db.T_PTF_DicDetail.Where(p => (p.DicType == "JG01" && p.IsValid == "1")).ToList();

            ViewData["AgencyType"] = new SelectList(AgencyType, "ID", "Name", select);
        }

        /// <summary>
        /// 找机构
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public ActionResult zjg(int AgencyType, int id = 1)
        {
            BindAgencyType(AgencyType);
            var agencys = db.T_JG_Agency.Where(p => p.IsValid == true);
            if (AgencyType != 0)
            {
                agencys = agencys.Where(p => p.AgencyType == AgencyType);
            }
            var agencylist = agencys.OrderBy(p => p.CreateTime).ToPagedList(id, 5);
            return View(agencylist);
        }

        [HttpPost]
        public ActionResult zjg(FormCollection collection, int id = 1)
        {
            
            string agencyname = collection["agencyname"] == null ? "" : collection["agencyname"];
            int agencytype = collection["AgencyType"] == null || collection["AgencyType"] == "" ? 0 : Convert.ToInt32(collection["AgencyType"]);
            BindAgencyType(agencytype);
            ViewData["agencyname"] = agencyname;
            var t_jg_agency = db.T_JG_Agency.Where(c => c.IsValid == true && c.AgencyName.Contains(agencyname));
            if (agencytype != 0)
            {
                t_jg_agency = t_jg_agency.Where(c => c.AgencyType == agencytype);
            }

            var agencylist = t_jg_agency.OrderBy(p => p.CreateTime).ToPagedList(id, 5);

            return View(agencylist);
        }

        public void BindProperty(object select = null)
        {
            List<T_PTF_DicDetail> Property = db.T_PTF_DicDetail.Where(p => (p.DicType == "5" && p.IsValid == "1")).ToList();

            ViewData["Property"] = new SelectList(Property, "ID", "Name", select);
        }
        public void BindIndustry(object select = null)
        {
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();

            ViewData["Industry"] = new SelectList(Industry, "ID", "Name", select);
        }
        public void BindRegKey(object select = null)
        {
            List<SelectListItem> selkey = new List<SelectListItem>
          {
              new SelectListItem { Text = "大于", Value = "1" },
              new SelectListItem  { Text = "小于", Value = "0" }
            };

            ViewData["RegKey"] = new SelectList(selkey, "Value", "Text", select);
        }
        public ActionResult zqy(int id = 1)
        {
            BindProperty();
            BindIndustry();
            BindRegKey();
            var corplist = db.T_QY_Corp.Where(p => p.IsValid == true).OrderBy(p => p.CreateTime).ToPagedList(id, 5);

            return View(corplist);
        }
        /// <summary>
        /// 找企业
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult zqy(FormCollection collection, int id = 1)
        {
            BindProperty();
            BindIndustry();
            string corpname = collection["corpname"] == null ? "" : collection["corpname"];
            string property = collection["cbProperty"] == null ? "" : collection["cbProperty"];
            decimal regcapital = collection["regcapital"] == null || collection["regcapital"] == "" ? 0 : Convert.ToDecimal(collection["regcapital"]);
            string industry = collection["cbIndustry"] == null ? "" : collection["cbIndustry"];
            ViewData["corpname"] = corpname;
            ViewData["selproperty"] = property;
            ViewData["regcapital"] = regcapital;
            ViewData["selindustry"] = industry;
            BindRegKey(collection["regkey"]);
            var t_qy_corp = db.T_QY_Corp.Where(c => c.IsValid == true && c.CorpName.Contains(corpname));
            if (property != "")
            {
                int[] temp1 = new int[property.Split(',').Length];
                for (int i = 0; i < property.Split(',').Length; i++)
                {
                    temp1[i] = Convert.ToInt32(property.Split(',')[i]);
                }
                t_qy_corp = t_qy_corp.Where(c => temp1.Contains((int)c.Property));
            }
            if (regcapital != 0)
            {
                if (collection["regkey"] == "1")
                    t_qy_corp = t_qy_corp.Where(c => c.RegCapital > regcapital);
                else
                    t_qy_corp = t_qy_corp.Where(c => c.RegCapital < regcapital);
            }
            if (industry != "")
            {
                int[] temp = new int[industry.Split(',').Length];
                for (int i = 0; i < industry.Split(',').Length; i++)
                {
                    temp[i] = Convert.ToInt32(industry.Split(',')[i]);
                }
                t_qy_corp = t_qy_corp.Where(c => temp.Contains((int)c.Industry));
            }
            var corplist = t_qy_corp.OrderBy(p => p.CreateTime).ToPagedList(id, 5);

            return View(corplist);
        }

        #endregion
    }  
}
