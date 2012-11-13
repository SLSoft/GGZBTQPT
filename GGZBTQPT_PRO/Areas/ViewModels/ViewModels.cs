using System;
using System.Collections.Generic;
using GGZBTQPT_PRO.Models;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Areas.ViewModels
{
    public class VM_AttentionedPerson
    {
        public T_HY_Member Member { get; set; }
        public T_QY_Person Person { get; set; }
    }

    public class VM_AttentionedCorp
    {
        public T_HY_Member Member { get; set; }
        public T_QY_Corp Corp { get; set; }
    }

    public class VM_AttentionedAgency
    {
        public T_HY_Member Member { get; set; }
        public T_JG_Agency Agency { get; set; }
    }

    public class ValidateMember
    {
        public T_HY_Member Member { get; set; }



        [DataType(DataType.Password)]
        [Required(ErrorMessage = "必须填写登陆密码")]
        [Display(Name = "登录密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码必须一致")]
        public string ConfirmPassword { get; set; }

    }
}