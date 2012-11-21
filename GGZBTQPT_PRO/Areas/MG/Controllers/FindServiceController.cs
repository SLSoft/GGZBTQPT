using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using Webdiyer.WebControls.Mvc;

namespace GGZBTQPT_PRO.Areas.MG.Controllers
{
    public class FindServiceController : BaseController
    {


        //
        // GET: /Member/FindService/

        //机构一览表

        public ActionResult Index(int id = 1)
        {
            BindAgencyType();
            return View();
        }

        public ActionResult AgencyType(int AgencyType = 2112, int id = 1)
        {
            PagedList<T_JG_Agency> jg_agency = db.T_JG_Agency.Where(p => p.AgencyType == AgencyType).OrderBy(p => p.CreateTime).ToPagedList(id, 5);
            if (jg_agency.Count == 0)
            {
                ViewBag.Message = "暂无该类别机构!";
            }
            ViewBag.AgencyName = db.T_PTF_DicDetail.Find(AgencyType).Name;
            return PartialView(jg_agency);
        }

        public void BindAgencyType()
        {
            List<T_PTF_DicDetail> AgencyType = db.T_PTF_DicDetail.Where(p => (p.DicType == "JG01")).ToList();

            ViewData["AgencyType"] = new SelectList(AgencyType, "ID", "Name");
        }
        public ActionResult AgencyList(int AgencyType, bool isin)
        {
            ViewBag.isin = isin;
            ViewBag.AgencyType = AgencyType;
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
                ViewBag.isin = isin;
                if ((bool)isin == true)
                {
                    var jg_agency = db.T_JG_Agency.Where(p => p.IsIn == (bool)isin).ToList();
                    return View(jg_agency);
                }
                else
                {
                    var jg_agency = db.T_JG_Agency.ToList();
                    return View(jg_agency);
                }
            }
        }
        //机构详情
        public ActionResult AgencyShow(int id)
        {
            T_JG_Agency t_jg_agency = db.T_JG_Agency.Find(id);
            return View(t_jg_agency);
        }

        public ActionResult Agency(int AgencyType, bool isin)
        {
            ViewBag.isin = isin;
            ViewBag.AgencyType = AgencyType;
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
                ViewBag.isin = isin;
                if ((bool)isin == true)
                {
                    var jg_agency = db.T_JG_Agency.Where(p => p.IsIn == (bool)isin).ToList();
                    return View(jg_agency);
                }
                else
                {
                    var jg_agency = db.T_JG_Agency.ToList();
                    return View(jg_agency);
                }
            }
        }

        public ActionResult CorpList()
        {
            var qy_corp = db.T_QY_Corp.Where(c => c.IsValid == true).ToList();
            return View(qy_corp);
        }
    }
}
