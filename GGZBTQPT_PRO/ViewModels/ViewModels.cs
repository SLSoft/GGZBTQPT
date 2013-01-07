using System;
using System.Collections.Generic;
using GGZBTQPT_PRO.Models;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Webdiyer.WebControls.Mvc;

namespace GGZBTQPT_PRO.ViewModels
{
    public class VM_SelectUser
    {
        public ICollection<T_ZC_User> Users { get; set; }
        public ICollection<T_ZC_Department> Departments { get; set; }
    }

    public class VM_SystemMenu
    {
        public ICollection<T_ZC_System> Systems { get; set; }
        public ICollection<T_ZC_Menu> Menus { get; set; } 
    }

    /// <summary>
    /// 会员查询(项目、意向、产品发布条数)
    /// </summary>
    public class VM_MemberRelease
    {
        public int FinancingCount { get; set; }
        public int InvestmentCount { get; set; }
        public int ProductCount { get; set; }
        public T_HY_Member Member { get; set; }
    }

    public class VM_AttendanceStat
    {
        public int StateCount { get; set; }
        public int State { get; set; }
        public T_NB_Attendance Attendance { get; set; }
    }

    public class AnalysisResult
    {
        public Decimal Turnover { get; set; }//平均成交额
        public Double ReturnRatio { get; set; }//平均回报率
        public int Num { get; set; }//成交数量
        public int TurnoverRatio { get; set; }//成交比例 
    }

    public class VM_EditMember
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "必须填写昵称")]
        [Display(Name = "昵称")]
        public string MemberName { get; set; }

        [Required(ErrorMessage = "必须填写手机号码")]
        [Display(Name = "手机号码")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "手机格式不正确")]
        [DataType(DataType.PhoneNumber)]
        public string CellPhone { get; set; }

        [Display(Name = "邮箱")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "邮件格式不正确")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }

    //个人办公主页信息汇总
    public class VM_InformationCollect
    {
        public ICollection<T_NB_File> Files { get; set; }//发送给我的文件
        public ICollection<T_NB_Meeting> Meetings { get; set; }//最新的会议
        public ICollection<T_XM_Financing> Financials { get; set; }//最新的投资项目
        public ICollection<T_XM_Investment> Investments { get; set; }//最新的融资意向
    }

    /// <summary>
    /// 机构类别统计
    /// </summary>
    public class VM_AgencyReport
    {
        public string TypeName { get; set; }
        public int AgencyCount { get; set; }
    }

    /// <summary>
    /// 项目统计
    /// </summary>
    public class VM_XMReport
    {
        public string TypeName { get; set; }
        public int Count { get; set; }
    }

    /// <summary>
    /// 项目统计
    /// </summary>
    public class VM_CPReport
    {
        public string TypeName { get; set; }
        public int Count { get; set; }
    }
}

