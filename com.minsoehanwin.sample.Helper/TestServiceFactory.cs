using com.minsoehanwin.sample.Core;
using com.minsoehanwin.sample.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Helper
{
    public class TestServiceFactory
    {
        public IProductService _productService;
        public IStoreService _storeService;
        public IEmployeeService _employeeService;
        public IUnitOfWork _unitOfWork;
        public IPassportInfoService _passportInfoService;
        public IWifeService _wifeService;
        public IEmailService _emailService;

        public TestServiceFactory(IUnitOfWork unitOfWork
            , IEmployeeService employeeService
            , IStoreService storeService
            , IProductService productService
            , IWifeService wifeService
            , IPassportInfoService passportInfoService
            , IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _employeeService = employeeService;
            _storeService = storeService;
            _productService = productService;
            _wifeService = wifeService;
            _passportInfoService = passportInfoService;
            _emailService = emailService;
        }
    }
}
