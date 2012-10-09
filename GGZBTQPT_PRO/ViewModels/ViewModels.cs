using System;
using System.Collections.Generic;
using GGZBTQPT_PRO.Models;

namespace GGZBTQPT_PRO.ViewModels
{
    public class VM_SelectUser
    {
        public ICollection<T_ZC_User> Users { get; set; }
        public ICollection<T_ZC_Department> Departments { get; set; }
        public ICollection<T_ZC_Role> Roles { get; set; }
        public ICollection<T_ZC_System> Systems { get; set; }
    }



}