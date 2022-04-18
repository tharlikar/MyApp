using Mvc4.Core;
using Mvc4.Core.Models;
using Mvc4.Core.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Mvc4.Test
{
    [TestFixture]
    public class StoreServiceTest : BaseTest
    {
        [Test]
        public void RemoveEmployee()
        {
            var fn = "name" + DateTime.Now.Ticks;
            var e = new Employee()
            {
                FirstName=fn
                ,LastName=fn
            };
            var s = new Store()
            {
                Name = fn
            };
            _unitOfWork.BeginTransaction();
            _storeService.AddEmployees(s,e);
            _unitOfWork.Commit();

            Assert.IsNotNull(_storeService.GetById(s.Id));
            Assert.AreEqual(_storeService.GetById(s.Id).Staffs.First().FirstName,e.FirstName);

            _unitOfWork.BeginTransaction();
            var savedS=_storeService.GetById(s.Id);
            var eId= savedS.Staffs.First().Id;
            savedS.Staffs.Remove(savedS.Staffs.Where(x=>x.FirstName==e.FirstName).First());
            _storeService.Save(savedS);
            _unitOfWork.Commit();
            
            Assert.AreEqual(savedS.Staffs.Count,0);
            Assert.IsNotNull(_employeeService.GetById(eId));

            _unitOfWork.BeginTransaction();
            _storeService.Delete(savedS);
            _employeeService.Delete(_employeeService.GetById(eId));
            _unitOfWork.Commit();

        }

        [Test]
        public void AddProduct()
        {
            var fn="name"+DateTime.Now.Ticks;
            var p1 = new Product()
            {
                Name=fn
                ,Price=11
                ,CreatedBy=fn
            };

            var p2 = new Product()
            {
                Name="2"+fn
                ,Price=11
                ,CreatedBy=fn
            };

            var p3 = new Product()
            {
                Name = "3" + fn
                ,
                Price = 11
                ,
                CreatedBy = fn
            };


            var p4 = new Product()
            {
                Name = "4" + fn
                ,
                Price = 11
                ,
                CreatedBy = fn
            };

            var s = new Store()
            {
                Name = fn
                ,
                CreatedBy = fn
            };

            _unitOfWork.BeginTransaction();
            _storeService.AddProducts(s, new List<Product>() { p1,p2});
            _unitOfWork.Commit();

            Assert.AreEqual(s.Products.Count, 2);
            Assert.IsNotNull(s.Products.Contains(p1));
            Assert.IsNotNull(s.Products.Contains(p2));
            Assert.AreEqual(s.Products.Where(x => x.Name == p1.Name).Count(), 1);
            Assert.AreEqual(s.Products.Where(x => x.Name == p2.Name).Count(), 1);

            var savedS = _storeService.GetById(s.Id);

            _unitOfWork.BeginTransaction();
            _storeService.AddProducts(savedS, new List<Product>() { p3, p4 });
            _unitOfWork.Commit();

            Assert.AreEqual(savedS.Products.Count, 4);
            Assert.IsNotNull(savedS.Products.Contains(p3));
            Assert.IsNotNull(savedS.Products.Contains(p4));
            Assert.AreEqual(savedS.Products.Where(x => x.Name == p3.Name).Count(), 1);
            Assert.AreEqual(savedS.Products.Where(x => x.Name == p4.Name).Count(), 1);

            _unitOfWork.BeginTransaction();
            List<int> ids = savedS.Products.Select(x => x.Id).ToList<int>();
            int savedSId= savedS.Id;
            savedS.Products.Clear();
            _storeService.Delete(savedS);
            foreach (var id in ids)
            {
                _productService.DeleteById(id);
            }
            _unitOfWork.Commit();
            
            Assert.IsNull(_storeService.GetById(savedSId));
            foreach(var id in ids)
            {
                Assert.IsNull(_productService.GetById(id));
            }
        }
        
        [Test]
        public void Save()
        {
            string fname = "MinSoe" + DateTime.Now.Ticks;
            var storeToBeSaved = new Store() { Name = fname };
            Employee e = new Employee() { FirstName = fname, LastName = fname };
            PassportInfo pi = new PassportInfo() { PassportNo = fname, IssueDate = DateTime.Now, ExpiredDate = DateTime.Now };
            Product p1 = new Product() { Name = fname, Price = 10 };
            Product p2 = new Product() { Name = fname, Price = 20 };
            Wife w = new Wife() { FirstName = fname, LastName = fname };

            _unitOfWork.BeginTransaction();
            _employeeService.AddWife(e, w);
            _employeeService.AddPassportInfo(e, pi);
            _storeService.AddEmployees(storeToBeSaved, e);
            _storeService.AddProducts(storeToBeSaved, new List<Product>() { p1, p2 });
            _unitOfWork.Commit();

            _unitOfWork.BeginTransaction();
            Store anotherStore = new Store() { Name = "anotherStore" };
            Employee anotherEmployee = new Employee() { FirstName = "anotherFN", LastName = "anotherLN" };
            _storeService.AddEmployees(anotherStore, anotherEmployee);
            _storeService.Save(anotherStore);
            _unitOfWork.Commit();

            _unitOfWork.BeginTransaction();
            e.Store = anotherStore;
            _employeeService.Save(e);
            anotherEmployee.Store = storeToBeSaved;
            _employeeService.Save(anotherEmployee);
            _unitOfWork.Commit();

            var e1 = _employeeService.GetById(e.Id);
            var e2 = _employeeService.GetById(anotherEmployee.Id);

            Assert.True(e1.Store.Id == anotherStore.Id);
            Assert.True(e2.Store.Id == storeToBeSaved.Id);

            _unitOfWork.BeginTransaction();
            e.Store = storeToBeSaved;
            anotherEmployee.Store = anotherStore;
            _employeeService.Save(e);
            _employeeService.Save(anotherEmployee);
            _unitOfWork.Commit();

            var SavedStore = _storeService.GetById(storeToBeSaved.Id);

            Assert.IsTrue(SavedStore.Id > 0);
            Assert.IsTrue(SavedStore.Staffs.Count == 1);
            Assert.IsTrue(SavedStore.Staffs.First().Id > 0);

            Assert.IsTrue(SavedStore.Staffs.First().PassportInfo.EmployeeId > 0);
            Assert.IsTrue(SavedStore.Staffs.First().PassportInfo.Employee.Id == SavedStore.Staffs.First().Id);

            Assert.IsTrue(SavedStore.Staffs.First().Wifes.Count == 1);
            Assert.IsTrue(SavedStore.Staffs.First().Wifes.First().Id > 0);
            Assert.IsTrue(SavedStore.Staffs.First().Wifes.First().Employee.Id == SavedStore.Staffs.First().Id);

            Assert.IsTrue(SavedStore.Id == SavedStore.Staffs.First().Store.Id);

            Assert.IsTrue(SavedStore.Products.Count == 2);
            Assert.IsTrue(SavedStore.Products[0].Id > 0);
            Assert.IsTrue(SavedStore.Products[1].Id > 0);
            //Assert.IsTrue(SavedStore.Products[0].StoresStockedIn.Contains<Store>(SavedStore));
            //Assert.IsTrue(SavedStore.Products[1].StoresStockedIn.Contains<Store>(SavedStore));

            _unitOfWork.BeginTransaction();
            _employeeService.Delete(e);
            _employeeService.Delete(anotherEmployee);
            _storeService.Delete(storeToBeSaved);
            _storeService.Delete(anotherStore);
            _productService.Delete(p1);
            _productService.Delete(p2);
            _unitOfWork.Commit();
        }

    }
}
