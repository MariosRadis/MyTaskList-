using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Practice_1._1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //I add this code in order to open the incoming cookie and find out the role of the user 
        public override void Init()
        {
            base.PostAuthenticateRequest+= OnAuthenticateRequest;
        }

        private void OnAuthenticateRequest(object sender, EventArgs e)
        {
            if(HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                var decodedTicket = FormsAuthentication.Decrypt(cookie.Value);
                var roles = decodedTicket.UserData.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                var principal = new GenericPrincipal(HttpContext.Current.User.Identity, roles);
                HttpContext.Current.User = principal;
            }

            
        }
    }
}
