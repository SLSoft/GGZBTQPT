using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;

namespace GGZBTQPT_PRO.Controllers
{ 
    public class XM_RZController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /XM_RZ/

        public ViewResult Index()
        {
            return View(db.T_XM_Financing.ToList());
        }

        //
        // GET: /XM_RZ/Details/5

        public ViewResult Details(int id)
        {
            T_XM_Financing t_xm_financing = db.T_XM_Financing.Find(id);
            return View(t_xm_financing);
        }

        public void BindIndustry(object select = null)
        {
            List<SelectListItem> Industry = new List<SelectListItem>
            {
                new SelectListItem{Text="互联网",Value="0"},
                new SelectListItem{Text="房地产",Value="1"}
            };

            ViewData["Industry"] = new SelectList(Industry,"Value","Text",select);
        }

        public void BindArea(object select = null)
        {
            List<T_PTF_DicTreeDetail> Area = db.T_PTF_DicTreeDetail.Where(p => (p.DicType == "34" && p.Depth == 1)).ToList();

            ViewData["Province"] = new SelectList(Area, "ID", "Name", select);
        }

        //
        // GET: /XM_RZ/Create

        public ActionResult Create()
        {
            BindArea();
            BindIndustry();
            return View();
        } 

        //
        // POST: /XM_RZ/Create

        [HttpPost]
        public ActionResult Create(T_XM_Financing t_xm_financing)
        {
            if (ModelState.IsValid)
            {
                t_xm_financing.Industry = 1;
                db.T_XM_Financing.Add(t_xm_financing);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(t_xm_financing);
        }
        
        //
        // GET: /XM_RZ/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_XM_Financing t_xm_financing = db.T_XM_Financing.Find(id);
            BindArea(t_xm_financing.Province);
            BindIndustry(t_xm_financing.Industry);
            return View(t_xm_financing);
        }

        //
        // POST: /XM_RZ/Edit/5

        [HttpPost]
        public ActionResult Edit(T_XM_Financing t_xm_financing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_xm_financing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_xm_financing);
        }

        //
        // GET: /XM_RZ/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_XM_Financing t_xm_financing = db.T_XM_Financing.Find(id);
            return View(t_xm_financing);
        }

        //
        // POST: /XM_RZ/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //if (Request.IsAjaxRequest())
            //{
            //    T_XM_Financing t_xm_financing = db.T_XM_Financing.Find(id);
            //    db.T_XM_Financing.Remove(t_xm_financing);
            //    db.SaveChanges();
            //}
            //return RedirectToAction("Index");
            try
            {
                // TODO: Add delete logic here

                if (Request.IsAjaxRequest())
                {
                    T_XM_Financing t_xm_financing = db.T_XM_Financing.Find(id);
                    db.T_XM_Financing.Remove(t_xm_financing);
                    int result = db.SaveChanges();
                    return Content(result.ToString());
                }
                else
                {
                    return Content("-1");
                }
            }
            catch
            {
                return Content("-1");
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}