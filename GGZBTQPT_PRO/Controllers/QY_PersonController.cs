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
    public class QY_PersonController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /QY_Person/

        public ViewResult Index()
        {
            return View(db.T_QY_Person.ToList());
        }

        //
        // GET: /QY_Person/Details/5

        public ViewResult Details(int id)
        {
            T_QY_Person t_qy_person = db.T_QY_Person.Find(id);
            return View(t_qy_person);
        }

        public void BindCardType(object select = null)
        {
            List<T_PTF_DicDetail> CardType = db.T_PTF_DicDetail.Where(p => (p.DicType == "QY02")).ToList();

            ViewData["CardType"] = new SelectList(CardType, "ID", "Name", select);
        }
        public void BindEducation(object select = null)
        {
            List<T_PTF_DicDetail> Education = db.T_PTF_DicDetail.Where(p => (p.DicType == "22")).ToList();

            ViewData["Education"] = new SelectList(Education, "ID", "Name", select);
        }
        public void BindDegree(object select = null)
        {
            List<T_PTF_DicDetail> Degree = db.T_PTF_DicDetail.Where(p => (p.DicType == "23")).ToList();

            ViewData["Degree"] = new SelectList(Degree, "ID", "Name", select);
        }
        public void BindTitles(object select = null)
        {
            List<T_PTF_DicDetail> Titles = db.T_PTF_DicDetail.Where(p => (p.DicType == "25")).ToList();

            ViewData["Titles"] = new SelectList(Titles, "ID", "Name", select);
        }
        public void BindTitlesGrade(object select = null)
        {
            List<T_PTF_DicDetail> TitlesGrade = db.T_PTF_DicDetail.Where(p => (p.DicType == "29")).ToList();

            ViewData["TitlesGrade"] = new SelectList(TitlesGrade, "ID", "Name", select);
        }

        
        //
        // GET: /QY_Person/Create

        public ActionResult Create()
        {
            BindCardType(); 
            BindEducation(); 
            BindDegree(); 
            BindTitles(); 
            BindTitlesGrade();
            return View();
        } 

        //
        // POST: /QY_Person/Create

        [HttpPost]
        public ActionResult Create(T_QY_Person t_qy_person)
        {
            if (ModelState.IsValid)
            {
                db.T_QY_Person.Add(t_qy_person);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(t_qy_person);
        }
        
        //
        // GET: /QY_Person/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_QY_Person t_qy_person = db.T_QY_Person.Find(id);
            BindCardType(t_qy_person.CardType);
            BindEducation(t_qy_person.Education);
            BindDegree(t_qy_person.Degree);
            BindTitles(t_qy_person.Titles);
            BindTitlesGrade(t_qy_person.TitlesGrade);
            return View(t_qy_person);
        }

        //
        // POST: /QY_Person/Edit/5

        [HttpPost]
        public ActionResult Edit(T_QY_Person t_qy_person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_qy_person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_qy_person);
        }

        //
        // GET: /QY_Person/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_QY_Person t_qy_person = db.T_QY_Person.Find(id);
            return View(t_qy_person);
        }

        //
        // POST: /QY_Person/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            T_QY_Person t_qy_person = db.T_QY_Person.Find(id);
            db.T_QY_Person.Remove(t_qy_person);
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