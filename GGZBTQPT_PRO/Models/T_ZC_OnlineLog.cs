using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
    //用户在线日志
    //涉及用户的登录信息，离线信息，在线时间
    //对于外网用户使用的业务系统信息可以使用GoogleAnalysis进行分析，载入相应的脚本即可
    public class T_ZC_OnlineLog
    {
        public T_ZC_OnlineLog()
        {
        }

        public int ID { get; set; }

        public DateTime LoginInDate { get; set; }//登录时间
        public DateTime LoginOutDate { get; set; }//离开时间 
        public string IpAddress { get; set; }//访问的用户IP地址


        public int MemberID { get; set; }
        public T_HY_Member Member { get; set; } 

    }
}