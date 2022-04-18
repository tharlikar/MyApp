using com.minsoehanwin.sample.Core.Models;

namespace com.minsoehanwin.sample.Core.Services
{
    public interface IEmployeeService : IServiceBase<Employee>
    {
        void AddWife(Employee employee, Wife wife);

        void AddPassportInfo(Employee employee, PassportInfo passportInfo);
    }
}
