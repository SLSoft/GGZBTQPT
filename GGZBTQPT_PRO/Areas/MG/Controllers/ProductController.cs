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
    public class ProductController : BaseController
    {
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

        public ViewResult Details(int id)
        {
            T_JG_Product t_jg_product = db.T_JG_Product.Find(id);
            return View(t_jg_product);
        }

        //
        // GET: /JG_Product/Create

        public ActionResult Create(string notice_type)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Login", "Member");
            }
            if (notice_type == "success")
            {
                ViewData["notice"] = "投资项目发布成功，可进入我的发布中查阅！";
            }

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
                t_jg_product.Member = CurrentMember();
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
                    return RedirectToAction("Create", new { notice_type = "success" });
                else
                {

                    ViewData["error"] = "金融产品发布失败!请检查输入信息或联系我们!";
                    return View(t_jg_product);
                }
            }
            ViewData["error"] = "金融产品发布失败!请检查输入信息或联系我们!";
            return View(t_jg_product);
        }

        //
        // GET: /JG_Product/Edit/5

        public ActionResult Edit(int id, string notice_type)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Login", "Member");
            }
            if (notice_type == "success")
            {
                ViewData["notice"] = "金融产品更新成功，可进入我的发布中查阅！";
            }
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
                    return RedirectToAction("Edit", new { notice_type = "success" });
                else
                {
                    ViewData["error"] = "金融产品更新失败!请检查输入信息或联系我们!";
                    return View(t_jg_product);
                }
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
    }
}
