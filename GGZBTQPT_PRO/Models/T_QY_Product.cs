using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_QY_Product
    {
        public int ID { get; set; }
        public int CorpID { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> SalesTotal { get; set; }
        public string Content { get; set; }
        public string IsPublic { get; set; }
        public Nullable<bool> IsValid { get; set; }
        public int OP { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }

        public virtual T_QY_Corp Corp { get; set; }
    }
}
