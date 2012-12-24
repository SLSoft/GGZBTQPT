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
    public class JG_ProductController : BaseController
    {
        //private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /JG_Product/

        public ViewResult Index(int pageNum = 1, int numPerPage = 5)
        {
            var t_jg_product = db.T_JG_Product.Where(p => p.IsValid == true).OrderBy(s => s.ID)
                                                                    .Skip(numPerPage * (pageNum - 1))
                                                                    .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_JG_Product.Where(c => c.IsValid == true).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            return View(t_jg_product);
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
            var t_jg_product = new T_JG_Product();
            return View(t_jg_product);
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
                t_jg_product.AgencyID = Convert.ToInt32(collection["AgencyList"]);
                t_jg_product.IsValid = true;
                t_jg_product.OP = 0;
                t_jg_product.CreateTime = DateTime.Now;
                t_jg_product.UpdateTime = DateTime.Now;

                HttpPostedFileBase file = Request.Files[0];
                //存入文件
                if (file.ContentLength > 0)
                {
                    t_jg_product.Pic = new byte[Request.Files[0].InputStream.Length];
                    Request.Files[0].InputStream.Read(t_jg_product.Pic, 0, t_jg_product.Pic.Length);
                }

                db.T_JG_Product.Add(t_jg_product);
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
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
                t_jg_product.AgencyID = Convert.ToInt32(collection["AgencyList"]);
                t_jg_product.CustomerType = collection["checkboxType"];
                t_jg_product.UpdateTime = DateTime.Now;

                HttpPostedFileBase file = Request.Files[0];
                //存入文件
                if (file.ContentLength > 0)
                {
                    t_jg_product.Pic = new byte[Request.Files[0].InputStream.Length];
                    Request.Files[0].InputStream.Read(t_jg_product.Pic, 0, t_jg_product.Pic.Length);
                }

                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
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
            if (Request.IsAjaxRequest())
            {
                T_JG_Product t_jg_product = db.T_JG_Product.Find(id);
                t_jg_product.IsValid = false;
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
        }

        //helper
        public FileContentResult ShowPic(int product_id)
        {
            return File(db.T_JG_Product.Find(product_id).Pic, "image/jpeg");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //待审核产品一览
        public ActionResult CPUnCheckList(string keywords, int pageNum = 1, int numPerPage = 10)
        {
            keywords = keywords == null ? "" : keywords;
            IList<GGZBTQPT_PRO.Models.T_JG_Product> list = db.T_JG_Product.Where(p => (p.IsValid == true && p.PublicStatus == "1" && p.ProductName.Contains(keywords))).ToList()
                                                            .OrderByDescending(s => s.SubmitTime)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_JG_Product.Where(p => (p.IsValid == true && p.PublicStatus == "1")).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;

            return View(list);
        }

        //已审核产品一览
        public ActionResult CPCheckList(string keywords, int pageNum = 1, int numPerPage = 10)
        {
            keywords = keywords == null ? "" : keywords;
            IList<GGZBTQPT_PRO.Models.T_JG_Product> list = db.T_JG_Product.Where(p => (p.IsValid == true && p.PublicStatus == "2" && p.ProductName.Contains(keywords))).ToList()
                                                            .OrderByDescending(s => s.SubmitTime)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_JG_Product.Where(p => (p.IsValid == true && p.PublicStatus == "2")).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;

            return View(list);
        }

        /// <summary>
        /// 产品审核、撤销审核
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CPCheck(int id, string state)
        {
            T_JG_Product t_jg_product = db.T_JG_Product.Find(id);
            t_jg_product.PublicStatus = state;
            t_jg_product.PublicTime = DateTime.Now;
            t_jg_product.UpdateTime = DateTime.Now;

            if (db.SaveChanges() > 0)
                if (state == "2")
                    return ReturnJson(true, "审核成功", "", "RZCheckList", false, "");
                else
                    return ReturnJson(true, "撤销审核成功", "", "RZCheckList", false, "");
            else
                if (state == "2")
                    return ReturnJson(false, "审核失败", "", "", false, "");
                else
                    return ReturnJson(true, "撤销审核失败", "", "RZCheckList", false, "");
        }
    }
}