using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
    public class T_HY_Member
    {
        public T_HY_Member()
        {
            IsValid = true;
        }
        public int ID { get; set; }

        [Required(ErrorMessage = "必须填写登录名")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "必须填写登陆密码")]
        public string Password { get; set; }

        [Required(ErrorMessage = "必须填写手机号码")]
        public string CellPhone { get; set; }


        public int Number { get; set; } //会员编号 

        [Required(ErrorMessage = "必须选择会员类型")]
        public int Type { get; set; } //会员类别 1代表企业，2代表企业，3代表机构

        public int Intetral { get; set; } //会员积分

 
        public Boolean IsValid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 

        public virtual ICollection<T_HY_Attention> Attentions { get; set; }//会员能够关注多个人（企业），也能被多个人（企业）关注


        public virtual ICollection<T_HY_Favorite> Favorites { get; set; }//会员能够收藏多个项目、资本、服务
    }


}