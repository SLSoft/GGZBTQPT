using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_QY_Financial
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string CorpCode { get; set; }
        public Nullable<decimal> TotalAssets { get; set; }
        public string CurYear { get; set; }
        public Nullable<decimal> Revenue { get; set; }
        public string IsPublic { get; set; }
        public Nullable<bool> IsValid { get; set; }
        public string OP { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}
