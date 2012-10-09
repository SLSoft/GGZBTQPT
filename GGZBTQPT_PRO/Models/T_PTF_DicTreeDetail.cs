using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_PTF_DicTreeDetail
    {
        public int ID { get; set; }
        public string DicType { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
        public Nullable<int> Taxis { get; set; }
        public Nullable<int> Depth { get; set; }
        public string ParentCode { get; set; }
        public string IsValid { get; set; }
        public string UpdTime { get; set; }
        public string UpdUserCode { get; set; }
        public string UpdPgm { get; set; }
        public string Remark { get; set; }
    }
}
