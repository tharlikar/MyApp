using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using com.minsoehanwin.sample.Core.Services;
using com.minsoehanwin.sample.Core.Models;
using com.minsoehanwin.sample.Core;
using System.Threading;
using com.minsoehanwin.sample.Core.EmailEntity;
using com.minsoehanwin.sample.Services.Exception;

namespace com.minsoehanwin.sample.Test
{
    [TestFixture]
    public class EmployeeServiceTest : BaseTest
    {
        #region Save()
                
        [Test]
        public void AddWifeTest()
        {
            var fn = "MinSoe" + DateTime.Now.Ticks;
            var employeeToBeSaved = new Employee() { 
                FirstName = fn
                , LastName = fn
                , PassportInfo = null
                , Store = null
                , Wifes = null 
                ,CreatedBy=fn
            };
            var wifeToBeSaved = new Wife()
            { 
                FirstName = "Lin"
                , LastName = "Min"
                , Employee = null 
                ,CreatedBy =fn
            };

            _unitOfWork.BeginTransaction();
            _employeeService.AddWife(employeeToBeSaved, wifeToBeSaved);
            _unitOfWork.Commit();

            var savedEmployee = _employeeService.GetById(employeeToBeSaved.Id);

            Assert.IsNotNull(savedEmployee);
            Assert.IsNotNull(savedEmployee.Wifes);
            Assert.IsTrue(savedEmployee.Wifes.Count == 1, "wife count should be 1");
            Assert.IsTrue(savedEmployee.Wifes.First().Id > 0, "Id need to be >1");
            Assert.AreEqual(savedEmployee.Id, savedEmployee.Wifes.First().Employee.Id);
            Assert.IsNotNull(savedEmployee.Wifes.First().Employee);
            Assert.IsTrue(savedEmployee.Id > 0);
            Assert.AreEqual(employeeToBeSaved.FirstName, savedEmployee.FirstName);
            Assert.AreEqual(wifeToBeSaved.Id, savedEmployee.Wifes.First().Id);

            _unitOfWork.BeginTransaction();
            _employeeService.Delete(savedEmployee);
            _unitOfWork.Commit();
        }

        [Test]
        public void AddWifeToExistingEmployeeTest()
        {
            var fn = "MinSoe" + DateTime.Now.Ticks;
            var employeeToBeSaved = new Employee()
            {
                FirstName = fn
                ,
                LastName = fn
                ,
                PassportInfo = null
                ,
                Store = null
                ,
                Wifes = null
                ,
                CreatedBy = fn
            };
            var wifeToBeSaved = new Wife()
            {
                FirstName = "Lin"
                ,
                LastName = "Min"
                ,
                Employee = null
                ,
                CreatedBy = fn
            };
            _unitOfWork.BeginTransaction();
            _employeeService.Save(employeeToBeSaved);
            _unitOfWork.Commit();

            var efromDb = _employeeService.GetById(employeeToBeSaved.Id);

            _unitOfWork.BeginTransaction();
            _employeeService.AddWife(efromDb, wifeToBeSaved);
            _unitOfWork.Commit();

            var savedEmployee = _employeeService.GetById(employeeToBeSaved.Id);

            Assert.IsNotNull(savedEmployee);
            Assert.IsNotNull(savedEmployee.Wifes);
            Assert.IsTrue(savedEmployee.Wifes.Count == 1, "wife count should be 1");
            Assert.IsTrue(savedEmployee.Wifes.First().Id > 0, "Id need to be >1");
            Assert.AreEqual(savedEmployee.Id, savedEmployee.Wifes.First().Employee.Id);
            Assert.IsNotNull(savedEmployee.Wifes.First().Employee);
            Assert.IsTrue(savedEmployee.Id > 0);
            Assert.AreEqual(employeeToBeSaved.FirstName, savedEmployee.FirstName);
            Assert.AreEqual(wifeToBeSaved.Id, savedEmployee.Wifes.First().Id);

            _unitOfWork.BeginTransaction();
            _employeeService.Delete(savedEmployee);
            _unitOfWork.Commit();
        }

        [Test]
        public void AddWifeAlreadyHasWifeExceptionTest()
        {
            Assert.Throws(typeof(AlreadyHasWifeException), AddWifeAlreadyHasWifeExceptionGenerator);
        }

        private void AddWifeAlreadyHasWifeExceptionGenerator()
        {
            Employee savedEmployee = null;
            try
            {
                var fn = "MinSoe" + DateTime.Now.Ticks;
                var employeeToBeSaved1 = new Employee()
                {
                    FirstName = fn
                    ,
                    LastName = fn
                    ,
                    PassportInfo = null
                    ,
                    Store = null
                    ,
                    Wifes = null
                    ,
                    CreatedBy = fn
                };
                var wifeToBeSaved1 = new Wife()
                {
                    FirstName = fn
                    ,
                    LastName = fn
                    ,
                    Employee = null
                    ,
                    CreatedBy = fn
                };

                _unitOfWork.BeginTransaction();
                _employeeService.AddWife(employeeToBeSaved1, wifeToBeSaved1);
                _unitOfWork.Commit();

                savedEmployee = _employeeService.GetById(employeeToBeSaved1.Id);

                Thread.Sleep(1000);

                var fn2 = "MinSoe" + DateTime.Now.Ticks;
                var wifeToBeSaved2 = new Wife()
                {
                    FirstName = fn
                    ,
                    LastName = fn
                    ,
                    Employee = null
                    ,
                    CreatedBy = fn
                };

                _unitOfWork.BeginTransaction();
                _employeeService.AddWife(savedEmployee, wifeToBeSaved2);
                _unitOfWork.Commit();

                _unitOfWork.BeginTransaction();
                _employeeService.Delete(savedEmployee);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _unitOfWork.BeginTransaction();
                _employeeService.Delete(savedEmployee);
                _unitOfWork.Commit();
                    throw ex;
            }
        }


        [Test]
        public void SaveEmployeeAndPassportInfo()
        {
            var fn = "MinSoe" + DateTime.Now.Ticks;
            var employeeToBeSaved1 = new Employee()
            {
                FirstName = fn
                ,
                LastName = fn
                ,
                PassportInfo = null
                ,
                Store = null
                ,
                Wifes = null
                ,
                CreatedBy = fn
            };
            var passportToBeSaved1 = new PassportInfo()
            {
                PassportNo=fn
                ,IssueDate=DateTime.Now
                ,ExpiredDate=DateTime.Now
                ,CreatedBy=fn
            };
            _unitOfWork.BeginTransaction();
            _employeeService.AddPassportInfo(employeeToBeSaved1, passportToBeSaved1);
            _unitOfWork.Commit();

            var savedEmployee = _employeeService.GetById(employeeToBeSaved1.Id);

            Assert.IsNotNull(savedEmployee);
            Assert.IsTrue(savedEmployee.Id > 0);
            Assert.IsNotNull(savedEmployee.PassportInfo);
            Assert.IsTrue(savedEmployee.PassportInfo.EmployeeId == savedEmployee.Id);
            Assert.AreEqual(savedEmployee.PassportInfo.PassportNo,passportToBeSaved1.PassportNo);
            Assert.AreEqual(savedEmployee.PassportInfo.IssueDate,passportToBeSaved1.IssueDate);
            Assert.AreEqual(savedEmployee.PassportInfo.ExpiredDate,passportToBeSaved1.ExpiredDate);

            int eid=savedEmployee.Id;
            _unitOfWork.BeginTransaction();
            _employeeService.Delete(savedEmployee);
            _unitOfWork.Commit();
            Assert.IsNull(_passportInfoService.GetById(eid));
        }

        [Test]
        public void UpdateEmployeeAndPassportInfo()
        {
            var fn = "MS" + DateTime.Now.Ticks;
            var employeeToBeSaved = new Employee()
            {
                FirstName = fn
                ,
                LastName = fn
                ,
                PassportInfo = null
                ,CreatedBy = fn
            };
            var passportInfoToBeSaved = new PassportInfo()
            {
                PassportNo = fn
                ,
                IssueDate = DateTime.Now
                ,
                ExpiredDate = DateTime.Now
                ,
                Employee = null
            };

            _unitOfWork.BeginTransaction();
            _employeeService.AddPassportInfo(employeeToBeSaved, passportInfoToBeSaved);
            _unitOfWork.Commit();

            var savedEmployee = _employeeService.GetById(employeeToBeSaved.Id);

            string newPassportNo = "Updated"+DateTime.Now.Ticks;
            savedEmployee.PassportInfo.PassportNo = newPassportNo;

            _unitOfWork.BeginTransaction();
            _employeeService.Save(savedEmployee);
            _unitOfWork.Commit();

            Employee eFromDb = _employeeService.GetById(savedEmployee.Id);

            Assert.IsTrue(savedEmployee.PassportInfo.PassportNo == newPassportNo);

            _unitOfWork.BeginTransaction();
            _employeeService.Delete(eFromDb);
            _unitOfWork.Commit();
        }

        [Test]
        public void EmployeeFirstNameExceptionTest()
        {
            Assert.Throws(
                typeof(EmployeeFirstNameException),
                new TestDelegate(EmployeeFirstNameExceptionGenerator)
            );
        }

        public void EmployeeFirstNameExceptionGenerator()
        {
            try
            {
                var e = new Employee()
                {
                    FirstName = "m"
                    ,LastName = "l"
                };
                
                _unitOfWork.BeginTransaction();
                _employeeService.Save(e);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                    throw ex;
            }
        }

        #endregion
        
        public void GetList()
        {
            var fn = "ms"+DateTime.Now.Ticks;
            var e1 = new Employee()
            {
                FirstName = fn
                ,LastName = fn
            };

            Thread.Sleep(1000);
            var fn2 = "ms2"+DateTime.Now.Ticks;
            var e2 = new Employee()
            {
                FirstName = fn2
                ,
                LastName = fn2
            };

            _unitOfWork.BeginTransaction();
            _employeeService.Save(e1);
            _unitOfWork.Commit();

            var list = _employeeService.GetList();

            Assert.AreEqual(list.Where(x=>x.FirstName==e1.FirstName).Count(),1);
            
            _unitOfWork.BeginTransaction();
            _employeeService.Save(e2);
            _unitOfWork.Commit();

            list =_employeeService.GetList();
            Assert.AreEqual(list.Where(x=>x.FirstName==e2.FirstName).Count(),1);

            _unitOfWork.BeginTransaction();
            _employeeService.Delete(list.Where(x => x.FirstName == e1.FirstName).Select(x => x).First());
            _employeeService.DeleteById(list.Where(x => x.FirstName == e2.FirstName).Select(x=>x).First().Id);
            _unitOfWork.Commit();
        }
    }
}