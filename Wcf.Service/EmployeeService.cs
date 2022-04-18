using log4net;
using com.minsoehanwin.sample.Core;
using com.minsoehanwin.sample.Core.Services;
using com.minsoehanwin.sample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using AutoMapper;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Security.Permissions;

namespace com.minsoehanwin.sample.Wcf.Service
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall,IncludeExceptionDetailInFaults=true)]
    public class EmployeeService : com.minsoehanwin.sample.Wcf.Service.IEmployeeService
    {
        protected static readonly ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IUnitOfWork _unitOfWork;//remove static for concurrent requesst
        private com.minsoehanwin.sample.Core.Services.IEmployeeService _employeeService;

        public EmployeeService()
        {
        }

        public EmployeeService(IUnitOfWork unitOfWork, com.minsoehanwin.sample.Core.Services.IEmployeeService employeeService)
            :this()
        {
            _logger.Info("Init unitofwork.");
            _unitOfWork = unitOfWork;
            _employeeService = employeeService;
            AutoMapperWebConfiguration.Configure(new MyRestfulProfile());
            Mapper.AssertConfigurationIsValid();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public void AddWife(Employee employee, Wife wife)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                com.minsoehanwin.sample.Core.Models.Employee e = Mapper.Map<Employee, com.minsoehanwin.sample.Core.Models.Employee>(employee);
                com.minsoehanwin.sample.Core.Models.Wife w = Mapper.Map<Wife, com.minsoehanwin.sample.Core.Models.Wife>(wife);
                _employeeService.AddWife(e, w);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logger.Error(new List<Object>() { "Adding wife failed.", "Employee data is:", employee, "Wife data is:", wife }, ex);
                _unitOfWork.Rollback();
                throw;
            }
        }

        public void AddPassportInfo(Employee employee, PassportInfo passportInfo)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                com.minsoehanwin.sample.Core.Models.Employee e = Mapper.Map<Employee, com.minsoehanwin.sample.Core.Models.Employee>(employee);
                com.minsoehanwin.sample.Core.Models.PassportInfo pi = Mapper.Map<PassportInfo, com.minsoehanwin.sample.Core.Models.PassportInfo>(passportInfo);
                _employeeService.AddPassportInfo(e, pi);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _logger.Error(new List<Object>() { "AddPassportInfo failed.", "Employee data is:", employee, "PassportInfo data is:", passportInfo }, ex);
                _unitOfWork.Rollback();
                throw;
            }
        }

        public void Save(Employee obj)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                com.minsoehanwin.sample.Core.Models.Employee e = Mapper.Map<Employee, com.minsoehanwin.sample.Core.Models.Employee>(obj);
                _employeeService.Save(e);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error(new List<Object>() {"Saveing employee failed.","Employee data is:",obj }, ex);
                throw;
            }
        }

        public IList<Employee> GetList()
        {
                IList<com.minsoehanwin.sample.Core.Models.Employee> e = _employeeService.GetList();
                IList<Employee> wcfE = Mapper.Map<IList<com.minsoehanwin.sample.Core.Models.Employee>, IList<Employee>>(e);
                return wcfE;
        }

        public void Delete(Employee obj)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _employeeService.DeleteById(obj.Id);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error(new List<Object>() {"Transaction rollback.Failed to delete employee."
                    ,"Employee data is:",obj}, ex);
                throw;
            }
        }

        public void DeleteById(int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _employeeService.DeleteById(id);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error(new List<Object>() {"Transaction rollback.Failed to delete employee.","Employee id is:",id }, ex);
                throw;
            }
        }

        public Employee GetById(int id)
        {
            com.minsoehanwin.sample.Core.Models.Employee e= _employeeService.GetById(id);
            Employee wcfE = AutoMapper.Mapper.Map<com.minsoehanwin.sample.Core.Models.Employee, Employee>(e);
            return wcfE;
        }
    }
}