using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_JG_Linkman
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string AgencyCode { get; set; }
        public string Name { get; set; }
        public string Business { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsValid { get; set; }
        public string OP { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}
