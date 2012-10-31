using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
    public class T_ZC_Menu
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public int ParentID { get; set; }
        public Boolean IsValid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int SystemID { get; set; }
        public virtual T_ZC_System System { get; set; }
    }
}