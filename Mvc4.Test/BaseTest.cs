using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using com.minsoehanwin.sample.Core.Services;
using com.minsoehanwin.sample.Core;
using log4net;
using StructureMap.Configuration.DSL;
using Mvc4AppTestWebAPI.DependencyResolution;
using Mvc4AppTestWebAPI;
using StructureMap.Graph;
using System.Configuration;
using com.minsoehanwin.sample.Services;
using com.minsoehanwin.sample.Helper;
namespace com.minsoehanwin.sample.Test
{
    [TestFixture]
    public class BaseTest
    {
        private IContainer _container;
        protected IUnitOfWork _unitOfWork;
        protected static readonly ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected string _connectionString=ConfigurationManager.ConnectionStrings["MyDataContext"].ToString();
        protected TestServiceFactory _serviceFactory;
        protected IEmployeeService _employeeService;
        protected IStoreService _storeService;
        protected IProductService _productService;
        protected IWifeService _wifeService;
        protected IPassportInfoService _passportInfoService;
        protected IEmailService _emailService;
        
        /// <summary>
        /// this method run before runing each [Test] method
        /// see more here http://nunit.org/index.php?p=setup&r=2.6.4
        /// </summary>
        [SetUp]
        public void Init()
        {
            if (_container == null)
            {
                _container = Mvc4AppTestWebAPI.DependencyResolution.IoC.Initialize();
            }

            if (_serviceFactory == null)
            {
                _serviceFactory = _container.GetInstance<TestServiceFactory>();
                _unitOfWork = _serviceFactory._unitOfWork;
                _employeeService = _serviceFactory._employeeService;
                _storeService = _serviceFactory._storeService;
                _productService = _serviceFactory._productService;
                _wifeService = _serviceFactory._wifeService;
                _passportInfoService = _serviceFactory._passportInfoService;
                _emailService = _serviceFactory._emailService;
            }   
        }
    }
}