using com.minsoehanwin.sample.Core.Models;
using com.minsoehanwin.sample.Core.Repositories;
using com.minsoehanwin.sample.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services
{
    public class PassportInfoService : BaseService, IPassportInfoService
    {
        public IEmployeeRepository _employeeRepository;
        private EmployeeValidator _employeeValidator;
        private PassportInfoValidator _passportInfoValidator;
        private IWifeRepository _wifeRepository;
        private IPassportInfoRepository _passportInfoRepository;
        private IStoreRepository _storeRepository;

        public PassportInfoService(IEmployeeRepository employeeRepository
            , IStoreRepository storeRepository
            , IPassportInfoRepository passportInfoRepository
            , IWifeRepository wifeRepository
            , EmployeeValidator employeeValidator
            , PassportInfoValidator passportInfoValidator)
        {
            _employeeRepository = employeeRepository;
            _storeRepository = storeRepository;
            _passportInfoRepository = passportInfoRepository;
            _wifeRepository = wifeRepository;
            _employeeValidator = employeeValidator;
            _passportInfoValidator = passportInfoValidator;
            _wifeRepository = wifeRepository;
        }

        public void Save(com.minsoehanwin.sample.Core.Models.PassportInfo obj)
        {
            _passportInfoRepository.AddOrUpdate(obj);
        }

        public IList<com.minsoehanwin.sample.Core.Models.PassportInfo> GetList()
        {
            return new List<PassportInfo>(_passportInfoRepository.Get());
        }

        public void Delete(com.minsoehanwin.sample.Core.Models.PassportInfo obj)
        {
            _passportInfoRepository.Delete(obj);
        }

        public void DeleteById(int id)
        {
            _passportInfoRepository.GetByID(id);
        }

        public com.minsoehanwin.sample.Core.Models.PassportInfo GetById(int id)
        {
            return _passportInfoRepository.GetByID(id);
        }
    }
    
}
