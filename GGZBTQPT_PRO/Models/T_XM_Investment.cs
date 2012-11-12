using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_XM_Investment
    {
        public T_XM_Investment()
        {
            ItemName = " ";
            Province = "0";
            City = "0";
            Region = "0";
            Industry = 0;
            ValidDate = DateTime.MaxValue;
            Keys = " ";
            Description = " ";
            Linkman = " ";
            Position = " ";
            Phone = " ";
            Mobile = " ";
            Fax = " ";
            Email = " ";
            QQ = " ";
            IsPublic = " ";
            Investment = 0;
            StartInvestment = 0;
            AimIndustry = " ";
            AjmArea = " ";
            ReturnRatio = 0;
            TeamworkType = " ";
            InvestmentPeriod = " ";
            PartnerRequirements = " ";
            InvestmentNature = 0;
            InvestmentStage = " ";
            PublicStatus = " ";
            SubmitTime = DateTime.MaxValue;
            PublicTime = DateTime.MaxValue;
            IsValid = true;
            OP = 9999;
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
            Pic = new Byte[0];
            MemberID = 9999;

        }
        public int ID { get; set; }

        public string ItemName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
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
        public string TeamworkType { get; set; }
        public string InvestmentPeriod { get; set; }
        public string PartnerRequirements { get; set; }
        public Nullable<int> InvestmentNature { get; set; }
        public string InvestmentStage { get; set; }
        public string PublicStatus { get; set; }
        public Nullable<System.DateTime> SubmitTime { get; set; }
        public Nullable<System.DateTime> PublicTime { get; set; }
        public Nullable<bool> IsValid { get; set; }
        public int OP { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public byte[] Pic { get; set; }

        public int MemberID { get; set; }
        public virtual T_HY_Member Member { get; set; }

        public virtual ICollection<T_HY_Favorite> Favoites { get; set; }

        public string IndustryName
        {
            get
            {
                GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                return db.T_PTF_DicDetail.Find(this.Industry).Name;
            }
        }
        public string AimIndustryName
        {
            get
            {
                string result = "";
                if (AimIndustry.Length < 2)
                    return result;
                GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                string[] aimInd = this.AimIndustry.Split(',');
                foreach (string strInd in aimInd)
                {
                    result += db.T_PTF_DicDetail.Find(Convert.ToInt32(strInd)).Name + ',';
                }
                if (result.Length > 0)
                    result = result.Substring(0, result.Length - 1);
                return result;
            }
        }

        public string MemberName
        {
            get
            {
                GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                return db.T_HY_Member.Find(this.MemberID).LoginName;
            }
        }
    }
}
