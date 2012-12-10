using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_HY_Message
    {
        public int ID { get; set; }
        public string Content { get; set; }//短消息内容

        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }
    }
}