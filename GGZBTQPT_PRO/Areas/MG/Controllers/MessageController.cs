using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.Areas.ViewModels;
using Webdiyer.WebControls.Mvc;
using GGZBTQPT_PRO.Areas.MG.Filter;
using GGZBTQPT_PRO.Enums;

namespace GGZBTQPT_PRO.Areas.MG.Controllers
{
    [MemberFilter()]
    public class MessageController : BaseController
    {
        //
        // GET: /MG/Message/

        public ViewResult Index()
        {
            Logging((int)LogLevels.operate, "访问了个人消息列表", (int)OperateTypes.Visit, (int)GenerateTypes.FromMember, (int)GenerateSystem.Message);
            T_HY_Member current_member = CurrentMember();

            string un_read = db.T_HY_Message.Where(m => (m.ReceiveMemberID == current_member.ID && m.IsValid == true && m.Readed == false)).Count().ToString();
            ViewBag.UnRead = un_read;

            return View(current_member);
        }

        public ActionResult Sended(int id = 1)
        {
            Logging((int)LogLevels.operate, "访问了发件箱列表", (int)OperateTypes.Visit, (int)GenerateTypes.FromMember, (int)GenerateSystem.Message);
            T_HY_Member current_member = CurrentMember();
            ViewBag.current_member = current_member;

            try
            {
                IList<VM_Message> messages = current_member.SendedMessages.OrderByDescending(m => m.CreatedTime)
                                                           .Where(m => m.RelateID == 0 && m.IsValid == true)
                                                           .Select( m => new VM_Message { Message = m, 
                                                                                          RelateMessages = db.T_HY_Message.Where( g => g.RelateID == m.ID).ToList() })
                                                           .ToList();
                PagedList<VM_Message> paged_messages = new PagedList<VM_Message>(messages, id, 5);

                return PartialView(paged_messages);
            }
            catch
            {
                return PartialView();
            }
        }

        public PartialViewResult Received(int id = 1)
        {
            Logging((int)LogLevels.operate, "访问了收件箱列表", (int)OperateTypes.Visit, (int)GenerateTypes.FromMember, (int)GenerateSystem.Message);
            T_HY_Member current_member = CurrentMember();
            ViewBag.current_member = current_member;

            try
            {
                IList<VM_Message> messages = current_member.ReceivedMessages.OrderByDescending(m => m.CreatedTime)
                                                           .Where(m => m.RelateID == 0 && m.IsValid == true)
                                                           .Select(m => new VM_Message
                                                           {
                                                               Message = m,
                                                               RelateMessages = db.T_HY_Message.Where(g => g.RelateID == m.ID).ToList()
                                                           })
                                                           .ToList();
                PagedList<VM_Message> paged_messages = new PagedList<VM_Message>(messages, id, 5);

                return PartialView(paged_messages);
            }
            catch
            {
                return PartialView();
            }
        }

        public PartialViewResult UnRead(int id = 1)
        {
            Logging((int)LogLevels.operate, "访问了未读消息列表", (int)OperateTypes.Visit, (int)GenerateTypes.FromMember, (int)GenerateSystem.Message);
            T_HY_Member current_member = CurrentMember();
            ViewBag.current_member = current_member;

            try
            {
                IList<VM_Message> messages = current_member.ReceivedMessages.OrderByDescending(m => m.CreatedTime)
                                                           .Where(m => m.Readed == false )
                                                           .Select(m => new VM_Message
                                                           {
                                                               Message = m,
                                                               RelateMessages = db.T_HY_Message.Where(g => 
                                                                                                        (g.RelateID == m.RelateID && g.ID < m.ID && g.RelateID != 0) //与该消息相关的消息
                                                                                                        || 
                                                                                                        (g.RelateID == 0 && g.ID == m.RelateID ))//与该消息相关的主题消息
                                                                                               .OrderByDescending(g => g.CreatedTime)
                                                                                               .ToList()
                                                           })
                                                           .ToList();

                PagedList<VM_Message> paged_messages = new PagedList<VM_Message>(messages, id, 5);

                return PartialView(paged_messages);
            }
            catch
            {
                return PartialView();
            }
        }

        //
        // GET: /MG/Message/Create

        public ActionResult Create()
        {   
            VM_CreateMessage vm_create_message = new VM_CreateMessage();
            return View(vm_create_message);
        }

        //
        // POST: /MG/Message/Create

        [HttpPost]
        public ActionResult Create(VM_CreateMessage vm_create_message)
        {
            if (ModelState.IsValid)
            {
                T_HY_Message message = new T_HY_Message();
                message.Title = vm_create_message.Title;
                message.Content = vm_create_message.Content;
                message.SendMember = CurrentMember();
                message.ReceiveMember = db.T_HY_Member.Where(m => m.MemberName == vm_create_message.ReceiveMember).First();

                db.T_HY_Message.Add(message);
                db.SaveChanges();

                Logging((int)LogLevels.operate, "发送了新的消息", (int)OperateTypes.Create, (int)GenerateTypes.FromMember, (int)GenerateSystem.Message);
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // POST: /MG/Message/Delete/5

        [HttpDelete]
        public ActionResult Delete(int id)
        {            
            try
            { 
                T_HY_Message t_hy_message = db.T_HY_Message.Find(id);
                db.T_HY_Message.Remove(t_hy_message);

                db.SaveChanges();

                Logging((int)LogLevels.operate, "删除了消息--" + t_hy_message.Title , (int)OperateTypes.Delete, (int)GenerateTypes.FromMember, (int)GenerateSystem.Message);
                return Json(new { statusCode = "200", message = "消息删除成功！", type = "success", message_id = id });
            }
            catch
            {
                Logging((int)LogLevels.warn, "删除消息失败", (int)OperateTypes.Delete, (int)GenerateTypes.FromMember, (int)GenerateSystem.Message);
                return Json(new { statusCode = "300", message = "消息删除失败！", type = "error" }); 
            }
        }

        [HttpPost]
        public ActionResult SetReaded(int id)
        { 
            try
            {
                T_HY_Message t_hy_message = db.T_HY_Message.Find(id);

                db.Entry(t_hy_message).State = EntityState.Modified;
                t_hy_message.Readed = true;

                db.SaveChanges();

                Logging((int)LogLevels.operate, "设置了消息" + t_hy_message.Title + "为已读", (int)OperateTypes.Readed, (int)GenerateTypes.FromMember, (int)GenerateSystem.Personal);
                return Json(new { statusCode = "200", message = "成功标记已读！", type = "success", message_id = id });
            }
            catch
            { 
                return Json(new { statusCode = "300", message = "标记已读失败！", type = "error" });
            }
        }


        //
        //回复消息相关

        public ActionResult ReplyMessage(int message_id)
        {
            ViewBag.RelateID = message_id;
            return PartialView();
        }

        [HttpPost]
        public ActionResult ReplyMessage(VM_ReplyMessage reply_message, int relate_id)
        {
            if (ModelState.IsValid)
            {
                T_HY_Message relate_message = db.T_HY_Message.Find(relate_id);

                T_HY_Message message = new T_HY_Message();
                message.RelateID = relate_id;
                message.Content = reply_message.Content;
                message.SendMember = CurrentMember();

                if(relate_message.SendMember == CurrentMember())
                {
                    message.ReceiveMember = relate_message.ReceiveMember;
                }
                else
                {
                    message.ReceiveMember = relate_message.SendMember;
                }

                message.Title = "Re(" + (db.T_HY_Message.Where( m => m.RelateID == relate_id).Count() + 1) + "):" + relate_message.Title;

                db.T_HY_Message.Add(message);
                db.SaveChanges();

                Logging((int)LogLevels.operate, "回复了消息--" + relate_message.Title , (int)OperateTypes.Reply, (int)GenerateTypes.FromMember, (int)GenerateSystem.Message);
                return Json(new { statusCode = "200", message = "消息发送成功！", type = "success", reply_id = message.ID, message_id = relate_id });
            }

            Logging((int)LogLevels.operate, "回复消息失败", (int)OperateTypes.Reply, (int)GenerateTypes.FromMember, (int)GenerateSystem.Message); 
            return Json(new { statusCode = "300", message = "消息发送失败！", type = "error" });
        }



        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}