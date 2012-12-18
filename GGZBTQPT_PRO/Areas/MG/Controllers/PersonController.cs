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
    public class PersonController : BaseController
    {


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
        // GET: /QY_Person/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_QY_Person t_qy_person = db.T_QY_Person.Find(id);
            BindCardType(t_qy_person.CardType);
            BindEducation(t_qy_person.Education);
            BindDegree(t_qy_person.Degree);
            BindTitles(t_qy_person.Titles);
            BindTitlesGrade(t_qy_person.TitlesGrade);
            return PartialView(t_qy_person);
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
                return Json(new { statusCode = "200", message = "信息保存成功！", type = "success" });

            }
            BindCardType(t_qy_person.CardType);
            BindEducation(t_qy_person.Education);
            BindDegree(t_qy_person.Degree);
            BindTitles(t_qy_person.Titles);
            BindTitlesGrade(t_qy_person.TitlesGrade);
            return Json(new { statusCode = "200", message = "信息保存失败！", type = "error" });
        } 

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}