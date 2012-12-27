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

            ControllerType.Add("ZC_System", "系统管理");
            ControllerType.Add("ZC_Role", "角色管理");
            ControllerType.Add("ZC_User", "用户管理");
            ControllerType.Add("ZC_Menu", "功能菜单管理");
            ControllerType.Add("ZC_Department", "部门管理");

            ControllerType.Add("XM_TZ", "投资管理");
            ControllerType.Add("XM_RZ", "融资管理");
            ControllerType.Add("XM_Case", "案例管理");


            ActionType.Add("Create", "创建");
            ActionType.Add("Edit", "编辑");
            ActionType.Add("Delete", "删除");
            ActionType.Add("Index", "访问");

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
                return "模块:" + ControllerType[controller] + "     \n操作:" + ActionType[action];
            }
            return "模块:" + controller + "     \n操作:" + action;
        } 
    }
}