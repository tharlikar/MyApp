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
using System.ServiceModel.Web;
using System.Net;
using System.Security.Permissions;
using System.Web;
using com.minsoehanwin.sample.Services.AspNetIdentity;
using Microsoft.AspNet.Identity.Owin;

namespace com.minsoehanwin.sample.Wcf.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]
    public class EmployeeRestfulService : com.minsoehanwin.sample.Wcf.Service.IEmployeeRestfulService
    {
        protected static readonly ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IUnitOfWork _unitOfWork;//remove static for concurrent requesst
        private com.minsoehanwin.sample.Core.Services.IEmployeeService _employeeService;

        public EmployeeRestfulService()
        {
        }

        public void SetResponseHttpStatus(HttpStatusCode statusCode)
        {
            var context = WebOperationContext.Current;
            context.OutgoingResponse.StatusCode = statusCode;
        }

        public EmployeeRestfulService(IUnitOfWork unitOfWork, com.minsoehanwin.sample.Core.Services.IEmployeeService employeeService)
            : this()
        {
            _unitOfWork = unitOfWork;
            _employeeService = employeeService;
            AutoMapperWebConfiguration.Configure(new MyRestfulProfile());
            Mapper.AssertConfigurationIsValid();
        }

        
        public Employee AddWife(Employee employee, Wife wife)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                com.minsoehanwin.sample.Core.Models.Employee e = Mapper.Map<Employee, com.minsoehanwin.sample.Core.Models.Employee>(employee);
                com.minsoehanwin.sample.Core.Models.Wife w = Mapper.Map<Wife, com.minsoehanwin.sample.Core.Models.Wife>(wife);
                _employeeService.AddWife(e, w);
                _unitOfWork.Commit();
                Employee wcfSavedEmployee=Mapper.Map<com.minsoehanwin.sample.Core.Models.Employee,Employee>(e);
                SetLocationHeader(e.Id);
                SetResponseHttpStatus(HttpStatusCode.OK);
                return wcfSavedEmployee;
            }
            catch (com.minsoehanwin.sample.Services.Exception.EmployeeFirstNameException ex)
            {
                _unitOfWork.Rollback();
                throw new WebFaultException<string>(ex.Message
                        , System.Net.HttpStatusCode.BadRequest);
            }
            catch (com.minsoehanwin.sample.Services.Exception.EmployeeLastNameException ex)
            {
                _unitOfWork.Rollback();
                throw new WebFaultException<string>(ex.Message
                        , System.Net.HttpStatusCode.BadRequest);
            }
            catch (com.minsoehanwin.sample.Services.Exception.EmployeeWifesException ex)
            {
                _unitOfWork.Rollback();
                throw new WebFaultException<string>(ex.Message
                        , System.Net.HttpStatusCode.BadRequest);
            }
            catch (com.minsoehanwin.sample.Services.Exception.InvalidPassportNoException ex)
            {
                _unitOfWork.Rollback();
                throw new WebFaultException<string>(ex.Message
                        , System.Net.HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _logger.Error(new List<Object>(){"Test Exception occur. Employee info are:",employee,"Employee's Wife Info are:",wife},ex);
                _unitOfWork.Rollback();
                throw new WebFaultException<string>(ex.Message
                        , System.Net.HttpStatusCode.ServiceUnavailable);
            }
        }

        private static void SetLocationHeader(int id)
        {
            string url = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            WebOperationContext.Current.OutgoingResponse.Location = new Uri(string.Format("{0}/{1}", url,id)).AbsoluteUri;
        }

        
        public Employee AddPassportInfo(Employee employee, PassportInfo passportInfo)
        {
            try{
                _unitOfWork.BeginTransaction();
                com.minsoehanwin.sample.Core.Models.Employee e = Mapper.Map<Employee, com.minsoehanwin.sample.Core.Models.Employee>(employee);
                com.minsoehanwin.sample.Core.Models.PassportInfo pi = Mapper.Map<PassportInfo, com.minsoehanwin.sample.Core.Models.PassportInfo>(passportInfo);
                _employeeService.AddPassportInfo(e, pi);
                _unitOfWork.Commit();
                Employee wcfSavedEmployee = Mapper.Map<com.minsoehanwin.sample.Core.Models.Employee,Employee>(e);
                SetLocationHeader(e.Id);
                SetResponseHttpStatus(HttpStatusCode.OK);
                return wcfSavedEmployee;
            }
            catch (com.minsoehanwin.sample.Services.Exception.EmployeeFirstNameException ex)
            {

                _unitOfWork.Rollback();
                _logger.Error(ex);
                throw new WebFaultException<string>(ex.Message
                        , System.Net.HttpStatusCode.BadRequest);
            }
            catch (com.minsoehanwin.sample.Services.Exception.EmployeeLastNameException ex)
            {
                _unitOfWork.Rollback();
                _logger.Error(ex);
                throw new WebFaultException<string>(ex.Message
                        , System.Net.HttpStatusCode.BadRequest);
            }
            catch (com.minsoehanwin.sample.Services.Exception.EmployeeWifesException ex)
            {
                _unitOfWork.Rollback();
                _logger.Error(ex);
                throw new WebFaultException<string>(ex.Message
                        , System.Net.HttpStatusCode.BadRequest);
            }
            catch (com.minsoehanwin.sample.Services.Exception.InvalidPassportNoException ex)
            {
                _unitOfWork.Rollback();
                _logger.Error(ex);
                throw new WebFaultException<string>(ex.Message
                        , System.Net.HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error(ex);
                throw new WebFaultException<string>(ex.Message
                        , System.Net.HttpStatusCode.ServiceUnavailable);
            }
        }

        
        public Employee Save(Employee emploee)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                com.minsoehanwin.sample.Core.Models.Employee e = Mapper.Map<Employee, com.minsoehanwin.sample.Core.Models.Employee>(emploee);
                _employeeService.Save(e);
                _unitOfWork.Commit();
                Employee wcfSavedEmployee = Mapper.Map<com.minsoehanwin.sample.Core.Models.Employee, Employee>(e);
                SetLocationHeader(e.Id);
                SetResponseHttpStatus(HttpStatusCode.OK);
                return wcfSavedEmployee;
            }
            catch (com.minsoehanwin.sample.Services.Exception.EmployeeFirstNameException ex)
            {
                _unitOfWork.Rollback();
                _logger.Error(ex);
                throw new WebFaultException<string>(ex.Message
                        , System.Net.HttpStatusCode.BadRequest);
            }
            catch (com.minsoehanwin.sample.Services.Exception.EmployeeLastNameException ex)
            {
                _unitOfWork.Rollback();
                _logger.Error(ex);
                throw new WebFaultException<string>(ex.Message
                        , System.Net.HttpStatusCode.BadRequest);
            }
            catch (com.minsoehanwin.sample.Services.Exception.EmployeeWifesException ex)
            {
                _unitOfWork.Rollback();
                _logger.Error(ex);
                throw new WebFaultException<string>(ex.Message
                        , System.Net.HttpStatusCode.BadRequest);
            }
            catch (com.minsoehanwin.sample.Services.Exception.InvalidPassportNoException ex)
            {
                _unitOfWork.Rollback();
                _logger.Error(ex);
                throw new WebFaultException<string>(ex.Message
                        , System.Net.HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error(ex);
                throw new WebFaultException<string>(ex.Message
                        , System.Net.HttpStatusCode.ServiceUnavailable);
            }
        }

        
        public IList<Employee> GetList()
        {
            try
            {
                IList<com.minsoehanwin.sample.Core.Models.Employee> e = _employeeService.GetList();
                IList<Employee> wcfE = Mapper.Map<IList<com.minsoehanwin.sample.Core.Models.Employee>, IList<Employee>>(e);
                var error = 0;
                if (wcfE == null)
                {
                    error++;
                }
                else
                {
                    if (wcfE.ToList().Count == 0)
                    {
                        error++;
                    }
                }
                if (error > 0)
                {
                    throw new WebFaultException<string>("There is no customer."
                        , System.Net.HttpStatusCode.NotFound);
                }
                SetResponseHttpStatus(HttpStatusCode.OK);
                return wcfE;
            }
            catch (WebFaultException<string> wfex)
            {
                _logger.Error(wfex);
                throw wfex;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw new WebFaultException<string>(ex.Message
                        , System.Net.HttpStatusCode.ServiceUnavailable);
            }
        }

        
        public void Delete(Employee employee)
        {
            try
            {
                com.minsoehanwin.sample.Core.Models.Employee e=Mapper.Map<Employee,com.minsoehanwin.sample.Core.Models.Employee>(employee);
                _unitOfWork.BeginTransaction();
                _employeeService.Delete(e);
                _unitOfWork.Commit();
                SetResponseHttpStatus(HttpStatusCode.OK);
            }
            catch (WebFaultException<string> wfex)
            {
                _unitOfWork.Rollback();
                _logger.Error(wfex);
                throw wfex;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error(ex);
                throw new WebFaultException<string>(ex.Message
                        , System.Net.HttpStatusCode.ServiceUnavailable);
            }
        }

        
        public void DeleteById(string id)
        {
            try{
                var myId = Int16.Parse(id);
                _unitOfWork.BeginTransaction();
                _employeeService.DeleteById(myId);
                _unitOfWork.Commit();
                SetResponseHttpStatus(HttpStatusCode.OK);
            }
            catch (WebFaultException<string> wfex)
            {
                _unitOfWork.Rollback();
                _logger.Error(wfex);
                throw wfex;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _logger.Error(ex);
                throw new WebFaultException<string>(ex.Message
                        , System.Net.HttpStatusCode.ServiceUnavailable);
            }
        }

        //http://www.ultradevelopers.net/Blog/30
        //[OperationBehavior]//can configure operation behaviour
        
        public Employee GetById(string id)
        {
            try
            {
                int myid = Int16.Parse(id);
                com.minsoehanwin.sample.Core.Models.Employee e = _employeeService.GetById(myid);
                if (e == null)
                {
                    throw new WebFaultException<string>("There is no customer with this id."
                        , System.Net.HttpStatusCode.NotFound);
                }
                Employee wcfE = AutoMapper.Mapper.Map<com.minsoehanwin.sample.Core.Models.Employee, Employee>(e);
                SetResponseHttpStatus(HttpStatusCode.OK);
                return wcfE;
            }
            catch (WebFaultException<string> wfex)
            {
                _logger.Error(wfex);
                throw wfex;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw new WebFaultException<string>(ex.Message
                        , System.Net.HttpStatusCode.ServiceUnavailable);
            }
        }

        
        public Employee PUTSave(string id, Employee employee)
        {
            try
            {
                short myid;
                Int16.TryParse(id, out myid);
                if (myid != employee.Id)
                {
                    throw new Exception("employee.Id mis-matched.");
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex);
                throw new WebFaultException<string>(ex.Message,HttpStatusCode.BadRequest);
            }
            return Save(employee);
        }
        //[PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public string XMLData(string id)
        {
            try
            {
                IsInRole("arole","brole","cRole","Admin");
                _logger.Info("XML data");
                SetResponseHttpStatus(HttpStatusCode.OK);
                return "this is string ." + id;
            }
            catch (Exception ex)
            {
                SetResponseHttpStatus(HttpStatusCode.Forbidden);
                return ex.Message;
            }
        }

        private void IsInRole(params string[] roles)
        {
            var userId = HttpContext.Current.Items["RestfuluserId"].ToString();
            var identityManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var canAccess = false;
            foreach (var r in roles)
            {
                var isAdmin = identityManager.IsInRoleAsync(userId, r);
                if (isAdmin.Result)
                {
                    canAccess = true;
                    break;
                }
            }
            if (!canAccess)
            {
                throw new Exception("Access is denided.");
            }
        }
    }
}