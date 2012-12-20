using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using System.IO;

namespace GGZBTQPT_PRO.Areas.MG.Controllers
{ 
    public class CorpController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();



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
        // GET: /QY_Corp/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_QY_Corp t_qy_corp = db.T_QY_Corp.Find(id);
            
            BindStage(t_qy_corp.Stage);
            BindArea(t_qy_corp.Province);
            BindIndustry(t_qy_corp.Industry);
            BindProperty(t_qy_corp.Property);

            return PartialView(t_qy_corp);
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
                if (collection["RegTime"].Trim() == "")
                    t_qy_corp.RegTime = DateTime.MaxValue;
                string cyear = collection["FYear"].ToString();
                if (db.T_QY_Financial.Where(f => (f.CorpID == t_qy_corp.ID && f.CurYear == cyear)).Count() > 0)
                {
                    decimal assets = 0;
                    decimal revenue = 0;
                    if (collection["TotalAssets"] != "")
                        assets = Convert.ToDecimal(collection["TotalAssets"]);
                    if (collection["Revenue"] != "")
                        revenue = Convert.ToDecimal(collection["Revenue"]);

                    db.T_QY_Financial.Where(f => f.CurYear == cyear).FirstOrDefault().TotalAssets = assets;
                    db.T_QY_Financial.Where(f => f.CurYear == cyear).FirstOrDefault().Revenue =revenue;
                }
                else
                {
                    T_QY_Financial financial = new T_QY_Financial();
                    financial.CorpID = t_qy_corp.ID;
                    financial.CurYear = cyear;
                    if (collection["TotalAssets"] != "")
                        financial.TotalAssets = Convert.ToDecimal(collection["TotalAssets"]);
                    else
                        financial.TotalAssets = 0;
                    if (collection["Revenue"] != "")
                        financial.Revenue = Convert.ToDecimal(collection["Revenue"]);
                    else
                        financial.Revenue = 0;
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
                return Json(new { statusCode = "200", message = "信息保存成功！", type = "success" });
            }
            return Json(new { statusCode = "200", message = "信息保存失败！", type = "error" });
        }

        

        public ActionResult UploadLogo(string qqfile, int corp_id)
        { 
            var corp = db.T_QY_Corp.Find(corp_id);
            db.Entry(corp).State = EntityState.Modified;

            try
            { 
                Stream stream = Request.Files.Count > 0
                    ? Request.Files[0].InputStream
                    : Request.InputStream;
                //存入文件
                if (stream.Length > 0)
                {
                    corp.Logo = new byte[stream.Length];
                    stream.Read(corp.Logo, 0, corp.Logo.Length);
                } 
                db.SaveChanges();
                return Json(new { success = "true", message = "上传成功", logo = Convert.ToBase64String(corp.Logo) }, "text/x-json");
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, "text/x-json");
            }
        }

        public FileContentResult ShowLogo(int corp_id)
        {
            byte[] pic;
            if (db.T_QY_Corp.Find(corp_id).Logo != null)
                pic = db.T_QY_Corp.Find(corp_id).Logo;
            else
                pic = new byte[1];
            return File(pic, "image/jpeg");
        }
   

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}