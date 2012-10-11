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

        [ChildActionOnly]
        public PartialViewResult MemberBrief()
        {
            int member_id = GetCurrentMember();
            var member = db.T_HY_Member.Find(member_id);
            return PartialView(member);
        }

        public PartialViewResult TheLastestFinancials()
        {
            return PartialView();
        }

        public PartialViewResult TheLastestInvestMents()
        {
            return PartialView();
        }

        public PartialViewResult TheLastestProducts()
        {
            return PartialView();
        }
        public int GetCurrentMember()
        {
            //int member_id = Convert.ToInt32(Session["MemberID"]); 
            int member_id = 1;
            return member_id; 
        }

    }
}
