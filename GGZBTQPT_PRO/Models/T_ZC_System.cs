using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
  public class T_ZC_System
  {
        public T_ZC_System()
        {
            IsValid = true;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

    public int ID { get; set; }

      [Required(ErrorMessage = "必须填写系统名称")]
    public string Name { get; set; }

    public Boolean IsValid { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<T_ZC_Menu> Menus { get; set; }

  }
}