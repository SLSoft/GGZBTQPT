using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
    public class T_ZC_Menu
    {
        public T_ZC_Menu()
        {
            IsValid = true;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public int ID { get; set; }


        [Required(ErrorMessage = "必须填写菜单名称")]
        public string Name { get; set; }


        public string Url { get; set; }

        public int ParentID { get; set; }
        public Boolean IsValid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int SystemID { get; set; }
        public virtual T_ZC_System System { get; set; }

        public virtual ICollection<T_ZC_Role> Roles { get; set; }

    }
}