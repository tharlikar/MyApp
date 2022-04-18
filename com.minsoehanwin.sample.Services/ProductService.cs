using System;
using System.Collections.Generic;
using log4net;
using com.minsoehanwin.sample.Core.Models;
using com.minsoehanwin.sample.Core.Repositories;
using com.minsoehanwin.sample.Core.Services;
using com.minsoehanwin.sample.Core;

namespace com.minsoehanwin.sample.Services
{
    public class ProductService:BaseService, IProductService
    {
        private IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Save(Product obj)
        {
            _productRepository.AddOrUpdate(obj);
        }

        public IList<Product> GetList()
        {
            return new List<Product>(_productRepository.Get());
        }

        public void Delete(Product obj)
        {
            _productRepository.Delete(obj);
        }

        public void DeleteById(int id)
        {
            var p = _productRepository.GetByID(id);
            Delete(p);
        }

        public Product GetById(int id)
        {
            return _productRepository.GetByID(id);
        }
    }
}
