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
    [MemberFilter()]
    public class RecommendController : BaseController
    {
 
        //
        // GET: /Member/Publish/

        public ActionResult Index()
        { 
            return View(CurrentMember()); 
        }

        /// <summary>
        /// 系统所推荐的项目
        /// </summary>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public ActionResult RecommendFinancials(int id = 1)
        {
            var member = CurrentMember();

            try
            {
                //*********TO-DO************//
                //*********根据用户收藏的的内容和关注的人员进行特殊推荐*****************//
                //目前需要完成根据项目的收藏数进行排序，以下的投资和产品同

                PagedList<T_XM_Financing> finacials = db.T_XM_Financing.OrderByDescending(f => f.CreateTime).ToPagedList(id,5);
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
        public ActionResult RecommendInvestments(int id = 1)
        {
            var member = CurrentMember();
    
            try
            {
                PagedList<T_XM_Investment> investments = db.T_XM_Investment.OrderByDescending(f => f.CreateTime).ToPagedList(id, 5);
                ViewBag.FavoredInvestments = FavoredItems(2);
                ViewBag.AttentionedMembers = AttentionedMembers();
                ViewBag.CurrentMember = member.ID;
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
        public ActionResult RecommendProducts(int id = 1)
        {
            var member = CurrentMember();
           
            try
            {
                PagedList<T_JG_Product> products = db.T_JG_Product.OrderByDescending(p => p.CreateTime).ToPagedList(id, 5);
                ViewBag.FavoredInvestments = FavoredItems(3);
                ViewBag.AttentionedMembers = AttentionedMembers();
                ViewBag.CurrentMember = member.ID;
                return PartialView(products);
            }
            catch
            {
                return PartialView();
            }
        }


        /// <summary>
        /// 系统所推荐的金融机构
        /// </summary>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public ActionResult RecommendAgencies(int id = 1)
        {
            var member = CurrentMember();
         
            try
            {
                PagedList<T_JG_Agency> Agencies = db.T_JG_Agency.OrderByDescending(a => a.CreateTime).ToPagedList(id, 5); 
                ViewBag.AttentionedMembers = AttentionedMembers();//金融机构只有关注，而没有收藏
                ViewBag.CurrentMember = member.ID;
                return PartialView(Agencies);
            }
            catch
            {
                return PartialView();
            }
        } 




    }
}
