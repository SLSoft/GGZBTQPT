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

            IList<GGZBTQPT_PRO.Models.T_HY_Member> list = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords)).Where(p => p.IsValid == true)
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords)).Where(p => p.IsValid == true).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;

            return View(list);
        } 

        //
        // GET: /T_HY_Member/Details/5

        public ViewResult Details(int id)
        {
            T_HY_Member t_hy_member = db.T_HY_Member.Find(id);
            return View(t_hy_member);
        }

        //
        // GET: /T_HY_Member/Create

        public ActionResult Create()
        {
            var types = from MemberTypes type in Enum.GetValues(typeof(MemberTypes))
                        select new { ID = (int)type, Name = type.ToString() };
            ViewData["Type"] = new SelectList(types, "ID", "Name");

            return View();
        } 

        //
        // POST: /T_HY_Member/Create

        [HttpPost]
        public ActionResult Create(T_HY_Member t_hy_member)
        {
            if (Request.IsAjaxRequest())
            {
                var types = from MemberTypes type in Enum.GetValues(typeof(MemberTypes))
                            select new { ID = (int)type, Name = type.ToString() };
                ViewData["Type"] = new SelectList(types, "ID", "Name");

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
                    if (result > 0)
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
            }
            return Json(new { });
        }
        
        //
        // GET: /T_HY_Member/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_HY_Member t_hy_member = db.T_HY_Member.Find(id);

            var types = from MemberTypes mtype in Enum.GetValues(typeof(MemberTypes))
                        select new { ID = (int)mtype, Name = mtype.ToString() };
            ViewData["Types"] = new SelectList(types, "ID", "Name");
            
            return View(t_hy_member);
        }

        //
        // POST: /T_HY_Member/Edit/5

        [HttpPost]
        public ActionResult Edit(T_HY_Member t_hy_member)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    t_hy_member.UpdatedAt = Convert.ToDateTime(DateTime.Now.ToLongTimeString());
                    db.Entry(t_hy_member).State = EntityState.Modified;

                    int result = db.SaveChanges();
                    if (result > 0)
                        return ReturnJson(true, "操作成功", "", "", true, "");
                    else
                        return ReturnJson(false, "操作失败", "", "", false, "");
                }
            }
            var types = from MemberTypes mtype in Enum.GetValues(typeof(MemberTypes))
                        select new { ID = (int)mtype, Name = mtype.ToString() };
            SelectList list = new SelectList(types, "ID", "Name");
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text="---请选择---",Value="", Selected=true});
            li.AddRange(list);

            ViewData["Types"] = li;

            return Json(new { });
        }

        //
        // POST: /T_HY_Member/Delete/5

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

        #endregion

        #region 会员审核
        /// <summary>
        /// 待审核会员
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageNum"></param>
        /// <param name="numPerPage"></param>
        /// <returns></returns>
        public ActionResult UnVerified(string keywords, int pageNum = 1, int numPerPage = 15)
        {
            keywords = keywords == null ? "" : keywords;

            IList<GGZBTQPT_PRO.Models.T_HY_Member> list = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords)).Where(p => p.IsValid == true && p.IsVerified == false && p.State == 0)
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords)).Where(p => p.IsValid == true && p.IsVerified == false && p.State==0).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;

            return View(list);
        }

        /// <summary>
        /// 已审核会员查询
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageNum"></param>
        /// <param name="numPerPage"></param>
        /// <returns></returns>
        public ActionResult HasVerified(string keywords, int state = 1, int pageNum = 1, int numPerPage = 15)
        {
            var States = from MemberStates mstate in Enum.GetValues(typeof(MemberStates))
                         select new { ID = (int)mstate, Name = mstate.ToString() };
            ViewData["State"] = new SelectList(States, "ID", "Name");

            keywords = keywords == null ? "" : keywords;

            IList<GGZBTQPT_PRO.Models.T_HY_Member> list = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords)).Where(p => p.IsValid == true && p.State == state)
                                                            .OrderBy(s => s.ID)
                                                            .Skip(numPerPage * (pageNum - 1))
                                                            .Take(numPerPage).ToList();

            ViewBag.recordCount = db.T_HY_Member.Where(p => p.MemberName.Contains(keywords)).Where(p => p.IsValid == true && p.State==state).Count();
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            ViewBag.states = States;

            return View(list);
        } 

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
                return ReturnJson(true, "操作成功", "", "UnVerified", false, "");
            else
                return ReturnJson(false, "操作失败", "", "", false, ""); 

        }

        [HttpPost]
        public ActionResult UnVerify(int id)
        {
            T_HY_Member t_hy_member = db.T_HY_Member.Find(id);
            t_hy_member.IsVerified = false;
            db.Entry(t_hy_member).State = EntityState.Modified;

            if (db.SaveChanges() > 0)
                return ReturnJson(true, "撤销审核成功", "", "UnVerified", false, "");
            else
                return ReturnJson(false, "撤销审核失败", "", "", false, "");

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

            IList<VM_MemberStat> list = tq.Select(w => new VM_MemberStat{
                                                       FinancingCount = w.Financials.Count, 
                                                       InvestmentCount=w.Investments.Count,
                                                       ProductCount = w.Products.Count,
                                                       Member=w }
                                                  ).ToList();

            ViewBag.recordCount = tqCount;
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            ViewBag.type = type;
            ViewBag.Types = GetMemberType();

            return View(list);
        }

        public SelectList GetMemberType()
        {
            var types = from MemberTypes mtype in Enum.GetValues(typeof(MemberTypes))
                        select new { ID = (int)mtype, Name = mtype.ToString() };

            SelectList list = new SelectList(types, "ID", "Name");

            //List<SelectListItem> li = new List<SelectListItem>();
            //li.Add(new SelectListItem { Text = "---请选择---", Value = "-1" });
            //li.AddRange(list);

            return list;
        }

        /// <summary>
        /// 查询会员详细信息(如：发布项目、投资意向、金融产品)
        /// </summary>
        /// <param name="member_id"></param>
        /// <returns></returns>
        public ActionResult QueryDetails(int member_id,int type, int pageNum=1)
        {
            VM_SelectMember member_Details = new VM_SelectMember(); 
            switch (type)
            {
                case 1:
                    List<T_XM_Financing> fs = new List<T_XM_Financing>();
                    fs = db.T_XM_Financing.OrderByDescending(f => f.CreateTime).Where(f => f.MemberID == member_id).ToList();
                    member_Details.Financings = new PagedList<T_XM_Financing>(fs, pageNum, 10);
                    if (Request.IsAjaxRequest())
                    {
                        return PartialView("FinancingList", member_Details);
                    }
                    break;
                case 2:
                    List<T_XM_Investment> Investments = new List<T_XM_Investment>();
                    Investments = db.T_XM_Investment.OrderByDescending(f => f.CreateTime).Where(f => f.MemberID == member_id).ToList();
                    member_Details.Investments = new PagedList<T_XM_Investment>(Investments, pageNum, 10);
                    if (Request.IsAjaxRequest())
                    {
                        return PartialView("InvestmentList", member_Details);
                    }
                    break;
                case 3:
                    List<T_JG_Product> Products = new List<T_JG_Product>();
                    Products = db.T_JG_Product.OrderByDescending(p => p.CreateTime).Where(p => p.MemberID == member_id).ToList();
                    member_Details.Products = new PagedList<T_JG_Product>(Products, pageNum, 10);
                    if (Request.IsAjaxRequest())
                    {
                        return PartialView("ProductList", member_Details);
                    }
                    break;
            }
 
            return View(member_Details);
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
                    return GetHotProductList(keywords, memberType, pageNum, numPerPage);
                case 2://投资项目
                    return GetHotFinancingList(keywords, memberType, pageNum, numPerPage);
                case 3://投资意向
                    return GetHotInvestmentList(keywords, memberType, pageNum, numPerPage);
                case 4://金融服务(暂无实现)
                    return GetHotInvestmentList(keywords, memberType, pageNum, numPerPage);
            }
            return PartialView();
        }

        #region 获取列表
        public PartialViewResult GetHotProductList(string keywords, int memberType, int pageNum, int numPerPage)
        {
            var tqCount = 0;
            var tq = db.T_JG_Product.Where(p => p.ProductName.Contains(keywords)).Include(t => t.Member);
            
            if (memberType != -1)
            {
                tq = tq.Where(s => s.Member.Type == memberType);
            }
            tqCount = tq.Count();
            IList<T_JG_Product> list = tq.OrderBy(o => o.ID)
                                             .Skip(numPerPage * (pageNum - 1))
                                             .Take(numPerPage)
                                             .ToList();
            ViewBag.recordCount = tqCount;
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            ViewBag.memberType = memberType;
            ViewBag.Types = GetMemberType();
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
            IList<T_XM_Financing> list = tq.OrderBy(o => o.ID)
                                               .Skip(numPerPage * (pageNum - 1))
                                               .Take(numPerPage)
                                               .ToList();
            ViewBag.recordCount = tqCount;
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            ViewBag.memberType = memberType;
            ViewBag.Types = GetMemberType();
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
            IList<T_XM_Investment> list = tq.OrderBy(o => o.ID)
                                                .Skip(numPerPage * (pageNum - 1))
                                                .Take(numPerPage)
                                                .ToList();
            ViewBag.recordCount = tqCount;
            ViewBag.numPerPage = numPerPage;
            ViewBag.pageNum = pageNum;
            ViewBag.keywords = keywords;
            ViewBag.memberType = memberType;
            ViewBag.Types = GetMemberType();
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
            types.Add("4","金融服务");
            return types;
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}