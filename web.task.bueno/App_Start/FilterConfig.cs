using System.Web;
using System.Web.Mvc;
using web.task.bueno.Common.Filters;

namespace web.task.bueno
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new LoginFilterAtribute());
        }
    }
}
