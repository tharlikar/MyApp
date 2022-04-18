using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
namespace com.minsoehanwin.sample.Wcf.Service
{
    [ServiceContract]
    public interface IEmployeeRestfulService
    {
        [OperationContract]
        //[WebGet("GET",UriTemplate = "xml/{id}", ResponseFormat = WebMessageFormat.Xml)]
        [WebInvoke(Method = "GET"
            , UriTemplate = "xml/{id}"
            , RequestFormat=WebMessageFormat.Json
            , ResponseFormat = WebMessageFormat.Json
            , BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string XMLData(string id);

        [OperationContract]
        [WebInvoke(Method = "GET"
            , UriTemplate = "employee/{id}"
            , ResponseFormat = WebMessageFormat.Json
            , RequestFormat = WebMessageFormat.Json
            , BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Employee GetById(string id);

        [OperationContract]
        [WebInvoke(Method = "POST"
            , UriTemplate = "employee/addwife"
            , ResponseFormat = WebMessageFormat.Json
            , RequestFormat = WebMessageFormat.Json
            , BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Employee AddWife(Employee employee, Wife wife);

        [OperationContract]
        [WebInvoke(Method = "POST"
            , UriTemplate = "employee/addpassportinfo"
            , ResponseFormat = WebMessageFormat.Json
            , RequestFormat = WebMessageFormat.Json
            , BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Employee AddPassportInfo(Employee employee, PassportInfo passportInfo);

        //user post to store new item
        //http://restcookbook.com/HTTP%20Methods/put-vs-post/
        [OperationContract]
        [WebInvoke(Method = "POST"
            , UriTemplate = "employee/"
            , RequestFormat = WebMessageFormat.Json
            , ResponseFormat = WebMessageFormat.Json
            , BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Employee Save(Employee employee);

        //use put to update
        //http://restcookbook.com/HTTP%20Methods/put-vs-post/
        [OperationContract]
        [WebInvoke(Method = "PUT"
            , UriTemplate = "employee/{id}"
            , ResponseFormat = WebMessageFormat.Json
            , RequestFormat = WebMessageFormat.Json
            , BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Employee PUTSave(string id, Employee employee);

        [OperationContract]
        [WebInvoke(Method = "GET"
            , UriTemplate = "employee/"
            , ResponseFormat = WebMessageFormat.Json
            , RequestFormat = WebMessageFormat.Json
            , BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        IList<Employee> GetList();

        [OperationContract]
        [WebInvoke(Method = "POST"
            , UriTemplate = "employee/delete"
            , ResponseFormat = WebMessageFormat.Json
            , RequestFormat = WebMessageFormat.Json
            , BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void Delete(Employee employee);

        [OperationContract]
        [WebInvoke(Method = "DELETE"
            , UriTemplate = "employee/{id}"
            , ResponseFormat = WebMessageFormat.Json
            , RequestFormat = WebMessageFormat.Json
            , BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        void DeleteById(string id);
    }
}