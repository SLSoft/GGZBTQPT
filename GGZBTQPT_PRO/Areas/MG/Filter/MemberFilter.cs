using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using System.Web.Routing;

namespace GGZBTQPT_PRO.Areas.MG.Filter
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class MemberFilter : ActionFilterAttribute
    { 
        //在Action执行之后执行  
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            //在页面上输出一段文字表示在Action执行完后执行了此段代码 

            base.OnActionExecuted(filterContext);

        }

        //在Action执行前执行
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        { 
            // If the browser session or authentication session has expired...
            //if (filterContext.HttpContext.Session["MemberID"] == null || !filterContext.HttpContext.Request.IsAuthenticated)
            if (filterContext.HttpContext.Session["MemberID"] == null)
            {
                if (filterContext.HttpContext.Request.Url != null)
                {
                    filterContext.HttpContext.Session["RedirectUrl"] = filterContext.HttpContext.Request.Url;
                }
                if (filterContext.HttpContext.Request.UrlReferrer != null)
                {
                    filterContext.HttpContext.Session["RedirectUrl"] = filterContext.HttpContext.Request.UrlReferrer;
                }
                if (filterContext.HttpContext.Request["url"] != null)
                {
                    filterContext.HttpContext.Session["RedirectUrl"] = filterContext.HttpContext.Request["url"];
                }
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    // For AJAX requests, we're overriding the returned JSON result with a simple string,
                    // indicating to the calling JavaScript code that a redirect should be performed.
                    filterContext.Result = new JsonResult { Data = new { login= true }};
                }
                else
                {
                    // For round-trip posts, we're forcing a redirect to Home/TimeoutRedirect/, which
                    // simply displays a temporary 5 second notification that they have timed out, and
                    // will, in turn, redirect to the logon page.
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                        { "Controller", "Member" },
                        { "Action", "Login" }
                });
                }
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



    }


}
