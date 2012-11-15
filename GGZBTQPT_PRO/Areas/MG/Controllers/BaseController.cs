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

    }
}
