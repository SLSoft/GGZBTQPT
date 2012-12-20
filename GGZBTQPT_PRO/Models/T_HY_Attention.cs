using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
    public class T_HY_Attention
    {
        public int ID { get; set; }

        [Display(Name = "��ע�Ļ�Ա����")]
        public int AttentionedMemberType { get; set; } //��ע�Ļ�Ա����

        public int AttentionedMemberID { get; set; } //��ע�Ļ�ԱID

        public Nullable<bool> IsValid { get; set; }

        [Display(Name = "��ע��ʱ��")]
        public Nullable<System.DateTime> CreateTime { get; set; }

        [Display(Name = "���µ�ʱ��")]
        public Nullable<System.DateTime> UpdateTime { get; set; }
 
        public virtual ICollection<T_HY_Member> Members { get; set; }



        //********TO-DO**********//
        //�޸Ĺ�ע��ӳ��,Ӧ�ø�Ϊ��ע��Ա���������ǹ�ע��ҵ�����

        public T_HY_Attention()
        {
            IsValid = true;
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        } 
    }
}
