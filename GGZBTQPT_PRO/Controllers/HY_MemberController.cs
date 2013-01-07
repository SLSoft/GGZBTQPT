using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;
using GGZBTQPT_PRO.Enums;
using GGZBTQPT_PRO.ViewModels;
using Webdiyer.WebControls.Mvc;
using GGZBTQPT_PRO.Areas.MG.Controllers;
using GGZBTQPT_PRO.Util;

namespace GGZBTQPT_PRO.Controllers
{ 
    public class HY_MemberController : BaseController
    {
        #region 会员基础管理
        /// <summary>
        /// 会员基础管理
        /// </summary>
        /// <param name="pageNum">当前页码</param>
        /// <param name="numPerPage">每页显示多少条</param>
        /// <param name="keywords">搜索关键字</param>
        /// <returns></returns>
        public ActionResult Index(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            IList<GGZBTQPT_PRO.Models.T_HY_Member> list = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords) && p.IsValid == true && p.IsVerified == true)
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords) && p.IsValid == true && p.IsVerified == true).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;

            return View(list);
        } 

        public ActionResult Create()
        {
            ViewBag.Type = GetMemberType();
            return View();
        } 

        [HttpPost]
        public ActionResult Create(T_HY_Member t_hy_member)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    var member = new T_HY_Member();
                    member.LoginName = t_hy_member.LoginName;
                    member.MemberName = t_hy_member.MemberName;
                    member.CellPhone = t_hy_member.CellPhone;
                    member.Email = t_hy_member.Email;
                    member.CreatedAt = DateTime.Now;
                    member.UpdatedAt = DateTime.Now;
                    member.Password = t_hy_member.Password;
                    member.Type = t_hy_member.Type;
                    member.IsVerified = true;
                    member.State = 1;
                    member.Password = "123456";

                    db.T_HY_Member.Add(member);
                    int result = db.SaveChanges();

                    MemberController m = new MemberController();
                    bool boo = m.InitMemberDetail(member.Type, member.ID); 

                    if (result > 0 && boo == true)
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
            }
            ViewBag.Type = GetMemberType();
            return Json(new { });
        }
 
        public ActionResult Edit(int id)
        {
            T_HY_Member t_hy_member = db.T_HY_Member.Find(id);
            if (t_hy_member != null)
            {
                VM_EditMember member = new VM_EditMember();
                member.ID = t_hy_member.ID;
                member.MemberName = t_hy_member.MemberName;
                member.CellPhone = t_hy_member.CellPhone;
                member.Email = t_hy_member.Email;
                return View(member);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id,VM_EditMember member)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    T_HY_Member t_hy_member = db.T_HY_Member.Find(id);

                    if (db.T_HY_Member.Where(m => m.ID != t_hy_member.ID).Any(m => m.MemberName == member.MemberName))
                    {
                        return ReturnJson(false, "该用户名已经被使用了，请尝试更改!", "", "", false, "");
                    }

                    if (db.T_HY_Member.Where(m => m.ID != t_hy_member.ID).Any(m => m.CellPhone == member.CellPhone))
                    {
                        return ReturnJson(false, "该手机号码已经被使用了，请尝试更改!", "", "", false, "");
                    }

                    t_hy_member.UpdatedAt = Convert.ToDateTime(DateTime.Now.ToLongTimeString());
                    t_hy_member.MemberName = member.MemberName;
                    t_hy_member.CellPhone = member.CellPhone;
                    t_hy_member.Email = member.Email;
                    db.Entry(t_hy_member).State = EntityState.Modified;

                    int result = db.SaveChanges();
                    if (result > 0)
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
            }
            return Json(new { });
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.IsAjaxRequest())
            {
                T_HY_Member t_hy_member = db.T_HY_Member.Find(id);
                t_hy_member.IsValid = false;
                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "", "", false, "");
                else
                    return ReturnJson(false, "操作失败", "", "", false, "");
            }
            return Json(new { });
        }

        //----------------验证-----------------//
        public JsonResult CheckLoginName(string loginname)
        {
            return Json(!db.T_HY_Member.Any(m => m.LoginName == loginname), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckCellPhone(string cellphone)
        {
            return Json(!db.T_HY_Member.Any(m => m.CellPhone == cellphone), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 会员审核
        /// <summary>
        /// 待审核会员列表
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageNum"></param>
        /// <param name="numPerPage"></param>
        /// <returns></returns>
        public ActionResult UnVerified(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            IList<GGZBTQPT_PRO.Models.T_HY_Member> list = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords) && p.IsValid == true && p.State == 0)
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords) && p.IsValid == true && p.State==0).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;

            return View(list);
        }

        /// <summary>
        /// 已审核会员列表
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageNum"></param>
        /// <param name="numPerPage"></param>
        /// <returns></returns>
        public ActionResult HasVerified(string keywords, int state = -1, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;
            var tqCount = 0;
            var tq = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords)).Where(p => p.IsValid == true);
            if (state == -1)
            {
                tq = tq.Where(p=>p.State != 0);
            }
            else
            {
                tq = tq.Where(p => p.State == state);
            }
            tqCount = tq.Count();

            IList<T_HY_Member> list = tq.OrderBy(s => s.ID)
                                                       .Skip(numPerPage * (pageNum - 1))
                                                       .Take(numPerPage).ToList();

            ViewBag.recordCount = tqCount;
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            ViewBag.state = GetMemberState();

            return View(list);
        } 

        /// <summary>
        /// 审核、驳回会员
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stateType"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Verify(int id,int stateType)
        {
            T_HY_Member t_hy_member = db.T_HY_Member.Find(id);

            if (stateType == 1)
            {
                t_hy_member.IsVerified = true;
            }
            t_hy_member.State = stateType;

            db.Entry(t_hy_member).State = EntityState.Modified;

            if (db.SaveChanges() > 0)
            {
                BusinessService.SendMessageFromManage(t_hy_member,"您的信息已经通过审核，您现在可以登陆会员专区发布各类金融信息了！", "您的信息已经通过审核!");
                return ReturnJson(true, "操作成功", "", "UnVerified", false, "");
            }
            else
                return ReturnJson(false, "操作失败", "", "", false, ""); 

        }

        /// <summary>
        /// 批量审核、驳回会员
        /// </summary>
        /// <param name="stateType"></param>
        /// <param name="checkedIds"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult batch_Verify(int stateType,string checkedIds)
        {
            checkedIds = RemoveTheLastComma(checkedIds);
            string[] strIds = checkedIds.Split(',');

            foreach (string id in strIds)
            {
                T_HY_Member t_hy_member = db.T_HY_Member.Find(Convert.ToInt32(id));

                if (stateType == 1)
                {
                    t_hy_member.IsVerified = true;
                }
                t_hy_member.State = stateType;

                db.Entry(t_hy_member).State = EntityState.Modified;

                BusinessService.SendMessageFromManage(t_hy_member, "您的信息已经通过审核，您现在可以登陆会员专区发布各类金融信息了！", "您的信息已经通过审核!");
            }
            if (db.SaveChanges() == strIds.Length)
                return ReturnJson(true, "批量操作成功", "", "", false, "");
            else
                return ReturnJson(false, "批量操作失败", "", "", false, "");
            
        }

        /// <summary>
        /// 撤销审核、撤销驳回
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UnVerify(int id)
        {
            T_HY_Member t_hy_member = db.T_HY_Member.Find(id);
            t_hy_member.IsVerified = false;
            t_hy_member.State = 0;
            db.Entry(t_hy_member).State = EntityState.Modified;

            if (db.SaveChanges() > 0)
                return ReturnJson(true, "撤销成功", "", "UnVerified", false, "");
            else
                return ReturnJson(false, "撤销失败", "", "", false, "");

        }

        public SelectList GetMemberState()
        {
            var States = from MemberStates mstate in Enum.GetValues(typeof(MemberStates))
                         select new { ID = (int)mstate, Name = mstate.ToString() };
            SelectList list = new SelectList(States, "ID", "Name");
            return list;
        }

        #endregion

        #region 会员查询
        /// <summary>
        /// 会员查询
        /// </summary>
        /// <param name="keywords">会员用户名</param>
        /// <param name="type">会员类型</param>
        /// <param name="pageNum"></param>
        /// <param name="numPerPage"></param>
        /// <returns></returns>
        public ActionResult Query(string keywords, int type=-1, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;
            var tqCount = 0;
            var tq = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords)).Where(p => p.IsValid == true && p.IsVerified==true);
            if (type != -1)
            {
                tq = tq.Where(p => p.Type == type);
            }
            tqCount = tq.Count();
            tq = tq.OrderBy(s => s.ID).Skip(numPerPage * (pageNum - 1)).Take(numPerPage);

            IList<VM_MemberRelease> list = tq.Select(w => new VM_MemberRelease
            {
                                                       FinancingCount = w.Financials.Where(f=>f.IsValid == true).Count(), 
                                                       InvestmentCount=w.Investments.Where(f=>f.IsValid == true).Count(),
                                                       ProductCount = w.Products.Where(f=>f.IsValid == true).Count(),
                                                       Member=w }
                                                  ).ToList();
            ViewBag.recordCount = tqCount;
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            ViewBag.type = GetMemberType();

            return View(list);
        }
        
        public SelectList GetMemberType()
        {
            var types = from MemberTypes mtype in Enum.GetValues(typeof(MemberTypes))
                        select new { ID = (int)mtype, Name = mtype.ToString() };

            SelectList list = new SelectList(types, "ID", "Name");
            return list;
        }

        // 查询会员(发布项目、投资意向、金融产品)详细信息
        public ActionResult FinancingList(int memberId, int pageNum = 1, int numPerPage = 10)
        {
            IList<T_XM_Financing> list = db.T_XM_Financing.Where(f => f.IsValid == true && f.MemberID == memberId)
                                                         .OrderByDescending(f => f.CreateTime)
                                                         .Skip(numPerPage * (pageNum - 1))
                                                         .Take(numPerPage)
                                                         .ToList();
            ViewBag.recordCount = db.T_XM_Financing.Where(f => f.IsValid == true && f.MemberID == memberId).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.memberId = memberId;

            return View(list);
        }

        public ActionResult InvestmentList(int memberId, int pageNum = 1, int numPerPage = 10)
        {
            IList<T_XM_Investment> list = db.T_XM_Investment.Where(f => f.IsValid == true && f.MemberID == memberId)
                                                           .OrderByDescending(f => f.CreateTime)
                                                           .Skip(numPerPage * (pageNum - 1))
                                                           .Take(numPerPage)
                                                           .ToList();
            ViewBag.recordCount = db.T_XM_Investment.Where(f => f.IsValid == true && f.MemberID == memberId).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.memberId = memberId;

            return View(list);
        }

        public ActionResult ProductList(int memberId, int pageNum = 1, int numPerPage = 10)
        {
            IList<T_JG_Product> list = db.T_JG_Product.Where(f => f.IsValid == true && f.MemberID == memberId)
                                                     .OrderByDescending(p => p.CreateTime)
                                                     .Skip(numPerPage * (pageNum - 1))
                                                     .Take(numPerPage)
                                                     .ToList();
            ViewBag.recordCount = db.T_JG_Product.Where(f => f.IsValid == true && f.MemberID == memberId).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.memberId = memberId;

            return View(list);
        }
        #endregion

        #region 热门信息发布
        public ActionResult HotInformation()
        {
            ViewData["HotTypes"] = GetHotTypes();

            return View();
        }

        /// <summary>
        /// 热门信息分类列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public PartialViewResult HotList(string keywords, int type,int memberType=-1, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;
            switch (type)
            {
                case 1://金融产品
                    return GetHotProductList(keywords,type, memberType, pageNum, numPerPage);
                case 2://投资项目
                    return GetHotFinancingList(keywords, memberType, pageNum, numPerPage);
                case 3://投资意向
                    return GetHotInvestmentList(keywords, memberType, pageNum, numPerPage);
            }
            return PartialView();
        }

        #region 获取列表
        public PartialViewResult GetHotProductList(string keywords, int type, int memberType, int pageNum, int numPerPage)
        {
            var tqCount = 0;
            var tq = db.T_JG_Product.Where(p => p.ProductName.Contains(keywords)).Include(t => t.Member);
            
            if (memberType != -1)
            {
                tq = tq.Where(s => s.Member.Type == memberType);
            }
            tqCount = tq.Count();
            IList<T_JG_Product> list = tq.OrderByDescending(o => o.Clicks).Skip(numPerPage * (pageNum - 1)).Take(numPerPage).ToList();

            ViewBag.recordCount = tqCount;
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            ViewBag.memberType = GetMemberType();
            ViewBag.type = type;
            return PartialView("HotProductList", list);
        }

        public PartialViewResult GetHotFinancingList(string keywords, int memberType, int pageNum, int numPerPage)
        {
            var tqCount = 0;
            var tq = db.T_XM_Financing.Where(p => p.ItemName.Contains(keywords)).Include(t => t.Member);
            if (memberType != -1)
            {
                tq = tq.Where(s => s.Member.Type == memberType);
            }
            tqCount = tq.Count();
            IList<T_XM_Financing> list = tq.OrderByDescending(o => o.Clicks)
                                               .Skip(numPerPage * (pageNum - 1))
                                               .Take(numPerPage)
                                               .ToList();
            ViewBag.recordCount = tqCount;
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            ViewBag.memberType = GetMemberType();
            return PartialView("HotFinancingList", list);
        }

        public PartialViewResult GetHotInvestmentList(string keywords, int memberType, int pageNum, int numPerPage)
        {
            var tqCount = 0;
            var tq = db.T_XM_Investment.Where(p => p.ItemName.Contains(keywords)).Include(t => t.Member);
            if (memberType != -1)
            {
                tq = tq.Where(s => s.Member.Type == memberType);
            }
            tqCount = tq.Count();
            IList<T_XM_Investment> list = tq.OrderByDescending(o => o.Clicks)
                                                .Skip(numPerPage * (pageNum - 1))
                                                .Take(numPerPage)
                                                .ToList();
            ViewBag.recordCount = tqCount;
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            ViewBag.memberType = GetMemberType();
            return PartialView("HotInvestmentList", list);
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 产品编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult HotProductEdit(int id)
        {
            T_JG_Product t_jg_product = db.T_JG_Product.Find(id);
            return View(t_jg_product);
        }

        [HttpPost]
        public ActionResult HotProductEdit(int id, FormCollection collection)
        {
            if (Request.IsAjaxRequest())
            {
                T_JG_Product t_jg_product = db.T_JG_Product.Find(id);

                t_jg_product.ProductName = collection["ProductName"];
                t_jg_product.Process = collection["Process"];
                t_jg_product.Linkman = collection["Linkman"];
                t_jg_product.UpdateTime = Convert.ToDateTime(DateTime.Now.ToLongTimeString());

                db.Entry(t_jg_product).State = EntityState.Modified;

                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "hotInfoBox", "", true, "HotProductList");
                else
                    return ReturnJson(false, "操作失败", "hotInfoBox", "", false, "HotProductList");
                
            }
            return Json(new { });
        }

        /// <summary>
        /// 项目编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult HotFinancingEdit(int id)
        {
            T_XM_Financing t_xm_financing = db.T_XM_Financing.Find(id);
            return View(t_xm_financing);
        }

        [HttpPost]
        public ActionResult HotFinancingEdit(int id,FormCollection collection)
        {
            if (Request.IsAjaxRequest())
            {
                T_XM_Financing t_xm_financing = db.T_XM_Financing.Find(id);

                t_xm_financing.ItemName = collection["ItemName"];
                t_xm_financing.ItemContent = collection["ItemContent"];
                t_xm_financing.Linkman = collection["Linkman"];
                t_xm_financing.UpdateTime = Convert.ToDateTime(DateTime.Now.ToLongTimeString());

                db.Entry(t_xm_financing).State = EntityState.Modified;

                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "hotInfoBox", "", true, "HotFinancingList");
                else
                    return ReturnJson(false, "操作失败", "hotInfoBox", "", false, "HotFinancingList");
                
            }
            return Json(new { });
        }

        /// <summary>
        /// 意向编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult HotInvestmentEdit(int id)
        {
            T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);
            return View(t_xm_investment);
        }

        [HttpPost]
        public ActionResult HotInvestmentEdit(int id,FormCollection collection)
        {
            if (Request.IsAjaxRequest())
            {
                T_XM_Investment t_xm_investment = db.T_XM_Investment.Find(id);

                t_xm_investment.ItemName = collection["ItemName"];
                t_xm_investment.Description = collection["Description"];
                t_xm_investment.Linkman = collection["Linkman"];
                t_xm_investment.UpdateTime = Convert.ToDateTime(DateTime.Now.ToLongTimeString());

                db.Entry(t_xm_investment).State = EntityState.Modified;

                int result = db.SaveChanges();
                if (result > 0)
                    return ReturnJson(true, "操作成功", "hotInfoBox", "", true, "HotInvestmentList");
                else
                    return ReturnJson(false, "操作失败", "hotInfoBox", "", false, "HotInvestmentList");
               
            }
            return Json(new { });
        }
        #endregion

        public PartialViewResult HotTypes()
        {
            ViewData["HotTypes"] = GetHotTypes();
            return PartialView();
        }

        public Dictionary<string, string> GetHotTypes()
        {
            Dictionary<string,string> types = new Dictionary<string,string>();
            types.Add("1","金融产品");
            types.Add("2","投资项目");
            types.Add("3","投资意向");
            return types;
        }

        #endregion

        #region 会员统计
        /// <summary>
        /// 会员统计
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberStatList()
        {
            var list = db.T_HY_Member.Where(p => p.IsValid == true && p.IsVerified == true).ToList();
            return View(list);
        }

        public ActionResult MemberStatData()
        {
            var members = db.T_HY_Member.Where(p => p.IsValid == true && p.IsVerified == true);

            Dictionary<String, int> dic = new Dictionary<string, int>();
            if (members != null)
            {
                dic.Add("个人", members.Where(m => m.Type == (int)MemberTypes.个人).Count());
                dic.Add("企业", members.Where(m => m.Type == (int)MemberTypes.企业).Count());
                dic.Add("机构", members.Where(m => m.Type == (int)MemberTypes.机构).Count());
            }
            else
            {
                dic.Add("会员数", 0);
            }

            return Json(new { statData = dic }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}