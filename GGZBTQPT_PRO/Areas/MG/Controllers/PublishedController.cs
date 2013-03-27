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
using GGZBTQPT_PRO.Enums;

namespace GGZBTQPT_PRO.Areas.MG.Controllers
{
    [MemberFilter()]
    public class PublishedController : BaseController
    {

        //
        // GET: /Member/Publish/

        public ActionResult Index()
        { 
            Logging("访问了我的发布", (int)OperateTypes.Visit,  (int)GenerateSystem.Publish);
            var member = db.T_HY_Member.Find(CurrentMember().ID);
            return View(member);

        }

        /// <summary>
        /// 用户所发布的项目
        /// </summary>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public ActionResult PublishedFinancials(int member_id, int id = 1)
        {
            Logging( "访问了所发布的项目", (int)OperateTypes.Visit, (int)GenerateSystem.Publish);
            var member = CurrentMember();
            
            try
            {
                PagedList<T_XM_Financing> finacials = db.T_XM_Financing.Where(p => p.PublicStatus == "2" && p.IsValid == true).OrderByDescending(f => f.CreateTime).Where(f => f.MemberID == member.ID && f.IsValid == true).ToPagedList(id, 5);
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
        public ActionResult PublishedInvestments(int member_id, int id = 1)
        {
            Logging( "访问了所发布的意向", (int)OperateTypes.Visit, (int)GenerateSystem.Publish);
            var member = CurrentMember();
            
            try
            {
                PagedList<T_XM_Investment> investments = db.T_XM_Investment.Where(p => p.PublicStatus == "2" && p.IsValid == true).OrderByDescending(f => f.CreateTime).Where(f => f.MemberID == member.ID && f.IsValid == true).ToPagedList(id, 5);
                return PartialView(investments);
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
        public ActionResult PublishedProducts(int member_id, int id = 1 )
        {
            Logging( "访问了所发布的产品", (int)OperateTypes.Visit, (int)GenerateSystem.Publish);
            var member = CurrentMember();
           
            try
            {
                PagedList<T_JG_Product> products = db.T_JG_Product.Where(p => p.PublicStatus == "2" && p.IsValid == true).OrderByDescending(p => p.CreateTime).Where(p => p.MemberID == member.ID && p.IsValid == true).ToPagedList(id, 5);
                return PartialView(products);
            }
            catch
            {
                return PartialView();
            } 
        } 

    }
}
