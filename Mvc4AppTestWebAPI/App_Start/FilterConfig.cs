using System.Web.Mvc;

namespace Mvc4AppTestWebAPI.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new RequireHttpsAttribute());
        }
    }
}
