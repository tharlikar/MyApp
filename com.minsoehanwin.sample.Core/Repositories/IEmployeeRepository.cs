
using com.minsoehanwin.sample.Core.Models;

namespace com.minsoehanwin.sample.Core.Repositories
{
    public interface IEmployeeRepository:IRepositoryBase<Employee>
    {
        void Save(Employee employee);
    }
}
