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
            Index = 0;
        }

        public int ID { get; set; }


        [Required(ErrorMessage = "必须填写菜单名称")]
        [Display(Name = "菜单名称")]
        public string Name { get; set; }

        [Display(Name = "菜单路径")]
        public string Url { get; set; }

        [Required(ErrorMessage = "必须填写菜单序号")]
        [Display(Name = "菜单序号")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "序号格式不正确")]
        public int Index { get; set; }

        [Display(Name = "上级菜单")]
        public int ParentID { get; set; }
        public Boolean IsValid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Display(Name = "所属系统")]
        public int SystemID { get; set; }
        public virtual T_ZC_System System { get; set; }

        public virtual ICollection<T_ZC_Role> Roles { get; set; }

    }
}