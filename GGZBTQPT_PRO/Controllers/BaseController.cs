using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using GGZBTQPT_PRO.Enums;
using GGZBTQPT_PRO.Filter;
using GGZBTQPT_PRO.Models;

namespace GGZBTQPT_PRO.Controllers
{
    [ActionAttributeFilter()]
    public class BaseController : Controller
    {
        public GGZBTQPT_PRO.Models.GGZBTQPTDBContext db = new GGZBTQPT_PRO.Models.GGZBTQPTDBContext();

        //"statusCode":"返回的状态值，200--success 300--fail 301--timeout",
        //"message":"提示信息",
        //"navTabId":"定navTab页面标记为需要“重新载入”。注意navTabId不能是当前navTab页面的",
        //"rel":"指定ID",该ID用于指定回调后,需要局部刷新的页面元素ID
        //"callbackType":"closeCurrent或forward", 关闭当前页面/转发到其他页面
        //"forwardUrl":"跳转的URL，callbackType是forward时使用"
        //"confirmMsg":"需要确定的信息"
        public JsonResult ReturnJson(bool _IfSuccess, string _message, string _navTabId, string _rel, bool _IfColse, string _forwardUrl)//批量删除会自动刷新所在的navTab。 
        {
            string _statusCode = _IfSuccess ? "200" : "300";
            string _callbackType = _IfColse ? "closeCurrent" : null;
            return Json(new { statusCode = _statusCode, message = _message, navTabId = _navTabId, rel = _rel, callbackType = _callbackType, forwardUrl = _forwardUrl }, "text/html", JsonRequestBehavior.AllowGet);
        } 

        //日志处理
        public void Logging(int level, string message, int operate_type, int generate_type, string exception = "无")
        {
            log4net.ILog log = log4net.LogManager.GetLogger(this.GetType());

            if (Session["UserName"] != null)
            {
                log4net.LogicalThreadContext.Properties["user"] = Session["UserName"];
            }
            else
            {
                log4net.LogicalThreadContext.Properties["user"] = "无";
            }
            log4net.LogicalThreadContext.Properties["opeate_type"] = operate_type;
            log4net.LogicalThreadContext.Properties["generate_type"] = generate_type;

            switch (level)
            {
                case (int)LogLevels.error: 
                    log.Error(message, new Exception(exception));
                    break;
                case (int)LogLevels.warn:
                    log.Warn(message);
                    break;
                case (int)LogLevels.login:
                    //------ToDo------//
                    //创建登录日志，登录日志需要记录用户登录和离开系统的时间
                    break;
                case (int)LogLevels.operate:
                    log.Info(message);
                    break;
            }
        }

        protected T_ZC_User CurrentUser()
        {
            if (Session["UserID"] != null && Session["UserID"].ToString() != "")
            {
                var user = db.T_ZC_User.Find(Convert.ToInt32(Session["UserID"].ToString()));
                return user;
            }
            return null;
        }

        //Helper
        //去掉字符串最后的逗号
        public string RemoveTheLastComma(string str)
        {
            if(str.Last() == ',')
            {
                str = str.Substring(0,str.Length - 1);
            }
            return str;
        }
    }
}
