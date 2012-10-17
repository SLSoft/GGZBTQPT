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
    public class RecommendController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();
        //
        // GET: /Member/Publish/

        public ActionResult Index()
        {

            if (CurrentMember() != null)
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
        /// 系统所推荐的项目
        /// </summary>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public ActionResult RecommendFinancials(int member_id)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Login", "Member");
            }
            try
            {
                //*********TO-DO************//
                //*********根据用户收藏的的内容和关注的人员进行特殊推荐*****************//
                //目前需要完成根据项目的收藏数进行排序，以下的投资和产品同
                var finacials = db.T_XM_Financing.ToList();
                ViewBag.FavoredFinacials = FavoredFinancials(1);
                return PartialView(finacials);
            }
            catch
            {
                return PartialView();
            }
        }

        /// <summary>
        /// 系统所推荐的投资意向
        /// </summary>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public ActionResult RecommendInvestments(int member_id)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Login", "Member");
            }
            try
            {
                var investments = db.T_XM_Investment.ToList();
                return PartialView(investments);
            }
            catch
            {
                return PartialView();
            }
        }

        /// <summary>
        /// 系统所推荐的金融产品
        /// </summary>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public ActionResult RecommendProducts(int member_id)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Login", "Member");
            }
            try
            {
                var products = db.T_JG_Product.ToList();
                return PartialView(products);
            }
            catch
            {
                return PartialView();
            }
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

        /// <summary>
        /// 读取当前用户所关注的项目的ID集合
        /// </summary>
        /// <returns></returns>
        private string FavoredFinancials(int favorite_type)
        {
            string favored_financials = "";

            List<int> financial_ids = CurrentMember().Favorites.Where(f => f.FavoriteType == favorite_type).Select(f => f.FavoriteID).ToList();
            foreach (int finacial_id in financial_ids)
            {
                favored_financials += "|" + finacial_id + "|";
            }
            
            return favored_financials;
        }


    }
}
