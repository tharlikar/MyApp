using com.minsoehanwin.sample.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Services
{
    public class ProductValidator
    {
        internal void Validate(IList<com.minsoehanwin.sample.Core.Models.Product> products)
        {
            foreach (var p in products)
            {
                Validate(p);
            }
        }

        private void Validate(com.minsoehanwin.sample.Core.Models.Product p)
        {
            ValidateProudctExist(p);
            ValidateProductName(p.Name);
        }

        private void ValidateProductName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new com.minsoehanwin.sample.Services.Exception.ProductNameIsInvalidException("Product name is invalid.");
            }
        }

        private void ValidateProudctExist(com.minsoehanwin.sample.Core.Models.Product p)
        {
            if (IsNull(p))
            {
                throw new com.minsoehanwin.sample.Services.Exception.ProductIsNullException("Product is null.");
            }
        }

        private bool IsNull(com.minsoehanwin.sample.Core.Models.Product p)
        {
            return p == null;
        }

        
        internal void ValidateCanAddStore(com.minsoehanwin.sample.Core.Models.Product p, com.minsoehanwin.sample.Core.Models.Store store)
        {
            if (p.StoresStockedIn.Where(x => x.Id == store.Id && x.Id > 0).Count() > 0)
            {
                throw new com.minsoehanwin.sample.Services.Exception.ProductAlreadExistInStoreException("Product already exist in store.");
            }
        }

        public void ValidateCanAddStores(Product product, IList<com.minsoehanwin.sample.Core.Models.Store> stores)
        {
            foreach (var store in stores)
            {
                if (product.StoresStockedIn.Where(x => x.Id == store.Id && x.Id>0).Count() > 0)
                {
                    throw new com.minsoehanwin.sample.Services.Exception.ProductAlreadExistInStoreException("Product already exist in store");
                }
            }
        }
    }
}
