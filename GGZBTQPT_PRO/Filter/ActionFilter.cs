using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GGZBTQPT_PRO.Filter
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ActionAttributeFilter : ActionFilterAttribute
    {

        public string Message { get; set; } 

        //在Action执行之后执行  
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            //在页面上输出一段文字表示在Action执行完后执行了此段代码 
            filterContext.HttpContext.Response.Write(@"<br />After Action Excute" + "\t " + Message);

            base.OnActionExecuted(filterContext);

        } 

        //在Action执行前执行
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            filterContext.HttpContext.Response.Write(@"<br />Before Action Excute" + "\t " + Message);

            base.OnActionExecuting(filterContext);

        } 

        //在Result执行之后  
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

            filterContext.HttpContext.Response.Write(@"<br />After ViewResult Excute" + "\t " + Message);

            base.OnResultExecuted(filterContext);

        } 

        //在Result执行之前
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

            filterContext.HttpContext.Response.Write(@"<br />Before ViewResult Excute" + "\t " + Message);

            base.OnResultExecuting(filterContext);

        }



    }

 
}
