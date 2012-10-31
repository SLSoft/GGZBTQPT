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
    public class SideBarController : Controller
    {
        //
        // GET: /Member/SideBar/
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();


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

            var finacials = db.T_XM_Financing.Take(4).ToList();
            return PartialView(finacials);

        }

        public ActionResult TheLastestInvestments()
        {

            var investments = db.T_XM_Investment.Take(4).ToList();
            return PartialView(investments);

        }

        public ActionResult TheLastestProducts()
        {
            return PartialView();
        }


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
