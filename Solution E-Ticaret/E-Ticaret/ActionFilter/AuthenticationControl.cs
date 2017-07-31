using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.ActionFilter
{
    public class AuthenticationControl : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["LogInId"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/Login/SingIn");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}