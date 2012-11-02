using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using Webdiyer.WebControls.Mvc;

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
                return View(CurrentMember());
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
        public ActionResult RecommendFinancials(int id = 1)
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

                PagedList<T_XM_Financing> finacials = db.T_XM_Financing.OrderBy(f => f.ID).ToPagedList(id,5);
                ViewBag.FavoredFinacials = FavoredItems(1);
                ViewBag.AttentionedMembers = AttentionedMembers();
                ViewBag.CurrentMember = member.ID;
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
        public ActionResult RecommendInvestments()
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Login", "Member");
            }
            try
            {
                var investments = db.T_XM_Investment.ToList();
                ViewBag.FavoredInvestments = FavoredItems(2);
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
        public ActionResult RecommendProducts()
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Login", "Member");
            }
            try
            {
                var products = db.T_JG_Product.ToList();
                ViewBag.FavoredInvestments = FavoredItems(3);
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
        /// 读取当前用户所收藏的项目的ID集合
        /// </summary>
        /// <returns></returns>
        private string FavoredItems(int favorite_type)
        {
            string favored_items = "";
            List<int?> item_ids = new List<int?>();


            switch(favorite_type)
            {
                case 1:
                    item_ids = CurrentMember().Favorites.Where(f => f.FavoriteType == favorite_type).Select(f => f.FinancialID).ToList();
                    break;
                case 2:
                    item_ids = CurrentMember().Favorites.Where(f => f.FavoriteType == favorite_type).Select(f => f.InvestmentID).ToList();
                    break;
                case 3:
                    item_ids = CurrentMember().Favorites.Where(f => f.FavoriteType == favorite_type).Select(f => f.ProductID).ToList();
                    break; 
            }

            foreach (int finacial_id in item_ids)
            {
                favored_items += "|" + finacial_id + "|";
            }

            return favored_items;
        }

        /// <summary>
        /// 读取当前用户所关注的用户的ID集合
        /// </summary>
        /// <returns></returns>
        private string AttentionedMembers()
        {
            string attentioned_members = "";
            List<int> member_ids = new List<int>();

            member_ids = CurrentMember().Attentions.Select(a => a.AttentionedMemberID).ToList();

            foreach (int member_id in member_ids)
            {
                attentioned_members += "|" + member_id + "|";
            }

            return attentioned_members;
        }


    }
}
