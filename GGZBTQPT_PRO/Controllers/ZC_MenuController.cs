using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;

namespace GGZBTQPT_PRO.Controllers
{ 
    public class ZC_MenuController : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();

        //
        // GET: /ZC_Menu/

        public ViewResult Index()
        {
          var t_zc_system = db.T_ZC_System.ToList();
            var t_zc_menu = db.T_ZC_Menu.Include(t => t.System);
            ViewBag.systems = t_zc_system;
            return View(t_zc_menu.ToList());
        }

        //
        // GET: /ZC_Menu/Details/5

        public ViewResult Details(int id)
        {
            T_ZC_Menu t_zc_menu = db.T_ZC_Menu.Find(id);
            return View(t_zc_menu);
        }

        //
        // GET: /ZC_Menu/Create

        public ActionResult Create()
        {
            ViewBag.SystemID = new SelectList(db.T_ZC_System, "ID", "Name");
            return View();
        } 

        //
        // POST: /ZC_Menu/Create

        [HttpPost]
        public ActionResult Create(T_ZC_Menu t_zc_menu)
        {
            if (ModelState.IsValid)
            {
                t_zc_menu.CreatedAt = DateTime.Now;
                t_zc_menu.UpdatedAt = DateTime.Now;
                db.T_ZC_Menu.Add(t_zc_menu);
                db.SaveChanges();
                return Json(new { statusCode = "200", message = "操作成功" });  
            }

            ViewBag.SystemID = new SelectList(db.T_ZC_System, "ID", "Name", t_zc_menu.SystemID);
            return View(t_zc_menu);
        }
        
        //
        // GET: /ZC_Menu/Edit/5
 
        public ActionResult Edit(int id)
        {
            T_ZC_Menu t_zc_menu = db.T_ZC_Menu.Find(id);
            ViewBag.SystemID = new SelectList(db.T_ZC_System, "ID", "Name", t_zc_menu.SystemID);
            return View("Edit",t_zc_menu);
        }

        //
        // POST: /ZC_Menu/Edit/5

        [HttpPost]
        public ActionResult Edit(T_ZC_Menu t_zc_menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_zc_menu).State = EntityState.Modified;
                db.SaveChanges();
                //return View("Edit",t_zc_menu);
            }
            ViewBag.SystemID = new SelectList(db.T_ZC_System, "ID", "Name", t_zc_menu.SystemID);
            return PartialView("Edit",t_zc_menu);
        }

        //
        // GET: /ZC_Menu/Delete/5
 
        public ActionResult Delete(int id)
        {
            T_ZC_Menu t_zc_menu = db.T_ZC_Menu.Find(id);
            return View(t_zc_menu);
        }

        //
        // POST: /ZC_Menu/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            T_ZC_Menu t_zc_menu = db.T_ZC_Menu.Find(id);
            db.T_ZC_Menu.Remove(t_zc_menu);
            db.SaveChanges();
            return Json(new { statusCode = "200", message = "操作成功", forwardUrl = "", callbackType = "dialogAjaxDone" }); 
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        public PartialViewResult SystemLinks()
        {
            var links = db.T_ZC_System.ToList();
            return PartialView(links);
        }

        public PartialViewResult MenuInfo(int system_id)
        {
            var menus = db.T_ZC_Menu.Include("System").Where(m => m.SystemID == system_id);
            return PartialView(menus);
        }
    }
}