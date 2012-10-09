using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_HY_Attention
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string AttentionCode { get; set; }
        public Nullable<System.DateTime> KeepTime { get; set; }
        public Nullable<bool> IsValid { get; set; }
        public string OP { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}
