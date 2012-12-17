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
        [Display(Name = "全部")]
        全部 = -1,
        [Display(Name = "个人")]
        个人 = 1,
        [Display(Name = "企业")]
        企业 = 2,
        [Display(Name = "机构")]
        机构 = 3
    }

    //会员状态
    public enum MemberStates : int
    {
        //[Display(Name = "待审核")]
        //待审核 = 0,
        [Display(Name = "通过")]
        通过 = 1,
        [Display(Name = "驳回")]
        驳回 = 2
    }

    public enum LogTypes : int
    { 
        //操作日志、登录日志、错误日志、警告日志（用于操作失败）
        operate, login, error, warn
    }
}

