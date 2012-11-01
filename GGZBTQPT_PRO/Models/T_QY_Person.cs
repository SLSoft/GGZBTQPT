using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_QY_Person
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string HomeTown { get; set; }
        public Nullable<int> CardType { get; set; }
        public string CardID { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> Birth { get; set; }
        public string College { get; set; }
        public string Specialty { get; set; }
        public Nullable<int> Education { get; set; }
        public Nullable<int> Degree { get; set; }
        public Nullable<int> Titles { get; set; }
        public Nullable<int> TitlesGrade { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string MSN { get; set; }
        public string QQ { get; set; }
        public string EduExperience { get; set; }
        public string WorkExperience { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsValid { get; set; }
        public int OP { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }

        public int MemberID { get; set; }

        public string Sex { get { return this.Gender == "1" ? "ÄÐ" : "Å®"; } }

        public string DegreeName
        {
            get
            {
                GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                //return db.T_PTF_DicDetail.Find(this.Degree).Name;
                return "Ñ§Î»";
            }
        }
    }
}
