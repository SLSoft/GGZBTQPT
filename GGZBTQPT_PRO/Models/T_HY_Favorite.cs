using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_HY_Favorite
    {
        public int ID { get; set; }
        public int FavoriteType { get; set; }//�ղ����� 1������Ŀ 2����Ͷ�� 3�������
        public int FavoriteID { get; set; }//�ղص���Ŀ��ID //�´θ���Ӧ�ø�ΪFavoriteItemID


        public Nullable<bool> IsValid { get; set; } 
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }

        public virtual ICollection<T_HY_Member>  Members { get; set; }

        public virtual T_XM_Financing Financial { get; set; }

        public T_HY_Favorite()
        {
            IsValid = true;
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        } 
    }
}
