using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.Enums;
using GGZBTQPT_PRO.Areas.ViewModels; 
 

namespace GGZBTQPT_PRO.Areas.MG.Controllers
{ 
    public class MemberController : BaseController
    { 
        public ViewResult Index()
        { 
            return View(); 
        } 
 
        public PartialViewResult Details(int id)
        {
            T_HY_Member t_hy_member = db.T_HY_Member.Find(id);
            return PartialView(t_hy_member);
        } 

        public ActionResult SignUp()
        {
            var types = from MemberTypes type in Enum.GetValues(typeof(MemberTypes))
                        select new { ID = (int)type, Name = type.ToString() };
            ViewData["Type"] = new SelectList(types, "ID", "Name");

            VM_SignUp vm_signup = new VM_SignUp();

            return View(vm_signup);
        } 

        [HttpPost]
        public ActionResult SignUp(VM_SignUp vm_signup)
        {
            var types = from MemberTypes type in Enum.GetValues(typeof(MemberTypes))
                        select new { ID = (int)type, Name = type.ToString() };
            ViewData["Type"] = new SelectList(types, "ID", "Name");

            if (ModelState.IsValid)
            {
                //if( !VerifyCode(Request["verify"].ToString(),t_hy_member.CellPhone) )
                //{
                //    ViewData["error"] = "验证码校验失败，请核对后重试!";
                //    return View(t_hy_member);
                //} 
                var member = new T_HY_Member();
                member.LoginName = vm_signup.LoginName;
                member.MemberName = vm_signup.MemberName;
                member.CellPhone = vm_signup.CellPhone;
                member.CreatedAt = DateTime.Now;
                member.UpdatedAt = DateTime.Now;
                member.Password = vm_signup.Password;
                member.Type = vm_signup.Type; 

                db.T_HY_Member.Add(member);
                db.SaveChanges();

                //根据用户类型，往不同的业务用户数据表中初始化信息
                InitMemberDetail(member.Type, member.ID); 
                Session["MemberID"] = member.ID;

                if(Session["RedirectUrl"] != null && Session["RedirectUrl"].ToString() != "")
                {
                    return Redirect(Session["RedirectUrl"].ToString());
                }
                return RedirectToAction("Index","Home");  
            }

            return View(vm_signup);
        }


        public PartialViewResult Edit()
        { 
            var member = CurrentMember();
            if(member != null)
            {
                VM_EditMember vm_edit_member = new VM_EditMember();
                vm_edit_member.MemberName = member.MemberName;
                vm_edit_member.Password = member.Password;
                vm_edit_member.CellPhone = member.CellPhone;
                return PartialView(vm_edit_member);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult Edit(VM_EditMember vm_edit_member)
        {

            //if (!VerifyCode(Request["verify"].ToString(), t_hy_member.CellPhone))
            //{
            //    return Json(new { statusCode = "200", message = "验证码错误！请检查后输入", type = "error" });
            //} 
            var member = CurrentMember();
            if(member != null)
            {
                db.Entry(member).State = EntityState.Modified;
                member.UpdatedAt = DateTime.Now;
                member.MemberName = vm_edit_member.MemberName;
                member.Password = vm_edit_member.Password;
                member.CellPhone = vm_edit_member.CellPhone;
                db.SaveChanges();
                return Json(new { statusCode = "200", message = "信息保存成功！", type = "success" });
            } 

            return Json(new { statusCode = "200", message = "信息保存失败！", type = "error" });
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
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.MemberType = member.Type;
            ViewBag.MemberID = member.ID;
            ViewBag.MemberDetailID = FindIDForDetail(member.Type);
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
            try
            {
                T_HY_Member member = db.T_HY_Member.Where(m => m.LoginName == loginname).First();
                if (member != null && member.Password == password)
                {
                    Session["MemberID"] = member.ID;
                    RegisterLoginInfo();
                    if (Session["RedirectUrl"] != null && Session["RedirectUrl"].ToString() != "")
                    {
                        return Redirect(Session["RedirectUrl"].ToString());
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["error"] = "密码错误，请检查后重新尝试!";
                    return View();
                }
            }
            catch
            {
                ViewData["error"] = "登陆名错误，请检查后重新尝试!";
                return View();
            } 
        }

        public ActionResult Logout()
        {
            Session["MemberID"] = null;
            RegisterLogoutInfo();
            return RedirectToAction("Index", "Home");
        }

        //-----------msgHelper-------------------// 
        public bool SendVerifyCodeToPhone(string phone_number)
        {
            Random r = new Random();
            string verify_code = r.Next(100000,999999).ToString();

            if (SendMsg(verify_code, phone_number))
            {
                //Session[phone_number] = verify_code;
                Session[phone_number] = "123456";
                return true;
            }
            return false; 
        }

        //根据用户提交的验证码进行身份验证
        public bool VerifyCode(string verify_code, string phone_number)
        {
            try
            {
                if (Session[phone_number].ToString() == verify_code)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool SendMsg(string msg, string phone_number)
        {
            //-------TO-DO---------//
            //实现发送短信到手机
            return true;
        }

        //
        //发送随机的登录密码，用于忘记密码的用户临时登录用 
        public JsonResult SendRandomPwd(string loginname)
        {
            Random r = new Random();
            string random_pwd = r.Next(100000000,999999999).ToString();
            T_HY_Member member = T_HY_Member.CurrentMemberByLoginname(loginname);

            if (member == null)
                return Json("不存在该用户!!", JsonRequestBehavior.AllowGet);

            //if (SendMsg(random_pwd, member.CellPhone))
            if (SendMsg("123456", member.CellPhone))
            {
                member.Password = T_HY_Member.EncryptPwd(random_pwd);
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return Json("已向该用户名所绑定的手机号发送了临时登陆密码，请及时登陆并修改密码！", JsonRequestBehavior.AllowGet);
            }
            return Json("发送失败!", "text/html", JsonRequestBehavior.AllowGet);
        } 
 
        //----------------登录验证-----------------//
        public JsonResult CheckLoginName(string loginname)
        { 
            return Json(!db.T_HY_Member.Any(m => m.LoginName == loginname), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckCellPhone(string cellphone)
        {
            return Json(!db.T_HY_Member.Any(m => m.CellPhone == cellphone), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckCellPhoneForRegistered(string cellphone)
        {
            var current_member_cellphone = CurrentMember().CellPhone;
            return Json(!db.T_HY_Member.Where(m => m.CellPhone != current_member_cellphone).Any(m => m.CellPhone == cellphone), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckMemberName(string membername)
        {
            var current_member_membername = CurrentMember().MemberName;
            return Json(!db.T_HY_Member.Where(m => m.MemberName != current_member_membername).Any(m => m.MemberName == membername), JsonRequestBehavior.AllowGet);
        }

        public void RegisterLoginInfo()
        {
            var member = CurrentMember();
            if(member != null)
            {
                var online_log = new T_ZC_OnlineLog();
                online_log.IpAddress = HttpContext.Request.UserHostAddress;
                online_log.LoginInDate = DateTime.Now;
                online_log.LoginOutDate = DateTime.Now;
                member.OnlineLogs.Add(online_log);
                db.SaveChanges();

                Session["OnlineID"] = online_log.ID;
            } 
        }

        public void RegisterLogoutInfo()
        {
            var member = CurrentMember();
            if (member != null)
            {
                var online_log = new T_ZC_OnlineLog(); 
                online_log.LoginOutDate = DateTime.Now;
                member.OnlineLogs.Add(online_log);
                db.SaveChanges();

                Session["OnlineID"] = null; 
            } 
        }


        //
        //----------------三类用户的信息维护-----------//

        public bool InitMemberDetail(int type, int member_id)
        {
            bool result = false;
            switch(type)
            {
                case 1:
                    result =  InitPerson(member_id);
                break;
                case 2:
                    result =  InitCorp(member_id);
                break;
                case 3:
                    result = InitAgency(member_id);
                break;
            }
            return result;
        }

        public bool InitCorp(int member_id)
        {
            T_QY_Corp corp = new T_QY_Corp();
            corp.MemberID = member_id;
            db.T_QY_Corp.Add(corp);

            if (db.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public bool InitPerson(int member_id)
        {
            T_QY_Person person = new T_QY_Person();
            person.MemberID = member_id;
            db.T_QY_Person.Add(person);

            if (db.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public bool InitAgency(int member_id)
        {
            T_JG_Agency agency = new T_JG_Agency();
            agency.MemberID = member_id;
            db.T_JG_Agency.Add(agency);

            if (db.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public int FindIDForDetail(int member_type)
        {

            int result = 0;
            var member = CurrentMember();
            switch (member_type)
            {
                case 1:
                    result = db.T_QY_Person.First(p => p.MemberID == member.ID).ID;
                    break;
                case 2:
                    result = db.T_QY_Corp.First(p => p.MemberID == member.ID).ID;
                    break;
                case 3:
                    result = db.T_JG_Agency.First(p => p.MemberID == member.ID).ID;
                    break;
            }
            return result;
        }

        #region//为外网服务
        public string CurrentMemberForPortal()
        {
            if(CurrentMember() != null)
            {
                return CurrentMember().MemberName + "!";
            }
            return "";
        }

        public void LogoutForPortal()
        {
            Session["MemberID"] = null; 
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}