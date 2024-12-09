using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using web.task.bueno.Common.Session;

namespace web.task.bueno.Common.Filters
{
    
    public class LoginFilterAtribute : ActionFilterAttribute

    {
        private WebCurrentSession miSession;

        public LoginFilterAtribute() {
            miSession = new WebCurrentSession();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            miSession._session = filterContext.HttpContext;
           
            if ( filterContext.ActionDescriptor.ActionName.Equals("Index")
                && filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.Equals("Home")
                )
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                if (miSession.EsLogeado == null)
                {
                    filterContext.Result = new RedirectResult("~/Home/Index");
                    return;
                }
                else
                {
                    base.OnActionExecuting(filterContext);
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
    }
}