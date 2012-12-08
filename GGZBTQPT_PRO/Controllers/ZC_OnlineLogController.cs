using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.Enums;

namespace GGZBTQPT_PRO.Controllers
{ 
    public class ZC_OnlineLogController : BaseController
    { 
        public ViewResult OnlineLogs(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            IList<GGZBTQPT_PRO.Models.T_ZC_OnlineLog> list = db.T_ZC_OnlineLog.Include("Member").Where(o => o.Member.MemberName.Contains(keywords)).ToList()
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_ZC_OnlineLog.Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            return View(list); 
        }

        //
        // GET: /ZC_CommonLog/Details/5

        public ViewResult LogDetails(int id)
        {
            T_ZC_OnlineLog t_zc_online_log = db.T_ZC_OnlineLog.Find(id);
            return View(t_zc_online_log);
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

