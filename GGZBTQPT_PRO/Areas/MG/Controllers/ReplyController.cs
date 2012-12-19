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
    public class ReplyController : BaseController
    {

        public ActionResult Details(int id)
        {
            return PartialView(db.T_HY_Reply.Find(id));
        }
        //
        // GET: /MG/Reply/Create

        public ActionResult Create(int message_id)
        {
            ViewBag.MessageID = message_id;
            return PartialView();
        } 

        //
        // POST: /MG/Reply/Create

        [HttpPost]
        public ActionResult Create(T_HY_Reply t_hy_reply, int message_id )
        {
            if (ModelState.IsValid)
            {

                T_HY_Message message = db.T_HY_Message.Find(message_id);
                t_hy_reply.Message = message;
                t_hy_reply.Member = CurrentMember();

                db.T_HY_Reply.Add(t_hy_reply);
                db.SaveChanges();


                return Json(new { statusCode = "200", message = "消息发送成功！", type = "success", reply_id = t_hy_reply.ID, message_id = message_id});
            }
            return View(t_hy_reply);
        }
        
        //
        // GET: /MG/Reply/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_HY_Reply t_hy_reply = db.T_HY_Reply.Find(id);
            return View(t_hy_reply);
        }

        //
        // POST: /MG/Reply/Edit/5

        [HttpPost]
        public ActionResult Edit(T_HY_Reply t_hy_reply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_hy_reply).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MessageID = new SelectList(db.T_HY_Message, "ID", "Content", t_hy_reply.MessageID);
            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName", t_hy_reply.MemberID);
            return View(t_hy_reply);
        }

        //
        // GET: /MG/Reply/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_HY_Reply t_hy_reply = db.T_HY_Reply.Find(id);
            return View(t_hy_reply);
        }

        //
        // POST: /MG/Reply/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            T_HY_Reply t_hy_reply = db.T_HY_Reply.Find(id);
            db.T_HY_Reply.Remove(t_hy_reply);
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