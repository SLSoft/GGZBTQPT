using System;
using System.Collections.Generic;
using GGZBTQPT_PRO.Models;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.ViewModels
{
    public class VM_SelectUser
    {
        public ICollection<T_ZC_User> Users { get; set; }
        public ICollection<T_ZC_Department> Departments { get; set; }
    }

    public class VM_SystemMenu
    {
        public ICollection<T_ZC_System> Systems { get; set; }
        public ICollection<T_ZC_Menu> Menus { get; set; } 
    }

   

}