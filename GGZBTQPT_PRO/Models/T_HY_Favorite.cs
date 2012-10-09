using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_HY_Favorite
    {
        public int ID { get; set; }
        public int Code { get; set; }
        public string FavoriteCode { get; set; }
        public Nullable<System.DateTime> KeepTime { get; set; }
        public Nullable<bool> IsValid { get; set; }
        public int OP { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}
