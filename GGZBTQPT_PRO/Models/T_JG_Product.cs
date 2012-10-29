using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_JG_Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public int AgencyID { get; set; }
        public Nullable<decimal> FinancingAmount { get; set; }
        public Nullable<int> FinancingLimit { get; set; }
        public Nullable<double> InterestRate { get; set; }
        public Nullable<int> CustomerType { get; set; }
        public string Superiority { get; set; }
        public string RepaymentType { get; set; }
        public string AppCondition { get; set; }
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

        public virtual ICollection<T_HY_Favorite> Favoites { get; set; }
    }
}
