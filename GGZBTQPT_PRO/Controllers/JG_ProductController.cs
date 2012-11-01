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
    public class JG_ProductController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /JG_Product/

        public ViewResult Index()
        {
            return View(db.T_JG_Product.ToList());
        }
        public void BindCustomerType()
        {
            List<T_PTF_DicDetail> CustomerType = db.T_PTF_DicDetail.Where(p => (p.DicType == "JG02" && p.IsValid == "1")).ToList();
            ViewBag.CustomerType = CustomerType;
        }
        public void BindAgency(object select = null)
        {
            List<T_JG_Agency> AgencyList = db.T_JG_Agency.ToList();
            ViewData["AgencyList"] = new SelectList(AgencyList, "ID", "AgencyName", select);
        }
        //
        // GET: /JG_Product/Details/5

        public ViewResult Details(int id)
        {
            T_JG_Product t_jg_product = db.T_JG_Product.Find(id);
            return View(t_jg_product);
        }

        //
        // GET: /JG_Product/Create

        public ActionResult Create()
        {
            BindCustomerType();
            BindAgency();
            return View();
        } 

        //
        // POST: /JG_Product/Create

        [HttpPost]
        public ActionResult Create(T_JG_Product t_jg_product, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                if (collection.GetValue("checkboxType") != null)
                {
                    string strType = collection.GetValue("checkboxType").AttemptedValue;
                    t_jg_product.CustomerType = strType;
                }
                db.T_JG_Product.Add(t_jg_product);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(t_jg_product);
        }
        
        //
        // GET: /JG_Product/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_JG_Product t_jg_product = db.T_JG_Product.Find(id);
            BindAgency(t_jg_product.AgencyID);
            BindCustomerType();
            return View(t_jg_product);
        }

        //
        // POST: /JG_Product/Edit/5

        [HttpPost]
        public ActionResult Edit(T_JG_Product t_jg_product, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_jg_product).State = EntityState.Modified;
                t_jg_product.CustomerType = collection["checkboxType"];
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_jg_product);
        }

        //
        // GET: /JG_Product/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_JG_Product t_jg_product = db.T_JG_Product.Find(id);
            return View(t_jg_product);
        }

        //
        // POST: /JG_Product/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            T_JG_Product t_jg_product = db.T_JG_Product.Find(id);
            db.T_JG_Product.Remove(t_jg_product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}