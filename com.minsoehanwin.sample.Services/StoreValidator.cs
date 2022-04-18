using log4net;
using com.minsoehanwin.sample.Core.Models;
using com.minsoehanwin.sample.Services.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services
{
    public class StoreValidator : BaseValidator
    {
        public void Validate(Store store)
        {
            ValidateStoreExist(store);
            ValidateStoreName(store.Name);
        }

        private void ValidateStoreExist(Store store)
        {
            if (IsStoreNull(store))
            {

                throw new com.minsoehanwin.sample.Services.Exception.StoreIsEmptyOrNullException("Store is null.");
            }
        }

        private bool IsStoreNull(Store store)
        {
            return store == null;
        }


        private void ValidateStoreName(string storeName)
        {
            if (string.IsNullOrEmpty(storeName))
            {
                throw new com.minsoehanwin.sample.Services.Exception.StoreNameInvalidException("Store name is invalid.");
            }
        }

        public void ValidateCanRemoveEmployee(Store store, Employee employee)
        {
            if (!store.Staffs.Contains(employee))
            {
                throw new com.minsoehanwin.sample.Services.Exception.NoEmployeeFoundInStoreException("No employee found in store.");
            }
        }

        internal void ValidateCanAddProducts(Store store, IList<Product> products)
        {
            foreach (var p in products)
            {
                if (store.Products.Where(x => x.Id == p.Id && x.Id>0).Count() > 0)
                {
                    throw new com.minsoehanwin.sample.Services.Exception.ProductAlreadExistInStoreException("Product already exist in store.");
                }
            }
        }

        internal void ValidateCanAddEmployee(Store store, Employee employee)
        {
            if (store.Staffs.Where(x => x.Id == employee.Id && x.Id>0).Count() > 0)
            {
                throw new com.minsoehanwin.sample.Services.Exception.EmployeeAlreadyExistInStoreException("Employee already exist in store.");
            }
        }
    }
}