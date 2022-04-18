using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
namespace com.minsoehanwin.sample.Wcf.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        void AddWife(Employee employee, Wife wife);

        [OperationContract]
        void AddPassportInfo(Employee employee, PassportInfo passportInfo);

        [OperationContract]
        void Save(Employee obj);

        [OperationContract]
        IList<Employee> GetList();

        [OperationContract]
        void Delete(Employee obj);

        [OperationContract]
        void DeleteById(int id);

        [OperationContract]
        Employee GetById(int id);
    }
}
