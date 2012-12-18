using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;

namespace GGZBTQPT_PRO.Areas.MG.Controllers
{ 
    public class ReplyController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /MG/Reply/

        public ViewResult Index()
        {
            var t_hy_reply = db.T_HY_Reply.Include(t => t.Message).Include(t => t.Member);
            return View(t_hy_reply.ToList());
        }

        //
        // GET: /MG/Reply/Details/5

        public ViewResult Details(int id)
        {
            T_HY_Reply t_hy_reply = db.T_HY_Reply.Find(id);
            return View(t_hy_reply);
        }

        //
        // GET: /MG/Reply/Create

        public ActionResult Create()
        {
            ViewBag.MessageID = new SelectList(db.T_HY_Message, "ID", "Content");
            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName");
            return View();
        } 

        //
        // POST: /MG/Reply/Create

        [HttpPost]
        public ActionResult Create(T_HY_Reply t_hy_reply)
        {
            if (ModelState.IsValid)
            {
                db.T_HY_Reply.Add(t_hy_reply);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.MessageID = new SelectList(db.T_HY_Message, "ID", "Content", t_hy_reply.MessageID);
            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName", t_hy_reply.MemberID);
            return View(t_hy_reply);
        }
        
        //
        // GET: /MG/Reply/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_HY_Reply t_hy_reply = db.T_HY_Reply.Find(id);
            ViewBag.MessageID = new SelectList(db.T_HY_Message, "ID", "Content", t_hy_reply.MessageID);
            ViewBag.MemberID = new SelectList(db.T_HY_Member, "ID", "LoginName", t_hy_reply.MemberID);
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