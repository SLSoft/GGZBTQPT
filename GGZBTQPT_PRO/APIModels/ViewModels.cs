using System;
using System.Collections.Generic;
using GGZBTQPT_PRO.Models;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Webdiyer.WebControls.Mvc;

namespace GGZBTQPT_PRO.APIModels
{
   public class AM_MemberInfo
    {
        public string MemberName { get; set; }
        public string Email { get; set; } 
        public string CellPhone { get; set; } 
    }
}

