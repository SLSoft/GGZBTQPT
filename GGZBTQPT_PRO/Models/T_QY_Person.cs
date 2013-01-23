using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
    public class T_QY_Person
    {
        public T_QY_Person()
        {
            Name = " ";
            HomeTown = " ";
            CardType = 0;
            CardID = " ";
            Gender = "1";
            Birth = DateTime.Now;
            College = " ";
            Specialty = " ";
            Education = 0;
            Degree = 0;
            Titles = 0;
            TitlesGrade = 0;
            Address = " ";
            Phone = " ";
            Mobile = " ";
            Email = " ";
            MSN = " ";
            QQ = " ";
            EduExperience = " ";
            WorkExperience = " ";
            Remark = " ";
            IsValid = true;
            OP = 9999;
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        }
        public int ID { get; set; }
        [Required(ErrorMessage = "姓名不能为空")]
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

        public string Sex { get { return this.Gender == "1" ? "男" : "女"; } }

        public string DegreeName
        {
            get
            {
                if (this.Degree != null)
                {
                    GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                    return db.T_PTF_DicDetail.Find(this.Degree).Name;
                }
                else
                    return "";
            }
        }


    }
}
