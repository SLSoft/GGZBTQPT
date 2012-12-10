using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;

namespace GGZBTQPT_PRO.Controllers
{
    public class ChildActionController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();
        // GET: /ChildAction/

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult SystemLinks()
        {
            var links = db.T_ZC_System.Where(p => p.IsValid == true).ToList();
            return PartialView(links);
        }

    }
}
