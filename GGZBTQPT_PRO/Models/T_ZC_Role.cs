using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
    public class T_ZC_Role
    {
        public T_ZC_Role()
        {
            IsValid = true;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "必须填写角色名称")]
        public string Name { get; set; }//角色名称

        public Boolean IsValid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        //角色与用户存在多对多的映射关系
        public virtual ICollection<T_ZC_User> Users { get; set; }
    }
}