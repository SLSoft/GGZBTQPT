using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;

namespace GGZBTQPT_PRO.Controllers
{
    public class HomeController : Controller
    {
      private GGZBTQPTDBContext db = new GGZBTQPTDBContext();
        //
        // GET: /Home/

        public ActionResult Index()
        {
          var menus = db.T_ZC_Menu.ToList();
          return View(menus);
        }

    }
}
