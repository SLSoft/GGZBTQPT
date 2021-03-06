using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using GGZBTQPT_PRO.Util;

namespace GGZBTQPT_PRO.Models
{
    public class T_QY_Corp
    {
        public T_QY_Corp()
        {
            CorpName = " ";
            CorpCode = " ";
            RegTime = DateTime.Now;
            Property = 0;
            Province = "0";
            City = "0";
            Region = "0";
            RegCapital = 0;
            Industry = 0;
            Stage = 0;
            LegalPerson = " ";
            Employees = 0;
            Website = " ";
            Logo = new byte[0];
            Range = " ";
            Remark = " ";
            Linkman = " ";
            Position = " ";
            Phone = " ";
            Mobile = " ";
            Fax = " ";
            Email = " ";
            QQ = " ";
            IsPublic= " ";
            IsValid = true;
            OP = 9999;
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
            Address = " ";
            Industry2 = 0;
            Industry3 = 0;

        }

        public int ID { get; set; }
        [Required(ErrorMessage = "企业名称不能为空")]
        //[Remote("CheckCorpName", "QY_Corp", ErrorMessage = "该企业已经存在")]
        public string CorpName { get; set; }
        public string CorpCode { get; set; }
        public Nullable<System.DateTime> RegTime { get; set; }
        public Nullable<int> Property { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public Nullable<decimal> RegCapital { get; set; }
        public Nullable<int> Industry { get; set; }
        public Nullable<int> Stage { get; set; }
        public string LegalPerson { get; set; }
        public Nullable<int> Employees { get; set; }
        public string Website { get; set; }
        public byte[] Logo { get; set; }
        public string Range { get; set; }
        [AllowHtml]
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
        public string Address { get; set; }
        public Nullable<int> Industry2 { get; set; }
        public Nullable<int> Industry3 { get; set; }

        public int MemberID { get; set; }

        public string PropertyName
        {
            get
            {
                GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                if (db.T_PTF_DicDetail.Find(this.Property) != null)
                    return db.T_PTF_DicDetail.Find(this.Property).Name;
                else
                    return "";
            }
        }
        public string IndustryName
        {
            get
            {
                GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                if (db.T_PTF_DicDetail.Find(this.Industry) != null)
                    return db.T_PTF_DicDetail.Find(this.Industry).Name;
                else
                    return "";
            }
        }

        public virtual ICollection<T_QY_Financial> Financials { get; set; }
        public virtual ICollection<T_QY_Product> Products { get; set; }

        public string ShowText
        {
            get
            {
                return StringHelper.checkStr(this.Remark);
            }
        }
    }
}
