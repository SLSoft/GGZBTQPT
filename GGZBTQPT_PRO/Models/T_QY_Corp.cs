using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_QY_Corp
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string CorpName { get; set; }
        public string CorpCode { get; set; }
        public Nullable<System.DateTime> RegTime { get; set; }
        public Nullable<int> Property { get; set; }
        public Nullable<int> Province { get; set; }
        public Nullable<int> City { get; set; }
        public Nullable<int> Region { get; set; }
        public Nullable<decimal> RegCapital { get; set; }
        public Nullable<int> Industry { get; set; }
        public Nullable<int> Stage { get; set; }
        public string LegalPerson { get; set; }
        public Nullable<int> Employees { get; set; }
        public string Website { get; set; }
        public byte[] Logo { get; set; }
        public string Range { get; set; }
        public string Remark { get; set; }
        public string Linkman { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string QQ { get; set; }
        public string IsPublic { get; set; }
        public Nullable<bool> IsValid { get; set; }
        public int OP { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }

        public int MemberID { get; set; }





    }
}
