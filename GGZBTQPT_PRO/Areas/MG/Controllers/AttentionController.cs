using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.Areas.ViewModels;
using GGZBTQPT_PRO.Enums;
using Webdiyer.WebControls.Mvc;
using GGZBTQPT_PRO.Areas.MG.Filter;

namespace GGZBTQPT_PRO.Areas.MG.Controllers
{
    [MemberFilter()]
    public class AttentionController : BaseController
    {


        //
        // GET: /Member/Publish/

        public ActionResult Index()
        {
            Logging((int)LogLevels.operate, "访问了我的关注", (int)OperateTypes.Visit, (int)GenerateTypes.FromMember, (int)GenerateSystem.Attention);
            var member = db.T_HY_Member.Find(CurrentMember().ID);
            return View(member);
           
        }

        /// <summary>
        /// 用户所关注的机构
        /// </summary>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public ActionResult AttentionedAgencies(int id = 1)
        {
            Logging((int)LogLevels.operate, "访问了我关注的机构", (int)OperateTypes.Visit, (int)GenerateTypes.FromMember, (int)GenerateSystem.Attention);
            var member = CurrentMember();
           
            try
            {
                IList<VM_AttentionedAgency> attentions = member.Attentions.Where(a => a.AttentionedMemberType == 3)
                                    .Join(db.T_JG_Agency, a => a.AttentionedMemberID, p => p.MemberID,
                                    (a, g) => new VM_AttentionedAgency { Agency = new T_JG_Agency {AgencyName = g.AgencyName, Address = g.Address, Phone = g.Phone,
                                                                Services = g.Services, Linkmans = g.Linkmans, RegTime = g.RegTime  }, Member = db.T_HY_Member.Find(g.MemberID) })
                                    .ToList();
                PagedList<VM_AttentionedAgency> paged_attentions = new PagedList<VM_AttentionedAgency>(attentions, id, 5);
                return PartialView(paged_attentions);
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
        public ActionResult AttentionedCorps(int id = 1)
        {
            Logging((int)LogLevels.operate, "访问了我关注的企业", (int)OperateTypes.Visit, (int)GenerateTypes.FromMember, (int)GenerateSystem.Attention);
            var member = CurrentMember();
            
            try
            {
                IList<VM_AttentionedCorp> corps = member.Attentions.Where(a => a.AttentionedMemberType == 2)
                                .Join(db.T_QY_Corp, a => a.AttentionedMemberID, p => p.MemberID,
                                    (a, c) => new  VM_AttentionedCorp { Corp = new T_QY_Corp {CorpName = c.CorpName, Mobile = c.Mobile, Email = c.Email, Fax = c.Fax,
                                                              Linkman = c.Linkman, Phone = c.Phone, QQ = c.QQ, Website = c.Website,
                                                              City = c.City, CorpCode = c.CorpCode, RegTime = c.RegTime }, Member = db.T_HY_Member.Find(c.MemberID) })
                                .ToList();
                PagedList<VM_AttentionedCorp> paged_corps = new PagedList<VM_AttentionedCorp>(corps, id, 5);
                return PartialView(paged_corps);
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
        public ActionResult AttentionedPersons(int id = 1)
        {
            Logging((int)LogLevels.operate, "访问了我关注的创业者", (int)OperateTypes.Visit, (int)GenerateTypes.FromMember, (int)GenerateSystem.Attention);
            var member = CurrentMember();
            
            try
            {
                //var attentions = member.Attentions.Where(a => a.AttentionType == 1 || a.AttentionType == 2).ToList(); 

                IList<VM_AttentionedPerson> persons = member.Attentions.Where(a => a.AttentionedMemberType == 1)
                                .Join( db.T_QY_Person, a => a.AttentionedMemberID, p => p.MemberID, 
                                        (a,p) => new VM_AttentionedPerson { Person = new T_QY_Person{ MemberID = p.MemberID, Name = p.Name, Mobile = p.Mobile, Email = p.Email, College = p.College, 
                                                                   Phone = p.Phone, WorkExperience = p.WorkExperience, Education = p.Education, Specialty = p.Specialty, CreateTime = p.CreateTime }, Member = db.T_HY_Member.Find(p.MemberID) })
                                .ToList();
                ViewBag.CurrentMemberID = member.ID;
                PagedList<VM_AttentionedPerson> paged_persons = new PagedList<VM_AttentionedPerson>(persons, id, 5);
                return PartialView(paged_persons);
            }
            catch
            {
                return PartialView();
            } 
        } 

        /// <summary>
        /// 关注的个人、企业和机构
        /// </summary>
        /// <param name="type_id">关注的会员类别</param>
        /// <param name="id">会员ID</param>
        [HttpPost]
        public ActionResult Attentioned(int type, int id)
        { 
            var member = CurrentMember();
           
            var attentioned_item = new T_HY_Attention();

            attentioned_item.AttentionedMemberID = id;
            attentioned_item.AttentionedMemberType = type; 
            member.Attentions.Add(attentioned_item);
            db.SaveChanges();

            Logging((int)LogLevels.operate, "关注了XZXX", (int)OperateTypes.Attention, (int)GenerateTypes.FromMember, (int)GenerateSystem.Attention);
            return Json(new { statusCode = "200", message = "关注成功", type = "success" });
        }

        /// <summary>
        /// 取消关注的个人、企业和机构
        /// </summary>
        /// <param name="type_id">关注的会员类别</param>
        /// <param name="id">会员ID</param>
        [HttpPost]
        public ActionResult UnAttentioned(int id)
        {
            var member = CurrentMember(); 

            var unattentioned_item = member.Attentions.First( a => a.AttentionedMemberID == id);

            member.Attentions.Remove(unattentioned_item);
            db.T_HY_Attention.Remove(unattentioned_item);
            db.SaveChanges();

            Logging((int)LogLevels.operate, "取消关注了XZXX", (int)OperateTypes.Attention, (int)GenerateTypes.FromMember, (int)GenerateSystem.Attention);
            return Json(new { statusCode = "200", message = "取消关注成功", type = "success" });
        }

    }
}
