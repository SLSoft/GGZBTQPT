using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using GGZBTQPT_PRO.Util;

namespace GGZBTQPT_PRO.Models
{
    public class T_QY_RZXQ
    {
        public T_QY_RZXQ()
        { 
            //构造
            this.IsValid = true;
            this.CreateTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
        }

        #region 基本信息
        public int ID { get; set; }
        [Required(ErrorMessage = "企业名称不能为空")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CorpName { get; set; }//企业名称
        public string Park { get; set; }//所属园区
        [Required(ErrorMessage = "联系人不能为空")]
        public string Linkman { get; set; }//联系人
        [Required(ErrorMessage = "职务不能为空")]
        public string Duty { get; set; }
        [Required(ErrorMessage = "电话不能为空")]
        public string Phone { get; set; }//电话
        [Required(ErrorMessage = "手机不能为空")]
        public string Mobile { get; set; }//手机
        [Required(ErrorMessage = "电子邮件不能为空")]
        public string Email { get; set; }//电子邮件
        public string Property { get; set; }//所有制性质
        [Required(ErrorMessage = "法人代表不能为空")]
        public string LegalPerson { get; set; }//法人代表
        [Required(ErrorMessage = "成立时间不能为空")]
        public string RegTime { get; set; }//成立时间
        public string Employees { get; set; }//员工人数
        public string Industry { get; set; }//行业领域
        [Required(ErrorMessage = "核心技术或主要产品不能为空")]
        public string Products { get; set; }//核心技术或主要产品
        public string Customers { get; set; }//主要客户
        public string Competitor { get; set; }//竞争对手
        [Required(ErrorMessage = "注册资本不能为空")]
        public string RegCapital { get; set; }//注册资本
        [Required(ErrorMessage = "实际到位金额不能为空")]
        public string ActualCapital { get; set; }//实际到位金额
        public string CapitalRatio { get; set; }//无形资产出资数额及占注册资本的比例
        public string StockRight { get; set; }//股权结构
        #endregion

        #region 财务状况
        public string TotalAssets1 { get; set; }//总资产
        public string NetAssets1 { get; set; }//净资产
        public string BusinessIncome1 { get; set; }//主营业务收入
        public string NetProfit1 { get; set; }//净利润
        public string TurnoverRate1 { get; set; }//资金周转率
        public string TotalAssets2 { get; set; }//总资产
        public string NetAssets2 { get; set; }//净资产
        public string BusinessIncome2 { get; set; }//主营业务收入
        public string NetProfit2 { get; set; }//净利润
        public string TurnoverRate2 { get; set; }//资金周转率
        #endregion

        #region 融资经历
        public string b_num1 { get; set; }//贷款余额
        public string b_bank1 { get; set; }//贷款银行
        public string b_date1 { get; set; }//起止日期
        public string b_rate1 { get; set; }//年利率
        public string b_way1 { get; set; }//担保方式
        public string b_num2 { get; set; }//贷款余额
        public string b_bank2 { get; set; }//贷款银行
        public string b_date2 { get; set; }//起止日期
        public string b_rate2 { get; set; }//年利率
        public string b_way2 { get; set; }//担保方式
        public string b_num3 { get; set; }//贷款余额
        public string b_bank3 { get; set; }//贷款银行
        public string b_date3 { get; set; }//起止日期
        public string b_rate3 { get; set; }//年利率
        public string b_way3 { get; set; }//担保方式
        public string b_num4 { get; set; }//贷款余额
        public string b_bank4 { get; set; }//贷款银行
        public string b_date4 { get; set; }//起止日期
        public string b_rate4 { get; set; }//年利率
        public string b_way4 { get; set; }//担保方式
        public string b_num5 { get; set; }//贷款余额
        public string b_bank5 { get; set; }//贷款银行
        public string b_date5 { get; set; }//起止日期
        public string b_rate5 { get; set; }//年利率
        public string b_way5 { get; set; }//担保方式

        public string t_nun1 { get; set; }//投资金额
        public string t_agency1 { get; set; }//投资机构
        public string t_date1 { get; set; }//投资日期
        public string t_round1 { get; set; }//投资轮次
        public string t_nun2 { get; set; }//投资金额
        public string t_agency2 { get; set; }//投资机构
        public string t_date2 { get; set; }//投资日期
        public string t_round2 { get; set; }//投资轮次
        public string t_nun3 { get; set; }//投资金额
        public string t_agency3 { get; set; }//投资机构
        public string t_date3 { get; set; }//投资日期
        public string t_round3 { get; set; }//投资轮次
        public string t_nun4 { get; set; }//投资金额
        public string t_agency4 { get; set; }//投资机构
        public string t_date4 { get; set; }//投资日期
        public string t_round4 { get; set; }//投资轮次
        public string t_nun5 { get; set; }//投资金额
        public string t_agency5 { get; set; }//投资机构
        public string t_date5 { get; set; }//投资日期
        public string t_round5 { get; set; }//投资轮次

        public string d_nun1 { get; set; }//担保金额
        public string d_agency1 { get; set; }//担保机构
        public string d_date1 { get; set; }//担保期限
        public string d_way1 { get; set; }//担保方式
        public string d_nun2 { get; set; }//担保金额
        public string d_agency2 { get; set; }//担保机构
        public string d_date2 { get; set; }//担保期限
        public string d_way2 { get; set; }//担保方式
        public string d_nun3 { get; set; }//担保金额
        public string d_agency3 { get; set; }//担保机构
        public string d_date3 { get; set; }//担保期限
        public string d_way3 { get; set; }//担保方式
        public string d_nun4 { get; set; }//担保金额
        public string d_agency4 { get; set; }//担保机构
        public string d_date4 { get; set; }//担保期限
        public string d_way4 { get; set; }//担保方式
        public string d_nun5 { get; set; }//担保金额
        public string d_agency5 { get; set; }//担保机构
        public string d_date5 { get; set; }//担保期限
        public string d_way5 { get; set; }//担保方式
        #endregion

        #region 核心团队
        public string team_name1 { get; set; }//姓名
        public string team_duty1 { get; set; }//职位
        public string team_sex1 { get; set; }//性别
        public string team_age1 { get; set; }//年龄
        public string team_edu1 { get; set; }//教育背景
        public string team_work1 { get; set; }//工作经历
        public string team_name2 { get; set; }//姓名
        public string team_duty2 { get; set; }//职位
        public string team_sex2 { get; set; }//性别
        public string team_age2 { get; set; }//年龄
        public string team_edu2 { get; set; }//教育背景
        public string team_work2 { get; set; }//工作经历
        public string team_name3 { get; set; }//姓名
        public string team_duty3 { get; set; }//职位
        public string team_sex3 { get; set; }//性别
        public string team_age3 { get; set; }//年龄
        public string team_edu3 { get; set; }//教育背景
        public string team_work3 { get; set; }//工作经历
        public string team_name4 { get; set; }//姓名
        public string team_duty4 { get; set; }//职位
        public string team_sex4 { get; set; }//性别
        public string team_age4 { get; set; }//年龄
        public string team_edu4 { get; set; }//教育背景
        public string team_work4 { get; set; }//工作经历
        public string team_name5 { get; set; }//姓名
        public string team_duty5 { get; set; }//职位
        public string team_sex5 { get; set; }//性别
        public string team_age5 { get; set; }//年龄
        public string team_edu5 { get; set; }//教育背景
        public string team_work5 { get; set; }//工作经历
        #endregion

        #region 融资意向
        [Required(ErrorMessage = "融资方式及金额不能为空")]
        public string Financing { get; set; }//融资方式及金额
        public string FinancingDate { get; set; }//希望融资到位的时间
        [Required(ErrorMessage = "可承受的最高融资成本不能为空")]
        public string FinancingCost { get; set; }//可承受的最高融资成本
        public string Guarantee1 { get; set; }//第三方保证
        public string GuaranteeDetail1 { get; set; }//保证人
        public string Guarantee2 { get; set; }//抵押
        public string GuaranteeDetail2 { get; set; }//抵押物
        public string Guarantee3 { get; set; }//质押
        public string GuaranteeDetail3 { get; set; }//质押物
        public string Guarantee4 { get; set; }//其他
        public string GuaranteeDetail4 { get; set; }//其他内容
        #endregion

        #region 企业竞争力
        public string Intellectual { get; set; }//知识产权情况
        [Required(ErrorMessage = "经营模式和盈利模式不能为空")]
        public string Management { get; set; }//经营模式和盈利模式
        public string Competitive { get; set; }//企业核心竞争力
        [Required(ErrorMessage = "所获荣誉不能为空")]
        public string Honour { get; set; }//所获荣誉
        #endregion

        public bool IsValid { get; set; }
        public System.DateTime CreateTime { get; set; }
        public System.DateTime UpdateTime { get; set; }
    }
}