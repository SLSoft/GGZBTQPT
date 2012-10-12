using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GGZBTQPT_PRO.Models;

namespace GGZBTQPT_PRO.App_Code
{
    public class MemberHelper : Controller
    {
        private GGZBTQPTDBContext db = new GGZBTQPTDBContext();
    }
}