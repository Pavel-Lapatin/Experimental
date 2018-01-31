using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NetMastery.InventoryManager
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new InventoryAuthorizeAttribute());
        }
    }
    public class InventoryAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool skipAuthorization = filterContext
                .ActionDescriptor
                .IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor
                .ControllerDescriptor
                .IsDefined(typeof(AllowAnonymousAttribute), true);
            if (filterContext.HttpContext.Session["Account"] == null
                && !skipAuthorization)
            {
                filterContext.Result = new RedirectToRouteResult(new
                RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
            base.OnAuthorization(filterContext);
        }
    }
}