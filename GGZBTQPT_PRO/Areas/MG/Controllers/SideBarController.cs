using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models; 

namespace GGZBTQPT_PRO.Areas.MG.Controllers
{

    public class SideBarController : BaseController
    {
        //
        // GET: /Member/SideBar/ 

        public ActionResult MemberBrief()
        {
            var member = CurrentMember(); 
            return PartialView(member); 
        }

        public ActionResult TheLastestFinancials()
        {
            var finacials = db.T_XM_Financing.Where(p => p.PublicStatus == "2" && p.IsValid == true).OrderByDescending(f => f.CreateTime).Take(4).ToList();
            return PartialView(finacials); 
        }

        public ActionResult TheLastestInvestments()
        {
            var investments = db.T_XM_Investment.Where(p => p.PublicStatus == "2" && p.IsValid == true).OrderByDescending(i => i.CreateTime).Take(4).ToList();
            return PartialView(investments); 
        }

        public ActionResult TheLastestProducts()
        {
            var products = db.T_JG_Product.Where(p => p.PublicStatus == "2" && p.IsValid == true).OrderByDescending(p => p.CreateTime).Take(4).ToList();
            return PartialView(products);
        }

        public ActionResult TheLastestAgencies()
        {
            var agencies = db.T_JG_Agency.Where(p => p.IsValid == true).OrderByDescending(p => p.CreateTime).Take(4).ToList();
            return PartialView(agencies);
        } 
    }
}
