using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Util;

namespace GGZBTQPT_PRO.Areas.MG.Controllers
{
    public class ValidateController : Controller
    {
        //
        // GET: /MG/Validate/

        public ActionResult Index()
        {
            return View();
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
