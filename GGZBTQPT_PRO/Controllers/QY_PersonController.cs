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
    public class QY_PersonController : BaseController
    {
        //private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /QY_Person/

        public ViewResult Index(string keywords, int pageNum = 1, int numPerPage = 5)
        {
            keywords = keywords == null ? "" : keywords;
            var t_qy_person = db.T_QY_Person.Where(p => (p.IsValid == true && p.Name.Contains(keywords))).OrderByDescending(p => p.CreateTime)
                                                                    .Skip(numPerPage * (pageNum - 1))
                                                                    .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_QY_Person.Where(c => c.IsValid == true).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            return View(t_qy_person);
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
                t_qy_person.MemberID = Convert.ToInt32(Session["MemberID"] == null ? 0 : Session["MemberID"]);
                t_qy_person.IsValid = true;
                t_qy_person.OP = 0;
                t_qy_person.CreateTime = DateTime.Now;
                t_qy_person.UpdateTime = DateTime.Now;
                db.T_QY_Person.Add(t_qy_person);
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }

            return Json(new { });
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
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
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
            if (Request.IsAjaxRequest())
            {
                T_QY_Person t_qy_person = db.T_QY_Person.Find(id);
                t_qy_person.IsValid = false;
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //创业者查询功能
        public ActionResult PersonQuery(FormCollection collection, int pageNum = 1, int numPerPage = 5)
        {
            BindEducation();
            string personname = collection["personname"] == null ? "" : collection["personname"];
            string cardid = collection["cardid"] == null ? "" : collection["cardid"];
            string gender = collection["gender"] == null ? "" : collection["gender"];
            int edu = collection["Education"] == null || collection["Education"] == "" ? 0 : Convert.ToInt32(collection["Education"]);

            var t_qy_person = db.T_QY_Person.Where(p => (p.IsValid == true && p.Name.Contains(personname) && p.CardID.Contains(cardid)));

            if (gender != "")
            {
                t_qy_person = t_qy_person.Where(p => p.Gender == gender);
            }
            if (edu != 0)
            {
                t_qy_person = t_qy_person.Where(p => p.Education == edu);
            }

            IList<GGZBTQPT_PRO.Models.T_QY_Person> list = t_qy_person.OrderByDescending(s => s.CreateTime)
                                                        .Skip(numPerPage * (pageNum - 1))
                                                        .Take(numPerPage).ToList();
            ViewBag.recordCount = t_qy_person.Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;

            return PartialView(list);
        }

        //创业者统计
        public ActionResult personReport()
        {
            var ps = db.T_QY_Person.Where(p => p.IsValid == true).ToList();
            ViewBag.CorpCount = ps.Count();
            ViewBag.YearSum = ps.Where(p => p.CreateTime.Value.Year == DateTime.Now.Year).Count();
            ViewBag.MountSum = ps.Where(p => (p.CreateTime.Value.Year == DateTime.Now.Year && p.CreateTime.Value.Month == DateTime.Now.Month)).Count();
            ViewBag.DaySum = ps.Where(p => (p.CreateTime.Value.Year == DateTime.Now.Year && p.CreateTime.Value.Month == DateTime.Now.Month && p.CreateTime.Value.Day == DateTime.Now.Day)).Count();

            return PartialView(ps);
        }
    }
}