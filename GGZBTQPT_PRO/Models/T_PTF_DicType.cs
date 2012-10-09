using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_PTF_DicType
    {
        public int ID { get; set; }
        public string DicType { get; set; }
        public string DicName { get; set; }
        public string DicTableName { get; set; }
        public Nullable<int> SystemType { get; set; }
        public string IsValid { get; set; }
        public string UpdTime { get; set; }
        public string UpdUserCode { get; set; }
        public string UpdPgm { get; set; }
        public string Remark { get; set; }
    }
}
