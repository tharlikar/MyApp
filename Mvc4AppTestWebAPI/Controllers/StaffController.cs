using com.minsoehanwin.sample.Core;
using com.minsoehanwin.sample.Core.Models;
using com.minsoehanwin.sample.Core.Services;
using com.minsoehanwin.sample.Services;
using Mvc4AppTestWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Mvc4AppTestWebAPI.Controllers
{
    [System.Web.Mvc.Authorize(Roles = "Admin")]
    public class StaffController : BaseController
    {
        private IEmployeeService _employeeService;
        private EmployeeValidator _employeeValidator;
        public StaffController()
            :base()
        {
            
        }
        public StaffController(IUnitOfWork unitOfWork, IEmployeeService employeeService,EmployeeValidator employeeValidator )
            :base(unitOfWork)
        {
            ViewBag.ActiveManage = "active";
            ViewBag.ActiveStaffAdmin = "active";
            _employeeService = employeeService;
            _employeeValidator = employeeValidator;
            System.Web.HttpContext.Current.Items.Add("_"+typeof(EmployeeValidator).Name, _employeeValidator);
        }

        public ActionResult Index()
        {
            IList<Employee> list=_employeeService.GetList().Select(x=>x).ToList();
            IList<StaffCreateViewModel> viewList= AutoMapper.Mapper.Map<IList<StaffCreateViewModel>>(list);
            return View(viewList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Create(StaffCreateViewModel staffViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.BeginTransaction();
                    var employee = new Employee();
                    AutoMapper.Mapper.Map(staffViewModel,employee);
                    employee.PassportInfo=AutoMapper.Mapper.Map<PassportInfo>(staffViewModel);
                    var wife = AutoMapper.Mapper.Map<Wife>(staffViewModel);
                    employee.Wifes.Add(wife);
                    _employeeService.Save(employee);
                    _unitOfWork.Commit();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(new List<Object>() { 
                    "Create staff failed. Data are:",
                    staffViewModel
                }, ex);
                _unitOfWork.Rollback();
                throw;
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var staff=_employeeService.GetById(id);
            StaffCreateViewModel staffCreateViewModel = AutoMapper.Mapper.Map<StaffCreateViewModel>(staff);
            return View("Edit", staffCreateViewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(StaffCreateViewModel staffCreateViewModel)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                if (!ModelState.IsValid)
                {
                    throw new Exception("Model state not valid");
                }
                Employee employee = _employeeService.GetById(staffCreateViewModel.Id.Value);
                AutoMapper.Mapper.Map(staffCreateViewModel, employee);
                AutoMapper.Mapper.Map(staffCreateViewModel, employee.PassportInfo);
                AutoMapper.Mapper.Map(staffCreateViewModel, employee.Wifes[0]);
                _employeeService.Save(employee);
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.Error(new List<Object>()
                {
                    "Error editing staff.Input data is as follow:",
                    staffCreateViewModel
                }, ex);
                _unitOfWork.Rollback();
                throw;
            }
            return View();
        }

        public ActionResult Details(int id)
        {
            var staff = _employeeService.GetById(id);
            return View(new StaffCreateViewModel()
            {
                Id = staff.Id
                ,
                FirstName = staff.FirstName
                ,
                LastName = staff.LastName
            });
        }

        public ActionResult Delete(int id)
        {
            var staff = _employeeService.GetById(id);
            return View(new StaffCreateViewModel()
            {
                Id = staff.Id
                ,
                FirstName = staff.FirstName
                ,
                LastName = staff.LastName
            });
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Delete")]
        public ActionResult DeleteStaff(int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                _employeeService.DeleteById(id);
                _unitOfWork.Commit();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw;
            }
            return View();
        }
    }
}
