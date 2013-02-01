using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.ViewModels;
using System.Drawing;
using System.Threading;
using System.IO;
using GGZBTQPT_PRO.Util;

namespace GGZBTQPT_PRO.Controllers
{
    public class JG_ProductController : Controller
    {
        public GGZBTQPT_PRO.Models.GGZBTQPTDBContext db = new GGZBTQPT_PRO.Models.GGZBTQPTDBContext();

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
        //private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /JG_Product/

        public ViewResult Index(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;
            var t_jg_product = db.T_JG_Product.Where(p => (p.IsValid == true && p.ProductName.Contains(keywords))).OrderByDescending(s => s.CreateTime)
                                                                    .Skip(numPerPage * (pageNum - 1))
                                                                    .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_JG_Product.Where(p => (p.IsValid == true && p.ProductName.Contains(keywords))).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
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
        [ValidateInput(false)]
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
                t_jg_product.Superiority = t_jg_product.Superiority == null ? "" : t_jg_product.Superiority;
                t_jg_product.RepaymentType = t_jg_product.RepaymentType == null ? "" : t_jg_product.RepaymentType;
                t_jg_product.AppCondition = t_jg_product.AppCondition == null ? "" : t_jg_product.AppCondition;
                t_jg_product.Process = t_jg_product.Process == null ? "" : t_jg_product.Process;
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
        [ValidateInput(false)]
        public ActionResult Edit(T_JG_Product t_jg_product, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_jg_product).State = EntityState.Modified;
                t_jg_product.AgencyID = Convert.ToInt32(collection["AgencyList"]);
                t_jg_product.Superiority = t_jg_product.Superiority == null ? "" : t_jg_product.Superiority;
                t_jg_product.RepaymentType = t_jg_product.RepaymentType == null ? "" : t_jg_product.RepaymentType;
                t_jg_product.AppCondition = t_jg_product.AppCondition == null ? "" : t_jg_product.AppCondition;
                t_jg_product.Process = t_jg_product.Process == null ? "" : t_jg_product.Process;
                t_jg_product.CustomerType = collection["checkboxType"] == null ? "" : collection["checkboxType"];
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
                    return ReturnJson(true, "操作成功", "", "", false, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
        }

        //helper
        public FileContentResult ShowPic(int product_id)
        {
            byte[] pic;
            if (db.T_JG_Product.Find(product_id).Pic != null)
                pic = db.T_JG_Product.Find(product_id).Pic;
            else
                pic = new byte[1];
            return File(pic, "image/jpeg");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //待审核产品一览
        public ActionResult CPUnCheckList(string keywords, int pageNum = 1, int numPerPage = 15)
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
        public ActionResult CPCheckList(string keywords, int pageNum = 1, int numPerPage = 15)
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

        //产品查询功能
        public ActionResult ProductQuery(FormCollection collection, string productname, string agencyname, decimal amount = 0, int pageNum = 1, int numPerPage = 15)
        {
            productname = productname == null ? "" : productname;
            agencyname = agencyname == null ? "" : agencyname;
            ViewBag.regkey = collection["regkey"];
            var t_jg_product = db.T_JG_Product.Where(p => p.IsValid == true && p.ProductName.Contains(productname));
            if (agencyname != "")
            {
                IList<GGZBTQPT_PRO.Models.T_JG_Agency> t_jg_agency = db.T_JG_Agency.Where(a=>a.AgencyName.Contains(agencyname)).ToList();
                int [] iid = new int[t_jg_agency.Count];
                for(int i=0;i< t_jg_agency.Count;i++)
                {
                    iid[i] = t_jg_agency[i].ID;
                }
                t_jg_product = t_jg_product.Where(p => iid.Contains(p.AgencyID));
            }
            if(amount!=0)
            {
                if (collection["regkey"] == "1")
                    t_jg_product = t_jg_product.Where(p=> p.FinancingAmount>amount);
                else
                    t_jg_product = t_jg_product.Where(p=> p.FinancingAmount<amount);    
            }

            IList<GGZBTQPT_PRO.Models.T_JG_Product> list = t_jg_product.OrderByDescending(s => s.CreateTime)
                                                        .Skip(numPerPage * (pageNum - 1))
                                                        .Take(numPerPage).ToList();
            ViewBag.recordCount = t_jg_product.Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.productname = productname;
            ViewBag.agencyname = agencyname;
            ViewBag.amount = amount;
            return PartialView(list);
        }

        //机构发布的产品一览表
        public ViewResult AgencyProductList(int agencyid=1, int pageNum = 1, int numPerPage = 10)
        {
            var t_jg_product = db.T_JG_Product.Where(p => (p.IsValid == true && p.AgencyID == agencyid)).OrderByDescending(s => s.CreateTime);
            ViewBag.recordCount = db.T_JG_Product.Where(c => (c.IsValid == true && c.AgencyID == agencyid)).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            return View(t_jg_product);
        }

        //产品统计
        public ActionResult ProductReport()
        {
            var cp = db.T_JG_Product.Where(p => (p.IsValid == true && p.PublicStatus == "2")).ToList();
            ViewBag.CPCount = cp.Count();

            return PartialView();
        }

        #region 机构登记产品数量统计
        public ActionResult DataReportbyAgency()
        {
            var cp = db.T_JG_Product.Where(p => (p.IsValid == true && p.PublicStatus == "2")).ToList();
            ViewBag.CPCount = cp.Count();
            var list = db.T_JG_Product.Where(p => (p.IsValid == true && p.PublicStatus == "2")).GroupBy(g => g.AgencyID)
                                    .Select(s => new { cnt = s.Count(), type = (int)s.Key })
                                    .Join(db.T_JG_Agency, s => s.type, g => g.ID, (s, g) => new VM_CPReport { TypeName = g.SubName, Count = s.cnt });

            return PartialView(list.ToList());
        }

        public ActionResult ChartReportbyAgency()
        {
            var list = db.T_JG_Product.Where(p => (p.IsValid == true && p.PublicStatus == "2")).GroupBy(g => g.AgencyID)
                                    .Select(s => new { cnt = s.Count(), type = (int)s.Key });
            Dictionary<String, int> dic = new Dictionary<string, int>();
            foreach (var li in list.ToList())
            {
                dic.Add(db.T_JG_Agency.Find(li.type).SubName, li.cnt);
            }

            return Json(new { statData = dic }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //查看产品增加点击量
        public ActionResult ProductClicks(int id)
        {
            T_JG_Product t_jg_product = db.T_JG_Product.Find(id);
            if (t_jg_product != null)
            {
                t_jg_product.Clicks = t_jg_product.Clicks + 1;

                db.Entry(t_jg_product).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GenerateImage(int width, int height, string action_name)
        {
            Bitmap m_Bitmap = WebSiteThumbnail.GetWebSiteThumbnail("http://" + HttpContext.Request.ServerVariables["HTTP_HOST"] + Url.Action(action_name), width, height, width, height);

            MemoryStream ms = new MemoryStream();
            m_Bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);//JPG、GIF、PNG等均可
            byte[] buff = ms.ToArray();

            return File(buff, "image/jpeg");
        }

        public ActionResult PrintProductReport()
        {
            var cp = db.T_JG_Product.Where(p => (p.IsValid == true && p.PublicStatus == "2")).ToList();
            ViewBag.CPCount = cp.Count();
            return PartialView();
        }
    }
}