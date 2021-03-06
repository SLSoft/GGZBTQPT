using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.Enums;
using GGZBTQPT_PRO.Util;

namespace GGZBTQPT_PRO.Areas.MG.Controllers
{
    public class BaseController : Controller
    {
        public GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        protected T_HY_Member CurrentMember()
        {
            if (Session["MemberID"] != null && Session["MemberID"].ToString() != "")
            {
                var member = db.T_HY_Member.Find(Convert.ToInt32(Session["MemberID"].ToString()));
                return member;
            }
            return null;
        }

        public ActionResult CheckMemberForRedirect(string redirect_url)
        {
            if(CurrentMember() != null)
            { 
                return Redirect(redirect_url);
            }
            else
            {
                return RedirectToAction("Login", "Member", new { url = redirect_url });
            }
        }

        /// <summary>
        /// 读取当前用户所关注的用户的ID集合
        /// </summary>
        /// <returns></returns>
        protected string AttentionedMembers()
        {
            string attentioned_members = "";
            List<int> member_ids = new List<int>();
            member_ids = CurrentMember().Attentions.Select(a => a.AttentionedMemberID).ToList();

            foreach (int member_id in member_ids)
            {
                attentioned_members += "|" + member_id + "|";
            }

            return attentioned_members;
        }

        /// <summary>
        /// 获取关注特定机构的会员集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //protected IEnumerable<T_HY_Member> AttentionedMembersForAgency(T_JG_Agency agency)
        //{
        //    var members = db.T_HY_Member.Where(m => m.Attentions.Where( a => a.AttentionedMemberID == agency.MemberID && a.AttentionedMemberType == 3)).ToList();
        //}

        /// <summary>
        /// 读取当前用户所收藏的项目的ID集合
        /// </summary>
        /// <returns></returns>
        protected string FavoredItems(int favorite_type)
        {
            var member = CurrentMember();
            if (member == null)
            {
                return null;
            }

            string favored_items = "";
            List<int?> item_ids = new List<int?>();

            switch (favorite_type)
            {
                case 1:
                    item_ids = CurrentMember().Favorites.Where(f => f.FavoriteType == favorite_type).Select(f => f.FinancialID).ToList();
                    break;
                case 2:
                    item_ids = CurrentMember().Favorites.Where(f => f.FavoriteType == favorite_type).Select(f => f.InvestmentID).ToList();
                    break;
                case 3:
                    item_ids = CurrentMember().Favorites.Where(f => f.FavoriteType == favorite_type).Select(f => f.ProductID).ToList();
                    break;
            }

            foreach (int finacial_id in item_ids)
            {
                favored_items += "|" + finacial_id + "|";
            }

            return favored_items;
        }

        //日志处理
        /// <summary>
        /// 会员错误日志
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="operate_type"></param>
        /// <param name="generate_type"></param>
        /// <param name="generate_system"></param>
        /// <param name="exception"></param>
        public void LoggingError(int level, string message, int operate_type, int generate_type, int generate_system, string exception = "无")
        {
            log4net.ILog log = log4net.LogManager.GetLogger(this.GetType());

            if (Session["MemberName"] != null)
            {
                log4net.LogicalThreadContext.Properties["user"] = Session["MemberName"];
            }
            else
            {
                log4net.LogicalThreadContext.Properties["user"] = "无";
            }
            log4net.LogicalThreadContext.Properties["operate_type"] = operate_type;
            log4net.LogicalThreadContext.Properties["generate_type"] = generate_type;
            log4net.LogicalThreadContext.Properties["generate_system"] = generate_system;

            switch (level)
            {
                case (int)LogLevels.error:
                    log.Error(message, new Exception(exception));
                    break;
                case (int)LogLevels.warn:
                    log.Warn(message);
                    break;
                case (int)LogLevels.login:
                    //------ToDo------//
                    //创建登录日志，登录日志需要记录用户登录和离开系统的时间
                    break;
                case (int)LogLevels.operate:
                    log.Info(message);
                    break;
            }
        }

        /// <summary>
        /// 会员操作日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="operate_type"></param>
        /// <param name="generate_module"></param>
        public void Logging(string message, int operate_type, int generate_module)
        {
            UpdateEndDateTimeWithMemberLog();
            T_ZC_MemberLog member_log = new T_ZC_MemberLog();

            member_log.Message = message;
            member_log.Member = CurrentMember();
            member_log.OperateType = operate_type;
            member_log.GenerateModule = generate_module;

            //为了避免用户的意外退出，造成最后一个操作无法记录结束的时间，这里近似的给一个结束时间
            member_log.EndDateTime = DateTime.Now;
            member_log.Continuance = member_log.EndDateTime - member_log.StartDateTime;

            db.T_ZC_MemberLog.Add(member_log);
            db.SaveChanges();

            Session["MemberLogID"] = member_log.ID;
        }

        public void UpdateEndDateTimeWithMemberLog()
        {
            if (Session["MemberLogID"] != null && Session["MemberLogID"].ToString() != "")
            {
                var member_log = db.T_ZC_MemberLog.Find((int)Session["MemberLogID"]);

                db.Entry(member_log).State = EntityState.Modified;
                member_log.EndDateTime = DateTime.Now;
                member_log.Continuance = member_log.EndDateTime - member_log.StartDateTime;

                db.SaveChanges();
            }
        }

        public ActionResult GetValidateCode()
        {

            ValidateCode vCode = new ValidateCode();

            string code = vCode.CreateValidateCode(4);

            Session["ValidateCode"] = code;

            byte[] bytes = vCode.CreateValidateGraphic(code);

            return File(bytes, @"image/jpeg");

        }

        public ActionResult GetCurrentValidateCode()
        {

            return Content(Session["ValidateCode"].ToString());

        }
    }
}

