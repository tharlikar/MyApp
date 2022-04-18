using System;
using System.Collections.Generic;
using log4net;
using com.minsoehanwin.sample.Core.Models;
using com.minsoehanwin.sample.Core.Repositories;
using com.minsoehanwin.sample.Core.Services;
using com.minsoehanwin.sample.Core;
using System.Collections;

namespace com.minsoehanwin.sample.Services
{
    public class StoreService:BaseService,IStoreService
    {
        public IStoreRepository _storeRepository;
        private EmployeeValidator _employeeValidator;
        private StoreValidator _storeValidator;
        private IEmployeeRepository _employeeRepository;
        private IProductRepository _productRepository;
        private ProductValidator _productValidator;
        public StoreService(IStoreRepository storeRepository
            ,IProductRepository productRepository
            ,IEmployeeRepository employeeRepository
            ,EmployeeValidator employeeValidator
            ,StoreValidator storeValidator
            ,ProductValidator productValidator)
        {
            _storeRepository = storeRepository;
            _productRepository = productRepository;
            _employeeRepository = employeeRepository;
            _employeeValidator = employeeValidator;
            _storeValidator = storeValidator;
            _productValidator = productValidator;
        }

        public void Save(Store store)
        {
            _storeValidator.Validate(store);
            _storeRepository.AddOrUpdate(store);
        }

        public IList<Store> GetList()
        {
            IEnumerable<Store> storeList = _storeRepository.Get();
            return new List<Store>(storeList);
        }

        public void Delete(Store store)
        {
            _storeRepository.Delete(store);
        }

        public void DeleteById(int storeId)
        {
            var s=_storeRepository.GetByID(storeId);
            Delete(s);
        }

        public Store GetById(int id)
        {
            return _storeRepository.GetByID(id);
        }

        public void AddEmployees(Store store, Employee employee)
        {
            _storeValidator.Validate(store);
            _employeeValidator.Validate(employee);
            _storeValidator.ValidateCanAddEmployee(store, employee);
            store.Staffs.Add(employee);
            _storeRepository.AddOrUpdate(store);
        }


        public void AddProducts(Store store, IList<Product> products)
        {
            _storeValidator.Validate(store);
            _productValidator.Validate(products);
            foreach (var p in products)
            {
                _productValidator.ValidateCanAddStores(p, new List<Store>() { store });
                _storeValidator.ValidateCanAddProducts(store, new List<Product>() { p });
                store.Products.Add(p);
            }
            _storeRepository.AddOrUpdate(store);
        }

        public void RemoveEmployee(Store store, Employee employee)
        {
            _employeeValidator.Validate(employee);
            _storeValidator.Validate(store);
            _storeValidator.ValidateCanRemoveEmployee(store, employee);
            store.Staffs.Remove(employee);
            _storeRepository.AddOrUpdate(store);
        }


        public void Add(Store store, Employee employee, IList<Product> products)
        {
            _storeValidator.Validate(store);
            _employeeValidator.Validate(employee);
            _productValidator.Validate(products);
            _storeValidator.ValidateCanAddEmployee(store, employee);
            _storeValidator.ValidateCanAddProducts(store, products);
            store.Staffs.Add(employee);
            foreach (var p in products)
            {
                store.Products.Add(p);
            }
            _storeRepository.AddOrUpdate(store);
        }
    }
}