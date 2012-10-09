using System;
using System.Collections.Generic;


namespace GGZBTQPT_PRO.Models
{
    public class T_ZC_Department
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public int Order { get; set; }

        public int ParentID { get; set; }
        public Boolean IsValid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<T_ZC_User> Users { get; set; }
    }
}