using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
    public class T_ZC_MemberLog
    {
        public T_ZC_MemberLog()
        {
            StartDateTime = DateTime.Now;
        }

        public int ID { get; set; }

        public DateTime StartDateTime { get; set; }//操作开始时间
        public DateTime EndDateTime { get; set; }//操作结束时间
        public TimeSpan Continuance { get; set; }//持续时间
        public string Message { get; set; }//提示信息
        public int OperateType { get; set; }//操作类型记录，详细参考枚举类
        public int GenerateModule { get; set; }//操作所属的系统模块


        public int MemberID { get; set; }
        public T_HY_Member Member { get; set; } 

    }
}