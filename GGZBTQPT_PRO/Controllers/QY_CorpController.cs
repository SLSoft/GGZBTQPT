using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using System.IO;
using GGZBTQPT_PRO.ViewModels;

namespace GGZBTQPT_PRO.Controllers
{
    public class QY_CorpController : BaseController
    { 
        //
        // GET: /QY_Corp/

        public ViewResult Index(string keywords, int pageNum = 1, int numPerPage = 5)
        {
            keywords = keywords == null ? "" : keywords;
            var t_qy_corp = db.T_QY_Corp.Where(c => (c.IsValid == true && c.CorpName.Contains(keywords))).OrderByDescending(p => p.CreateTime)
                                                                    .Skip(numPerPage * (pageNum - 1))
                                                                    .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_QY_Corp.Where(c => (c.IsValid == true && c.CorpName.Contains(keywords))).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            return View(t_qy_corp);
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
            List<T_PTF_DicDetail> Property = db.T_PTF_DicDetail.Where(p => (p.DicType == "5" && p.IsValid=="1")).ToList();

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
            var t_qy_corp = new T_QY_Corp();
            return View(t_qy_corp);
        } 

        //
        // POST: /QY_Corp/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(T_QY_Corp t_qy_corp, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                t_qy_corp.MemberID = Convert.ToInt32(Session["MemberID"] == null ? 0 : Session["MemberID"]);
                t_qy_corp.IsValid = true;
                t_qy_corp.OP = 0;
                t_qy_corp.CreateTime = DateTime.Now;
                t_qy_corp.UpdateTime = DateTime.Now;


                HttpPostedFileBase file = Request.Files[0];
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
                db.T_QY_Corp.Add(t_qy_corp);
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName", t_qy_corp.MemberID);
            return Json(new { });
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
        [ValidateInput(false)]
        public ActionResult Edit(T_QY_Corp t_qy_corp, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_qy_corp).State = EntityState.Modified;
                t_qy_corp.UpdateTime = DateTime.Now;

                HttpPostedFileBase file = Request.Files[0];
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
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName", t_qy_corp.MemberID);
            return Json(new { });
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
            if (Request.IsAjaxRequest())
            {
                T_QY_Corp t_qy_corp = db.T_QY_Corp.Find(id);
                t_qy_corp.IsValid = false;
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", false, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
        }

        //helper

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
            return File(db.T_QY_Corp.Find(corp_id).Logo, "image/jpeg");
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //企业查询功能
        public ActionResult CorpQuery(FormCollection collection, int pageNum = 1, int numPerPage = 5)
        {
            BindProperty();
            BindIndustry();
            string corpname = collection["corpname"] == null ? "" : collection["corpname"];
            string property = collection["cbProperty"] == null ? "" : collection["cbProperty"];
            decimal regcapital = collection["regcapital"] == null || collection["regcapital"] == "" ? 0 : Convert.ToDecimal(collection["regcapital"]);
            string industry = collection["cbIndustry"] == null ? "" : collection["cbIndustry"];

            var t_qy_corp = db.T_QY_Corp.Where(c => c.IsValid == true && c.CorpName.Contains(corpname));
            if (property != "")
            {
                int[] temp1 = new int[property.Split(',').Length];
                for (int i = 0; i < property.Split(',').Length; i++)
                {
                    temp1[i] = Convert.ToInt32(property.Split(',')[i]);
                }
                t_qy_corp = t_qy_corp.Where(c => temp1.Contains((int)c.Property));
            }
            if (regcapital != 0)
            {
                if (collection["regkey"] == "1")
                    t_qy_corp = t_qy_corp.Where(c => c.RegCapital > regcapital);
                else
                    t_qy_corp = t_qy_corp.Where(c => c.RegCapital < regcapital);
            }
            if (industry != "")
            {
                int[] temp = new int[industry.Split(',').Length];
                for (int i = 0; i < industry.Split(',').Length; i++)
                {
                    temp[i] = Convert.ToInt32(industry.Split(',')[i]);
                }
                t_qy_corp = t_qy_corp.Where(c => temp.Contains((int)c.Industry));
            }
            IList<GGZBTQPT_PRO.Models.T_QY_Corp> list = t_qy_corp.OrderByDescending(s => s.CreateTime)
                                                        .Skip(numPerPage * (pageNum - 1))
                                                        .Take(numPerPage).ToList();
            ViewBag.recordCount = t_qy_corp.Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;

            return PartialView(list);
        }

        //企业统计
        public ActionResult CorpReport()
        {
            var qy = db.T_QY_Corp.Where(p => p.IsValid == true).ToList();
            ViewBag.CorpCount = qy.Count();
            ViewBag.YearSum = qy.Where(p => p.CreateTime.Value.Year == DateTime.Now.Year).Count();
            ViewBag.MountSum = qy.Where(p => (p.CreateTime.Value.Year == DateTime.Now.Year && p.CreateTime.Value.Month == DateTime.Now.Month)).Count();
            ViewBag.DaySum = qy.Where(p => (p.CreateTime.Value.Year == DateTime.Now.Year && p.CreateTime.Value.Month == DateTime.Now.Month && p.CreateTime.Value.Day == DateTime.Now.Day)).Count();

            var up = db.T_QY_Corp.Where(p => p.IsValid == true).GroupBy(g => g.Industry)
                                    .Select(s => new { cnt = s.Count(), type = (int)s.Key });


            var list = from u in db.T_PTF_DicDetail
                       where u.DicType == "XM01"
                       join p in up on u.ID equals p.type into gj
                       from x in gj.DefaultIfEmpty()
                       orderby u.ID
                       select new VM_CPReport
                       {
                           TypeName = u.Name,
                           Count = x.type == null ? 0 : x.cnt
                       };
            return PartialView(list.ToList());
        }

        public ActionResult CorpReportData()
        {
            var up = db.T_QY_Corp.Where(p => p.IsValid == true).GroupBy(g => g.Industry)
                                    .Select(s => new { cnt = s.Count(), type = (int)s.Key });


            var list = from u in db.T_PTF_DicDetail
                       where u.DicType == "XM01"
                       join p in up on u.ID equals p.type into gj
                       from x in gj.DefaultIfEmpty()
                       orderby u.ID
                       select new VM_CPReport
                       {
                           TypeName = u.Name,
                           Count = x.type == null ? 0 : x.cnt
                       };

            Dictionary<String, int> dic = new Dictionary<string, int>();
            foreach (VM_CPReport vmx in list.ToList())
            {
                dic.Add(vmx.TypeName, vmx.Count);
            }

            return Json(new { statData = dic }, JsonRequestBehavior.AllowGet);
        }
    }
}