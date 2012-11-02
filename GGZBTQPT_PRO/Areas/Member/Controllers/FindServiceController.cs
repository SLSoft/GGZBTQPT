using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;

namespace GGZBTQPT_PRO.Areas.Member.Controllers
{
    public class FindServiceController : Controller
    {

        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();
        //
        // GET: /Member/FindService/

        public ActionResult Index()
        {
            var jg_agency = db.T_JG_Agency.ToList();
            return View(jg_agency);
        }

    }
}
