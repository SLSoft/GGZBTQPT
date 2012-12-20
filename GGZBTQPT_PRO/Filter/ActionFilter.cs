using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using System.Web.Routing;

namespace GGZBTQPT_PRO.Filter
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ActionAttributeFilter : ActionFilterAttribute, IExceptionFilter
    {

        public string Message { get; set; } 

        //在Action执行之后执行  
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        { 
            base.OnActionExecuted(filterContext); 
        } 

        //在Action执行前执行
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["UserID"] == null)
            {
                // For round-trip posts, we're forcing a redirect to Home/TimeoutRedirect/, which
                // simply displays a temporary 5 second notification that they have timed out, and
                // will, in turn, redirect to the logon page.
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                    { "Controller", "Home" },
                    { "Action", "Login" }
                });
            }

            base.OnActionExecuting(filterContext); 
        } 

        //在Result执行之后  
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        { 
            base.OnResultExecuted(filterContext); 
        } 

        //在Result执行之前
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        { 
            base.OnResultExecuting(filterContext); 
        }

        public  void OnException(ExceptionContext filterContext)
        {
            string controller = filterContext.RouteData.Values["controller"] as string;
            string action = filterContext.RouteData.Values["action"] as string; 

            log4net.ILog log = log4net.LogManager.GetLogger(this.GetType());

            if(filterContext.HttpContext.Session["UserName"] != null)
            {
                log4net.LogicalThreadContext.Properties["user"] = filterContext.HttpContext.Session["UserName"];
            }
            else
            {
                log4net.LogicalThreadContext.Properties["user"] = "无";
            }

            log.Error("----方法:[controller]" + controller + "[action]" + action + ";\n----目标:" + filterContext.Exception.TargetSite + ";\n----来源:" + filterContext.Exception.Source + ";\n----错误信息:" + filterContext.Exception.Message + ";\n----链接地址:" + filterContext.Exception.HelpLink, new Exception(";\n----详细信息:" + filterContext.Exception.StackTrace)); 
        } 
    }

 
}
