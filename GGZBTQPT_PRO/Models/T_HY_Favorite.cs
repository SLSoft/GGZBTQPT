using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_HY_Favorite
    {
        public int ID { get; set; }
        public int FavoriteType { get; set; }//收藏类型 1代表项目 2代表投资 3代表服务
        public int FavoriteID { get; set; }//收藏的项目的ID //下次更新应该改为FavoriteItemID


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
