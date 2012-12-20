using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
    public class T_HY_Message
    {
        public T_HY_Message()
        {
            IsValid = true;
            Readed = false;
            CreatedTime = DateTime.Now;
            UpdatedTime = DateTime.Now;
        }
        public int ID { get; set; }

        [Required(ErrorMessage = "必须填写消息标题")]
        [Display(Name = "消息标题")]
        public string Title { get; set; }

        [Required(ErrorMessage = "必须填写消息内容")]
        [Display(Name = "消息内容")]
        public string Content { get; set; }//短消息内容

        public bool Readed { get; set; }//已读
        public int RelateID { get; set; }//关联的短消息 如果为0 则代表是新的消息

        [Display(Name = "创建时间")]
        public DateTime CreatedTime { get; set; }
        [Display(Name = "更新时间")]
        public DateTime UpdatedTime { get; set; }

        public int SendMemberID { get; set; }//发送短消息的会员ID
        public virtual T_HY_Member SendMember { get; set; }
 
        [Required(ErrorMessage = "必须选择接收人")]
        [Display(Name = "接收人")] 
        [Remote("CheckMember", "Member", ErrorMessage = "不存在该用户")]
        public int ReceiveMemberID { get; set; }//接收短消息的会员ID 
        public virtual T_HY_Member ReceiveMember { get; set; }

        public bool IsValid { get; set; }

    }
}