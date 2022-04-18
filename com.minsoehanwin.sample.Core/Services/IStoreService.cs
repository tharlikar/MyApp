using com.minsoehanwin.sample.Core.Models;
using System.Collections.Generic;
namespace com.minsoehanwin.sample.Core.Services
{
    public interface IStoreService:IServiceBase<Store>
    {
        void AddEmployees(Store store, Employee employee);
        void AddProducts(Store store, IList<Product> products);
        void RemoveEmployee(Store store, Employee employee);
        void Add(Store store, Employee employee, IList<Product> products);
    }
}
