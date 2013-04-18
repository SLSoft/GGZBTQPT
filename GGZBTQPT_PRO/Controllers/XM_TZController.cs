using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using System.IO;
using System.Data.Objects.SqlClient;

namespace GGZBTQPT_PRO.Controllers
{
    public class XM_TZController : BaseController
    {
        //private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /XM_TZ/

        public ViewResult Index(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;
            var t_xm_investment = db.T_XM_Investment.Where(p => (p.IsValid == true && p.ItemName.Contains(keywords))).OrderByDescending(p => p.CreateTime)
                                                                    .Skip(numPerPage * (pageNum - 1))
                                                                    .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_XM_Investment.Where(p => (p.IsValid == true && p.ItemName.Contains(keywords))).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            return View(t_xm_investment);
        }

        //
        // GET: /XM_TZ/Details/5

        public ViewResult Details(int id)
        {
            T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);
            return View(t_xm_investment);
        }
        public void BindIndustry(object select = null)
        {
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();

            ViewData["Industry"] = new SelectList(Industry, "ID", "Name", select);
        }
        public void BindTeamworkType(object select = null)
        {
            List<T_PTF_DicDetail> TeamworkType = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM06")).ToList();

            ViewData["TeamworkType"] = new SelectList(TeamworkType, "ID", "Name", select);
        }
        public void BindArea(object select = null)
        {
            List<T_PTF_DicTreeDetail> Area = db.T_PTF_DicTreeDetail.Where(p => (p.DicType == "34" && p.Depth == 1)).OrderBy(p => p.Taxis).ToList();

            ViewData["Province"] = new SelectList(Area, "Code", "Name", select);
        }

        public JsonResult GetCity(string ParentCode)
        {
            List<T_PTF_DicTreeDetail> City = db.T_PTF_DicTreeDetail.Where(p => (p.DicType == "34" && p.ParentCode == ParentCode)).ToList();

            return Json(City, JsonRequestBehavior.AllowGet);
        }
        public void BindInvestmentStage(object select = null)
        {
            List<T_PTF_DicDetail> InvestmentStage = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM04")).ToList();

            ViewData["InvestmentStage"] = new SelectList(InvestmentStage, "ID", "Name", select);
        }
        public void BindInvestmentNature(object select = null)
        {
            List<T_PTF_DicDetail> InvestmentNature = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM07")).ToList();

            ViewData["InvestmentNature"] = new SelectList(InvestmentNature, "ID", "Name", select);
        }
        //
        // GET: /XM_TZ/Create

        public ActionResult Create()
        {
            BindArea();
            BindIndustry();
            BindTeamworkType();
            BindInvestmentStage();
            BindInvestmentNature();
            var t_xm_investment = new T_XM_Investment();
            return View(t_xm_investment);
        } 

        //
        // POST: /XM_TZ/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(T_XM_Investment t_xm_investment, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                string checkedIndustry = (collection["cbIndustry"] + ",").Replace("false,", "");
                if (checkedIndustry.Length > 1)
                    checkedIndustry = checkedIndustry.Remove(checkedIndustry.Length - 1);
                string checkedProvince = (collection["cbProvince"] + ",").Replace("false,", "");
                if (checkedProvince.Length > 1)
                    checkedProvince = checkedProvince.Remove(checkedProvince.Length - 1);
                string checkedcbTeamWorkType = (collection["cbTeamWorkType"] + ",").Replace("false,", "");
                if (checkedcbTeamWorkType.Length > 1)
                    checkedcbTeamWorkType = checkedcbTeamWorkType.Remove(checkedcbTeamWorkType.Length - 1);
                string checkedcbInvestmentStage = (collection["cbInvestmentStage"] + ",").Replace("false,", "");
                if (checkedcbInvestmentStage.Length > 1)
                    checkedcbInvestmentStage = checkedcbInvestmentStage.Remove(checkedcbInvestmentStage.Length - 1);
                t_xm_investment.AimIndustry = checkedIndustry == "," ? "" : checkedIndustry;
                t_xm_investment.AjmArea = checkedProvince == "," ? "" : checkedProvince;
                t_xm_investment.TeamworkType = checkedcbTeamWorkType == "," ? "" : checkedcbTeamWorkType;
                t_xm_investment.InvestmentStage = checkedcbInvestmentStage == "," ? "" : checkedcbInvestmentStage;
                t_xm_investment.City = collection["ddlCity"];
                t_xm_investment.Description = t_xm_investment.Description == null ? "" : t_xm_investment.Description;
                t_xm_investment.IsValid = true;
                t_xm_investment.OP = 0;
                t_xm_investment.CreateTime = DateTime.Now;
                t_xm_investment.SubmitTime = DateTime.Now;

                HttpPostedFileBase file = Request.Files[0];
                //存入文件
                if (file.ContentLength > 0)
                {
                    t_xm_investment.Pic = new byte[Request.Files[0].InputStream.Length];
                    Request.Files[0].InputStream.Read(t_xm_investment.Pic, 0, t_xm_investment.Pic.Length);
                }

                db.T_XM_Investment.Add(t_xm_investment);
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", true, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
        }
        
        //
        // GET: /XM_TZ/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);
            BindArea(t_xm_investment.Province);
            BindIndustry(t_xm_investment.Industry);
            BindTeamworkType(t_xm_investment.TeamworkType);
            BindInvestmentStage(t_xm_investment.InvestmentStage);
            BindInvestmentNature(t_xm_investment.InvestmentNature);
            return View(t_xm_investment);
        }

        //
        // POST: /XM_TZ/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(T_XM_Investment t_xm_investment, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                string checkedIndustry = (collection["cbIndustry"] + ",").Replace("false,", "");
                if (checkedIndustry.Length > 1)
                    checkedIndustry = checkedIndustry.Remove(checkedIndustry.Length - 1);
                string checkedProvince = (collection["cbProvince"] + ",").Replace("false,", "");
                if (checkedProvince.Length > 1)
                    checkedProvince = checkedProvince.Remove(checkedProvince.Length - 1);
                string checkedcbTeamWorkType = (collection["cbTeamWorkType"] + ",").Replace("false,", "");
                if (checkedcbTeamWorkType.Length > 1)
                    checkedcbTeamWorkType = checkedcbTeamWorkType.Remove(checkedcbTeamWorkType.Length - 1);
                string checkedcbInvestmentStage = (collection["cbInvestmentStage"] + ",").Replace("false,", "");
                if (checkedcbInvestmentStage.Length > 1)
                    checkedcbInvestmentStage = checkedcbInvestmentStage.Remove(checkedcbInvestmentStage.Length - 1);
                db.Entry(t_xm_investment).State = EntityState.Modified;
                t_xm_investment.AimIndustry = checkedIndustry == "," ? "" : checkedIndustry;
                t_xm_investment.AjmArea = checkedProvince == "," ? "" : checkedProvince;
                t_xm_investment.TeamworkType = checkedcbTeamWorkType == "," ? "" : checkedcbTeamWorkType;
                t_xm_investment.InvestmentStage = checkedcbInvestmentStage == "," ? "" : checkedcbInvestmentStage;
                t_xm_investment.City = collection["ddlCity"];
                t_xm_investment.Description = t_xm_investment.Description == null ? "" : t_xm_investment.Description;
                t_xm_investment.UpdateTime = DateTime.Now;

                HttpPostedFileBase file = Request.Files[0];
                //存入文件
                if (file.ContentLength > 0)
                {
                    t_xm_investment.Pic = new byte[Request.Files[0].InputStream.Length];
                    Request.Files[0].InputStream.Read(t_xm_investment.Pic, 0, t_xm_investment.Pic.Length);
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
        // GET: /XM_TZ/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);
            return View(t_xm_investment);
        }

        //
        // POST: /XM_TZ/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.IsAjaxRequest())
            {
                T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);
                t_xm_investment.IsValid = false;
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", false, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
        }

        //helper
        public FileContentResult ShowPic(int xm_id)
        {
            byte[] pic;
            if (db.T_XM_Investment.Find(xm_id).Pic != null)
                pic = db.T_XM_Investment.Find(xm_id).Pic;
            else
                pic = new byte[1];
            return File(pic, "image/jpeg");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //待审核意向
        public ActionResult TZCheckList(int pageNum = 1, int numPerPage = 15)
        {
            IList<GGZBTQPT_PRO.Models.T_XM_Investment> list = db.T_XM_Investment.Where(p => (p.IsValid == true && p.PublicStatus == "1")).ToList()
                                                            .OrderByDescending(s => s.SubmitTime)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_XM_Investment.Where(p => (p.IsValid == true && p.PublicStatus == "1")).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;

            return View(list);
        }

        //已审核意向
        public ActionResult TZCheckList_Pass(int pageNum = 1, int numPerPage = 15)
        {
            IList<GGZBTQPT_PRO.Models.T_XM_Investment> list = db.T_XM_Investment.Where(p => (p.IsValid == true && p.PublicStatus == "2")).ToList()
                                                            .OrderByDescending(s => s.SubmitTime)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_XM_Investment.Where(p => (p.IsValid == true && p.PublicStatus == "2")).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;

            return View(list);
        }

        //意向审核
        public ActionResult TZCheck(int id)
        {
            T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);
            t_xm_investment.PublicStatus = "2";
            t_xm_investment.PublicTime = DateTime.Now;
            t_xm_investment.UpdateTime = DateTime.Now;

            if (db.SaveChanges() > 0)
                return ReturnJson(true, "审核成功", "", "", false, "");
            else
                return ReturnJson(false, "审核失败", "", "", false, "");
        }

        //意向撤销审核
        public ActionResult UnTZCheck(int id)
        {
            T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);
            t_xm_investment.PublicStatus = "1";
            t_xm_investment.PublicTime = DateTime.Now;
            t_xm_investment.UpdateTime = DateTime.Now;

            if (db.SaveChanges() > 0)
                return ReturnJson(true, "撤销审核成功", "", "", false, "");
            else
                return ReturnJson(false, "撤销审核失败", "", "", false, "");
        }

        //项目分析--投资意向列表
        public ActionResult TZMatch(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;
            IList<GGZBTQPT_PRO.Models.T_XM_Investment> list = db.T_XM_Investment.Where(p => (p.IsValid == true && p.ItemName.Contains(keywords) && p.PublicStatus == "2")).ToList()
                                                            .OrderByDescending(s => s.SubmitTime)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_XM_Investment.Where(p => (p.IsValid == true && p.ItemName.Contains(keywords) && p.PublicStatus == "2")).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            return View(list);
        }

        //根据投资意向匹配对应的项目
        public ActionResult TZMatchResult(int id, int pageNum = 1, int numPerPage = 15)
        {
            T_XM_Investment inverst = db.T_XM_Investment.Find(id);
            decimal investment = inverst.Investment == null ? 0 : (decimal)inverst.Investment;
            string[] industry = inverst.AimIndustry.Split(',');
            int[] temp = new int[industry.Length];
            for (int i = 0; i < industry.Length; i++)
            {
                temp[i] = Convert.ToInt32(industry[i]);
            }
            ViewData["inverstName"] = inverst.ItemName;
            IList<GGZBTQPT_PRO.Models.T_XM_Financing> list = db.T_XM_Financing.Where(p => (p.IsValid == true && p.PublicStatus == "2" && (p.FinancSum < investment || p.TransferPrice < investment || p.Investment < investment || p.OtherItemFinancSum < investment) && temp.Contains((int)p.Industry))).ToList()
                                                        .OrderByDescending(s => s.SubmitTime)
                                                        .Skip(numPerPage * (pageNum - 1))
                                                        .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_XM_Financing.Where(p => (p.IsValid == true && p.PublicStatus == "2" && (p.FinancSum > investment || p.TransferPrice > investment || p.Investment > investment || p.OtherItemFinancSum > investment) && industry.Contains(SqlFunctions.StringConvert((double)p.Industry)))).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;

            return PartialView(list);
        }

        //意向查询
        public ActionResult TZQuery(FormCollection collection, int pageNum = 1, int numPerPage = 15)
        {
            ViewBag.condition1 = collection["condition1"];
            ViewBag.condition2 = collection["condition2"];
            ViewBag.condition3 = collection["condition3"];
            ViewBag.condition4 = collection["condition4"];
            ViewBag.condition5 = collection["condition5"];
            ViewBag.context = collection["context"];
            ViewBag.keys = collection["keys"];
            List<T_PTF_DicDetail> TeamworkType = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM06")).ToList();
            ViewData["TeamworkType"] = new SelectList(TeamworkType, "ID", "Name");
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();
            ViewData["Industry"] = new SelectList(Industry, "ID", "Name");
            List<T_PTF_DicDetail> InvestmentNature = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM07")).ToList();
            ViewData["InvestmentNature"] = new SelectList(InvestmentNature, "ID", "Name");
            List<T_PTF_DicDetail> InvestmentStage = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM04")).ToList();
            ViewData["InvestmentStage"] = new SelectList(InvestmentStage, "ID", "Name");

            string keys = "";
            string select_TeamworkType = "";
            string select_industry = "";
            string select_Investment = "";
            string select_InvestmentStage = "";
            string select_InvestmentNature = "";
            if (ViewBag.keys != null && ViewBag.keys.ToString().Trim() != "")
                keys = ViewBag.keys.ToString();
            if (ViewBag.condition1 != null && ViewBag.condition1.ToString().Trim() != "")
            {
                string[] temp = ViewBag.condition1.ToString().Substring(1).Split(',');
                select_InvestmentNature += " and (";
                foreach (string str in temp)
                {
                    select_InvestmentNature += " InvestmentNature = '" + str + "' or";
                }
                select_InvestmentNature = select_InvestmentNature.Substring(0, select_InvestmentNature.Length - 3);
                select_InvestmentNature += ")";
            }
            if (ViewBag.condition4 != null && ViewBag.condition4.ToString().Trim() != "")
            {
                string[] temp = ViewBag.condition4.ToString().Substring(1).Split(',');
                select_InvestmentStage += " and (";
                foreach (string str in temp)
                {
                    select_InvestmentStage += " InvestmentStage like '%" + str + "%' or";
                }
                select_InvestmentStage = select_InvestmentStage.Substring(0, select_InvestmentStage.Length - 3);
                select_InvestmentStage += ")";
            }
            if (ViewBag.condition2 != null && ViewBag.condition2.ToString().Trim() != "")
            {
                string[] temp = ViewBag.condition2.ToString().Substring(1).Split(',');
                select_TeamworkType += " and (";
                foreach (string str in temp)
                {
                    select_TeamworkType += " TeamworkType like '%" + str + "%' or";
                }
                select_TeamworkType = select_TeamworkType.Substring(0, select_TeamworkType.Length - 3);
                select_TeamworkType += ")";
            }
            if (ViewBag.condition3 != null && ViewBag.condition3.ToString().Trim() != "")
            {
                string[] temp = ViewBag.condition3.ToString().Substring(1).Split(',');
                select_industry += " and (";
                foreach (string str in temp)
                {
                    select_industry += " AimIndustry like '%" + str + "%' or";
                }
                select_industry = select_industry.Substring(0, select_industry.Length - 3);
                select_industry += ")";
            }
            if (ViewBag.condition5 != null && ViewBag.condition5.ToString().Trim() != "")
            {
                string[] temp = ViewBag.condition5.ToString().Substring(1).Split(',');
                select_Investment += " and (";
                foreach (string str in temp)
                {
                    select_Investment += " Investment " + str + " or";
                }
                select_Investment = select_Investment.Substring(0, select_Investment.Length - 3);
                select_Investment += ")";
            }
            string order = "ID";
            System.Data.SqlClient.SqlParameter[] selparms = new System.Data.SqlClient.SqlParameter[7];
            selparms[0] = new System.Data.SqlClient.SqlParameter("@keys", keys);
            selparms[1] = new System.Data.SqlClient.SqlParameter("@InvestmentNature", select_InvestmentNature);
            selparms[2] = new System.Data.SqlClient.SqlParameter("@TeamworkType", select_TeamworkType);
            selparms[3] = new System.Data.SqlClient.SqlParameter("@Industry", select_industry);
            selparms[4] = new System.Data.SqlClient.SqlParameter("@InvestmentStage", select_InvestmentStage);
            selparms[5] = new System.Data.SqlClient.SqlParameter("@FinancSum", select_Investment);
            selparms[6] = new System.Data.SqlClient.SqlParameter("@Order", order);
            System.Data.SqlClient.SqlParameter[] selparms_new = new System.Data.SqlClient.SqlParameter[selparms.Length];

            for (int i = 0, j = selparms.Length; i < j; i++)
            {
                selparms_new[i] = (System.Data.SqlClient.SqlParameter)((ICloneable)selparms[i]).Clone();
            }
            IList<T_XM_Investment> investments = (from p in db.T_XM_Investment.SqlQuery("exec dbo.P_GetTZXMByCondition @keys,@InvestmentNature,@TeamworkType,@Industry,@InvestmentStage,@FinancSum,@Order", selparms) select p).ToList().OrderByDescending(s => s.SubmitTime)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();
            ViewBag.recordCount = (from p in db.T_XM_Investment.SqlQuery("exec dbo.P_GetTZXMByCondition @keys,@InvestmentNature,@TeamworkType,@Industry,@InvestmentStage,@FinancSum,@Order", selparms_new) select p).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            if (investments.Count == 0)
                ViewBag.Message = "未找到符合要求的项目!";
            return View(investments); 
        }

        //指定会员发布的意向
        public ActionResult MemberInvestmentList(int memberid, int pageNum = 1, int numPerPage = 10)
        {
            var t_xm_investment = db.T_XM_Investment.Where(p => (p.IsValid == true && p.MemberID == memberid)).OrderByDescending(p => p.CreateTime);
            ViewBag.recordCount = db.T_XM_Investment.Where(p => (p.IsValid == true && p.MemberID == memberid)).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            return View(t_xm_investment);
        }

        //投资意向统计
        public ActionResult XMTZReport()
        {
            var tzxm = db.T_XM_Investment.Where(p=>(p.IsValid==true && p.PublicStatus=="2")).ToList();
            ViewBag.RZXMCount = tzxm.Count();
            ViewBag.RZXMSum = tzxm.Sum(s => s.Investment);
            return View();
        }

        #region 投资额度范围统计
        public ActionResult DataReportbyInvestment()
        {
            var tzxm = db.T_XM_Investment.Where(p => (p.IsValid == true && p.PublicStatus == "2")).ToList();
            ViewBag.RZXMCount = tzxm.Count();
            ViewBag.RZXMSum = tzxm.Sum(s => s.Investment);
            return PartialView(tzxm);
        }

        public ActionResult ChartReportbyInvestment()
        {
            var tzxm = db.T_XM_Investment.Where(p => (p.IsValid == true && p.PublicStatus == "2")).ToList();
            Dictionary<String, int> dic = new Dictionary<string, int>();

            if (tzxm != null)
            {
                dic.Add("10万以下", tzxm.Where(t => t.Investment<=10).Count());
                dic.Add("10-100万", tzxm.Where(t => (t.Investment > 10 && t.Investment <=100)).Count());
                dic.Add("100-500万", tzxm.Where(t => (t.Investment > 100 && t.Investment <= 500)).Count());
                dic.Add("500-1000万", tzxm.Where(t => (t.Investment > 500 && t.Investment <= 1000)).Count());
                dic.Add("1000-5000万", tzxm.Where(t => (t.Investment > 1000 && t.Investment <= 5000)).Count());
                dic.Add("5000万-1亿", tzxm.Where(t => (t.Investment > 5000 && t.Investment <= 10000)).Count());
                dic.Add("大于1亿", tzxm.Where(t => t.Investment > 10000).Count());
            }

            return Json(new { statData = dic}, JsonRequestBehavior.AllowGet);
        }     
        #endregion

        //查看意向增加点击量
        public ActionResult InvestmentClicks(int id)
        {
            T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);
            if (t_xm_investment != null)
            {
                t_xm_investment.Clicks = t_xm_investment.Clicks + 1;

                db.Entry(t_xm_investment).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}