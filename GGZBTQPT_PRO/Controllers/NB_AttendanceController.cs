using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.ViewModels;
using GGZBTQPT_PRO.Enums;
using System.Data;

namespace GGZBTQPT_PRO.Controllers
{
    public class NB_AttendanceController : BaseController
    {
        //
        // GET: /T_NB_Attendance/

        public ActionResult Index(int userId = -1,int pageNum = 1, int numPerPage = 15)
        {
            IList<T_NB_Attendance> list = null;
            int count = 0;

            if (userId != -1)
            {
                list = db.T_NB_Attendance.Where(t => t.UserId == userId).OrderByDescending(s => s.WorkTime).OrderByDescending(s=>s.ID)
                                                    .Skip(numPerPage * (pageNum - 1))
                                                    .Take(numPerPage).ToList();
                count = db.T_NB_Attendance.Where(t => t.UserId == userId).Count();
            }
            else
            {
                list = db.T_NB_Attendance.OrderByDescending(s => s.WorkTime).OrderByDescending(s => s.ID)
                                                    .Skip(numPerPage * (pageNum - 1))
                                                    .Take(numPerPage).ToList();
                count = db.T_NB_Attendance.Count();
            }
            ViewBag.recordCount = count;
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.UserId = GetUser();

            return View(list);
        }

        public ActionResult Create()
        {
            ViewBag.UserId = GetUser();
            ViewBag.State = GetAttendanceState();
            ViewBag.WorkTime =DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }

        [HttpPost]
        public ActionResult Create(T_NB_Attendance t_nb_attendance)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    if (db.T_NB_Attendance.Where(m => m.WorkTime == t_nb_attendance.WorkTime).Any(m => m.UserId == t_nb_attendance.UserId))
                    {
                        return ReturnJson(false, "重复登记!", "", "", false, "");
                    }

                    t_nb_attendance.RecordUser = CurrentUser().UserName;

                    db.T_NB_Attendance.Add(t_nb_attendance);
                    int result = db.SaveChanges();
                    if (result > 0)
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
                else
                {
                    return ReturnJson(false, "请选择考勤信息", "", "", false, "");
                }
            }
            return Json(new { });
        }

        public ActionResult Edit(int id)
        {
            T_NB_Attendance t_nb_attendance = db.T_NB_Attendance.Find(id);

            var states = from AttendanceStates mtype in Enum.GetValues(typeof(AttendanceStates))
                         select new { ID = (int)mtype, Name = mtype.ToString() };

            ViewBag.UserId = new SelectList(db.T_ZC_User.Where(p => p.IsValid == true), "ID", "UserName", t_nb_attendance.UserId);
            ViewBag.State = new SelectList(states, "ID", "Name", t_nb_attendance.State);
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id,T_NB_Attendance t_nb_attendance)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    t_nb_attendance.RecordUser = CurrentUser().UserName;
                    t_nb_attendance.RecordTime = DateTime.Now;

                    db.Entry(t_nb_attendance).State = EntityState.Modified;
                    int result = db.SaveChanges();
                    if (result >= 0)
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
            }
            return Json(new { });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (Request.IsAjaxRequest())
            {
                T_NB_Attendance t_nb_attendance = db.T_NB_Attendance.Find(id);
                db.T_NB_Attendance.Remove(t_nb_attendance);

                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "删除成功", "", "", false, "");
                else
                    return ReturnJson(false, "删除失败", "", "", false, "");
            }
            return Json(new { });
        }

        public ActionResult StatList(string beginTime,string endTime)
        {
            beginTime = beginTime == null ? DateTime.Now.AddDays(-7).ToShortDateString() : beginTime;
            endTime = endTime == null ? DateTime.Now.ToShortDateString() : endTime;

            if (beginTime == "" || endTime == "")
                return ReturnJson(false, "请选择日期", "", "", false, "");

            DateTime begin = Convert.ToDateTime(beginTime);
            DateTime end = Convert.ToDateTime(endTime);

            IList<VM_AttendanceStat> list = db.T_NB_Attendance.Where(t => t.WorkTime >= begin && t.WorkTime <= end)
                                                              .GroupBy(g => g.State)
                                                              .Select(s => new VM_AttendanceStat
                                                              {
                                                                   StateCount = s.Count(),
                                                                   State = s.Key
                                                               }).ToList();
            ViewBag.BeginTime = beginTime;
            ViewBag.EndTime = endTime;

            return View(list);
        }

        public ActionResult StateDetails(int state, string beginTime, string endTime, int pageNum = 1, int numPerPage = 10)
        {
            DateTime begin = Convert.ToDateTime(beginTime);
            DateTime end = Convert.ToDateTime(endTime);

            var tq = db.T_NB_Attendance.Where(t => t.State == state && t.WorkTime >= begin && t.WorkTime <= end);
            IList<T_NB_Attendance> list = tq.OrderByDescending(s => s.WorkTime).Skip(numPerPage * (pageNum - 1))
                                                    .Take(numPerPage).ToList();

            ViewBag.recordCount = tq.Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.BeginTime = beginTime;
            ViewBag.EndTime = endTime;
            ViewBag.state = state;

            return View(list);
        }

        public SelectList GetUser()
        {
            IList<T_ZC_User> Users = db.T_ZC_User.Where(t => t.IsValid == true).ToList();
            SelectList list = new SelectList(Users,"ID","UserName");
            return list;
        }

        public SelectList GetAttendanceState()
        {
            var states = from AttendanceStates mtype in Enum.GetValues(typeof(AttendanceStates))
                        select new { ID = (int)mtype, Name = mtype.ToString() };

            SelectList list = new SelectList(states, "ID", "Name");
            return list;
        }
    }
}
