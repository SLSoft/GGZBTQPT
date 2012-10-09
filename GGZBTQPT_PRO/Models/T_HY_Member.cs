using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GGZBTQPT_PRO.Models
{
    public class T_HY_Member_QY
    {
        public int ID { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string CellPhone { get; set; }
        public int Number { get; set; } //会员编号
 
        public Boolean IsValid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //public int CorpID { get; set; }
        //public virtual T_QY_CorpInfo Corp { get; set; }

        //可选的信息
        public string Name { get; set; }
        public string IdCard { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; } 
    }
}