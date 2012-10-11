using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;

namespace GGZBTQPT_PRO.Areas.Member.Controllers
{ 
    public class MemberController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /Member/Member/

        public ViewResult Index()
        {

            return View();
        }

        //
        // GET: /Member/Member/Details/5

        public PartialViewResult Details(int id)
        {
            T_HY_Member t_hy_member = db.T_HY_Member.Find(id);
            return PartialView(t_hy_member);
        }

        //
        // GET: /Member/Member/Create

        public ActionResult Create()
        {

            return View();
        } 

        //
        // POST: /Member/Member/Create

        [HttpPost]
        public ActionResult Create(T_HY_Member t_hy_member)
        {
            if (ModelState.IsValid)
            {
                if( !VerifyCode(Request["verify"].ToString(),t_hy_member.CellPhone) )
                {
                    return View(t_hy_member);
                } 

                t_hy_member.CreatedAt = DateTime.Now;
                t_hy_member.UpdatedAt = DateTime.Now;
                db.T_HY_Member.Add(t_hy_member);
                db.SaveChanges();
                return RedirectToAction("Index","Home");  
            }

            return View(t_hy_member);
        }
        
        //
        // GET: /Member/Member/Edit/5

        public PartialViewResult Edit(int id)
        {
            T_HY_Member t_hy_member = db.T_HY_Member.Find(id);

            return PartialView(t_hy_member);
        }

        //
        // POST: /Member/Member/Edit/5

        [HttpPost]
        public ActionResult Edit(T_HY_Member t_hy_member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_hy_member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return PartialView(t_hy_member);
        }

        //
        // GET: /Member/Member/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_HY_Member t_hy_member = db.T_HY_Member.Find(id);
            return View(t_hy_member);
        }

        //
        // POST: /Member/Member/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            T_HY_Member t_hy_member = db.T_HY_Member.Find(id);
            db.T_HY_Member.Remove(t_hy_member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        //------------ViewAction---------------//
        //个人设置
        public ActionResult Config()
        {
            return View();
        }

        //登录
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string loginname, string password)
        {
            T_HY_Member member = db.T_HY_Member.Where(m => m.LoginName == loginname).First();
            if (member != null && member.Password == password)
            {
                Session["MemberID"] = member.ID;

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["MemberID"] = null;
            return RedirectToAction("Index", "Home");
        }

        //-----------Helper-------------------//

        public bool SendVerifyCodeToPhone(string phone_number)
        {
            Random r = new Random();
            string verify_code = r.Next(100000,999999).ToString();

            if (SendMsg(verify_code, phone_number))
            {
                Session[phone_number] = verify_code;
                return true;
            }
            return false; 
        }

        //根据用户提交的验证码进行身份验证
        public bool VerifyCode(string verify_code, string phone_number)
        {
            if(Session[phone_number].ToString() == verify_code)
            {
                return true;
            }
            return false;
        }

        public bool SendMsg(string msg, string phone_number)
        {
            //-------TO-DO---------//
            //添加将随机验证码发送到手机的功能
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}