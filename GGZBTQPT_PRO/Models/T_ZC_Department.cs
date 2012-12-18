using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
    public class T_ZC_Department
    {
        public T_ZC_Department()
        {
            IsValid = true;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "必须填写部门名称")]
        public string Name { get; set; }

        [Required(ErrorMessage = "必须填写部门全称")]
        public string FullName { get; set; }

        public int Order { get; set; }

        public int UseLevel { get; set; } //使用级别，参考枚举类型

        public int ParentID { get; set; }
        public Boolean IsValid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<T_ZC_User> Users { get; set; }
    }
}