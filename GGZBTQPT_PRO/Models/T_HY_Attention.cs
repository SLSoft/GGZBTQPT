using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
    public class T_HY_Attention
    {
        public int ID { get; set; }

        [Display(Name = "关注的会员类型")]
        public int AttentionedMemberType { get; set; } //关注的会员类型

        public int AttentionedMemberID { get; set; } //关注的会员ID

        public Nullable<bool> IsValid { get; set; }

        [Display(Name = "关注的时间")]
        public Nullable<System.DateTime> CreateTime { get; set; }

        [Display(Name = "更新的时间")]
        public Nullable<System.DateTime> UpdateTime { get; set; }
 
        public virtual ICollection<T_HY_Member> Members { get; set; }



        //********TO-DO**********//
        //修改关注的映射,应该改为关注会员自身，而不是关注企业或个人

        public T_HY_Attention()
        {
            IsValid = true;
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        } 
    }
}
