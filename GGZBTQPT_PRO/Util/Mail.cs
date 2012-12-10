using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using System.Web.Routing;
using System.Web.Helpers;

namespace GGZBTQPT_PRO.Util
{
    public class Mail : Controller
    { 
        private void SendEmail(string customerName, string customerRequest, string[] filesPaths = null) {


            WebMail.SmtpServer = "smtp.gmail.com";//获取或设置要用于发送电子邮件的 SMTP 中继邮件服务器的名称。
            WebMail.SmtpPort = 25;//发送端口
            WebMail.EnableSsl = true;//是否启用 SSL GMAIL 需要 而其他都不需要 具体看你在邮箱中的配置
            WebMail.UserName = "facingwaller";//账号名
            WebMail.From = "facingwaller@gmail.com";//邮箱名
            WebMail.Password = "***";//密码
            WebMail.SmtpUseDefaultCredentials = true;//是否使用默认配置

            //    try {
            // Send email 
            WebMail.Send(to: "568264099@qq.com",
                         subject: customerName,
                         body: customerRequest

                //,cc: "抄送"
                //   ,filesToAttach: filesPaths
                //      , isBodyHtml: true,
                //additionalHeaders:new string[] { "additionalHeaders1", "additionalHeaders2" }
                );
            //} catch (Exception e) {

            //    Response.Write(e.ToString());
            //}
        }
        private void SendEmailUseDefault(string customerName, string customerRequest, string[] filesPaths) {
            WebMail.SmtpUseDefaultCredentials = true;// Send email 
            WebMail.Send(to: "568264099@qq.com",
                         subject: customerName,
                         body: customerRequest);
        }
    }
}