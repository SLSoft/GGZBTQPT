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
using GGZBTQPT_PRO.Util;
using GGZBTQPT_PRO.APIModels;
 

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

        #region SignUP
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
                InitMemberDetail(member.Type, member.ID,member.MemberName); 
                Session["MemberID"] = member.ID;

                Logging("注册了会员,登录名：" + member.LoginName, (int)OperateTypes.Create, (int)GenerateSystem.Authority);
                Mail.SendEmail(member.Email, "欢迎您注册光谷资本特区会员！", Welcome(member.LoginName, member.Password));
                BusinessService.SendMessageFromManage(member, "感谢您注册光谷资本特区，我们将在24小时内对您的资料进行审核！", "欢迎您注册光谷资本特区");

                if(Session["RedirectUrl"] != null && Session["RedirectUrl"].ToString() != "")
                {
                    return Redirect(Session["RedirectUrl"].ToString());
                }
                return RedirectToAction("Index","Home");  
            }
            
            return View(vm_signup);
        }
        #endregion

        #region Edit
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

                Logging("更新了会员信息：", (int)OperateTypes.Edit, (int)GenerateSystem.Personal);

                return Json(new { statusCode = "200", message = "信息保存成功！", type = "success" });
            } 

            return Json(new { statusCode = "200", message = "信息保存失败！", type = "error" });
        }
        #endregion

        #region Delete
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
        #endregion

        #region Config
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
        #endregion
 
        #region Login
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
                    Session["MemberName"] = member.MemberName;
                    Session["MemberType"] = member.Type;
                    Session["IsVerified"] = member.IsVerified;
                    Session["UnReadMsg"] = member.ReceivedMessages.Count();

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

        public string Welcome(string member_name, string member_pwd)
        {
            string welcome = "<h3>欢迎注册光谷资本特区会员</h3>";
            welcome += "<strong>尊敬的" + member_name + ":</strong>";
            welcome += "<p>非常感谢您注册成为光谷资本特区会员！您能享受到的服务如下：</p>";
            welcome += "<ul><li>优质的项目、资金及产品推荐</li>" + 
                           "<li>定期的会员活动和期刊</li>" + 
                           "<li>多样的会员互动环境</li>" + 
                           "<li>成熟、完善的金融服务支持</li>" + 
                           "</ul>";
            welcome += "<hr />";
            welcome += "<h3>以下是您注册的会员名和密码：</h3>";
            welcome += "<ul><li>会员名：" + member_name + "</li>" + 
                           "<li>密码：" + member_pwd + "</li>" + 
                           "</ul>";

            return welcome;
                            
        }

        public ActionResult Logout()
        {
            Session["MemberID"] = null;
            Session["MemberName"] = null;
            RegisterLogoutInfo();

            return RedirectToAction("Index", "Home");
        }
        #endregion
 
        #region Msg
        public bool SendVerifyCodeToPhone(string phone_number)
        {
            Random r = new Random();
            string verify_code = r.Next(100000, 999999).ToString();

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

        public bool SendPwdByEmail(string random_pwd, T_HY_Member member)
        {
            try
            {
                Mail.SendEmail(member.Email, "光谷资本特区密码重置！", ForgetPwd(member.LoginName, random_pwd));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string ForgetPwd(string member_name, string member_pwd)
        {
            string welcome = "<h2>密码重置</h2>";
            welcome += "<strong>尊敬的" + member_name + ":</strong>";
            welcome += "<h3>我们已经为您设置了临时登陆密码，请尽快登陆专区，并进行修改：</h3>";
            welcome += "<ul><li>会员名：" + member_name + "</li>" +
                           "<li>密码：" + member_pwd + "</li>" +
                           "</ul>";

            return welcome;

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
            //if (SendMsg("123456", member.CellPhone))
            //{
            //    member.Password = T_HY_Member.EncryptPwd(random_pwd);
            //    db.Entry(member).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return Json("已向该用户名所绑定的手机号发送了临时登陆密码，请及时登陆并修改密码！", JsonRequestBehavior.AllowGet);
            //}

            if(SendPwdByEmail(random_pwd, member))
            {
                member.Password = T_HY_Member.EncryptPwd(random_pwd);
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges(); 
                return Json("已向该用户名所绑定的邮件发送了临时登陆密码，请及时登陆并修改密码！", JsonRequestBehavior.AllowGet);
                
            }
            return Json("发送失败!", "text/html", JsonRequestBehavior.AllowGet);
        } 
        #endregion
        
        #region Authorition
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
        
        //用于短消息中的接收人验证，必须是系统中有的用户，并且不能是自己
        public JsonResult CheckMember([Bind(Prefix = "ReceiveMember")] string receivemembername)
        {
            var current_member_membername = CurrentMember().MemberName;
            return Json(db.T_HY_Member.Any(m => m.MemberName == receivemembername && m.MemberName != current_member_membername), JsonRequestBehavior.AllowGet);
        }
        //后台表单验证
        public bool CheckState(VM_SignUp vm_signup)
        {
            bool result = true;
            if (vm_signup.CellPhone.Trim() == "" || vm_signup.Email.Trim() == "")
                result = false;
            else
                result = (bool)CheckLoginName(vm_signup.LoginName).Data && (bool)CheckCellPhone(vm_signup.CellPhone).Data && (bool)CheckCellPhoneForRegistered(vm_signup.CellPhone).Data && (bool)CheckMemberName(vm_signup.MemberName).Data;
            return result;
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

                Logging("进行了登陆操作", (int)OperateTypes.Login, (int)GenerateSystem.Authority);
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

                Logging("进行了注销操作", (int)OperateTypes.Logout, (int)GenerateSystem.Authority);
                UpdateEndDateTimeWithMemberLog();

                Session["OnlineID"] = null; 
                Session["MemberLogID"] = null;
            } 
        }
        #endregion

        /**
        * 用于对注册会员的分类信息进行初始化
        * 包括企业、创业者、机构等
        */
        #region MemberDetailInit 
        public bool InitMemberDetail(int type, int member_id,string member_name)
        {
            bool result = false;
            switch(type)
            {
                case 1:
                    result = InitPerson(member_id, member_name);
                    break;
                case 2:
                    result = InitCorp(member_id, member_name);
                    break;
                case 3:
                    result = InitAgency(member_id, member_name);
                    break;
            }
            return result;
        }

        public bool InitCorp(int member_id, string member_name)
        {
            try
            {
                T_QY_Corp corp = new T_QY_Corp();
                corp.MemberID = member_id;
                corp.CorpName = member_name;
                db.T_QY_Corp.Add(corp);

                if (db.SaveChanges() > 0)
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

        public bool InitPerson(int member_id,string member_name)
        {
            try
            {
                T_QY_Person person = new T_QY_Person();
                person.MemberID = member_id;
                person.Name = member_name;
                db.T_QY_Person.Add(person);

                if (db.SaveChanges() > 0)
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

        public bool InitAgency(int member_id,string member_name)
        {
            try
            {
                T_JG_Agency agency = new T_JG_Agency();
                agency.MemberID = member_id;
                agency.AgencyName = member_name;
                db.T_JG_Agency.Add(agency);

                if (db.SaveChanges() > 0)
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
        #endregion

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

        //为外网返回会员信息
        public ActionResult MemberInfo()
        {
            try
            {
                return Json(db.T_HY_Member.Select(m => new { 
                                                                name = m.MemberName, 
                                                                email = m.Email, 
                                                                cell_phone = m.CellPhone  
                                                            })
                                          .ToList(),JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(new {});
            }
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}