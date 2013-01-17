using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
    /// <summary>
    /// 内部办公--会议
    /// </summary>
    public class T_NB_Meeting
    {
        public T_NB_Meeting()
        {
            IsValid = true;
            State = 0;
            IsRecord = false;
            CreatedTime = DateTime.Now;            
        }

        public int ID { get; set; }
        public int CreateUserId { get; set; }

        [Required(ErrorMessage = "必须填会议主题")]
        public string Title { get; set; }

        public string Content { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "必须填参加人数")]
        public int Number { get; set; }

        [Required(ErrorMessage = "必须填会议用时")]
        public int UsedTime { get; set; }

        [Required(ErrorMessage = "必须填会议日期")]
        [DataType(DataType.DateTime)]
        public DateTime MeetingTime { get; set; }

        public Boolean IsValid { get; set; }
        public int State { get; set; }
        public DateTime CreatedTime { get; set; }
        public string FeedBack { get; set; }
        public string AuditUser { get; set; }
        public DateTime? AuditTime { get; set; }
        public Boolean IsRecord { get; set; }
        public string RecordUser { get; set; }
        public DateTime? RecordTime { get; set; }

        public string StateName
        {
            get
            {
                switch (this.State)
                {
                    case 0:
                        return "待审核";
                    case 1:
                        return "通过";
                    case 2:
                        return "不通过";
                }
                return "";
            }
        }

        public virtual T_ZC_User CreateUser { get; set; }
        public virtual ICollection<T_ZC_User> PartakeUsers { get; set; } //参与人员
    }
}