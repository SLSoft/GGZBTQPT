using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
    public class T_JG_Product
    {
        public T_JG_Product()
        {
            ProductName = " ";
            AgencyID = 0;
            FinancingAmount = 0;
            FinancingLimit = 0;
            InterestRate = 0;
            CustomerType = " ";
            Superiority = " ";
            RepaymentType = " ";
            AppCondition = " ";
            Linkman = " ";
            Position = " ";
            Phone = " ";
            Mobile = " ";
            Fax = " ";
            Email = " ";
            QQ = " ";
            IsPublic = " ";
            IsValid = true;
            OP = 9999;
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
            Pic = new byte[0];
            Process = " ";
            PublicStatus = "1";
            PublicTime = DateTime.Now;
            SubmitTime = DateTime.Now;
            Clicks = 0;
            MemberID = 9999;
        }
        public int ID { get; set; }
        [Required(ErrorMessage = "产品名称不能为空")]
        public string ProductName { get; set; }
        public int AgencyID { get; set; }
        public Nullable<decimal> FinancingAmount { get; set; }
        public Nullable<int> FinancingLimit { get; set; }
        public Nullable<double> InterestRate { get; set; }
        public string CustomerType { get; set; }
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
        public byte[] Pic { get; set; }
        public string Process { get; set; }
        public string PublicStatus { get; set; }
        public DateTime PublicTime { get; set; }
        public DateTime SubmitTime { get; set; }
        public int Clicks { get; set; }

        public string AgencyName
        {
            get
            {
                GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                if (db.T_JG_Agency.Find(this.AgencyID) != null)
                    return db.T_JG_Agency.Find(this.AgencyID).SubName;
                else
                    return "";
            }
        }

        public int MemberID { get; set; }
        public virtual T_HY_Member Member { get; set; }


        public virtual ICollection<T_HY_Favorite> Favoites { get; set; }

    }
}
