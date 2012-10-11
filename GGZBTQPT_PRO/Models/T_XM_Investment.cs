using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_XM_Investment
    {
        public int ID { get; set; }

        public string ItemName { get; set; }
        public Nullable<int> Province { get; set; }
        public Nullable<int> City { get; set; }
        public Nullable<int> Region { get; set; }
        public Nullable<int> Industry { get; set; }
        public Nullable<System.DateTime> ValidDate { get; set; }
        public string Keys { get; set; }
        public string Description { get; set; }
        public string Linkman { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string QQ { get; set; }
        public string IsPublic { get; set; }
        public Nullable<decimal> Investment { get; set; }
        public Nullable<decimal> StartInvestment { get; set; }
        public string AimIndustry { get; set; }
        public string AjmArea { get; set; }
        public Nullable<double> ReturnRatio { get; set; }
        public Nullable<int> TeamworkType { get; set; }
        public string InvestmentPeriod { get; set; }
        public string PartnerRequirements { get; set; }
        public Nullable<bool> IsValid { get; set; }
        public int OP { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }

        public int MemberID { get; set; }
    }
}
