using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_HY_Attention
    {
        public int ID { get; set; }
        public int AttentionType { get; set; } //关注类型，企业/个人或机构{ 个人：1， 企业：2， 机构：3 }
        public int AttentionedID { get; set; } //关注的个人/企业或机构ID

        public Nullable<bool> IsValid { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
 
        public virtual ICollection<T_HY_Member> Members { get; set; }

        //********TO-DO**********//
        //修改关注的映射,应该改为关注会员自身，而不是关注企业或个人
    }
}
