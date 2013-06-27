using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.Util;

namespace GGZBTQPT_PRO.Areas.MG.Controllers
{
    public class CorpInvInfoController : BaseController
    {
        public void BindIndustry(object select = null)
        {
            List<T_PTF_DicDetail> Industry = db.T_PTF_DicDetail.Where(p => (p.DicType == "XM01")).ToList();

            ViewData["AttIndustry"] = new SelectList(Industry, "Name", "Name", select);
        }

        public ActionResult Create()
        {
            BindIndustry();
            var t_qy_tzxq = new T_QY_TZXQ();
            return View(t_qy_tzxq);
        }

        //
        // POST: /CorpInvInfo/Create

        [HttpPost]
        public ActionResult Create(T_QY_TZXQ t_qy_tzxq, FormCollection collection)
        {
            BindIndustry();
            if (collection["VCode"] == Session["ValidateCode"].ToString())
            {
                if (ModelState.IsValid)
                {
                    t_qy_tzxq.AgencyNature = collection["AgencyNature"];
                    t_qy_tzxq.AttIndustry = collection["AttIndustry"];
                    t_qy_tzxq.AttCorpType = collection["AttCorpType"];
                    db.T_QY_TZXQ.Add(t_qy_tzxq);
                    int result = db.SaveChanges();
                    if (result > 0)
                        Response.Write("<script>alert('您的信息已提交成功!');</script>");
                }
            }
            else
            { 
                Response.Write("<script>alert('验证码错误，请重新输入!');</script>");
            }
            return View(t_qy_tzxq);
        }
    }
}
