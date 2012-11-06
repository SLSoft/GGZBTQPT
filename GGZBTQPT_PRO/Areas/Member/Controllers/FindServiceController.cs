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

        //机构一览表

        public ActionResult Index()
        {
            var jg_agency = db.T_JG_Agency.Where(p => p.IsIn == true).ToList();
            return View(jg_agency);
        }

        public ActionResult AgencyList(int AgencyType, bool isin)
        {
            if (AgencyType != 0)
            {
                if ((bool)isin == true)
                {
                    var jg_agency = db.T_JG_Agency.Where(p => (p.AgencyType == (int)AgencyType && p.IsIn == (bool)isin)).ToList();
                    return View(jg_agency);
                }
                else
                {
                    var jg_agency = db.T_JG_Agency.Where(p => p.AgencyType == (int)AgencyType).ToList();
                    return View(jg_agency);
                }
            }
            else
            {
                var jg_agency = db.T_JG_Agency.ToList();
                return View(jg_agency);
            }
        }
        //机构详情
        public ActionResult AgencyShow(int id)
        {
            T_JG_Agency t_jg_agency = db.T_JG_Agency.Find(id);
            return View(t_jg_agency);
        }
    }
}
