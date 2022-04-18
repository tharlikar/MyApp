using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc4AppTestWebAPI.ExtensionLib
{
    public static class HttpContextExtension
    {
        public static T GetMyInstance<T>(this System.Web.HttpContext context)
        {
            string index = "_" + typeof(T).Name;
            return (T)context.Items[index];
        }
    }
}