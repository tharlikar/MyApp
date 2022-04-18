using com.minsoehanwin.sample.Core.Models;
using com.minsoehanwin.sample.Core.Repositories;
using com.minsoehanwin.sample.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services
{
    public class WifeService : BaseService, IWifeService
    {
        public IEmployeeRepository _employeeRepository;
        private EmployeeValidator _employeeValidator;
        private PassportInfoValidator _passportInfoValidator;
        private IWifeRepository _wifeRepository;
        private IPassportInfoRepository _passportInfoRepository;
        private IStoreRepository _storeRepository;

        public WifeService(IEmployeeRepository employeeRepository
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

        public void Save(com.minsoehanwin.sample.Core.Models.Wife obj)
        {
            _wifeRepository.AddOrUpdate(obj);
        }

        public IList<com.minsoehanwin.sample.Core.Models.Wife> GetList()
        {
            return new List<Wife>(_wifeRepository.Get());
        }

        public void Delete(com.minsoehanwin.sample.Core.Models.Wife obj)
        {
            _wifeRepository.Delete(obj);
        }

        public void DeleteById(int id)
        {
            var obj=_wifeRepository.GetByID(id);
            _wifeRepository.Delete(obj);
        }

        public com.minsoehanwin.sample.Core.Models.Wife GetById(int id)
        {
            return _wifeRepository.GetByID(id);
        }
    }
}
