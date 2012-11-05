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
    public class SideBarController : BaseController
    {
        //
        // GET: /Member/SideBar/
        //private GGZBTQPTDBContext db = new GGZBTQPTDBContext();


        public ActionResult MemberBrief()
        {
            var member = CurrentMember();
            if (member == null)
            {
                return PartialView();
            }
            return PartialView(member);

        }

        public ActionResult TheLastestFinancials()
        {

            var finacials = db.T_XM_Financing.OrderByDescending(f => f.CreateTime).Take(4).ToList();
            return PartialView(finacials);

        }

        public ActionResult TheLastestInvestments()
        {

            var investments = db.T_XM_Investment.OrderByDescending(i => i.CreateTime).Take(4).ToList();
            return PartialView(investments);

        }

        public ActionResult TheLastestProducts()
        {
            var products = db.T_JG_Product.OrderByDescending(p => p.CreateTime).Take(4).ToList();
            return PartialView(products);
        } 

    }
}
