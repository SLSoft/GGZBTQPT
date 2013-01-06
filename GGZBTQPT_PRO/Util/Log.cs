using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using GGZBTQPT_PRO.Enums;
using GGZBTQPT_PRO.Filter;
using GGZBTQPT_PRO.Models;

namespace GGZBTQPT_PRO.Util
{
    public class Log
    {
        public Log()
        {
            ControllerType = new StringDictionary();
            ActionType = new StringDictionary();

            //Type generate_system = typeof(GenerateSystem);
            //foreach (string type in Enum.GetNames(generate_system))

            //注册所有的controller名称；
            ControllerType.Add("ZC_System", "系统管理");
            ControllerType.Add("ZC_Role", "角色管理");
            ControllerType.Add("ZC_User", "用户管理");
            ControllerType.Add("ZC_Menu", "功能菜单管理");
            ControllerType.Add("ZC_Department", "部门管理");

            ControllerType.Add("XM_TZ", "投资管理");
            ControllerType.Add("XM_RZ", "融资管理");
            ControllerType.Add("XM_Case", "案例管理");
            ControllerType.Add("QY_Corp", "企业管理");
            ControllerType.Add("QY_Person", "创业者管理");
            ControllerType.Add("JG_Agency", "机构管理");
            ControllerType.Add("JG_Product", "产品管理");
            ControllerType.Add("XM_JY", "项目交易管理"); 

            ControllerType.Add("HY_Member","会员管理");
            ControllerType.Add("NB_File", "文件管理");
            ControllerType.Add("NB_Meeting", "会议管理");
            ControllerType.Add("NB_Attendance", "考勤管理");

            //注册所有的Action操作；
            //通用操作：
            ActionType.Add("Create", "创建");
            ActionType.Add("Edit", "编辑");
            ActionType.Add("Delete", "删除");
            ActionType.Add("Index", "访问");

            //各个业务系统特有Action操作：
            //系统管理

            //角色管理
            ActionType.Add("CheckUser", "");
            ActionType.Add("SelectUser", "角色所属用户选择");
            ActionType.Add("SetPurview", "角色对应菜单选择");
            
            //用户管理
            ActionType.Add("SelectUsers", "访问部门用户");
            ActionType.Add("UserInfo", "用户详情");

            //功能菜单管理
            ActionType.Add("SystemLinks", "系统菜单");
            ActionType.Add("MenuInfo", "菜单详情");


            //部门管理
            
            //案例管理
            ActionType.Add("CasesFromUser", "用户提交案例");
            ActionType.Add("CasesFromMember", "会员提交案例");
            ActionType.Add("CasesFromTransaction", "项目转化案例");
            ActionType.Add("DownLoadFile", "案例文件下载");
            ActionType.Add("Analysis", "案例分析");
            ActionType.Add("PublishCaseToCMS", "案例发布");


            //会员管理
            ActionType.Add("UnVerified", "待审核");
            ActionType.Add("HasVerified", "已审核");
            ActionType.Add("Verify", "审核、驳回");
            ActionType.Add("batch_Verify", "批量审核、驳回");
            ActionType.Add("UnVerify", "撤销审核、驳回");
            ActionType.Add("Query", "会员查询");
            ActionType.Add("QueryDetails", "会员发布详细");
            ActionType.Add("HotInformation", "热门信息列表");
            ActionType.Add("HotList", "热门信息分类列表");
            ActionType.Add("HotProductEdit", "产品编辑");
            ActionType.Add("HotFinancingEdit", "项目编辑");
            ActionType.Add("HotInvestmentEdit", "意向编辑");
            ActionType.Add("MemberStatList", "会员统计");


            //投资管理
            ActionType.Add("TZCheck", "投资信息审核");
            ActionType.Add("TZMatch", "投资项目分析");
            ActionType.Add("TZMatchResult", "根据投资意向匹配项目");
            ActionType.Add("TZQuery", "投资项目查询");

            //融资管理
            ActionType.Add("RZCheck", "融资信息审核");
            ActionType.Add("RZMatch", "融资项目分析");
            ActionType.Add("RZMatchResult", "根据项目匹配资金");
            ActionType.Add("RZQuery", "融资项目查询");

            //交易管理

            //企业管理
            ActionType.Add("CorpQuery", "企业信息查询");
            //机构管理 
            ActionType.Add("AgencyQuery", "机构信息查询");
            //创业者管理
            ActionType.Add("PersonQuery", "创业者信息查询");
            //产品管理
            ActionType.Add("CPCheck", "产品审核");
            ActionType.Add("ProductQuery", "产品信息查询");

            //文件管理
            ActionType.Add("SendList", "发送列表");
            ActionType.Add("ReceivedList", "接收列表");
            ActionType.Add("FileList", "分类管理");

            //会议管理
            ActionType.Add("MeetingApply", "预定列表");
            ActionType.Add("MeetingAudit", "审核列表");
            ActionType.Add("Audit", "审核");
            ActionType.Add("MeetingRecord", "记录列表");
            ActionType.Add("RecordEdit", "会议记录");

            //考勤管理
            ActionType.Add("StatList","考勤统计");
            ActionType.Add("StateDetails", "统计详情");

        }
        private StringDictionary ControllerType { get; set; }
        private StringDictionary ActionType { get; set; }

        public void Logging(int level, string message, int operate_type, int generate_type, int generate_system, string exception = "无")
        {
            log4net.ILog log = log4net.LogManager.GetLogger(this.GetType());

            if (HttpContext.Current.Session["UserName"] != null)
            {
                log4net.LogicalThreadContext.Properties["user"] = HttpContext.Current.Session["UserName"];
            }
            else
            {
                log4net.LogicalThreadContext.Properties["user"] = "无";
            }
            log4net.LogicalThreadContext.Properties["operate_type"] = operate_type;
            log4net.LogicalThreadContext.Properties["generate_type"] = generate_type;
            log4net.LogicalThreadContext.Properties["generate_system"] = generate_system;


            switch (level)
            {
                case (int)LogLevels.error:
                    log.Error(message, new Exception(exception));
                    break;
                case (int)LogLevels.warn:
                    log.Warn(message);
                    break;
                case (int)LogLevels.operate:
                    log.Info(message);
                    break;
            }
        }

        //private
        public string GenerateLogMessage(string controller, string action)
        {
            if(ControllerType[controller] != null && ActionType[action] != null)
            {
                return "模块:" + ControllerType[controller] + "[" + controller + "]" + "     \n操作:" + ActionType[action] + "[" + action + "]";
            }
            return "模块:" + controller + "     \n操作:" + action;
        } 
    }
}