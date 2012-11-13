using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;


namespace GGZBTQPT_PRO.Models
{

    //public  class T_HY_Member : IValidatableObject
    public  class T_HY_Member 
    {
        public T_HY_Member()
        {
            IsValid = true;
        }
        public int ID { get; set; }

        [Required(ErrorMessage = "必须填写登录名")]
        [Display(Name = "登录名")]
        [Remote("CheckLoginName", "Member", ErrorMessage = "该登录名已经被注册了")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "必须填写登陆密码")]
        [Display(Name = "登录密码")]
        public string Password { get; set; }

        [Required(ErrorMessage = "必须填写手机号码")]
        [Display(Name = "手机号码")]
        [Remote("CheckCellPhone", "Member", ErrorMessage = "该手机已经被使用了，请尝试更改!")]
        public string CellPhone { get; set; }

        [Display(Name = "会员名")]
        public string MemberName { get; set; }//默认是登录名，用户可以修改



        public int Number { get; set; } //会员编号 

        [Required(ErrorMessage = "必须选择会员类型")]
        [Display(Name = "注册会员类型")]
        public int Type { get; set; } 

        public int Intetral { get; set; } //会员积分

 
        public Boolean IsValid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 

        public virtual ICollection<T_HY_Attention> Attentions { get; set; }//会员能够关注多个人（企业），也能被多个人（企业）关注 
        public virtual ICollection<T_HY_Favorite> Favorites { get; set; }//会员能够收藏多个项目、资本、服务

        public virtual ICollection<T_XM_Financing> Financials { get; set; }//会员所发布的项目
        public virtual ICollection<T_XM_Investment> Investments { get; set; }//会员所发布的意向
        
        
        // Validate method
        // 当需要处理模型内部的一些验证逻辑的时候，可以采用下面的方式
        //public IEnumerable<ValidationResult> Validate(ValidationContext validation_context)
        //{
            
        //    using(GGZBTQPTDBContext db = new GGZBTQPTDBContext())
        //    {
        //        if(db.T_HY_Member.Any(m => m.LoginName == LoginName))
        //        {
        //            yield return new ValidationResult("登陆名重名，请尝试修改！", new [] { "LoginName" });
        //        }

        //        if(db.T_HY_Member.Any(m => m.CellPhone == CellPhone))
        //        {
        //            yield return new ValidationResult("该手机已经被注册，请更换手机号码！", new [] { "CellPhone" }); 
        //        }
        //    }
        //}

        //--------------------Helper-----------------------------//
        public static T_HY_Member CurrentMemberByLoginname(string login_name)
        {
            using(GGZBTQPTDBContext db = new GGZBTQPTDBContext())
            {
                try
                {
                    var member = db.T_HY_Member.First(m => m.LoginName == login_name);
                    return member;
                }
                catch
                {
                    return null;
                } 
            }
        }

        public static string EncryptPwd(string password)
        {
            //---------TO-DO-----------------//
            //增加密码的加密方法
            return password;
        }


    } 
 }