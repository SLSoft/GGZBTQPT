using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.Util;
using GGZBTQPT_PRO.Enums;
using GGZBTQPT_PRO.ViewModels;


namespace GGZBTQPT_PRO.Controllers
{ 
    public class HomeController : Controller
    {
        public GGZBTQPT_PRO.Models.GGZBTQPTDBContext db = new GGZBTQPT_PRO.Models.GGZBTQPTDBContext();

        public ActionResult Index()
        {
            var current_user = CurrentUser();
            if (current_user == null)
            {
                return RedirectToAction("Login");
            }
            return View(current_user);
        }

        public ActionResult SystemLink()
        {
            var current_user = CurrentUser();
            var system_links = db.T_ZC_System.OrderBy(s => s.Index)
                                 .Where(s => s.IsValid == true)
                                 .ToList();

            if(current_user.LoginName == "admin")
            {
                return PartialView(system_links);
            }

            List<int> system_rights = GetSystemRightFromCurrentUser(current_user.Roles); 
            system_links = db.T_ZC_System.OrderBy(s => s.ID)
                                 .Where(s => (s.IsValid == true && system_rights.Contains(s.ID)))
                                 .ToList(); 
            return PartialView(system_links);
        }

        public ActionResult SystemMenus(int id = 1)
        { 
            var current_user = CurrentUser();
            var system = db.T_ZC_System.Find(id);
            var menu_links = system.Menus.OrderBy(m => m.Index)
                       .Where(m => m.IsValid == true)
                       .ToList();

            if (current_user.LoginName == "admin")
            {
                return PartialView(menu_links);
            }

            List<int> menu_rights = GetMenusRightFromCurrentUser(current_user.Roles);

            menu_links = system.Menus.OrderBy(m => m.ID)
                                   .Where(m => (m.IsValid == true && menu_rights.Contains(m.ID)))
                                   .ToList();

            return PartialView(menu_links);
        }

        #region//用户个人办公主页信息汇总
        
        public ActionResult Home()
        {
            T_ZC_User user = CurrentUser(); 
            VM_InformationCollect information_collect = new VM_InformationCollect();

            information_collect.Files = user.ReceiveFiles.Count() > 5 ? user.ReceiveFiles.Take(5).ToList() : user.ReceiveFiles;
            information_collect.Meetings = db.T_NB_Meeting.OrderBy(m => m.AuditTime).Count() > 5  ? db.T_NB_Meeting.OrderBy(m => m.AuditTime).Take(5).ToList() : db.T_NB_Meeting.OrderBy(m => m.AuditTime).ToList();
            information_collect.Financials = db.T_XM_Financing.Where(p => p.IsValid == true).OrderByDescending(f => f.CreateTime).Count() > 5 ? db.T_XM_Financing.Where(p => p.IsValid == true).OrderByDescending(f => f.CreateTime).Take(5).ToList() : db.T_XM_Financing.Where(p => p.IsValid == true).OrderByDescending(f => f.CreateTime).ToList();
            information_collect.Investments = db.T_XM_Investment.Where(p => p.IsValid == true).OrderByDescending(f => f.CreateTime).Count() > 5 ? db.T_XM_Investment.Where(p => p.IsValid == true).OrderByDescending(f => f.CreateTime).Take(5).ToList() : db.T_XM_Investment.Where(p => p.IsValid == true).OrderByDescending(f => f.CreateTime).ToList();


            return View(information_collect);
        }

        #endregion

        #region//登陆登出相关功能
        //登录
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string loginname, string password)
        {
            try
            {
                T_ZC_User user = db.T_ZC_User.Where(u => u.LoginName == loginname).First();
                if (user != null && user.Password == password)
                {
                    Session["UserID"] = user.ID;
                    Session["UserName"] = user.UserName;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["error"] = "密码错误，请检查后重新尝试!";
                    return View();
                }
            }
            catch
            {
                ViewData["error"] = "登陆名错误，请检查后重新尝试!";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["UserID"] = null; 
            Session["UserName"] = null;
            return RedirectToAction("Login", "Home");
        }

        #endregion

        //
        //Helper
        public T_ZC_System FindSystemByID(int id)
        {
            var system = db.T_ZC_System.Find(id);
            return system;
        }
 
        protected T_ZC_User CurrentUser()
        {
            if (Session["UserID"] != null && Session["UserID"].ToString() != "")
            {
                var user = db.T_ZC_User.Find(Convert.ToInt32(Session["UserID"].ToString()));
                return user;
            }
            return null;
        }

        private List<int> GetSystemRightFromCurrentUser(IEnumerable<T_ZC_Role> roles)
        {
            List<int> system_rights = new List<int>();

            var systems = db.T_ZC_System.Include("Menus").ToList();
            List<int> menu_rights = GetMenusRightFromCurrentUser(roles);

            foreach(var system in systems)
            {
                foreach(var menu in system.Menus)
                {
                    if(menu_rights.Contains(menu.ID))
                    {
                        system_rights.Add(system.ID);
                        break;
                    }
                }
            } 

            return system_rights;
        }

        private List<int> GetMenusRightFromCurrentUser(IEnumerable<T_ZC_Role> roles)
        {
            List<int> menu_rights = new List<int>();

            foreach (var role in roles)
            {
                foreach (var menu in role.Menus)
                {
                    menu_rights.Add(menu.ID);
                }
            }

            return menu_rights;
        }

    }
}
