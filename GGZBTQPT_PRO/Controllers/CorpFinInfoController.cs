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
    public class CorpFinInfoController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();
        //"statusCode":"返回的状态值，200--success 300--fail 301--timeout",
        //"message":"提示信息",
        //"navTabId":"定navTab页面标记为需要“重新载入”。注意navTabId不能是当前navTab页面的",
        //"rel":"指定ID",该ID用于指定回调后,需要局部刷新的页面元素ID
        //"callbackType":"closeCurrent或forward", 关闭当前页面/转发到其他页面
        //"forwardUrl":"跳转的URL，callbackType是forward时使用"
        //"confirmMsg":"需要确定的信息"
        public JsonResult ReturnJson(bool _IfSuccess, string _message, string _navTabId, string _rel, bool _IfColse, string _forwardUrl)//批量删除会自动刷新所在的navTab。 
        {
            string _statusCode = _IfSuccess ? "200" : "300";
            string _callbackType = _IfColse ? "closeCurrent" : null;
            return Json(new { statusCode = _statusCode, message = _message, navTabId = _navTabId, rel = _rel, callbackType = _callbackType, forwardUrl = _forwardUrl }, "text/html", JsonRequestBehavior.AllowGet);
        }
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
        //
        // GET: /CorpFinInfo/

        public ViewResult Index(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;
            var rzxq = db.T_QY_RZXQ.Where(r=>r.IsValid ==true && r.CorpName.Contains(keywords)).OrderByDescending(p => p.ID)
                                                                    .Skip(numPerPage * (pageNum - 1))
                                                                    .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_QY_RZXQ.Where(r => r.CorpName.Contains(keywords)).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            return View(rzxq);
        }

        //
        // GET: /CorpFinInfo/Details/5

        public ViewResult Details(int id)
        {
            T_QY_RZXQ t_qy_rzxq = db.T_QY_RZXQ.Find(id);
            return View(t_qy_rzxq);
        }

        //
        // GET: /CorpFinInfo/Create

        public ActionResult Create()
        {
            BindPark();
            var t_qy_rzxq = new T_QY_RZXQ();
            return View(t_qy_rzxq);
        } 

        //
        // POST: /CorpFinInfo/Create

        [HttpPost]
        public ActionResult Create(T_QY_RZXQ t_qy_rzxq, FormCollection collection)
        {
            BindPark();
            if (ModelState.IsValid)
            {
                //t_qy_rzxq.Park = collection["ddlPark"];
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
        
        //
        // GET: /CorpFinInfo/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_QY_RZXQ t_qy_rzxq = db.T_QY_RZXQ.Find(id);
            BindPark(t_qy_rzxq.Park);
            BindIndustry(t_qy_rzxq.Industry);
            BindProperty(t_qy_rzxq.Property);
            ViewData["property"] = t_qy_rzxq.Property;
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
        // POST: /CorpFinInfo/Edit/5

        [HttpPost]
        public ActionResult Edit(T_QY_RZXQ t_qy_rzxq, FormCollection collection)
        {
            BindPark(t_qy_rzxq.Park);
            if (ModelState.IsValid)
            {
                //t_qy_rzxq.Park = collection["ddlPark"];
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
                Response.Write("<script>alert('您的信息已提交成功!');</script>");
            }
            return View(t_qy_rzxq);
        }

        //
        // GET: /CorpFinInfo/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_QY_RZXQ t_qy_rzxq = db.T_QY_RZXQ.Find(id);
            return View(t_qy_rzxq);
        }

        //
        // POST: /CorpFinInfo/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            if (Request.IsAjaxRequest())
            {
                T_QY_RZXQ t_qy_rzxq = db.T_QY_RZXQ.Find(id);
                t_qy_rzxq.IsValid = false;
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", false, "");
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
    }
}