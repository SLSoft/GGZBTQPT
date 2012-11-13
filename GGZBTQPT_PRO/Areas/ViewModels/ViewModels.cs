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

    public class VM_SignUp
    { 

        [Required(ErrorMessage = "必须填写登录名")]
        [Display(Name = "登录名")]
        [Remote("CheckLoginName", "Member", ErrorMessage = "该登录名已经被注册了")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "必须填写会员名")]
        [Display(Name = "会员名")]
        public string MemberName { get; set; }//默认是登录名，用户可以修改
 
        [Required(ErrorMessage = "必须填写手机号码")]
        [Display(Name = "手机号码")]
        [Remote("CheckCellPhone", "Member", ErrorMessage = "该手机已经被使用了，请尝试更改!")]
        public string CellPhone { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "必须填写登陆密码")]
        [Display(Name = "登录密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码必须一致")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "必须选择会员类型")]
        [Display(Name = "注册会员类型")]
        public int Type { get; set; } 

    }

    public class VM_EditMember
    {

        [Required(ErrorMessage = "必须填写会员名")]
        [Display(Name = "会员名")]
        [Remote("CheckMemberName", "Member", ErrorMessage = "该用户名已经被使用了，请尝试更改!")]
        public string MemberName { get; set; }//默认是登录名，用户可以修改

        [Required(ErrorMessage = "必须填写手机号码")]
        [Display(Name = "手机号码")]
        [Remote("CheckCellPhoneForRegistered", "Member", ErrorMessage = "该手机已经被使用了，请尝试更改!")]
        public string CellPhone { get; set; }

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