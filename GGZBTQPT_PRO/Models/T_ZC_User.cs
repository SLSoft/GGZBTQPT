using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
  public class T_ZC_User
  {
    public T_ZC_User()
    {
        IsValid = true;
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    public int ID { get; set; }

    [Required(ErrorMessage = "必须填写用户名称")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "必须填写登录名称")]
    public string LoginName { get; set; }

    [Required(ErrorMessage = "必须填写登录密码")]
    public string Password { get; set; }

    [Required(ErrorMessage = "必须填写手机号码")]
    public string CellPhone { get; set; }

    public Boolean IsValid { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string IdCard { get; set; }
    public int Order { get; set; }

    public int UseLevel { get; set; } //使用级别，参考枚举类型

    public int DepartmentID { get; set; }
    public virtual T_ZC_Department Department { get; set; }

    public virtual ICollection<T_ZC_Role> Roles { get; set; }
    public virtual ICollection<T_NB_File> SendFiles { get; set; } //用户所发送的文件
    public virtual ICollection<T_NB_File> ReceiveFiles { get; set; } //用户所接收的文件
    public virtual ICollection<T_NB_Meeting> CreateMeetings { get; set; }//用户申请会议
    public virtual ICollection<T_NB_Meeting> PartakeMeetings { get; set; } //参与会议用户

  }
}