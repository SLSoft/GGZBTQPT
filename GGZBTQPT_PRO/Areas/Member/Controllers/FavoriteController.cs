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
    public class FavoriteController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();
        private Dictionary<string,string> notice = new Dictionary<string,string>();
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
        /// 用户所收藏的项目
        /// </summary>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public ActionResult FavoredFinancials(int member_id)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var finacials = member.Favorites.Where(a => a.FavoriteType == 1)
                                .Join(db.T_XM_Financing, a => a.FinancialID, p => p.ID, 
                                    (a, p) => new T_XM_Financing {  
                                        ItemName = p.ItemName, Investment = p.Investment, 
                                        TotalInvestment = p.TotalInvestment, 
                                        ID = p.ID, ItemContent = p.ItemContent, Favoites = p.Favoites 
                                    })
                                .ToList();

                return PartialView(finacials);
            }
            catch
            {
                return PartialView();
            } 
        }

        /// <summary>
        /// 用户所收藏的投资意向
        /// </summary>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public ActionResult FavoredInvestments(int member_id)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var investments = member.Favorites.Where(a => a.FavoriteType == 2)
                                .Join(db.T_XM_Investment, a => a.FinancialID, p => p.ID,
                                    (a, p) => new T_XM_Investment { 
                                        ItemName = p.ItemName, Investment = p.Investment, 
                                        StartInvestment = p.StartInvestment, Favoites = p.Favoites 
                                    })
                                .ToList();
                return PartialView(investments);
            }
            catch
            {
                return PartialView();
            } 
        }

        /// <summary>
        /// 用户所收藏的金融产品
        /// </summary>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public ActionResult FavoredProducts(int member_id)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                var products = member.Favorites.Where(a => a.FavoriteType == 3)
                                .Join(db.T_JG_Product, a => a.FinancialID, p => p.ID,
                                    (a, p) => new T_JG_Product { 
                                        ProductName = p.ProductName, RepaymentType = p.RepaymentType, 
                                        Favoites = p.Favoites 
                                    })
                                .ToList();
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





    }
}
