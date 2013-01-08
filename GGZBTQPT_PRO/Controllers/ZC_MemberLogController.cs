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
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Controllers
{
    public class ZC_MemberLogController : BaseController
    { 
        public ActionResult OperateNumAnalysis()
        {
            var commonlogs = db.T_ZC_MemberLog.ToList();
            return PartialView(commonlogs);
        }


        public ActionResult ContinuanceAnalysis()
        {
            var commonlogs = db.T_ZC_MemberLog.ToList();
            return PartialView(commonlogs);
        }

        public ViewResult MemberDynamic(string keywords, int systemType = -1, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;
            IList<GGZBTQPT_PRO.Models.T_ZC_MemberLog> list = db.T_ZC_MemberLog.Where(l => l.Message.Contains(keywords)).ToList();

            if (systemType != -1)
            {
                list = list.Where(l => l.GenerateModule == systemType).ToList();
                ViewBag.recordCount = list.Count();
            }
            else
            {
                ViewBag.recordCount = list.Count();
                list = list.OrderByDescending(l => l.ID)
                           .Skip(numPerPage * (pageNum - 1))
                           .Take(numPerPage).ToList();
            }

            ViewBag.systemType = GetSystemType();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            return View(list);
        }
 
        private SelectList GetSystemType()
        {
            var types = from GenerateSystem systemType in Enum.GetValues(typeof(GenerateSystem))
                        where systemType != 0
                        select new { ID = (int)systemType, Name = ((DisplayAttribute[])typeof(GenerateSystem).GetField(systemType.ToString()).GetCustomAttributes(typeof(DisplayAttribute), false)).First().Name };

            SelectList list = new SelectList(types, "ID", "Name");
            return list;
        }

        public ActionResult OperateNumStatWithFuntion()
        {
            var commonlogs = db.T_ZC_MemberLog.ToList();
            return PartialView(commonlogs);
        }

        public ActionResult OperateNumStatWithFuntionData()
        {
            try
            {
                var Memberlogs = db.T_ZC_MemberLog.ToList();
                Dictionary<String, int> dic = new Dictionary<string, int>();

                if (Memberlogs != null)
                {
                    dic.Add("金融推荐", Memberlogs.Where(m => m.OperateType == (int)GenerateSystem.Recommend).Count());
                    dic.Add("找项目", Memberlogs.Where(m => m.OperateType == (int)GenerateSystem.FindFinancial).Count());
                    dic.Add("找资金", Memberlogs.Where(m => m.OperateType == (int)GenerateSystem.FindInvestment).Count());
                    dic.Add("找服务", Memberlogs.Where(m => m.OperateType == (int)GenerateSystem.FindService).Count());
                    dic.Add("我的关注", Memberlogs.Where(m => m.OperateType == (int)GenerateSystem.Attention).Count());
                    dic.Add("我的收藏", Memberlogs.Where(m => m.OperateType == (int)GenerateSystem.Favorite).Count());
                    dic.Add("我的发布", Memberlogs.Where(m => m.OperateType == (int)GenerateSystem.Publish).Count());
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

        public ActionResult OperateNumStatWithOperateType()
        {
            var member_logs = db.T_ZC_MemberLog.ToList();
            return PartialView(member_logs);
        }

        public ActionResult OperateNumStatWithOperateTypeData()
        {
            try
            {
                var Memberlogs = db.T_ZC_MemberLog.ToList();
                Dictionary<String, int> dic = new Dictionary<string, int>();

                if(Memberlogs != null)
                {
                    dic.Add("访问操作", Memberlogs.Where(m => m.OperateType == (int)OperateTypes.Visit).Count());
                    dic.Add("添加操作", Memberlogs.Where(m => m.OperateType == (int)OperateTypes.Create).Count());
                    dic.Add("删除操作", Memberlogs.Where(m => m.OperateType == (int)OperateTypes.Delete).Count());
                    dic.Add("更新操作", Memberlogs.Where(m => m.OperateType == (int)OperateTypes.Edit).Count());
                    dic.Add("发布操作", Memberlogs.Where(m => m.OperateType == (int)OperateTypes.Publish).Count());
                    dic.Add("关注操作", Memberlogs.Where(m => m.OperateType == (int)OperateTypes.Attention).Count());
                    dic.Add("收藏操作", Memberlogs.Where(m => m.OperateType == (int)OperateTypes.Favorite).Count());
                    dic.Add("搜索操作", Memberlogs.Where(m => m.OperateType == (int)OperateTypes.Search).Count()); 
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

        public ActionResult ContinuanceStatWithOperateType()
        { 
            var member_logs = db.T_ZC_MemberLog.ToList();
            return PartialView(member_logs);
        }

        public ActionResult ContinuanceStatWithOperateTypeData()
        {
            try
            {
                var Memberlogs = db.T_ZC_MemberLog.ToList();
                Dictionary<String, double> dic = new Dictionary<string, double>();

                if (Memberlogs != null)
                {
                    dic.Add("访问操作", Memberlogs.Where(m => m.OperateType == (int)OperateTypes.Visit).Sum(m => m.Continuance.TotalMinutes));
                    dic.Add("添加操作", Memberlogs.Where(m => m.OperateType == (int)OperateTypes.Create).Sum(m => m.Continuance.TotalMinutes));
                    dic.Add("删除操作", Memberlogs.Where(m => m.OperateType == (int)OperateTypes.Delete).Sum(m => m.Continuance.TotalMinutes));
                    dic.Add("更新操作", Memberlogs.Where(m => m.OperateType == (int)OperateTypes.Edit).Sum(m => m.Continuance.TotalMinutes));
                    dic.Add("发布操作", Memberlogs.Where(m => m.OperateType == (int)OperateTypes.Publish).Sum(m => m.Continuance.TotalMinutes));
                    dic.Add("关注操作", Memberlogs.Where(m => m.OperateType == (int)OperateTypes.Attention).Sum(m => m.Continuance.TotalMinutes));
                    dic.Add("收藏操作", Memberlogs.Where(m => m.OperateType == (int)OperateTypes.Favorite).Sum(m => m.Continuance.TotalMinutes));
                    dic.Add("搜索操作", Memberlogs.Where(m => m.OperateType == (int)OperateTypes.Search).Sum(m => m.Continuance.TotalMinutes)); 
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


        public ActionResult ContinuanceStatWithFuntion()
        {
            var member_logs = db.T_ZC_MemberLog.ToList();
            return PartialView(member_logs);
        }

        public ActionResult ContinuanceStatWithFuntionData()
        {
            try
            {
                var Memberlogs = db.T_ZC_MemberLog.ToList();
                Dictionary<String, double> dic = new Dictionary<string, double>();

                if (Memberlogs != null)
                {
                    dic.Add("金融推荐", Memberlogs.Where(m => m.OperateType == (int)GenerateSystem.Recommend).Sum(m => m.Continuance.TotalMinutes));
                    dic.Add("找项目", Memberlogs.Where(m => m.OperateType == (int)GenerateSystem.FindFinancial).Sum(m => m.Continuance.TotalMinutes));
                    dic.Add("找资金", Memberlogs.Where(m => m.OperateType == (int)GenerateSystem.FindInvestment).Sum(m => m.Continuance.TotalMinutes));
                    dic.Add("找服务", Memberlogs.Where(m => m.OperateType == (int)GenerateSystem.FindService).Sum(m => m.Continuance.TotalMinutes));
                    dic.Add("我的关注", Memberlogs.Where(m => m.OperateType == (int)GenerateSystem.Attention).Sum(m => m.Continuance.TotalMinutes));
                    dic.Add("我的收藏", Memberlogs.Where(m => m.OperateType == (int)GenerateSystem.Favorite).Sum(m => m.Continuance.TotalMinutes));
                    dic.Add("我的发布", Memberlogs.Where(m => m.OperateType == (int)GenerateSystem.Publish).Sum(m => m.Continuance.TotalMinutes));
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
    }
}
