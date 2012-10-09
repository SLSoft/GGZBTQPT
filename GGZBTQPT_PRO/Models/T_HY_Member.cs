using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GGZBTQPT_PRO.Models
{
    public class T_HY_Member
    {
        public int ID { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string CellPhone { get; set; }
        public int Number { get; set; } //会员编号
        public int Type { get; set; }
 
        public Boolean IsValid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public int CorpID { get; set; }
        public virtual T_QY_Corp Corp { get; set; }

        public int AgencyID { get; set; }
        public virtual T_JG_Agency Agency { get; set; }

        public int PersonID { get; set; }
        public virtual T_QY_Person Person { get; set; } 
    }



}