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
}
