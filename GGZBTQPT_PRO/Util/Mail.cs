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
        public static void SendEmail(string receiver, string subject, string body, string[] filesPaths = null) 
        {
            WebMail.SmtpServer = "192.168.1.97";//获取或设置要用于发送电子邮件的 SMTP 中继邮件服务器的名称。
            WebMail.SmtpPort = 25;//发送端口
            WebMail.EnableSsl = false;//是否启用 SSL 
            WebMail.UserName = "admin";//账号名
            WebMail.From = "admin@ovcstf.com";//邮箱名
            WebMail.Password = "!?gg2012";//密码

            try
            {
                WebMail.Send(to: receiver,
                             subject: subject,
                             body: body

                    //,cc: "抄送"
                    //   ,filesToAttach: filesPaths
                    //      , isBodyHtml: true,
                    //additionalHeaders:new string[] { "additionalHeaders1", "additionalHeaders2" }
                    );
            }
            catch (Exception e)
            {

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