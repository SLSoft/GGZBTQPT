using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.ViewModels;
using GGZBTQPT_PRO.Enums;

namespace GGZBTQPT_PRO.Controllers
{
    public class ZC_RoleController : BaseController
    {
        public ActionResult Index(string keywords, int pageNum = 1, int numPerPage = 15)
        { 
            keywords = keywords == null ? "" : keywords;

            IList<GGZBTQPT_PRO.Models.T_ZC_Role> list = db.T_ZC_Role.Where(p => p.Name.Contains(keywords)).Where(p => p.IsValid == true)
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_ZC_Role.Where(p => p.Name.Contains(keywords)).Where(p => p.IsValid == true).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;

            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult Create(T_ZC_Role t_zc_role)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    t_zc_role.CreatedAt = DateTime.Now;
                    t_zc_role.UpdatedAt = DateTime.Now;
                    t_zc_role.IsValid = true;
                    db.T_ZC_Role.Add(t_zc_role);
                    int result = db.SaveChanges();
                    if (result > 0)
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
            }
            return Json(new { });
        }
        
        public ActionResult Edit(int id)
        {
            T_ZC_Role t_zc_role = db.T_ZC_Role.Find(id);
            return View(t_zc_role);
        }

        [HttpPost]
        public ActionResult Edit(T_ZC_Role t_zc_role)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    t_zc_role.UpdatedAt = DateTime.Now;
                    t_zc_role.IsValid = true;
                    db.Entry(t_zc_role).State = EntityState.Modified;
                    int result = db.SaveChanges();
                    if (result >= 0)
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
            }
            return Json(new { });
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {      
            if (Request.IsAjaxRequest())
            {
                T_ZC_Role t_zc_role = db.T_ZC_Role.Find(id);
                t_zc_role.IsValid = false;
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", false, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
        } 

        [HttpPost]
        public ActionResult CheckUser(FormCollection collection, int id)
        {
            if (Request.IsAjaxRequest())
            {
                bool flag = false;
                int result = 0;
                T_ZC_Role role = db.T_ZC_Role.Find(id);
                string checkIDList = (collection["ID1"] + ",").Replace("false,", "");
                if (checkIDList.Length > 1)
                    checkIDList = checkIDList.Remove(checkIDList.Length - 1);
                string[] arrCheckedID = checkIDList.Split(new char[1] { ',' });
                for (int i = 0; i < arrCheckedID.Length; i++)
                {
                    int userID = Int32.Parse(arrCheckedID[i]);
                    var user_item = db.T_ZC_User.FirstOrDefault(f => f.ID == userID);
                    role.Users.Add(user_item);
                    result = db.SaveChanges();
                    if (result < 0)
                        flag = true;
                }
                if (!flag)
                    return ReturnJson(true, "设置成功", "", "", false, "");
                else
                    return ReturnJson(false, "设置失败", "", "", false, "");
            }
            return Json(new { });
        }

        #region//角色所属用户选择
        public PartialViewResult SelectUser(int id)
        {
            string selected_users = GenerateStringFromList(db.T_ZC_Role.Find(id).Users.Where(p => p.IsValid == true && p.UseLevel == (int)UseLevel.System).ToList());
            ViewBag.selected_users = selected_users;

            var select_user = new VM_SelectUser();
            select_user.Users = db.T_ZC_User.Where(p => p.IsValid == true).ToList();
            select_user.Departments = db.T_ZC_Department.Where(p => p.IsValid == true).ToList();

            ViewBag.RoleID = id;
            return PartialView(select_user);
        }

        public string GenerateStringFromList(ICollection<T_ZC_User> users)
        {
            string select_users = "";
            foreach (T_ZC_User user in users)
            {
                select_users += user.ID + "|";
            }
            return select_users;
        }

        [HttpPost]
        public ActionResult SelectUser(int id, string select_users)
        {
            T_ZC_Role current_role = db.T_ZC_Role.Find(id);
            select_users = RemoveTheLastComma(select_users);
            string[] user_ids = select_users.Split(','); 

            foreach( string user_id in user_ids)
            {
                T_ZC_User _user = db.T_ZC_User.Find(Convert.ToInt32(user_id));
                current_role.Users.Add(_user);
            }

            try
            {
                db.SaveChanges();
                return ReturnJson(true, "用户设置成功", "", "", false, "");
            }
            catch
            {
                return ReturnJson(false, "用户设置失败", "", "", false, "");
            }
        }
        #endregion

        #region//角色所拥有的功能菜单选择
        public ActionResult SetPurview(int id)
        {
            var selected_menus = db.T_ZC_Role.Find(id).Menus.Where(m => m.IsValid == true).ToList();
            ViewBag.SelectedMenus = GenerateStringFromMenus(selected_menus);

            VM_SystemMenu system_and_menu = new VM_SystemMenu();
            system_and_menu.Menus = db.T_ZC_Menu.Where(m => m.IsValid == true).ToList();
            system_and_menu.Systems = db.T_ZC_System.Where(s => s.IsValid == true).ToList();

            ViewBag.RoleID = id;
            return PartialView(system_and_menu);
        }

        public string GenerateStringFromMenus(ICollection<T_ZC_Menu> menus)
        {
            string select_menus = "";
            foreach (T_ZC_Menu menu in menus)
            {
                select_menus += menu.ID + "|";
            }
            return select_menus;
        }

        [HttpPost]
        public ActionResult SetPurview(int id, string select_menus)
        {
            T_ZC_Role current_role = db.T_ZC_Role.Find(id);
            current_role.Menus.Clear();

            select_menus = RemoveTheLastComma(select_menus);
            string[] menu_ids = select_menus.Split(',');

            foreach (string menu_id in menu_ids)
            {
                T_ZC_Menu _menu = db.T_ZC_Menu.Find(Convert.ToInt32(menu_id));
                current_role.Menus.Add(_menu);
            }

            try
            {
                db.SaveChanges();
                return ReturnJson(true, "功能菜单设置成功", "", "", false, "");
            }
            catch
            {
                return ReturnJson(false, "功能菜单设置失败", "", "", false, "");
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