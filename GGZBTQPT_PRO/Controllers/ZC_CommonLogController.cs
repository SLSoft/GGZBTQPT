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

            ViewBag.recordCount = db.T_ZC_CommonLog.Count();
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

            ViewBag.recordCount = db.T_ZC_CommonLog.Count();
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


        //
        // GET: /ZC_CommonLog/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_ZC_CommonLog t_zc_commonlog = db.T_ZC_CommonLog.Find(id);
            return View(t_zc_commonlog);
        }

        //
        // POST: /ZC_CommonLog/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            T_ZC_CommonLog t_zc_commonlog = db.T_ZC_CommonLog.Find(id);
            db.T_ZC_CommonLog.Remove(t_zc_commonlog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}