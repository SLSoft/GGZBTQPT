using System;
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
    public class FavoriteController : BaseController
    {

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
        public ActionResult FavoredFinancials(int member_id, int id = 1)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                IList<T_XM_Financing> financials = member.Favorites.Where(a => a.FavoriteType == 1)
                                .Join(db.T_XM_Financing, a => a.FinancialID, p => p.ID, 
                                    (a, p) => new T_XM_Financing {  
                                        ItemName = p.ItemName, Investment = p.Investment, 
                                        TotalInvestment = p.TotalInvestment, 
                                        ID = p.ID, ItemContent = p.ItemContent, Favoites = p.Favoites, Member = p.Member,
                                        CreateTime = p.CreateTime, UpdateTime = p.UpdateTime 
                                    }).OrderByDescending(f => f.CreateTime).ToList();
                PagedList<T_XM_Financing> paged_financials = new PagedList<T_XM_Financing>(financials,id,5); 

                return PartialView(paged_financials);
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
        public ActionResult FavoredInvestments(int member_id, int id = 1)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Index", "Home");
            }
            try
            {
                IList<T_XM_Investment> investments = member.Favorites.Where(a => a.FavoriteType == 2)
                                .Join(db.T_XM_Investment, a => a.InvestmentID, p => p.ID,
                                    (a, p) => new T_XM_Investment { 
                                        ItemName = p.ItemName, Investment = p.Investment, Description = p.Description,
                                        StartInvestment = p.StartInvestment, Favoites = p.Favoites, ID = p.ID, Member = p.Member,
                                        CreateTime = p.CreateTime,
                                        UpdateTime = p.UpdateTime 
                                    })
                                .OrderByDescending(i => i.CreateTime)
                                .ToList();
                PagedList<T_XM_Investment> paged_investments = new PagedList<T_XM_Investment>(investments, id, 5);
                return PartialView(paged_investments);
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
        public ActionResult FavoredProducts(int member_id, int id = 1)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                IList<T_JG_Product> products = member.Favorites.Where(a => a.FavoriteType == 3)
                                .Join(db.T_JG_Product, a => a.FinancialID, p => p.ID,
                                    (a, p) => new T_JG_Product { 
                                        ProductName = p.ProductName, RepaymentType = p.RepaymentType,
                                        Favoites = p.Favoites,
                                        CreateTime = p.CreateTime,
                                        UpdateTime = p.UpdateTime 
                                    })
                                .OrderByDescending(p => p.CreateTime)
                                .ToList();
                PagedList<T_JG_Product> paged_products = new PagedList<T_JG_Product>(products, id, 5);
                return PartialView(paged_products);
            }
            catch
            {
                return PartialView();
            } 
        } 



        /// <summary>
        /// 收藏项目、资金、服务
        /// </summary>
        /// <param name="type_id">收藏的类别</param>
        /// <param name="id">项目、资金、服务ID</param>
        [HttpPost]
        public ActionResult Favored(int type_id, int id)
        {
            var favored_item = CreateFavoredItem(type_id, id);
            var member = CurrentMember();

            member.Favorites.Add(favored_item);
            db.SaveChanges();

            return Json(new { statusCode = "200", message = "项目收藏成功", type = "success" });
        } 

        /// <summary>
        /// 取消收藏项目、资金、服务
        /// </summary>
        /// <param name="type_id">取消收藏的类别</param>
        /// <param name="id">项目、资金、服务ID</param>
        [HttpPost]
        public ActionResult UnFavored(int id)
        {
            var member = CurrentMember();
            var favored_item = member.Favorites.First( f => f.FinancialID == id);

            member.Favorites.Remove(favored_item);
            db.T_HY_Favorite.Remove(favored_item);
            db.SaveChanges();

            return Json(new { statusCode = "200", message = "项目取消收藏成功", type = "success" });
        }

        //
        //FavoredHelper
        public T_HY_Favorite CreateFavoredItem(int type_id, int id)
        {
            var favored_item = new T_HY_Favorite();
            switch (type_id)
            {
                case 1:
                    favored_item.FinancialID = id;
                    break;
                case 2:
                    favored_item.InvestmentID = id;
                    break;
                case 3:
                    break;
            }
            favored_item.FavoriteType = type_id;
            return favored_item;
        }

    }
}
