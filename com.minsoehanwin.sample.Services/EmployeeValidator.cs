using log4net;
using com.minsoehanwin.sample.Core.Models;
using com.minsoehanwin.sample.Services.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services
{
    public class EmployeeValidator:BaseValidator
    {
        public void Validate(Employee employee)
        {
            _logger.Info("Validating employee started.");
            ValidateEmployeeFirstName(employee.FirstName);
            ValidateEmployeeLastName(employee.LastName);
            ValidateEmployeeWifes(employee.Wifes);
            _logger.Info("Validating employee ended.");
        }

        public void ValidateEmployeeWifes(IList<Wife> wifes)
        {
            _logger.Info("Start validating wifes");
            var error=0;
            var message=string.Empty;
            if (IsWifesNull(wifes))
            {
                message += "Wifes is null,";
                error++;
                throw new EmployeeWifesException(message);
            }

            if (!HasOneWifeOrNoWife(wifes))
            {
                message += "Employee has more than one wife,";
                error++;
                throw new com.minsoehanwin.sample.Services.Exception.AlreadyHasWifeException(message);
            }

            //if (error > 0)
            //{
            //    _logger.Error(message);
            //    throw new EmployeeWifesException(message);
            //}
            _logger.Info("Validating wifes.OK.Done");
        }

        private bool HasWife(IList<Wife> wifes)
        {
            if (IsWifesNull(wifes)) return false;
            if (wifes.Count > 0) return true;
            return false;
        }

        private bool HasOneWifeOnly(IList<Wife> wifes)
        {
            if (IsWifesNull(wifes)) return false;
            if (wifes.Count != 1) return false;
            return true;
        }

        private bool IsFirstWifeHasEmployee(IList<Wife> wifes)
        {
            return !(wifes.First().Employee == null);
        }

        private bool IsWifesNull(IList<Wife> wifes)
        {
            return wifes == null;
        }

        private bool HasOneWifeOrNoWife(IList<Wife> wifes)
        {
            if (HasNoWife(wifes)) return true;
            if (wifes.Count() == 1) return true;
            return false;
        }

        private bool HasNoWife(IList<Wife> wifes)
        {
            if (IsWifesNull(wifes)) return true;
            if (wifes.Count() < 1) return true;
            return false;
        }

        public void ValidateEmployeeLastName(string lastName)
        {
            _logger.Info(string.Format("Start validating lastname:{0}", lastName));
            var error = 0;
            var message = string.Empty;
            if (string.IsNullOrEmpty(lastName))
            {
                message += string.Format("Employee lastname is null or empty,");
                error++;
                throw new EmployeeLastNameException(message);
            }

            if (lastName.Trim().Length < 3)
            {
                message += string.Format("Employee lastname [{0}] is less than 3 character,", lastName);
                error++;
            }

            if (error > 0)
            {
                _logger.Error(message);
                throw new EmployeeLastNameException(message);
            }
            _logger.Info("Validating lastname.OK.Done.");
        }

        public void ValidateEmployeeFirstName(string firstName)
        {
            _logger.Info(string.Format("Start validating firstname:{0}", firstName));
            var error = 0;
            var message = string.Empty;
            if (string.IsNullOrEmpty(firstName))
            {
                var tempMsg="Employee firstname is null or empty,";
                message += string.Format(tempMsg);
                error++;
                throw new EmployeeFirstNameException(message);
            }

            if (firstName.Trim().Length < 3)
            {
                var tempMsg="Employee firstname [{0}] is less than 3 character,";
                message += string.Format(tempMsg, firstName);
                error++;
            }

            if (error > 0)
            {
                _logger.Error(message);
                throw new EmployeeFirstNameException(message);
            }
            _logger.Info("Validating firstname.OK.Done.");
        }

        public void ValidateAddWife(Employee employee, Wife wife)
        {
            _logger.Info("Adding wife.Validating.");
            if (HasWife(employee.Wifes))
            {
                var msg = "Employee already has wife";
                _logger.Info(msg);
                throw new AlreadyHasWifeException(msg);
            }
            _logger.Info("Adding wife.Validating.Done.");
        }

        public void ValidateCanDeleteEmployee(Employee employee)
        {
            EmployeeIsNull(employee);
            //ValidateEmployeeHasWfie(employee);
        }

        private void ValidateEmployeeHasWfie(Employee employee)
        {
            if(employee.Wifes.Count>0){
                throw new EmployeeHasWifeException("Employee has wife(s).");
            }
        }

        public void EmployeeIsNull(Employee employee)
        {
            if (IsNull(employee))
            {
                throw new EmployeeNotFoundException("Emploee is null or employee not found.");
            }
        }

        public static bool IsNull(Employee employee)
        {
            return employee == null;
        }

        public bool HasWifes(Employee employee)
        {
            return employee.Wifes.Count > 0;
        }

        public bool HasPassport(Employee employeeToBeDeleted)
        {
            return employeeToBeDeleted.PassportInfo != null;
        }

        internal bool HasStore(Employee employeeToBeDeleted)
        {
            return employeeToBeDeleted.Store != null;
        }
    }
}