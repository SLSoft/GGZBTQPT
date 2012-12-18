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
    public class Mail
    { 
        public static void SendEmail(string customerName, string customerRequest, string[] filesPaths = null) 
        { 
            WebMail.SmtpServer = "192.168.1.97";//获取或设置要用于发送电子邮件的 SMTP 中继邮件服务器的名称。
            WebMail.SmtpPort = 25;//发送端口
            WebMail.EnableSsl = false;//是否启用 SSL GMAIL 需要 而其他都不需要 具体看你在邮箱中的配置
            WebMail.UserName = "admin";//账号名
            WebMail.From = "admin@ovcstf.com";//邮箱名
            WebMail.Password = "!?gg2012";//密码

            try {
            WebMail.Send(to: "12160571@qq.com",
                         subject: customerName,
                         body: customerRequest

                //,cc: "抄送"
                //   ,filesToAttach: filesPaths
                //      , isBodyHtml: true,
                //additionalHeaders:new string[] { "additionalHeaders1", "additionalHeaders2" }
                );
            } catch (Exception e) {

                
            }
        }

        private void SendEmailUseDefault(string customerName, string customerRequest, string[] filesPaths) 
        {
            WebMail.SmtpUseDefaultCredentials = true;// Send email 
            WebMail.Send(to: "568264099@qq.com",
                         subject: customerName,
                         body: customerRequest);
        }
    }
}