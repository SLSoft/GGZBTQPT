using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Enums
{
    //会员类别
    public enum MemberTypes
    {
        [Display(Name = "个人")]
        Corp = 1,
        [Display(Name = "企业")]
        Person = 2,
        [Display(Name = "机构")]
        Agency = 3

    }
}
