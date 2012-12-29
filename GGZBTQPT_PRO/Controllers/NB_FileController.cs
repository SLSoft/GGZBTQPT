using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using System.IO;
using GGZBTQPT_PRO.Enums;
using GGZBTQPT_PRO.ViewModels;
using System.Data;

namespace GGZBTQPT_PRO.Controllers
{
    public class NB_FileController : BaseController
    {
        //
        // GET: /NB_File/

        #region 列表
        //发送文件列表
        public ActionResult SendList(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            T_ZC_User current_user = CurrentUser();

            IList<T_NB_File> list = db.T_NB_File.Where(p =>p.SendUserId == current_user.ID && p.Title.Contains(keywords) && p.IsValid == true)
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_NB_File.Where(p => p.Title.Contains(keywords) && p.IsValid == true).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;

            return View(list);
        }

        // 接收文件列表
        public ActionResult ReceivedList(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            T_ZC_User current_user = CurrentUser();

            IList<T_NB_File> list = current_user.ReceiveFiles.Where(p => p.Title.Contains(keywords) && p.IsValid == true)
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = current_user.ReceiveFiles.Where(p => p.Title.Contains(keywords) && p.IsValid == true).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;

            return View(list);
        }

        //文件分类管理
        public ActionResult FileList(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            IList<T_NB_File> list = db.T_NB_File.Where(p=> p.Title.Contains(keywords) && p.IsValid == true)
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();
            ViewBag.recordCount = db.T_NB_File.Where(p => p.Title.Contains(keywords) && p.IsValid == true).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            return View(list);
        }
        #endregion

        #region 新增
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public ActionResult Create(T_NB_File t_nb_file)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    t_nb_file.CreatedTime = DateTime.Now;
                    t_nb_file.UpdatedTime = DateTime.Now;
                    t_nb_file.SendUserId = CurrentUser().ID;

                    if (Session["NbFile"] != null && Session["NbFile"].ToString() != "")
                    {
                        Stream stream = (Stream)Session["NbFile"];
                        //存入文件
                        if (stream.Length > 0)
                        {
                            t_nb_file.File = new byte[stream.Length];
                            stream.Read(t_nb_file.File, 0, t_nb_file.File.Length);
                        }
                    }
                    else
                    {
                        return ReturnJson(false, "请选择上传文件", "", "", false, "");
                    }

                    db.T_NB_File.Add(t_nb_file);

                    int result = db.SaveChanges();
                    if (result > 0)
                    {
                        Logging((int)LogLevels.operate, "成功新增了一条内部文件:" + t_nb_file.Title, (int)OperateTypes.Create, (int)GenerateTypes.FromUser);
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    }
                    else
                    {
                        Logging((int)LogLevels.warn, "新增文件失败:" + t_nb_file.Title, (int)OperateTypes.Create, (int)GenerateTypes.FromUser);
                        return ReturnJson(false, "操作失败", "", "", false, "");
                    }
                }
            }
            return Json(new { });
        }

        #endregion

        #region 编辑
        public ActionResult Edit(int id)
        {
            T_NB_File t_nb_file = db.T_NB_File.Find(id);
            return View(t_nb_file);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    T_NB_File t_nb_file = db.T_NB_File.Find(id);
                    t_nb_file.Title = collection["Title"];
                    t_nb_file.UpdatedTime = DateTime.Now;

                    if (Session["NbFile"] != null && Session["NbFile"].ToString() != "")
                    {
                        Stream stream = (Stream)Session["NbFile"];
                        //存入文件
                        if (stream.Length > 0)
                        {
                            t_nb_file.File = new byte[stream.Length];
                            stream.Read(t_nb_file.File, 0, t_nb_file.File.Length);
                        }
                    }
                    db.Entry(t_nb_file).State = EntityState.Modified;
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

        #region 删除
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteFile(int id)
        {
            if (Request.IsAjaxRequest())
            {
                T_NB_File t_nb_file = db.T_NB_File.Find(id);
                t_nb_file.IsValid = false;
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", false, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
        }
        #endregion

        #region 选择用户 发送用户
        /// <summary>
        /// 选择发送用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult SelectUser(int id)
        {
            ZC_RoleController zc_role = new ZC_RoleController();

            //查询接收改文件的用户
            string selected_users = zc_role.GenerateStringFromList(db.T_NB_File.Find(id).ReceiveUsers.Where(p => p.IsValid == true).ToList());
            ViewBag.selected_users = selected_users;

            var select_user = new VM_SelectUser();
            select_user.Users = db.T_ZC_User.Where(p => p.IsValid == true).ToList();
            select_user.Departments = db.T_ZC_Department.Where(p => p.IsValid == true).ToList();

            ViewBag.FileId = id;
            return PartialView(select_user);
        }

        /// <summary>
        /// 选择发送用户
        /// </summary>
        /// <param name="id">文件id</param>
        /// <param name="select_users">用户</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SelectUser(int id, string select_users)
        {
            T_NB_File current_file = db.T_NB_File.Find(id);

            if (current_file.ReceiveUsers.Count == 0 && select_users == "")
            {
                return ReturnJson(false, "请选择用户", "", "", false, "");
            }

            for (int i = current_file.ReceiveUsers.Count - 1; i >= 0; i--)
            {
                var receive_user = current_file.ReceiveUsers.ElementAtOrDefault(i);
                if (receive_user != null)
                {
                    current_file.ReceiveUsers.Remove(receive_user);
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
                        current_file.ReceiveUsers.Add(_user);
                    }
                }
            }
            try
            {
                db.SaveChanges();
                return ReturnJson(true, "操作成功", "", "", true, "");
            }
            catch
            {
                return ReturnJson(false, "操作失败", "", "", false, "");
            }
        }
        #endregion

        #region 文件上传 下载
        [HttpPost]
        public ActionResult TemporariedFile()
        {
            try
            {
                Stream stream = Request.Files.Count > 0
                                        ? Request.Files[0].InputStream
                                        : Request.InputStream;

                Session["NbFile"] = stream;
                return Json(new { success = "上传成功!" });
            }
            catch
            {
                return Json(new { error = "上传失败!" });
            }
        }

        public ActionResult DownLoadFile(int id)
        {
            var nb_file = db.T_NB_File.Find(id);
            byte[] file;
            if (nb_file.File != null)
                file = nb_file.File;
            else
                file = new byte[1];
            return File(file, "application/msword",nb_file.Title);
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
