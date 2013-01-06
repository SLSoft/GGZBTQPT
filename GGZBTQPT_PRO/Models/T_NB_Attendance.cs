using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
    /// <summary>
    /// 内部办公--考勤
    /// </summary>
    public class T_NB_Attendance
    {
        public T_NB_Attendance()
        {
            WorkTime = DateTime.Now.Date;
            RecordTime = DateTime.Now;
        }
        public int ID { get; set; }
        [Required(ErrorMessage = "必须选择用户")]
        public int UserId { get; set; }
        public int State { get; set; }//状态
        public DateTime WorkTime { get; set; }
        public string RecordUser { get; set; }
        public DateTime RecordTime { get; set; }

        public string StateName
        {
            get
            {
                switch (this.State)
                {
                    case 0:
                        return "迟到";
                    case 1:
                        return "正常";
                    case 2:
                        return "早退";
                    case 3:
                        return "请假";
                    case 4:
                        return "事假";
                }
                return "";
            }
        }

        public virtual T_ZC_User User { get; set; }
    }
}