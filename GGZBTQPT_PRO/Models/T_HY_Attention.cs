using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_HY_Attention
    {
        public int ID { get; set; }
        public int AttentionType { get; set; } //��ע���ͣ���ҵ/���˻����{ ���ˣ�1�� ��ҵ��2�� ������3 }
        public int AttentionedID { get; set; } //��ע�ĸ���/��ҵ�����ID

        public Nullable<bool> IsValid { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
 
        public virtual ICollection<T_HY_Member> Members { get; set; }

        //********TO-DO**********//
        //�޸Ĺ�ע��ӳ��,Ӧ�ø�Ϊ��ע��Ա���������ǹ�ע��ҵ�����
    }
}
