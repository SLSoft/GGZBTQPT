using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_JG_Agency
    {
        public int ID { get; set; } 

        public string AgencyName { get; set; }
        public Nullable<int> AgencyType { get; set; }
        public Nullable<System.DateTime> RegTime { get; set; }
        public string Address { get; set; }
        public Nullable<decimal> RegCapital { get; set; }
        public Nullable<int> Province { get; set; }
        public Nullable<int> City { get; set; }
        public Nullable<int> Region { get; set; }
        public string Phone { get; set; }
        public string Services { get; set; }
        public string Remark { get; set; }
        public Boolean IsValid { get; set; }
        public int OP { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public Boolean IsIn { get; set; }
        public byte[] Pic { get; set; }
        public int MemberID { get; set; }

        public string AgencyTypeName
        {
            get
            {
                GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                return db.T_PTF_DicDetail.Find(this.AgencyType).Name;
            }
        }

        public virtual ICollection<T_JG_Linkman> Linkmans { get; set; }
    }
}
