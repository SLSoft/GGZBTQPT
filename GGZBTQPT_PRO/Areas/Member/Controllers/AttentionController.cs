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
    public class AttentionController : Controller
    {

        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();
        //
        // GET: /Member/Publish/

        public ActionResult Index()
        {

            if (CurrentMember() != null)
            {
                var member = db.T_HY_Member.Find(CurrentMember().ID);
                return View(member);
            }
            else
            {
                return RedirectToAction("Login", "Member");
            }
        }

        /// <summary>
        /// 用户所关注的机构
        /// </summary>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public ActionResult AttentionedAgencies(int member_id)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Login", "Member");
            }
            try
            {
                var attentions = member.Attentions.Where(a => a.AttentionType == 3).ToList();
                return PartialView(attentions);
            }
            catch
            {
                return PartialView();
            } 
        }

        /// <summary>
        /// 用户所关注的企业
        /// </summary>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public ActionResult AttentionedCorps(int member_id)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Login", "Member");         
            } 
            try
            {
                var corps = member.Attentions.Where(a => a.AttentionType == 1)
                                .Join(db.T_QY_Corp, a => a.AttentionedID, p => p.MemberID, (a, c) => new T_QY_Corp { CorpName = c.CorpName, Mobile = c.Mobile })
                                .ToList();
                return PartialView(corps);
            }
            catch
            {
                return PartialView();
            } 
        }

        /// <summary>
        /// 用户所关注的个人
        /// </summary>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public ActionResult AttentionedPersons(int member_id)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return RedirectToAction("Login", "Member");       
            } 
            try
            {
                //var attentions = member.Attentions.Where(a => a.AttentionType == 1 || a.AttentionType == 2).ToList(); 

                var persons = member.Attentions.Where(a => a.AttentionType == 2)
                                .Join( db.T_QY_Person, a => a.AttentionedID, p => p.MemberID, 
                                        (a,p) => new T_QY_Person { Name = p.Name, Mobile = p.Mobile, Email = p.Email, College = p.College, 
                                                                   Phone = p.Phone, WorkExperience = p.WorkExperience, Education = p.Education, Specialty = p.Specialty })
                                .ToList();
                return PartialView(persons);
            }
            catch
            {
                return PartialView();
            } 
        }

        //Helper
        private T_HY_Member CurrentMember()
        {
            if (Session["MemberID"] != null && Session["MemberID"].ToString() != "")
            {
                var member = db.T_HY_Member.Find(Convert.ToInt32(Session["MemberID"].ToString()));
                return member;
            }
            return null;
        }


        /// <summary>
        /// 收藏项目、资金、服务
        /// </summary>
        /// <param name="type_id">收藏的类别</param>
        /// <param name="id">项目、资金、服务ID</param>
        [HttpPost]
        public ActionResult Attentioned(int type_id, int id)
        {
            var attentioned_item = new T_HY_Attention();
            attentioned_item.AttentionedID = id;
            attentioned_item.AttentionType = type_id;

            db.T_HY_Attention.Add(attentioned_item);
            db.SaveChanges();

            return Json(new { statusCode = "200", message = "关注成功" });
        }

        /// <summary>
        /// 取消收藏项目、资金、服务
        /// </summary>
        /// <param name="type_id">取消收藏的类别</param>
        /// <param name="id">项目、资金、服务ID</param>
        [HttpPost]
        public ActionResult UnAttentioned(int id)
        {
            var unattentioned_item = db.T_HY_Attention.Find(id);

            db.T_HY_Attention.Remove(unattentioned_item);
            db.SaveChanges();

            return Json(new { statusCode = "200", message = "取消关注成功" });
        }

    }
}
