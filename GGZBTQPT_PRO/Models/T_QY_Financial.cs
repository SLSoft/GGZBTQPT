using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_QY_Financial
    {
        public int ID { get; set; }
        public int CorpID { get; set; }
        public Nullable<decimal> TotalAssets { get; set; }
        public string CurYear { get; set; }
        public Nullable<decimal> Revenue { get; set; }
        public string IsPublic { get; set; }
        public Nullable<bool> IsValid { get; set; }
        public System.Guid OP { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }

        public virtual T_QY_Corp Corp { get; set; }
    }
}
