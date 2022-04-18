using AutoMapper;
using log4net;
using com.minsoehanwin.sample.Core;
using com.minsoehanwin.sample.Core.Models;
using com.minsoehanwin.sample.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Net.Http.Headers;
using com.minsoehanwin.sample.Services.Exception;

namespace Mvc4AppTestWebAPI.Controllers
{
    [Authorize(Roles="Admin")]
    public class EmployeesController : BaseApiController
    {
        private IUnitOfWork _unitOfWork;
        private IEmployeeService _employeeService;
        private IStoreService _storeService;

        public EmployeesController(IUnitOfWork unitOfWork
            , IEmployeeService employeeService
            , IStoreService storeService)
        {
            _unitOfWork = unitOfWork;
            _employeeService = employeeService;
            _storeService = storeService;
            com.minsoehanwin.sample.Wcf.Service.AutoMapperWebConfiguration.Configure(new com.minsoehanwin.sample.Wcf.Service.MyRestfulProfile());
            Mapper.AssertConfigurationIsValid();
        }

        //http://blog.appliedis.com/2013/03/25/web-api-mixing-traditional-verb-based-routing/
        //https://github.com/kstreith/webapi-routing-sample/blob/master/OrderController.cs
        // GET /api/emploees
        //http://localhost:1759/api/values
        public HttpResponseMessage Get()
        {
            //http://codebetter.com/glennblock/2012/05/24/two-ways-to-work-with-http-responses-in-apicontroller-httpresponsemessage-and-httpresponseexception/
            //return Request.CreateResponse(HttpStatusCode.OK
            //    , new string[] { "value1", "value2" }, "application/xml");

            //return Request.CreateResponse(HttpStatusCode.OK
            //    , new string[] { "value1", "value2" }, "application/json");

            //return Request.CreateResponse(HttpStatusCode.OK
            //   , new CustomerViewModel(){ Id=1,FirstName="value1",LastName="value2" }, "application/xml");

            //return Request.CreateResponse(HttpStatusCode.OK
            //    , new CustomerViewModel() { Id = 1, FirstName = "value1", LastName = "value2" }, "application/json");

            //return Request.CreateResponse(HttpStatusCode.BadRequest);
            
            //http://trycatch.me/xml-json-serialization-of-object-graphs-with-cyclic-references-in-net/
            //Object graph for type 
            //'System.Collections.Generic.List`1[[com.minsoehanwin.sample.Core.Models.Wife, com.minsoehanwin.sample.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]'
            //contains cycles and cannot be serialized if reference tracking is disabled.

            try
            {
                IList<Employee> employeeList = _employeeService.GetList();
                IList<com.minsoehanwin.sample.Wcf.Service.Employee> employeeListDTO = Mapper.Map<IList<Employee>, IList<com.minsoehanwin.sample.Wcf.Service.Employee>>(employeeList);
                var error = 0;
                if (employeeListDTO == null)
                {
                    error++;
                }
                else
                {
                    if (employeeListDTO.ToList().Count == 0)
                    {
                        error++;
                    }
                }
                if (error > 0)
                {
                    throw new NoEmployeeFoundException("No employee found.");
                }
                return Request.CreateResponse(HttpStatusCode.OK, employeeListDTO);
            }
            catch (NoEmployeeFoundException ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        
        /// <summary>
        /// GET /api/employees/5
        /// http://localhost:1759/api/values/34
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Employee employee = _employeeService.GetById(id);
                if (employee == null)
                {
                    throw new NoEmployeeFoundException("Employee not found.");
                }
                com.minsoehanwin.sample.Wcf.Service.Employee wcfEmployee = AutoMapper.Mapper.Map<Employee, com.minsoehanwin.sample.Wcf.Service.Employee>(employee);
                return Request.CreateResponse(HttpStatusCode.OK,wcfEmployee);
            }
            catch (NoEmployeeFoundException ex)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        // POST /api/employees
        public HttpResponseMessage Post(com.minsoehanwin.sample.Wcf.Service.Employee employee)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                Employee employeeEntity = Mapper.Map<com.minsoehanwin.sample.Wcf.Service.Employee, Employee>(employee);
                _employeeService.Save(employeeEntity);
                _unitOfWork.Commit();
                com.minsoehanwin.sample.Wcf.Service.Employee wcfEmployeeEntity = Mapper.Map<Employee, com.minsoehanwin.sample.Wcf.Service.Employee>(employeeEntity);
                var responseMessage = Request.CreateResponse(wcfEmployeeEntity);
                responseMessage.StatusCode = HttpStatusCode.OK;
                var requestUri = Request.RequestUri.AbsoluteUri;
                responseMessage.Headers.Location = new Uri(string.Format("{0}/{1}",requestUri, employeeEntity.Id));
                return responseMessage;
            }
            catch (EmployeeWifesException ex)
            {
                _unitOfWork.Rollback();
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (EmployeeFirstNameException ex)
            {
                _unitOfWork.Rollback();
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (EmployeeLastNameException ex)
            {
                _unitOfWork.Rollback();
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT /api/employees/5
        public HttpResponseMessage Put(int id,com.minsoehanwin.sample.Wcf.Service.Employee employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"employee.Id mismatched.");
                }
                _unitOfWork.BeginTransaction();
                Employee employeeEntity = Mapper.Map<com.minsoehanwin.sample.Wcf.Service.Employee, Employee>(employee);
                _employeeService.Save(employeeEntity);
                _unitOfWork.Commit();
                com.minsoehanwin.sample.Wcf.Service.Employee wcfEmployeeEntity = Mapper.Map<Employee, com.minsoehanwin.sample.Wcf.Service.Employee>(employeeEntity);
                return Request.CreateResponse(HttpStatusCode.OK,wcfEmployeeEntity);
            }
            catch (EmployeeWifesException ex)
            {
                _unitOfWork.Rollback();
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (EmployeeFirstNameException ex)
            {
                _unitOfWork.Rollback();
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (EmployeeLastNameException ex)
            {
                _unitOfWork.Rollback();
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return Request.CreateResponse(HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }

        
        /// <summary>
        /// DELETE /api/values/5
        /// To test a non 200 response code, you must check the 'Ignore Status' field in the Response Assertion. Without this, the test will always fail regardless of the response assertion.
        /// So here is what you need to do to test the http response code 400:
        /// Add a new Response Assertion. Set the following assertion properties:
        /// check the 'Response Code' radio button.
        /// check the 'Ignore Status' box.
        /// check the 'Equals' radio button in the Pattern Matching Rules.
        /// click the 'Add' button.
        /// enter '400' in the row in Patterns to Test.
        /// Done.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _employeeService.DeleteById(id);
                _unitOfWork.Commit();
                return Request.CreateResponse(HttpStatusCode.OK,"EmployeeDeleted");
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return Request.CreateResponse(HttpStatusCode.ServiceUnavailable, ex.Message);
            }
        }
    }
}