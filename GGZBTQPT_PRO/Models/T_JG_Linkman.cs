using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_JG_Linkman
    {
        public int ID { get; set; }
        public int AgencyID { get; set; }
        public string Name { get; set; }
        public string Business { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsValid { get; set; }
        public int OP { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }

        public virtual T_JG_Agency Agency { get; set; }
    }
}
