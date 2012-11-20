﻿using System;
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

        [Required(ErrorMessage = "必须填写昵称")]
        [Display(Name = "昵称")]
        public string MemberName { get; set; }
 
        [Required(ErrorMessage = "必须填写手机号码")]
        [Display(Name = "手机号码")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "手机格式不正确")]
        [DataType(DataType.PhoneNumber)]
        [Remote("CheckCellPhone", "Member", ErrorMessage = "该手机已经被使用了，请尝试更改!")]
        public string CellPhone { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "必须填写登陆密码")]
        [Display(Name = "登录密码")]
        [RegularExpression(@"^[a-zA-Z]\w{5,17}$", ErrorMessage = "密码格式不正确")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码必须一致")]
        [RegularExpression(@"^[a-zA-Z]\w{5,17}$", ErrorMessage = "密码格式不正确")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "邮箱")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "邮件格式不正确")]
        [Required(ErrorMessage = "必须填写邮箱地址")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "必须选择会员类型")]
        [Display(Name = "注册会员类型")]
        public int Type { get; set; } 

    }

    public class VM_EditMember
    {

        [Required(ErrorMessage = "必须填写昵称")]
        [Display(Name = "昵称")]
        [Remote("CheckMemberName", "Member", ErrorMessage = "该用户名已经被使用了，请尝试更改!")]
        public string MemberName { get; set; }//默认是登录名，用户可以修改

        [Required(ErrorMessage = "必须填写手机号码")]
        [Display(Name = "手机号码")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "手机格式不正确")]
        [DataType(DataType.PhoneNumber)]
        [Remote("CheckCellPhoneForRegistered", "Member", ErrorMessage = "该手机已经被使用了，请尝试更改!")]
        public string CellPhone { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "必须填写登陆密码")]
        [Display(Name = "登录密码")]
        [RegularExpression(@"^[a-zA-Z]\w{5,17}$", ErrorMessage = "密码格式不正确")]
        public string Password { get; set; }

        [Display(Name = "邮箱")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "邮件格式不正确")]
        [Required(ErrorMessage = "必须填写邮箱地址")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码必须一致")]
        public string ConfirmPassword { get; set; } 

    }
}