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

        public ActionResult Index(int id)
        {
            var member = db.T_HY_Member.Find(id);
            return View(member);
        }

        /// <summary>
        /// 用户所发布的项目
        /// </summary>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public ActionResult PublishedFinancials(int member_id)
        {
            var finacials = db.T_XM_Financing.Where( f => f.UserID == member_id).ToList();
            return PartialView(finacials);
        }

        ///// <summary>
        ///// 用户所发布的投资意向
        ///// </summary>
        ///// <param name="member_id"></param>
        ///// <returns></returns>
        //public ActionResult PublishedInvestments(int member_id)
        //{
        //    var finacials = db.T_XM_Financing.Where( f => f.UserID == member_id).ToList();
        //    return PartialView(finacials);
        //}

        ///// <summary>
        ///// 用户所发布的金融产品
        ///// </summary>
        ///// <param name="member_id"></param>
        ///// <returns></returns>
        //public ActionResult PublishedProducts(int member_id)
        //{
        //    var products = db.T_JG_Product.Where( f => f. == member_id).ToList();
        //    return PartialView(finacials);
        //}
    }
}
