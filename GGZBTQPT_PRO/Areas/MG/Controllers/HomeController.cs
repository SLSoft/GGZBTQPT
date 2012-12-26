using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.Areas.MG.Filter;

namespace GGZBTQPT_PRO.Areas.MG.Controllers
{
    [MemberFilter]
    public class HomeController : BaseController
    { 
        public ActionResult Index()
        { 
            var member = db.T_HY_Member.Find(CurrentMember().ID);
            return View(member); 
        } 
    } 
}
