﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.Enums;
using System.IO;
using GGZBTQPT_PRO.Util;
using GGZBTQPT_PRO.Filter;

namespace GGZBTQPT_PRO.Util
{
    [ActionAttributeFilter()]
    public class BusinessService
    {   
        
        /// <summary>
        /// 项目转化接口
        /// </summary>
        /// <param name="xm_id">项目ID</param>
        /// <param name="xm_name">项目名称</param>
        /// <param name="xm_type">项目类型（参考枚举）</param>
        /// <param name="xm_summary">项目描述</param>
        /// <returns></returns>
        public static bool TransformatFromXM(int xm_id, string xm_name, int xm_type, string xm_summary)
        {
            using(GGZBTQPT_PRO.Models.GGZBTQPTDBContext db = new GGZBTQPT_PRO.Models.GGZBTQPTDBContext())
            {
                try
                {
                    T_XM_Case t_xm_case = new T_XM_Case();
                    t_xm_case.GenerateFromID = xm_id;
                    t_xm_case.Type = xm_type;
                    t_xm_case.GenerateType = (int)GenerateTypes.FromSystem;
                    t_xm_case.Name = xm_name;
                    t_xm_case.Summary = xm_summary;
                    t_xm_case.Analysis = "无";
                    t_xm_case.CreatedAt = DateTime.Now;
                    t_xm_case.UpdatedAt = DateTime.Now;

                    db.T_XM_Case.Add(t_xm_case);
                    db.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        } 


        public static bool SendMessageFromManage(T_HY_Member receive_member, string content, string title)
        {            
            using(GGZBTQPT_PRO.Models.GGZBTQPTDBContext db = new GGZBTQPT_PRO.Models.GGZBTQPTDBContext())
            {
                try
                {
                    T_HY_Member send_member = db.T_HY_Member.Find(9999);
                    T_HY_Message message = new T_HY_Message();
                    message.RelateID = 0;
                    message.Content = content;
                    message.SendMember = send_member;
                    message.ReceiveMember = receive_member;
                    message.Title = title;

                    db.T_HY_Message.Add(message);
                    db.SaveChanges();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}