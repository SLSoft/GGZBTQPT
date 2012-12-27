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
            IsVerified = false;
            State = 0;
        }
        public int ID { get; set; }

        [Required(ErrorMessage = "必须填写登录名")]
        [Display(Name = "登录名")]
        [Remote("CheckLoginName", "HY_Member", ErrorMessage = "该登录名已经被注册了")]
        public string LoginName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "登录密码")]
        public string Password { get; set; }

        [Required(ErrorMessage = "必须填写手机号码")]
        [Display(Name = "手机号码")]
        [Remote("CheckCellPhone", "HY_Member", ErrorMessage = "该手机已经被使用了，请尝试更改!")]
        public string CellPhone { get; set; }

        [Required(ErrorMessage = "必须填写昵称")]
        [Display(Name = "昵称")]
        public string MemberName { get; set; }//默认是登录名，用户可以修改 

        [Display(Name = "邮箱")] 
        public string Email { get; set; }

        [Display(Name = "会员编号")] 
        public int Number { get; set; } //会员编号 

        [Required(ErrorMessage = "必须选择会员类型")]
        [Display(Name = "注册会员类型")]
        public int Type { get; set; }

        [Display(Name = "会员积分")] 
        public int Intetral { get; set; } //会员积分
        
        public Boolean IsValid { get; set; }
        public Boolean IsVerified { get; set; }//该会员是否通过审核
        public int State { get; set; }//会员状态 0：待审核 1：审核通过 2：驳回
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 

        public string TypeName
        {
            get
            {
                switch(this.Type)
                {
                    case 1:
                        return "个人";
                    case 2:
                        return "企业";
                    case 3:
                        return "机构";
                }
                return "其他";
            }
        }

        public string StateName
        {
            get
            {
                switch (this.State)
                {
                    case 0:
                        return "待审核";
                    case 1:
                        return "通过";
                    case 2:
                        return "驳回";
                }
                return "";
            }
        }

        public virtual ICollection<T_HY_Attention> Attentions { get; set; }//会员能够关注多个人（企业），也能被多个人（企业）关注 
        public virtual ICollection<T_HY_Favorite> Favorites { get; set; }//会员能够收藏多个项目、资本、服务

        public virtual ICollection<T_XM_Financing> Financials { get; set; }//会员所发布的项目
        public virtual ICollection<T_XM_Investment> Investments { get; set; }//会员所发布的意向
        public virtual ICollection<T_JG_Product> Products { get; set; }//会员所发布的金融产品

        public virtual ICollection<T_ZC_OnlineLog> OnlineLogs { get; set; } //会员的在线日志

        public virtual ICollection<T_HY_Message> SendedMessages { get; set; }//会员发送的站内消息
        public virtual ICollection<T_HY_Message> ReceivedMessages { get; set; }//会员接收的站内消息
        
        
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
