using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace Mvc4AppTestWebAPI.Controllers
{
    public class BaseApiController : ApiController
    {
        protected static readonly ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
