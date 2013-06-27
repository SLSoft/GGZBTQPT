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
    public class CorpInvInfoController : BaseController
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
        public void BindIndustry(object select = null)
        {
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();

            ViewData["AttIndustry"] = new SelectList(Industry, "Name", "Name", select);
        }
        //
        // GET: /CorpInvInfo/

        public ViewResult Index(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;
            var rzxq = db.T_QY_TZXQ.Where(r => r.IsValid == true && r.AgencyName.Contains(keywords)).OrderByDescending(p => p.ID)
                                                                    .Skip(numPerPage * (pageNum - 1))
                                                                    .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_QY_TZXQ.Where(r => r.AgencyName.Contains(keywords)).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            return View(rzxq);
        }

        //
        // GET: /CorpInvInfo/Details/5

        public ViewResult Details(int id)
        {
            T_QY_TZXQ t_qy_tzxq = db.T_QY_TZXQ.Find(id);
            return View(t_qy_tzxq);
        }

        //
        // GET: /CorpInvInfo/Create

        public ActionResult Create()
        {
            BindIndustry();
            var t_qy_tzxq = new T_QY_TZXQ();
            return View(t_qy_tzxq);
        } 

        //
        // POST: /CorpInvInfo/Create

        [HttpPost]
        public ActionResult Create(T_QY_TZXQ t_qy_tzxq, FormCollection collection)
        {
            BindIndustry();
            if (ModelState.IsValid)
            {
                t_qy_tzxq.AgencyNature = collection["AgencyNature"];
                t_qy_tzxq.AttIndustry = collection["AttIndustry"];
                t_qy_tzxq.AttCorpType = collection["AttCorpType"];
                db.T_QY_TZXQ.Add(t_qy_tzxq);
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
        }
        
        //
        // GET: /CorpInvInfo/Edit/5
 
        public ActionResult Edit(int id)
        {
            BindIndustry();
            T_QY_TZXQ t_qy_tzxq = db.T_QY_TZXQ.Find(id);
            return View(t_qy_tzxq);
        }

        //
        // POST: /CorpInvInfo/Edit/5

        [HttpPost]
        public ActionResult Edit(T_QY_TZXQ t_qy_tzxq, FormCollection collection)
        {
            BindIndustry();
            if (ModelState.IsValid)
            {
                t_qy_tzxq.AgencyNature = collection["cbAgencyNature"];
                t_qy_tzxq.AttIndustry = collection["cbAttIndustry"];
                t_qy_tzxq.AttCorpType = collection["cbAttCorpType"];
                db.Entry(t_qy_tzxq).State = EntityState.Modified;
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
        }

        //
        // GET: /CorpInvInfo/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_QY_TZXQ t_qy_tzxq = db.T_QY_TZXQ.Find(id);
            return PartialView(t_qy_tzxq);
        }

        //
        // POST: /CorpInvInfo/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.IsAjaxRequest())
            {
                T_QY_TZXQ t_qy_tzxq = db.T_QY_TZXQ.Find(id);
                t_qy_tzxq.IsValid = false;
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