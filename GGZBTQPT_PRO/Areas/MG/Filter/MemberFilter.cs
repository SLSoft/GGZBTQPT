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
            //记录用户的操作时间，写入用户的在线记录
            if(filterContext.HttpContext.Session["OnlineID"] != null)
            {
                using(GGZBTQPTDBContext db = new GGZBTQPTDBContext())
                {
                    T_ZC_OnlineLog online_log = db.T_ZC_OnlineLog.Find(Convert.ToInt32(filterContext.HttpContext.Session["OnlineID"].ToString()));
                    online_log.LoginOutDate = DateTime.Now;
                    db.SaveChanges();
                }
            }

            base.OnActionExecuted(filterContext); 
        }

        //在Action执行前执行
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        { 
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
