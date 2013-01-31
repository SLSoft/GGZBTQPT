using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
    /// <summary>
    /// 内部办公--收发文件
    /// </summary>
    public class T_NB_File
    {
        public T_NB_File()
        {
            IsValid = true;
            CreatedTime = DateTime.Now;
        }

        public int ID { get; set; }
        public int SendUserId { get; set; } //发送人

        [Required(ErrorMessage = "必须填标题")]
        public string Title { get; set; }//标题

        public byte[] File { get; set; }//文件
        public string FileUrl { get; set; }
        public string FileName { get; set; }
        public Boolean IsValid { get; set; }
        public DateTime CreatedTime { get; set; }
        public Boolean IsShare { get; set; }
        public DateTime UpdateTime { get; set; }

        public virtual T_ZC_User SendUser { get; set; }
        public virtual ICollection<T_ZC_User> ReceiveUsers { get; set; }//收件人(多个用户)


    }
}