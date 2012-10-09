using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
  public class T_ZC_System
  {
    public int ID { get; set; }
    public string Name { get; set; }
    public Boolean IsValid { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<T_ZC_Menu> Menus { get; set; }

  }
}