using System.Web.Mvc;

namespace GGZBTQPT_PRO.Areas.MG
{
    public class MGAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MG";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MG_default",
                "MG/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index",id = UrlParameter.Optional }
            );
        }
    }
}
