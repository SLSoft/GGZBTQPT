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
            return View(db.T_XM_Financing.Where(p  => p.IsValid == true).ToList());
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
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();

            ViewData["Industry"] = new SelectList(Industry,"ID","Name",select);
        }

        public void BindArea(object select = null)
        {
            List<T_PTF_DicTreeDetail> Area = db.T_PTF_DicTreeDetail.Where(p => (p.DicType == "34" && p.Depth == 1)).ToList();

            ViewData["Province"] = new SelectList(Area, "ID", "Name", select);
        }
        public void BindFinancType(object select = null)
        {
            List<T_PTF_DicDetail> FinancType = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM02")).ToList();

            ViewData["FinancType"] = new SelectList(FinancType, "ID", "Name", select);
        }
        
        //
        // GET: /XM_RZ/Create

        public ActionResult Create()
        {
            BindArea();
            BindIndustry();
            BindFinancType();

            return View();
        } 

        //
        // POST: /XM_RZ/Create

        [HttpPost]
        public ActionResult Create(T_XM_Financing t_xm_financing)
        {
            if (ModelState.IsValid)
            {
                t_xm_financing.IsValid = true;
                t_xm_financing.OP = 0;
                t_xm_financing.CreateTime = DateTime.Now;
                t_xm_financing.UpdateTime = DateTime.Now;
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
                t_xm_financing.UpdateTime = DateTime.Now;
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
                    //db.T_XM_Financing.Remove(t_xm_financing);
                    t_xm_financing.IsValid = false;
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