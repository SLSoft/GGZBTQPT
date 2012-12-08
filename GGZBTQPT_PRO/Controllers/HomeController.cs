using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;


namespace GGZBTQPT_PRO.Controllers
{
 
    public class HomeController : BaseController
    {

        public ActionResult Index()
        { 
            var menus = db.T_ZC_Menu.Where(p => p.IsValid == true).ToList();
            return View(menus);
        }

        public ActionResult SystemLink()
        {
            var system_links = db.T_ZC_System.OrderBy(s => s.ID).Where(s => s.IsValid == true).ToList();
            return PartialView(system_links);
        }

        public ActionResult SystemMenus(int id = 1)
        {
            var system = FindSystemByID(id);
            var menu_links = system.Menus.Where(m => m.IsValid == true).OrderBy(m => m.ID).ToList();
            return PartialView(menu_links);
        }

        //
        //Helper
        public T_ZC_System FindSystemByID(int id)
        {
            var system = db.T_ZC_System.Find(id);
            return system;
        }

    }
}
