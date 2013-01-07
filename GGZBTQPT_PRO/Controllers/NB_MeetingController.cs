using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.Enums;
using System.Data;
using GGZBTQPT_PRO.ViewModels;

namespace GGZBTQPT_PRO.Controllers
{
    public class NB_MeetingController : BaseController
    {
        //
        // GET: /NB_Meeting/

        #region 会议预定
        /// <summary>
        /// 会议预定列表
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageNum"></param>
        /// <param name="numPerPage"></param>
        /// <returns></returns>
        public ActionResult MeetingApply(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            T_ZC_User current_user = CurrentUser();

            IList<T_NB_Meeting> list = db.T_NB_Meeting.Where(p => p.CreateUserId == current_user.ID && p.Title.Contains(keywords) && p.IsValid == true)
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_NB_Meeting.Where(p => p.CreateUserId == current_user.ID && p.Title.Contains(keywords) && p.IsValid == true).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;

            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public ActionResult Create(T_NB_Meeting t_nb_meeting)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    t_nb_meeting.CreateUserId = CurrentUser().ID;
                    t_nb_meeting.MeetingTime = Convert.ToDateTime(t_nb_meeting.MeetingTime);

                    db.T_NB_Meeting.Add(t_nb_meeting);

                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    }
                    else
                    {
                        return ReturnJson(false, "操作失败", "", "", false, "");
                    }
                }
            }
            return Json(new { });
        }

        public ActionResult Edit(int id)
        {
            T_NB_Meeting t_nb_meeting = db.T_NB_Meeting.Find(id);
            return View(t_nb_meeting);
        }

        [HttpPost]
        public ActionResult Edit(int id,FormCollection collection)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    T_NB_Meeting t_nb_meeting = db.T_NB_Meeting.Find(id);
                    if (t_nb_meeting.State == 0)
                    {
                        t_nb_meeting.Title = collection["Title"];
                        t_nb_meeting.Address = collection["Address"];
                        t_nb_meeting.Number = int.Parse(collection["Number"]);
                        t_nb_meeting.UsedTime = int.Parse(collection["UsedTime"]);
                        t_nb_meeting.MeetingTime = Convert.ToDateTime(collection["MeetingTime"]);
                        t_nb_meeting.CreatedTime =  DateTime.Now;

                        int result = db.SaveChanges();
                        if (result >= 0)
                            return ReturnJson(true, "操作成功", "", "", true, "");
                        else
                            return ReturnJson(false, "操作失败", "", "", false, "");
                    }
                    else
                    {
                        return ReturnJson(false, "该预定已审核，不允许修改", "", "", false, "");
                    }

                }
            }
            return Json("");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (Request.IsAjaxRequest())
            {
                T_NB_Meeting t_nb_meeting = db.T_NB_Meeting.Find(id);
                if (t_nb_meeting.State == 0)
                {
                    t_nb_meeting.IsValid = false;
                    int result = db.SaveChanges();
                    if (result > 0)
                        return ReturnJson(true, "操作成功", "", "", false, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
                else
                {
                    return ReturnJson(false, "该预定已审核，不允许删除", "", "", false, "");
                }
            }
            return Json(new { });
        }
        #endregion

        #region 会议审核
        /// <summary>
        /// 会议审核列表
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageNum"></param>
        /// <param name="numPerPage"></param>
        /// <returns></returns>
        public ActionResult MeetingAudit(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            IList<T_NB_Meeting> list = db.T_NB_Meeting.Where(p => p.Title.Contains(keywords) && p.IsValid == true && p.State == 0)
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_NB_Meeting.Where(p => p.Title.Contains(keywords) && p.IsValid == true && p.State == 0).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;

            return View(list);
        }

        public ActionResult Audit(int id,int state)
        {
            T_NB_Meeting t_nb_meeting = db.T_NB_Meeting.Find(id);
            ViewBag.State = state;
            return View(t_nb_meeting);
        }

        [HttpPost]
        public ActionResult Audit(int id,int state,FormCollection collection)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    T_ZC_User current_user = CurrentUser();

                    T_NB_Meeting t_nb_meeting = db.T_NB_Meeting.Find(id);
                    t_nb_meeting.Address = collection["Address"];
                    t_nb_meeting.FeedBack = collection["FeedBack"];
                    t_nb_meeting.State = state;
                    t_nb_meeting.AuditUser = current_user.UserName;

                    t_nb_meeting.AuditTime = DateTime.Now;

                    db.Entry(t_nb_meeting).State = EntityState.Modified;

                    int result = db.SaveChanges();
                    if (result >= 0)
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");

                }
            }
            return Json("");
        }
        #endregion

        #region 会议记录
        /// <summary>
        /// 会议记录列表
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageNum"></param>
        /// <param name="numPerPage"></param>
        /// <returns></returns>
        public ActionResult MeetingRecord(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            IList<T_NB_Meeting> list = db.T_NB_Meeting.Where(p => p.Title.Contains(keywords) && p.IsValid == true && p.State == 1)
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_NB_Meeting.Where(p => p.Title.Contains(keywords) && p.IsValid == true && p.State == 1).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;

            return View(list);
        }

        public ActionResult RecordEdit(int id)
        {
            T_NB_Meeting t_nb_meeting = db.T_NB_Meeting.Find(id);
            return View(t_nb_meeting);
        }

        [HttpPost]
        public ActionResult RecordEdit(int id, FormCollection collection)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    T_ZC_User current_user = CurrentUser();

                    T_NB_Meeting t_nb_meeting = db.T_NB_Meeting.Find(id);

                    t_nb_meeting.Content = collection["Content"];
                    t_nb_meeting.RecordUser = current_user.UserName;
                    t_nb_meeting.IsRecord = true;
                    t_nb_meeting.RecordTime = DateTime.Now;

                    db.Entry(t_nb_meeting).State = EntityState.Modified;
                    int result = db.SaveChanges();
                    if (result >= 0)
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
            }
            return Json("");
        }
        #endregion

        #region 选择用户
        public PartialViewResult SelectUser(int id)
        {
            ZC_RoleController zc_role = new ZC_RoleController();

            string selected_users = zc_role.GenerateStringFromList(db.T_NB_Meeting.Find(id).PartakeUsers.Where(p => p.IsValid == true).ToList());
            ViewBag.selected_users = selected_users;

            var select_user = new VM_SelectUser();
            select_user.Users = db.T_ZC_User.Where(p => p.IsValid == true).ToList();
            select_user.Departments = db.T_ZC_Department.Where(p => p.IsValid == true).ToList();

            ViewBag.MeetingId = id;
            return PartialView(select_user);
        }

        /// <summary>
        /// 选择参与用户
        /// </summary>
        /// <param name="id">文件id</param>
        /// <param name="select_users">用户</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SelectUser(int id, string select_users)
        {
            T_NB_Meeting current_meeting = db.T_NB_Meeting.Find(id);

            if (current_meeting.PartakeUsers.Count == 0 && select_users == "")
            {
                return ReturnJson(false, "请选择用户", "", "", false, "");
            }

            for (int i = current_meeting.PartakeUsers.Count - 1; i >= 0; i--)
            {
                var receive_user = current_meeting.PartakeUsers.ElementAtOrDefault(i);
                if (receive_user != null)
                {
                    current_meeting.PartakeUsers.Remove(receive_user);
                }
            }
            if (select_users != "")
            {
                select_users = select_users.TrimEnd(',');
                string[] user_ids = select_users.Split(',');
                foreach (string user_id in user_ids)
                {
                    T_ZC_User _user = db.T_ZC_User.Find(Convert.ToInt32(user_id));
                    if (_user != null)
                    {
                        current_meeting.PartakeUsers.Add(_user);
                    }
                }
            }
            
            try
            {
                db.SaveChanges();
                return ReturnJson(true, "操作成功", "", "", false, "");
            }
            catch
            {
                return ReturnJson(false, "操作失败", "", "", false, "");
            }
        }
        #endregion
    }
}
