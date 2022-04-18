using log4net;
using com.minsoehanwin.sample.Core;
using Mvc4AppTestWebAPI.DependencyResolution;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using com.minsoehanwin.sample.Services;
using com.minsoehanwin.sample.Core.Services;

namespace Mvc4AppTestWebAPI.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IUnitOfWork _unitOfWork;

        //http://stackoverflow.com/questions/3143929/why-do-loggers-recommend-using-a-logger-per-class
        protected static readonly ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public BaseController()
        {
        }
        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //string userName = GetCurrentUserName(User);
            //string connectionString = ConfigurationManager.ConnectionStrings["MyDataContext"].ToString();
            //_container =((StructureMapDependencyResolver)GlobalConfiguration.Configuration.DependencyResolver).MyContainer;
            //_unitOfWork = IoCHelper<IUnitOfWork>.GetUnitOfWork(_container,userName,connectionString);
        }

        private string GetCurrentUserName(System.Security.Principal.IPrincipal User)
        {
            if (User == null)
            {
                return string.Empty;
            }
            if (User.Identity == null)
            {
                return string.Empty;
            }
            return User.Identity.Name;
        }
    }
}
