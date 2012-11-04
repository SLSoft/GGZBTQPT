using System;
using System.Collections.Generic;
using GGZBTQPT_PRO.Models;

namespace GGZBTQPT_PRO.Areas.ViewModels
{
    public class VM_AttentionedPerson
    {
        public T_HY_Member Member { get; set; }
        public T_QY_Person Person { get; set; }
    }

    public class VM_AttentionedCorp
    {
        public T_HY_Member Member { get; set; }
        public T_QY_Corp Corp { get; set; }
    }

    public class VM_AttentionedAgency
    {
        public T_HY_Member Member { get; set; }
        public T_JG_Agency Agency { get; set; }
    }
}