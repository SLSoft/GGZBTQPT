using System;
using System.Collections.Generic;


namespace GGZBTQPT_PRO.Models
{
  public class T_ZC_User
  {
    public int ID { get; set; }
    public string Name { get; set; }
    public string LoginName { get; set; }
    public string Password { get; set; }
    public string CellPhone { get; set; }
    public Boolean IsValid { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string IdCard { get; set; }
    public int Order { get; set; }

    public int DepartmentID { get; set; }
    public T_ZC_Department Department { get; set; }

    public ICollection<T_ZC_Role> Roles { get; set; }
  }
}