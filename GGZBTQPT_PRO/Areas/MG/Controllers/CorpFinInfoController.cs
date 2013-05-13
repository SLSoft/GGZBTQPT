using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;

namespace GGZBTQPT_PRO.Areas.MG.Controllers
{ 
    public class CorpFinInfoController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /MG/CorpFinInfo/
        public void BindPark(object select = null)
        {
            List<T_PTF_DicDetail> parklist = db.T_PTF_DicDetail.Where(p => (p.DicType == "QY03")).ToList();

            ViewData["Park"] = new SelectList(parklist, "Name", "Name", select);
        }
        public void BindProperty(object select = null)
        {
            List<T_PTF_DicDetail> Property = db.T_PTF_DicDetail.Where(p => (p.DicType == "5" && p.IsValid == "1")).ToList();

            ViewData["Property"] = new SelectList(Property, "Name", "Name", select);
        }
        public void BindIndustry(object select = null)
        {
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();

            ViewData["Industry"] = new SelectList(Industry, "ID", "Name", select);
        }
        public ViewResult Index()
        {
            return View(db.T_QY_RZXQ.ToList());
        }

        //
        // GET: /MG/CorpFinInfo/Details/5

        public ViewResult Details(int id)
        {
            T_QY_RZXQ t_qy_rzxq = db.T_QY_RZXQ.Find(id);
            return View(t_qy_rzxq);
        }

        //
        // GET: /MG/CorpFinInfo/Create

        public ActionResult Create()
        {
            BindPark();
            BindIndustry();
            BindProperty();
            return View();
        } 

        //
        // POST: /MG/CorpFinInfo/Create

        [HttpPost]
        public ActionResult Create(T_QY_RZXQ t_qy_rzxq, FormCollection collection)
        {
            BindPark();
            BindIndustry();
            BindProperty();
            if (ModelState.IsValid)
            {
                if (t_qy_rzxq.Industry == "其他")
                    t_qy_rzxq.Industry =collection["txtIndustry"];
                t_qy_rzxq.Guarantee1 = collection["Guarantee1"] == null ? "" : "第三方保证";
                t_qy_rzxq.Guarantee2 = collection["Guarantee2"] == null ? "" : "抵押";
                t_qy_rzxq.Guarantee3 = collection["Guarantee3"] == null ? "" : "质押";
                t_qy_rzxq.Guarantee4 = collection["Guarantee4"] == null ? "" : "其他";
                t_qy_rzxq.IsValid = true;
                t_qy_rzxq.CreateTime = DateTime.Now;
                db.T_QY_RZXQ.Add(t_qy_rzxq);
                db.SaveChanges();
                Response.Write("<script>alert('您的信息已提交成功!');</script>");
            }

            return View(t_qy_rzxq);
        }
        
        //
        // GET: /MG/CorpFinInfo/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_QY_RZXQ t_qy_rzxq = db.T_QY_RZXQ.Find(id);
            BindPark(t_qy_rzxq.Park);
            BindIndustry(t_qy_rzxq.Industry);
            BindProperty(t_qy_rzxq.Property);
            ViewData["team_sex1"] = t_qy_rzxq.team_sex1;
            ViewData["team_sex2"] = t_qy_rzxq.team_sex2;
            ViewData["team_sex3"] = t_qy_rzxq.team_sex3;
            ViewData["team_sex4"] = t_qy_rzxq.team_sex4;
            ViewData["team_sex5"] = t_qy_rzxq.team_sex5;
            ViewData["Guarantee1"] = t_qy_rzxq.Guarantee1;
            ViewData["Guarantee2"] = t_qy_rzxq.Guarantee2;
            ViewData["Guarantee3"] = t_qy_rzxq.Guarantee3;
            ViewData["Guarantee4"] = t_qy_rzxq.Guarantee4;
            return View(t_qy_rzxq);
        }

        //
        // POST: /MG/CorpFinInfo/Edit/5

        [HttpPost]
        public ActionResult Edit(T_QY_RZXQ t_qy_rzxq, FormCollection collection)
        {
            BindPark(t_qy_rzxq.Park);
            BindIndustry(t_qy_rzxq.Industry);
            BindProperty(t_qy_rzxq.Property);
            if (ModelState.IsValid)
            {
                if (t_qy_rzxq.Industry == "其他")
                    t_qy_rzxq.Industry = collection["txtIndustry"];
                t_qy_rzxq.Guarantee1 = collection["Guarantee1"] == null ? "" : "第三方保证";
                t_qy_rzxq.Guarantee2 = collection["Guarantee2"] == null ? "" : "抵押";
                t_qy_rzxq.Guarantee3 = collection["Guarantee3"] == null ? "" : "质押";
                t_qy_rzxq.Guarantee4 = collection["Guarantee4"] == null ? "" : "其他";
                t_qy_rzxq.UpdateTime = DateTime.Now;
                db.Entry(t_qy_rzxq).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_qy_rzxq);
        }

        //
        // GET: /MG/CorpFinInfo/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_QY_RZXQ t_qy_rzxq = db.T_QY_RZXQ.Find(id);
            return View(t_qy_rzxq);
        }

        //
        // POST: /MG/CorpFinInfo/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            T_QY_RZXQ t_qy_rzxq = db.T_QY_RZXQ.Find(id);
            db.T_QY_RZXQ.Remove(t_qy_rzxq);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //
        // GET: /MG/CorpFinInfo/Create

        public ActionResult SimpleCreate()
        {
            BindArea();
            BindIndustry();
            BindProperty();
            return View();
        }

        //
        // POST: /MG/CorpFinInfo/Create

        [HttpPost]
        public ActionResult SimpleCreate(T_QY_RZXQ t_qy_rzxq, FormCollection collection)
        {
            BindArea();
            BindIndustry();
            BindProperty();
            if (ModelState.IsValid)
            {
                t_qy_rzxq.Property = collection["ddlProperty"];
                if (t_qy_rzxq.Industry == "其他")
                    t_qy_rzxq.Industry = collection["txtIndustry"];
                t_qy_rzxq.Guarantee1 = collection["Guarantee1"] == null ? "" : "第三方保证";
                t_qy_rzxq.Guarantee2 = collection["Guarantee2"] == null ? "" : "抵押";
                t_qy_rzxq.Guarantee3 = collection["Guarantee3"] == null ? "" : "质押";
                t_qy_rzxq.Guarantee4 = collection["Guarantee4"] == null ? "" : "其他";
                t_qy_rzxq.IsValid = true;
                t_qy_rzxq.CreateTime = DateTime.Now;
                db.T_QY_RZXQ.Add(t_qy_rzxq);
                db.SaveChanges();
                Response.Write("<script>alert('您的信息已提交成功!');</script>");
            }

            return View(t_qy_rzxq);
        }

        public void BindArea(object select = null)
        {
            List<T_PTF_DicTreeDetail> Area = db.T_PTF_DicTreeDetail.Where(p => (p.DicType == "34" && p.ParentCode == "420000" && p.IsValid == "1")).ToList();

            ViewData["RegArea"] = new SelectList(Area, "Name", "Name", select);
        }

        //
        // GET: /MG/CorpFinInfo/Edit/5

        public ActionResult SimpleEdit(int id)
        {
            T_QY_RZXQ t_qy_rzxq = db.T_QY_RZXQ.Find(id);
            BindArea(t_qy_rzxq.RegArea);
            BindIndustry(t_qy_rzxq.Industry);
            BindProperty(t_qy_rzxq.Property);
            return View(t_qy_rzxq);
        }

        //
        // POST: /MG/CorpFinInfo/Edit/5

        [HttpPost]
        public ActionResult SimpleEdit(T_QY_RZXQ t_qy_rzxq, FormCollection collection)
        {
            BindArea(t_qy_rzxq.RegArea);
            BindIndustry(t_qy_rzxq.Industry);
            BindProperty(t_qy_rzxq.Property);
            if (ModelState.IsValid)
            {
                t_qy_rzxq.Property = collection["ddlProperty"];
                if (t_qy_rzxq.Industry == "其他")
                    t_qy_rzxq.Industry = collection["txtIndustry"];
                t_qy_rzxq.Guarantee1 = collection["Guarantee1"] == null ? "" : "第三方保证";
                t_qy_rzxq.Guarantee2 = collection["Guarantee2"] == null ? "" : "抵押";
                t_qy_rzxq.Guarantee3 = collection["Guarantee3"] == null ? "" : "质押";
                t_qy_rzxq.Guarantee4 = collection["Guarantee4"] == null ? "" : "其他";
                t_qy_rzxq.UpdateTime = DateTime.Now;
                db.Entry(t_qy_rzxq).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_qy_rzxq);
        }
    }
}