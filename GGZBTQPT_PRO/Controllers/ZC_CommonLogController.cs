using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.Enums;
using GGZBTQPT_PRO.Util;
using ExcelGenerator.SpreadSheet; 
using LinqToExcel;

namespace GGZBTQPT_PRO.Controllers
{ 
    public class ZC_CommonLogController : BaseController
    { 
        //
        // GET: /ZC_CommonLog/ 
        public ViewResult OperateLog(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            IList<GGZBTQPT_PRO.Models.T_ZC_CommonLog> list = db.T_ZC_CommonLog.Where(l => (l.Level == "INFO" || l.Level == "WARN") && l.Message.Contains(keywords)).ToList()
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_ZC_CommonLog.Where(l => (l.Level == "INFO" || l.Level == "WARN") && l.Message.Contains(keywords)).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            return View(list);
        }


        public ViewResult ErrorLog(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            IList<GGZBTQPT_PRO.Models.T_ZC_CommonLog> list = db.T_ZC_CommonLog.Where(l => l.Level == "ERROR" && l.Message.Contains(keywords)).ToList()
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_ZC_CommonLog.Where(l => l.Level == "ERROR" && l.Message.Contains(keywords)).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            return View(list);
        }

        public ViewResult ManageLog(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            IList<GGZBTQPT_PRO.Models.T_ZC_CommonLog> list = db.T_ZC_CommonLog.Where(l => ((l.Level == "INFO" || l.Level == "WARN") && l.GenerateSystem == (int)GenerateSystem.Manage) && l.Message.Contains(keywords)).ToList()
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_ZC_CommonLog.Where(l => ((l.Level == "INFO" || l.Level == "WARN") && l.GenerateSystem == (int)GenerateSystem.Manage) && l.Message.Contains(keywords)).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            return View(list);
        }
        
        public ViewResult MemberDynamic(string keywords, int pageNum = 1, int numPerPage = 15)
        { 
            keywords = keywords == null ? "" : keywords;

            IList<GGZBTQPT_PRO.Models.T_ZC_CommonLog> list = db.T_ZC_CommonLog.Where(l => (l.Level == "INFO" && l.GenerateSystem != (int)GenerateSystem.Manage) && l.Message.Contains(keywords)).ToList()
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_ZC_CommonLog.Where(l => (l.Level == "INFO" && l.GenerateSystem != (int)GenerateSystem.Manage) && l.Message.Contains(keywords)).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            return View(list);
        }


        #region//日志导出
        public ActionResult ExportErrorLogWithExcel()
        { 
            Workbook workbook = new Workbook(); 
            Worksheet worksheet = new Worksheet("错误日志");

            var list = db.T_ZC_CommonLog.Where(l => l.Level == "Error").ToList();
            foreach( var log in list)
            {
                ExcelGenerator.SpreadSheet.Row row = new ExcelGenerator.SpreadSheet.Row();

                row.Cells.Add(new ExcelGenerator.SpreadSheet.Cell(log.ID.ToString()));
                row.Cells.Add(new ExcelGenerator.SpreadSheet.Cell(log.Level.ToString()));
                row.Cells.Add(new ExcelGenerator.SpreadSheet.Cell(log.Logger.ToString()));
                row.Cells.Add(new ExcelGenerator.SpreadSheet.Cell(log.Message.ToString()));
                row.Cells.Add(new ExcelGenerator.SpreadSheet.Cell(log.Source.ToString()));
                if(log.Exception.Length <= 500)
                {
                    row.Cells.Add(new ExcelGenerator.SpreadSheet.Cell(log.Exception.ToString())); 
                }
                else
                {
                    row.Cells.Add(new ExcelGenerator.SpreadSheet.Cell(log.Exception.Substring(0, 500))); 
                }
                row.Cells.Add(new ExcelGenerator.SpreadSheet.Cell(log.User.ToString()));

                worksheet.Rows.Add(row);
            } 
            workbook.Worksheets.Add(worksheet); 
            return new ExcelResult(workbook.getBytes(), "Export.xls");
        }

        public ActionResult ExportOperateLogWithExcel()
        {
            Workbook workbook = new Workbook();
            Worksheet worksheet = new Worksheet("操作日志");

            var list = db.T_ZC_CommonLog.Where(l => l.Level == "INFO" || l.Level == "WARN").ToList();
            foreach (var log in list)
            {
                ExcelGenerator.SpreadSheet.Row row = new ExcelGenerator.SpreadSheet.Row();

                row.Cells.Add(new ExcelGenerator.SpreadSheet.Cell(log.ID.ToString()));
                row.Cells.Add(new ExcelGenerator.SpreadSheet.Cell(log.Level.ToString()));
                row.Cells.Add(new ExcelGenerator.SpreadSheet.Cell(log.Logger.ToString()));
                row.Cells.Add(new ExcelGenerator.SpreadSheet.Cell(log.Message.ToString()));
                row.Cells.Add(new ExcelGenerator.SpreadSheet.Cell(log.Source.ToString()));
                if (log.Exception.Length <= 500)
                {
                    row.Cells.Add(new ExcelGenerator.SpreadSheet.Cell(log.Exception.ToString()));
                }
                else
                {
                    row.Cells.Add(new ExcelGenerator.SpreadSheet.Cell(log.Exception.Substring(0, 500)));
                }
                row.Cells.Add(new ExcelGenerator.SpreadSheet.Cell(log.User.ToString()));

                worksheet.Rows.Add(row);
            }
            workbook.Worksheets.Add(worksheet);
            return new ExcelResult(workbook.getBytes(), "Export.xls");
        }
        #endregion

        #region //日志导入
        public ActionResult ImportOperateLogWithExcel()
        {
            var excel = new ExcelQueryFactory(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Public/Excel/Import.xls"));

            var logs = from c in excel.Worksheet()
                        select c; 

            foreach(var log in logs)
            {
                var x = log["Message"];
            }
            return ReturnJson(true, "导入成功", "", "", true, "");
        }
        #endregion

        //
        // GET: /ZC_CommonLog/Details/5

        public ViewResult ErrorDetails(int id)
        {
            T_ZC_CommonLog t_zc_commonlog = db.T_ZC_CommonLog.Find(id);
            return View(t_zc_commonlog);
        }

        #region//会员动态分析
        public ActionResult MemberAnalysis()
        { 
            var commonlogs = db.T_ZC_CommonLog.Where(l => l.GenerateType == (int)GenerateTypes.FromMember).ToList();
            return PartialView(commonlogs);
        }

        public ActionResult BasicStat()
        {
            var commonlogs = db.T_ZC_CommonLog.Where(l => l.GenerateType == (int)GenerateTypes.FromMember).ToList();  
            return PartialView(commonlogs); 
        }

        public ActionResult BasicStatData()
        {
            try
            {
                var commonlogs = db.T_ZC_CommonLog.Where(l => l.GenerateType == (int)GenerateTypes.FromMember).ToList(); 
                Dictionary<String, int> dic = new Dictionary<string, int>();

                if(commonlogs != null)
                {
                    dic.Add("访问操作", commonlogs.Where(m => m.OperateType == (int)OperateTypes.Visit).Count());
                    dic.Add("添加操作", commonlogs.Where(m => m.OperateType == (int)OperateTypes.Create).Count());
                    dic.Add("删除操作", commonlogs.Where(m => m.OperateType == (int)OperateTypes.Delete).Count());
                    dic.Add("更新操作", commonlogs.Where(m => m.OperateType == (int)OperateTypes.Edit).Count());
                    dic.Add("发布操作", commonlogs.Where(m => m.OperateType == (int)OperateTypes.Publish).Count());
                    dic.Add("关注操作", commonlogs.Where(m => m.OperateType == (int)OperateTypes.Attention).Count());
                    dic.Add("收藏操作", commonlogs.Where(m => m.OperateType == (int)OperateTypes.Favorite).Count());
                    dic.Add("搜索操作", commonlogs.Where(m => m.OperateType == (int)OperateTypes.Search).Count()); 
                }
                else
                {
                    dic.Add("操作数", 0);
                }

                return Json(new { statData = dic, statusCode = "200" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { statusCode = "300" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FunctionStat()
        {
            var commonlogs = db.T_ZC_CommonLog.Where(l => l.GenerateType == (int)GenerateTypes.FromMember).ToList();
            return PartialView(commonlogs);
        }

        public ActionResult FunctionStatData()
        { 
            try
            {
                var commonlogs = db.T_ZC_CommonLog.Where(l => l.GenerateType == (int)GenerateTypes.FromMember).ToList();
                Dictionary<String, int> dic = new Dictionary<string, int>();

                if(commonlogs != null)
                {
                    dic.Add("金融推荐", commonlogs.Where(m => m.OperateType == (int)GenerateSystem.Recommend).Count()); 
                    dic.Add("找项目", commonlogs.Where(m => m.OperateType == (int)GenerateSystem.FindFinancial).Count());
                    dic.Add("找资金", commonlogs.Where(m => m.OperateType == (int)GenerateSystem.FindInvestment).Count());
                    dic.Add("找服务", commonlogs.Where(m => m.OperateType == (int)GenerateSystem.FindService).Count());
                    dic.Add("我的关注", commonlogs.Where(m => m.OperateType == (int)GenerateSystem.Attention).Count());
                    dic.Add("我的收藏", commonlogs.Where(m => m.OperateType == (int)GenerateSystem.Favorite).Count());
                    dic.Add("我的发布", commonlogs.Where(m => m.OperateType == (int)GenerateSystem.Publish).Count());
                }
                else
                { 
                    dic.Add("操作数", 0);
                }

                return Json(new { statData = dic, statusCode = "200" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { statusCode = "300" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}