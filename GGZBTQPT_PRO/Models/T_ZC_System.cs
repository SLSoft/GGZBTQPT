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
        Index = 0;
    }

    public int ID { get; set; }

    [Required(ErrorMessage = "必须填写系统名称")]
    [Display(Name = "系统名称")]
    public string Name { get; set; }

    [Required(ErrorMessage = "必须填写系统序号")]
    [Display(Name = "系统序号")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "序号格式不正确")]
    public int Index { get; set; }//系统排序

    public Boolean IsValid { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<T_ZC_Menu> Menus { get; set; }
  }
}