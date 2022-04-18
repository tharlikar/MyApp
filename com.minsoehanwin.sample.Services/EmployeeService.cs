using System;
using System.Collections.Generic;
using com.minsoehanwin.sample.Core.Models;
using log4net;
using com.minsoehanwin.sample.Core.Repositories;
using com.minsoehanwin.sample.Core.Services;
using System.Linq.Expressions;
using System.Linq;
using com.minsoehanwin.sample.Core;
using com.minsoehanwin.sample.Services.Exception;
using com.minsoehanwin.sample.Core.EmailEntity;


namespace com.minsoehanwin.sample.Services
{
    public class EmployeeService : BaseService,IEmployeeService
    {
        public IEmployeeRepository _employeeRepository;
        private EmployeeValidator _employeeValidator;
        private PassportInfoValidator _passportInfoValidator;
        private IWifeRepository _wifeRepository;
        private IPassportInfoRepository _passportInfoRepository;
        private IStoreRepository _storeRepository;
        private IEmailRepository _emailRepository;
        private WifeValidator _wifeValidator;

        public EmployeeService(IEmployeeRepository employeeRepository
            ,IStoreRepository storeRepository
            ,IPassportInfoRepository passportInfoRepository
            ,IWifeRepository wifeRepository
            ,IEmailRepository emailRepository
            ,EmployeeValidator employeeValidator
            ,PassportInfoValidator passportInfoValidator
            ,WifeValidator wifeValidator)
        {
            _employeeRepository = employeeRepository;
            _storeRepository = storeRepository;
            _passportInfoRepository = passportInfoRepository;
            _wifeRepository = wifeRepository;
            _emailRepository = emailRepository;
            _employeeValidator = employeeValidator;
            _passportInfoValidator = passportInfoValidator;
            _wifeValidator = wifeValidator;
        }

        public void Save(Employee employee)
        {
            _logger.Info(string.Format("---Saving employee=>Id:{0}", employee.Id));
            _employeeValidator.Validate(employee);
            if (employee.PassportInfo != null)
            {
                _passportInfoValidator.Validate(employee.PassportInfo);
            }
            if (employee.Wifes.Count == 1)
            {
                _wifeValidator.Validate(employee.Wifes);
            }
            _employeeRepository.Save(employee);
            _logger.Info("---Saving employee.Done.");
        }

        public IList<Employee> GetList()
        {
            //Expression<Func<Employee, bool>> filter = e => e.FirstName.Contains("SOE");
            //Func<IQueryable<Employee>, IOrderedQueryable<Employee>> orderBy = e =>
            //    e.OrderByDescending(x => x.Id).OrderBy(x => x.FirstName);
            //List<Employee> result = new List<Employee>(_employeeRepository.Get(filter, orderBy, "Stores"));
            _logger.Info("---Getting employee list.Started");
            List<Employee> result = new List<Employee>(_employeeRepository.Get());
            _logger.Info("---Getting employee list.Done.");
            return result;
        }

        public void Delete(Employee employee)
        {
            _logger.Info("Deleting employee...");
            _employeeValidator.EmployeeIsNull(employee);
            DeleteById(employee.Id);
            _logger.Info("Deleting employee. Done.");
        }

        public void DeleteById(int id)
        {
            _logger.Info(string.Format("Deleting employee=>Id:{0}", id));
            Employee employeeToBeDeleted=_employeeRepository.GetByID(id);
            _employeeValidator.ValidateCanDeleteEmployee(employeeToBeDeleted);
            _employeeRepository.Delete(employeeToBeDeleted);
            _logger.Info("Deleting employee. Done.");
        }

        public Employee GetById(int id)
        {
            _logger.Info(string.Format("---Get employee=>Id:{0}", id));
            var e = _employeeRepository.GetByID(id);
            _logger.Info("---Got employee.Done");
            return e;
        }
        
        public void AddWife(Employee employee, Wife wife)
        {
            _logger.Info("---Add employee's wife.Started.");
            employee.Wifes.Add(wife);
            Save(employee);
            _logger.Info("---Add employee's wife.Done.");
        }

        public void AddPassportInfo(Employee employee, PassportInfo passportInfo)
        {
            _logger.Info("---Add employee's passportinfo.Started.");
            if (_employeeValidator.HasPassport(employee))
            {
                throw new System.Exception("Employee already has passport.Please use save() function.");
            }
            employee.PassportInfo = passportInfo;
            Save(employee);
            _logger.Info("---Add employee's passportinfo.Done.");
        }
    }
}