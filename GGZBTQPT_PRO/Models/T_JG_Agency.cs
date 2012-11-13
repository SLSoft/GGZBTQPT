using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_JG_Agency
    {
        public T_JG_Agency()
        {
            AgencyName = " ";
            AgencyType = 0;
            RegTime = DateTime.MaxValue;
            Address = " ";
            RegCapital = 0;
            Province = "0";
            City = "0";
            Region = "0";
            Phone = " ";
            Services = " ";
            Remark = " ";
            IsValid = true;
            OP = 9999;
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
            IsIn = true;
            Pic = new byte[0];
            SubName = " ";
        }

        public int ID { get; set; } 

        public string AgencyName { get; set; }
        public Nullable<int> AgencyType { get; set; }
        public Nullable<System.DateTime> RegTime { get; set; }
        public string Address { get; set; }
        public Nullable<decimal> RegCapital { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
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
        public string SubName { get; set; }

        public string AgencyTypeName
        {
            get
            {
                //GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                //return db.T_PTF_DicDetail.Find(this.AgencyType).Name;
                return "机构类型名称";
            }
        }

        public virtual ICollection<T_JG_Linkman> Linkmans { get; set; }
    }
}
