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

        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        public int SendMemberID { get; set; }//发送短消息的会员ID
        public virtual T_HY_Member SendMember { get; set; }
 
        [Required(ErrorMessage = "必须选择接收人")]
        [Display(Name = "接收人")] 
        [Remote("CheckMember", "Member", ErrorMessage = "不存在该用户")]
        public int ReceiveMemberID { get; set; }//接收短消息的会员ID


        public virtual T_HY_Member ReceiveMember { get; set; }

        public virtual ICollection<T_HY_Reply> Replies { get; set; }//该短消息所对应的回复信息

        public bool IsValid { get; set; }

    }

    public class T_HY_Reply
    {
        public T_HY_Reply()
        {
            Readed = false;
            CreatedTime = DateTime.Now;
            UpdatedTime = DateTime.Now;
        }

        public int ID { get; set; }
        public string Content { get; set; }//短消息回复内容
        public bool Readed { get; set; }//已读

        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        public int MessageID { get; set; }//所属短消息ID
        public virtual T_HY_Message Message { get; set; } 

        public int MemberID { get; set; }//发送回复的会员ID
        public virtual T_HY_Member Member { get; set; }

    }
}