using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;

namespace Mvc4AppTestWebAPI.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //solved error using the following link
            //http://stackoverflow.com/questions/16034626/routeparameter-error
            //The type 'System.Web.Http.RouteParameter' exists in both 
            //'z:\Ember.n.SignalR\bin\System.Web.Http.Common.dll' 
            //and 'z:\Program Files (x86)\Microsoft ASP.NET\ASP.NET MVC 4\Assemblies\System.Web.Http.dll'
            //->Right click on System.Web.Http and get its properties ->assign Aliases , example Aliases: MyAlias
            //now in your code do the changes like this
            //extern alias MyAlias;
            //using MyAlias.System.Web.Http;
            //using System.Web.Mvc;
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}