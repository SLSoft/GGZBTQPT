using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using GGZBTQPT_PRO.Util;

namespace GGZBTQPT_PRO.Models
{
    public class T_QY_TZXQ
    {
        public T_QY_TZXQ()
        { 
            //构造
            this.IsValid = true;
            this.CreateTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
        }

        public int ID { get; set; }
        [Required(ErrorMessage = "机构名称不能为空")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string AgencyName { get; set; }//机构名称
        public string AgencyNature { get; set; }//机构性质
        public string OtherNature { get; set; }//其他机构性质
        [Required(ErrorMessage = "机构地址不能为空")]
        public string Address { get; set; }//机构地址
        [Required(ErrorMessage = "联系人不能为空")]
        public string Linkman { get; set; }//联系人
        [Required(ErrorMessage = "职务不能为空")]
        public string Duty { get; set; }//职务
        [Required(ErrorMessage = "/电话不能为空")]
        public string Phone { get; set; }//电话
        [Required(ErrorMessage = "电子邮箱不能为空")]
        public string Email { get; set; }//电子邮箱
        [Required(ErrorMessage = "过往投资总额不能为空")]
        public string TotalInvestment { get; set; }//过往投资总额
        [Required(ErrorMessage = "年均投项目数量不能为空")]
        public string InvItemNum { get; set; }//年均投项目数量
        public string AttIndustry { get; set; }//最关注的领域
        public string AttCorpType { get; set; }//关注企业类型
        public string OtherCorpType { get; set; }//其他企业类型
        public string Amount { get; set; }//一般投资额度
        public string RateOfReturn { get; set; }//期望回报率

        public bool IsValid { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.DateTime UpdateTime { get; set; }
    }
}