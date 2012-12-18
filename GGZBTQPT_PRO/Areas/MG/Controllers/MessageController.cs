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

namespace GGZBTQPT_PRO.Areas.MG.Controllers
{
    [MemberFilter()]
    public class MessageController : BaseController
    {
        //
        // GET: /MG/Message/

        public ViewResult Index()
        { 
            T_HY_Member current_member = CurrentMember();
            return View(current_member);
        }

        public ActionResult Sended(int id = 1)
        {
            T_HY_Member current_member = CurrentMember();
            ViewBag.current_member = current_member;

            try
            {
                IList<T_HY_Message> messages = current_member.SendedMessages.ToList();
                PagedList<T_HY_Message> paged_messages = new PagedList<T_HY_Message>(messages, id, 5);

                return PartialView(paged_messages);
            }
            catch
            {
                return PartialView();
            }
        }

        public PartialViewResult Received(int id = 1)
        {
            T_HY_Member current_member = CurrentMember();

            try
            {
                IList<T_HY_Message> messages = current_member.ReceivedMessages.ToList();
                PagedList<T_HY_Message> paged_messages = new PagedList<T_HY_Message>(messages, id, 5);
                return PartialView(paged_messages);
            }
            catch
            {
                return PartialView();
            }
        }

        //
        // GET: /MG/Message/Details/5

        public ViewResult Details(int id)
        {
            T_HY_Message t_hy_message = db.T_HY_Message.Find(id);
            return View(t_hy_message);
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
                return Json(new { statusCode = "200", message = "消息发送成功！", type = "success" });
            }
            return View();
        }

        //
        // POST: /MG/Message/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            T_HY_Message t_hy_message = db.T_HY_Message.Find(id);
            db.T_HY_Message.Remove(t_hy_message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}