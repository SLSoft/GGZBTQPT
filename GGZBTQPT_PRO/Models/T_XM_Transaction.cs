using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
    public class T_XM_Transaction
    {
        public T_XM_Transaction()
        {
            ItemID = 0;
            TranTitle = " ";
            AcceptMember = " ";
            GiveMember = " ";
            TranTime = DateTime.Now;
            TranContent = " ";
            IsValid = true;
            OP = 0;
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
        }
        
        public int ID { get; set; }
        public int ItemID { get; set; }
        [Required(ErrorMessage = "必须填写交易标题")]
        public string TranTitle { get; set; }

        [Required(ErrorMessage = "必须填写受资方")]
        public string AcceptMember { get; set; }

        [Required(ErrorMessage = "必须填写投资方")]
        public string GiveMember { get; set; }

        [Required(ErrorMessage = "必须填写交易金额")]
        public decimal Amount { get; set; }

        public DateTime TranTime { get; set; }
        [AllowHtml]
        public string TranContent { get; set; }
        public bool IsValid { get; set; }
        public int OP { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}