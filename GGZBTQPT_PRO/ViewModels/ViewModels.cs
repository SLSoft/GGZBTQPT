using System;
using System.Collections.Generic;
using GGZBTQPT_PRO.Models;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Webdiyer.WebControls.Mvc;

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

   /// <summary>
    /// 会员查询
    /// </summary>
    public class VM_SelectMember
    {
        public PagedList<T_XM_Financing> Financings { get; set; }
        public PagedList<T_XM_Investment> Investments { get; set; }
        public PagedList<T_JG_Product> Products { get; set; }
    }

    public class VM_MemberStat
    {
        public int FinancingCount { get; set; }
        public int InvestmentCount { get; set; }
        public int ProductCount { get; set; }
        public T_HY_Member Member { get; set; }
    }

    public class VM_Member_Stat
    {
        public int MemberCount { get; set; }
        public int Type { get; set; }
        public T_HY_Member Member { get; set; }
    }
}
