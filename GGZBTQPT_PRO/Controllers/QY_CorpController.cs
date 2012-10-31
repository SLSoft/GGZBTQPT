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
    public class QY_CorpController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /QY_Corp/

        public ViewResult Index()
        {
            return View(db.T_QY_Corp.ToList());
        }

        //
        // GET: /QY_Corp/Details/5

        public ViewResult Details(int id)
        {
            T_QY_Corp t_qy_corp = db.T_QY_Corp.Find(id);
            return View(t_qy_corp);
        }


        public void BindProperty(object select = null)
        {
            List<T_PTF_DicDetail> Property = db.T_PTF_DicDetail.Where(p => (p.DicType == "5")).ToList();

            ViewData["Property"] = new SelectList(Property, "ID", "Name", select);
        }
        public void BindIndustry(object select = null)
        {
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();

            ViewData["Industry"] = new SelectList(Industry, "ID", "Name", select);
        }

        public void BindArea(object select = null)
        {
            List<T_PTF_DicTreeDetail> Area = db.T_PTF_DicTreeDetail.Where(p => (p.DicType == "34" && p.Depth == 1)).ToList();

            ViewData["Province"] = new SelectList(Area, "ID", "Name", select);
        }
        public void BindStage(object select = null)
        {
            List<T_PTF_DicDetail> Stage = db.T_PTF_DicDetail.Where(p => (p.DicType == "QY01")).ToList();

            ViewData["Stage"] = new SelectList(Stage, "ID", "Name", select);
        }
        //
        // GET: /QY_Corp/Create

        public ActionResult Create()
        {
            BindStage();
            BindArea();
            BindIndustry();
            BindProperty();
            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName");
            return View();
        } 

        //
        // POST: /QY_Corp/Create

        [HttpPost]
        public ActionResult Create(T_QY_Corp t_qy_corp)
        {
            if (ModelState.IsValid)
            {
                t_qy_corp.IsValid = true;
                t_qy_corp.OP = 0;
                t_qy_corp.CreateTime = DateTime.Now;
                t_qy_corp.UpdateTime = DateTime.Now;
                HttpPostedFileBase file = Request.Files["file1"];
                //存入文件
                if (file.ContentLength > 0)
                {
                    t_qy_corp.Logo = System.IO.File.ReadAllBytes(file.FileName);
                    //file.SaveAs(Server.MapPath("~/") + System.IO.Path.GetFileName(file.FileName));
                }
                db.T_QY_Corp.Add(t_qy_corp);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName", t_qy_corp.MemberID);
            return View(t_qy_corp);
        }
        
        //
        // GET: /QY_Corp/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_QY_Corp t_qy_corp = db.T_QY_Corp.Find(id);

            BindStage(t_qy_corp.Stage);
            BindArea(t_qy_corp.Province);
            BindIndustry(t_qy_corp.Industry);
            BindProperty(t_qy_corp.Property);
            
            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName", t_qy_corp.MemberID);
            return View(t_qy_corp);
        }

        //
        // POST: /QY_Corp/Edit/5

        [HttpPost]
        public ActionResult Edit(T_QY_Corp t_qy_corp, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_qy_corp).State = EntityState.Modified;
                t_qy_corp.UpdateTime = DateTime.Now;
                HttpPostedFileBase file = Request.Files["file1"];
                //存入文件
                if (file.ContentLength > 0)
                {
                    t_qy_corp.Logo = new byte[Request.Files[0].InputStream.Length];
                    Request.Files[0].InputStream.Read(t_qy_corp.Logo, 0, t_qy_corp.Logo.Length);
                }
                string cyear = collection["FYear"].ToString();
                if (db.T_QY_Financial.Where(f => (f.CorpID == t_qy_corp.ID && f.CurYear == cyear)).Count() > 0)
                {
                    db.T_QY_Financial.Where(f => f.CurYear == cyear).FirstOrDefault().TotalAssets = Convert.ToDecimal(collection["TotalAssets"]);
                    db.T_QY_Financial.Where(f => f.CurYear == cyear).FirstOrDefault().Revenue = Convert.ToDecimal(collection["Revenue"]);
                }
                else
                {
                    T_QY_Financial financial = new T_QY_Financial();
                    financial.CorpID = t_qy_corp.ID;
                    financial.CurYear = cyear;
                    financial.TotalAssets = Convert.ToDecimal(collection["TotalAssets"]);
                    financial.Revenue = Convert.ToDecimal(collection["Revenue"]); ;
                    db.T_QY_Financial.Add(financial);
                }
                if (db.T_QY_Product.Where(f => f.CorpID == t_qy_corp.ID).Count() > 0)
                {
                    db.T_QY_Product.Where(f => f.CorpID == t_qy_corp.ID).FirstOrDefault().ProductName = collection["ProductName"];
                    db.T_QY_Product.Where(f => f.CorpID == t_qy_corp.ID).FirstOrDefault().Content = collection["Content"];
                }
                else
                {
                    T_QY_Product Product = new T_QY_Product();
                    Product.CorpID = t_qy_corp.ID;
                    Product.ProductName = collection["ProductName"];
                    Product.Content = collection["Content"];
                    db.T_QY_Product.Add(Product);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName", t_qy_corp.MemberID);
            return View(t_qy_corp);
        }

        //
        // GET: /QY_Corp/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_QY_Corp t_qy_corp = db.T_QY_Corp.Find(id);
            return View(t_qy_corp);
        }

        //
        // POST: /QY_Corp/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            T_QY_Corp t_qy_corp = db.T_QY_Corp.Find(id);
            db.T_QY_Corp.Remove(t_qy_corp);
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