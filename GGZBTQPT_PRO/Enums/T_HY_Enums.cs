using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Enums
{
    //会员类别
    public enum MemberTypes : int
    {
        [Display(Name = "个人")]
        个人 = 1,
        [Display(Name = "企业")]
        企业 = 2,
        [Display(Name = "机构")]
        机构 = 3

    }

    public enum LogTypes : int
    { 
        //操作日志、登录日志、错误日志、警告日志（用于操作失败）
        operate, login, error, warn
    }

    public enum CaseTypes : int
    {
        [Display(Name = "融资项目")]
        Financing, 
        [Display(Name = "投资项目")] 
        Investment
    }

    //使用级别，用于系统用户分类，部门分类等，是为了区分系统用户和系统内置的管理用户
    public enum UseLevel : int
    { 
        [Display(Name = "用户级别")]
        System, 
        [Display(Name = "管理级别")] 
        Manage
    }
}
