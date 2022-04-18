using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using com.minsoehanwin.sample.Core.Models;
namespace com.minsoehanwin.sample.Test
{
    [TestFixture]
    public class EFvsNhibernateTest : BaseTest
    {
        [Test]
        public void RemoveEmployeePassportInfoTest()
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
            var passportInfoToBeSaved = new PassportInfo()
            {
                PassportNo = fn
               ,
                IssueDate = DateTime.Now
               ,
                ExpiredDate = DateTime.Now
            };
            //EFonlyNeed #1 but NHibernate need both #1 and #2; one -> one shared PK
            //#1
            employeeToBeSaved.PassportInfo = passportInfoToBeSaved;
            //#2
            passportInfoToBeSaved.Employee = employeeToBeSaved;
            _unitOfWork.BeginTransaction();
            _employeeService.Save(employeeToBeSaved);
            _unitOfWork.Commit();

            var savedEmployee = _employeeService.GetById(employeeToBeSaved.Id);

            Assert.AreEqual(employeeToBeSaved.FirstName, savedEmployee.FirstName);
            Assert.AreEqual(employeeToBeSaved.LastName, savedEmployee.LastName);
            Assert.AreEqual(savedEmployee.PassportInfo.PassportNo, passportInfoToBeSaved.PassportNo);

            _unitOfWork.BeginTransaction();
            savedEmployee.PassportInfo = null;
            _employeeService.Save(savedEmployee);
            _passportInfoService.Delete(_passportInfoService.GetById(savedEmployee.Id));
            _unitOfWork.Commit();
            Assert.IsNull(_passportInfoService.GetById(savedEmployee.Id));

            int eId=savedEmployee.Id;
            _unitOfWork.BeginTransaction();
            _employeeService.Delete(savedEmployee);
            _unitOfWork.Commit();
            Assert.IsNull(_employeeService.GetById(eId));
        }
        [Test]
        public void DeleteEmployeeAndWifeTest()
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
                FirstName = fn
                ,
                LastName = fn
            };
            employeeToBeSaved.Wifes.Add(wifeToBeSaved);
            _unitOfWork.BeginTransaction();
            _employeeService.Save(employeeToBeSaved);
            _unitOfWork.Commit();

            var savedEmployee = _employeeService.GetById(employeeToBeSaved.Id);

            Assert.AreEqual(employeeToBeSaved.FirstName, savedEmployee.FirstName);
            Assert.AreEqual(employeeToBeSaved.LastName, savedEmployee.LastName);
            Assert.IsNull(savedEmployee.PassportInfo);
            Assert.AreEqual(1, savedEmployee.Wifes.Count);
            Assert.IsNull(savedEmployee.Store);
            Assert.IsNull(savedEmployee.MyStoreId);
            Assert.IsNotNull(savedEmployee);
            Assert.IsTrue(savedEmployee.Id > 0);
            Assert.IsInstanceOf(typeof(Employee), savedEmployee);
            Assert.AreEqual(savedEmployee.Wifes.First().FirstName, wifeToBeSaved.FirstName);

            int wifeId = savedEmployee.Wifes.First().Id;
            int eId = savedEmployee.Id;
            Assert.IsNotNull(_wifeService.GetById(wifeId));
            _unitOfWork.BeginTransaction();
            _employeeService.Delete(savedEmployee);
            _unitOfWork.Commit();
            Assert.IsNull(_employeeService.GetById(eId));
            Assert.IsNull(_wifeService.GetById(wifeId));
        }

        [Test]
        public void RemoveEmployeeWifeTest()
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
                FirstName = fn
                ,
                LastName = fn
            };
            employeeToBeSaved.Wifes.Add(wifeToBeSaved);
            _unitOfWork.BeginTransaction();
            _employeeService.Save(employeeToBeSaved);
            _unitOfWork.Commit();

            var savedEmployee = _employeeService.GetById(employeeToBeSaved.Id);

            Assert.AreEqual(employeeToBeSaved.FirstName, savedEmployee.FirstName);
            Assert.AreEqual(employeeToBeSaved.LastName, savedEmployee.LastName);
            Assert.IsNull(savedEmployee.PassportInfo);
            Assert.AreEqual(1, savedEmployee.Wifes.Count);
            Assert.IsNull(savedEmployee.Store);
            Assert.IsNull(savedEmployee.MyStoreId);
            Assert.IsNotNull(savedEmployee);
            Assert.IsTrue(savedEmployee.Id > 0);
            Assert.IsInstanceOf(typeof(Employee), savedEmployee);
            Assert.AreEqual(savedEmployee.Wifes.First().FirstName, wifeToBeSaved.FirstName);

            int wifeId = savedEmployee.Wifes.First().Id;
            int eId = savedEmployee.Id;            
            Assert.IsNotNull(_wifeService.GetById(wifeId));
            _unitOfWork.BeginTransaction();
            savedEmployee.Wifes.Remove(savedEmployee.Wifes.First());
            _employeeService.Save(savedEmployee);
            _wifeService.Delete(_wifeService.GetById(wifeId));
            _unitOfWork.Commit();
            Assert.IsNull(_wifeService.GetById(wifeId));
            Assert.IsNotNull(_employeeService.GetById(eId));
            _unitOfWork.BeginTransaction();
            _employeeService.Delete(savedEmployee);
            _unitOfWork.Commit();
            Assert.IsNull(_employeeService.GetById(eId));
        }
        [Test]
        public void SaveProductStoresTest()
        {
            var fn = "MinSoe" + DateTime.Now.Ticks;
            var p1ToBeSaved = new Product()
            {
                Name = fn
            };
            var p2ToBeSaved = new Product()
            {
                Name = fn
            };
            var store1ToBeSaved = new Store()
            {
                Name = fn
            };
            var store2ToBeSaved = new Store()
            {
                Name = fn
            };
            p1ToBeSaved.StoresStockedIn.Add(store1ToBeSaved);
            p2ToBeSaved.StoresStockedIn.Add(store2ToBeSaved);
            
            _unitOfWork.BeginTransaction();
            _productService.Save(p1ToBeSaved);
            _productService.Save(p2ToBeSaved);
            _unitOfWork.Commit();
            var s1Id = store1ToBeSaved.Id;
            var s2Id = store2ToBeSaved.Id;
            var p1Id = p1ToBeSaved.Id;
            var p2Id = p2ToBeSaved.Id;
            Assert.IsNotNull(_storeService.GetById(s1Id));
            Assert.IsNotNull(_storeService.GetById(s2Id));
            Assert.IsNotNull(_productService.GetById(p1Id));
            Assert.IsNotNull(_productService.GetById(p2Id));
            _unitOfWork.BeginTransaction();
            _productService.Delete(_productService.GetById(p1Id));
            _unitOfWork.Commit();
            Assert.IsNotNull(_storeService.GetById(s1Id));
            Assert.IsNotNull(_storeService.GetById(s2Id));
            Assert.IsNull(_productService.GetById(p1Id));
            Assert.IsNotNull(_productService.GetById(p2Id));
            _unitOfWork.BeginTransaction();
            _productService.Delete(_productService.GetById(p2Id));
            _unitOfWork.Commit();
            Assert.IsNotNull(_storeService.GetById(s1Id));
            Assert.IsNotNull(_storeService.GetById(s2Id));
            Assert.IsNull(_productService.GetById(p1Id));
            Assert.IsNull(_productService.GetById(p2Id));
            _unitOfWork.BeginTransaction();
            _storeService.Delete(_storeService.GetById(s1Id));
            _unitOfWork.Commit();
            Assert.IsNull(_storeService.GetById(s1Id));
            Assert.IsNotNull(_storeService.GetById(s2Id));
            Assert.IsNull(_productService.GetById(p1Id));
            Assert.IsNull(_productService.GetById(p2Id));
            _unitOfWork.BeginTransaction();
            _storeService.Delete(_storeService.GetById(s2Id));
            _unitOfWork.Commit();
            Assert.IsNull(_storeService.GetById(s1Id));
            Assert.IsNull(_storeService.GetById(s2Id));
            Assert.IsNull(_productService.GetById(p1Id));
            Assert.IsNull(_productService.GetById(p2Id));
        }
        [Test]
        public void SaveStoreProductsTest()
        {
            var fn = "MinSoe" + DateTime.Now.Ticks;
            var p1ToBeSaved = new Product()
            {
                Name=fn
            };
            var p2ToBeSaved = new Product()
            {
                Name = fn
            };
            var store1ToBeSaved = new Store()
            {
                Name=fn
            };
            var store2ToBeSaved = new Store()
            {
                Name = fn
            };
            store1ToBeSaved.Products.Add(p1ToBeSaved);
            store1ToBeSaved.Products.Add(p2ToBeSaved);
            p1ToBeSaved.StoresStockedIn.Add(store2ToBeSaved);
            p2ToBeSaved.StoresStockedIn.Add(store2ToBeSaved);
            _unitOfWork.BeginTransaction();
            _storeService.Save(store1ToBeSaved);
            _productService.Save(p1ToBeSaved);
            _productService.Save(p2ToBeSaved);
            _unitOfWork.Commit();

            var savedStore1 = _storeService.GetById(store1ToBeSaved.Id);
            var savedStore2 = _storeService.GetById(store2ToBeSaved.Id);

            //saving store will also save both product
            Assert.AreEqual(savedStore1.Products[0].Name, p1ToBeSaved.Name);
            Assert.AreEqual(savedStore1.Products[1].Name, p2ToBeSaved.Name);
            //Assert.AreEqual(savedStore2.Products[0].Name, p1ToBeSaved.Name);
            //Assert.AreEqual(savedStore2.Products[1].Name, p2ToBeSaved.Name);
            var savedP1 = _productService.GetById(p1ToBeSaved.Id);
            var savedP2 = _productService.GetById(p2ToBeSaved.Id);
            Assert.IsTrue(savedP1.StoresStockedIn.Contains(store2ToBeSaved));
            Assert.IsTrue(savedP2.StoresStockedIn.Contains(store2ToBeSaved));

            var store1Id = savedStore1.Id;
            var store2Id = savedStore2.Id;
            var savedP1Id = savedP1.Id;
            var savedP2Id = savedP2.Id;

            _unitOfWork.BeginTransaction();
            _storeService.Delete(savedStore1);
            _unitOfWork.Commit();
            
            Assert.IsNull(_storeService.GetById(store1Id));

            _unitOfWork.BeginTransaction();
            _productService.Delete(savedP1);
            _unitOfWork.Commit();

            Assert.IsNull(_productService.GetById(savedP1Id));

            _unitOfWork.BeginTransaction();
            _storeService.DeleteById(store2Id);
            _productService.DeleteById(savedP2Id);
            _unitOfWork.Commit();

            Assert.IsNull(_storeService.GetById(store2Id));
            Assert.IsNull(_productService.GetById(savedP2Id));

        }
        [Test]
        public void SaveStoreEmployeeTest()
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
            var employeeToBeSaved2 = new Employee()
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
            var storeToBeSaved = new Store()
            {
                Name = fn
            };
            storeToBeSaved.Staffs.Add(employeeToBeSaved1);
            storeToBeSaved.Staffs.Add(employeeToBeSaved2);
            _unitOfWork.BeginTransaction();
            _storeService.Save(storeToBeSaved);
            _unitOfWork.Commit();

            var savedStore = _storeService.GetById(storeToBeSaved.Id);

            //saving store will also save both employee
            Assert.AreEqual(savedStore.Staffs[0].FirstName, employeeToBeSaved1.FirstName);
            Assert.AreEqual(savedStore.Staffs[1].FirstName, employeeToBeSaved2.FirstName);
            var storeId=savedStore.Id;
            var savedE1Id = savedStore.Staffs[0].Id;
            var savedE2Id = savedStore.Staffs[1].Id;
            _unitOfWork.BeginTransaction();
            _storeService.Delete(savedStore);
            _unitOfWork.Commit();

            //deleting store will not delete both employees
            Assert.IsNull(_storeService.GetById(storeId));
            Assert.IsNotNull(_employeeService.GetById(savedE1Id));
            Assert.IsNotNull(_employeeService.GetById(savedE2Id));

            _unitOfWork.BeginTransaction();
            _employeeService.DeleteById(savedE1Id);
            _employeeService.DeleteById(savedE2Id);
            _unitOfWork.Commit();

            Assert.IsNull(_employeeService.GetById(savedE1Id));
            Assert.IsNull(_employeeService.GetById(savedE2Id));
        }
        [Test]
        public void SaveEmployeeStoreTest()
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
            var storeToBeSaved = new Store()
            {
                Name = fn
            };
            employeeToBeSaved.Store = storeToBeSaved;
            _unitOfWork.BeginTransaction();
            _employeeService.Save(employeeToBeSaved);
            _unitOfWork.Commit();

            var savedEmployee = _employeeService.GetById(employeeToBeSaved.Id);
            
            //saving employee will also save store
            Assert.AreEqual(employeeToBeSaved.FirstName, savedEmployee.FirstName);
            Assert.AreEqual(employeeToBeSaved.LastName, savedEmployee.LastName);
            Assert.AreEqual(savedEmployee.Store.Name, storeToBeSaved.Name);

            int storedId = savedEmployee.Store.Id;
            int Id =savedEmployee.Id;
            _unitOfWork.BeginTransaction();
            _employeeService.Delete(savedEmployee);
            _unitOfWork.Commit();
            
            //deleting employee will not delete store
            Assert.IsNull(_employeeService.GetById(Id));
            Assert.IsNotNull(_storeService.GetById(storedId));

            _unitOfWork.BeginTransaction();
            _storeService.DeleteById(storedId);
            _unitOfWork.Commit();

            Assert.IsNull(_storeService.GetById(storedId));
        }
        [Test]
        public void SaveEmployeePassportInfoTest()
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
            var passportInfoToBeSaved = new PassportInfo()
            {
                PassportNo = fn
               ,
                IssueDate = DateTime.Now
               ,
                ExpiredDate = DateTime.Now
            };
            //EFonlyNeed #1 but NHibernate need both #1 and #2; one -> one shared PK
            //#1
            employeeToBeSaved.PassportInfo = passportInfoToBeSaved;
            //#2
            passportInfoToBeSaved.Employee = employeeToBeSaved;
            _unitOfWork.BeginTransaction();
            _employeeService.Save(employeeToBeSaved);
            _unitOfWork.Commit();

            var savedEmployee = _employeeService.GetById(employeeToBeSaved.Id);

            Assert.AreEqual(employeeToBeSaved.FirstName, savedEmployee.FirstName);
            Assert.AreEqual(employeeToBeSaved.LastName, savedEmployee.LastName);
            Assert.AreEqual(savedEmployee.PassportInfo.PassportNo,passportInfoToBeSaved.PassportNo);
            int eId = savedEmployee.Id;
            _unitOfWork.BeginTransaction();
            _employeeService.Delete(savedEmployee);
            _unitOfWork.Commit();
            Assert.IsNull(_passportInfoService.GetById(eId));
        }
        [Test]
        public void SaveEmployeeWifeTest()
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
                FirstName = fn
                ,
                LastName = fn
            };
            employeeToBeSaved.Wifes.Add(wifeToBeSaved);
            _unitOfWork.BeginTransaction();
            _employeeService.Save(employeeToBeSaved);
            _unitOfWork.Commit();

            var savedEmployee = _employeeService.GetById(employeeToBeSaved.Id);

            Assert.AreEqual(employeeToBeSaved.FirstName, savedEmployee.FirstName);
            Assert.AreEqual(employeeToBeSaved.LastName, savedEmployee.LastName);
            Assert.IsNull(savedEmployee.PassportInfo);
            Assert.AreEqual(1, savedEmployee.Wifes.Count);
            Assert.IsNull(savedEmployee.Store);
            Assert.IsNull(savedEmployee.MyStoreId);
            Assert.IsNotNull(savedEmployee);
            Assert.IsTrue(savedEmployee.Id > 0);
            Assert.IsInstanceOf(typeof(Employee), savedEmployee);
            Assert.AreEqual(savedEmployee.Wifes.First().FirstName, wifeToBeSaved.FirstName);
            int wId = savedEmployee.Wifes.First().Id;
            _unitOfWork.BeginTransaction();
            _employeeService.Delete(savedEmployee);
            _unitOfWork.Commit();
            Assert.IsNull(_wifeService.GetById(wId));
        }
        [Test]
        public void SaveEmployeeTest()
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

            _unitOfWork.BeginTransaction();
            _employeeService.Save(employeeToBeSaved);
            _unitOfWork.Commit();

            var savedEmployee = _employeeService.GetById(employeeToBeSaved.Id);

            Assert.AreEqual(employeeToBeSaved.FirstName, savedEmployee.FirstName);
            Assert.AreEqual(employeeToBeSaved.LastName, savedEmployee.LastName);
            Assert.IsNull(savedEmployee.PassportInfo);
            Assert.AreEqual(savedEmployee.Wifes.Count, 0);
            Assert.IsNull(savedEmployee.Store);
            Assert.IsNull(savedEmployee.MyStoreId);
            Assert.IsNotNull(savedEmployee);
            Assert.IsTrue(savedEmployee.Id > 0);
            Assert.IsInstanceOf(typeof(Employee), savedEmployee);

            _unitOfWork.BeginTransaction();
            _employeeService.Delete(savedEmployee);
            _unitOfWork.Commit();
        }

    }
}
