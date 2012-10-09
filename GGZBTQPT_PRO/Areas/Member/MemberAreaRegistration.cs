using System.Web.Mvc;

namespace GGZBTQPT_PRO.Areas.Member
{
    public class MemberAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Member";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Member_default",
                "Member/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index",id = UrlParameter.Optional }
            );
        }
    }
}
