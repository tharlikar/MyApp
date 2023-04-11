using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using com.minsoehanwin.sample.Test;
using com.minsoehanwin.sample.Test.EmployeeService;
using System.ServiceModel.Description;
namespace com.minsoehanwin.sample.Test
{
    [TestFixture]
    public class EmployeeServiceWCFTest
    {
        //[Test]
        //public void RestfulGetTest()
        //{
        //    //http://dotnetmentors.com/wcf/wcf-rest-service-to-get-or-post-json-data-and-retrieve-json-data-with-datacontract.aspx
        //    var client = GetEmployeeServiceProxy();
        //    string id = client.GetList().First().Id.ToString();
        //    string url = string.Format("http://localhost:3931/EmployeeRestfulService.svc/employee/{0}", id);
        //     WebClient proxy = new WebClient();
        //     string serviceURL = url;
        //    byte[] data = proxy.DownloadData(serviceURL);
            
        //    // Obtain the WebHeaderCollection instance containing the header name/value pair from the response.
        //    WebHeaderCollection myWebHeaderCollection = proxy.ResponseHeaders;
        //    // Loop through the ResponseHeaders and display the header name/value pairs.
        //    string str=string.Empty;
        //    for (int i = 0; i < myWebHeaderCollection.Count; i++)
        //        str+= myWebHeaderCollection.GetKey(i) + " = " + myWebHeaderCollection.Get(i)+"|";

        //    Stream stream = new MemoryStream(data);
        //    string strout = StreamToString(stream);
        //    stream.Close();
        //    Assert.IsNotNull(strout);
        //    Assert.Greater(strout.Length, 0);
        //    Assert.True(strout.Contains("FirstName"));
        //    Assert.True(strout.Contains("LastName"));


        //}

        //public static Stream StringToStream(string src)
        //{
        //    byte[] byteArray = Encoding.UTF8.GetBytes(src);
        //    return new MemoryStream(byteArray);
        //}

        //public static string StreamToString(Stream stream)
        //{
        //    stream.Position = 0;
        //    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
        //    {
        //        return reader.ReadToEnd();
        //    }
        //}

        
        //[Test]
        //public void RestfulPostTest()
        //{
        //    var client = GetEmployeeServiceProxy();
        //    string id="100";
        //    string url = string.Format("http://localhost:3931/RestfulService.svc/json/{0}", id);
        //    var data = "{employee:{\"FirstName\":\"MinSoe\",\"LastName\":\"Win\"}}";//"<employee><FirstName>Min Soe</FirstName><LastName>Win</LastName></employee>";//
        //     byte[] byteArray = Encoding.UTF8.GetBytes (data);
        //     HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:3931/EmployeeRestfulService.svc/employee");
        //    request.Method = "POST";
        //    request.Accept = "application/json; charset=utf-8";
        //    request.ContentType = "application/json; charset=utf-8";//"text/xml; charset=utf-8";//
        //    request.ContentLength = data.Length;
        //    Stream dataStream = request.GetRequestStream();
        //    dataStream.Write(byteArray, 0, byteArray.Length);
        //    dataStream.Close();
        //    WebResponse response = request.GetResponse();
        //    dataStream = response.GetResponseStream();
        //    StreamReader reader = new StreamReader(dataStream);
        //    string responseFromServer = reader.ReadToEnd();
        //    dataStream.Close();
        //    reader.Close();
        //    response.Close();

        //    Assert.IsNotNull(responseFromServer);
        //    Assert.IsNotEmpty(responseFromServer);
        //    Assert.Greater(responseFromServer.Length, 0);
        //    Assert.IsTrue(responseFromServer.Contains(id));
        //}

        //string JsonData(string id);
        //[Test]
        //public void JsonData()
        //{
        //    //var client = GetEmployeeServiceProxy();
        //    //string id = "100";
        //    //string data = client.JsonData(id);
        //    //Assert.IsNotNull(data);
        //    //Assert.IsNotEmpty(data);
        //    //Assert.Greater(data.Length, 0);
        //    //Assert.IsTrue(data.Contains(id));
        //}

 
        [Test]
        [Order(4)]
        public void RestfulXml()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            (se, cert, chain, sslerror) =>
            {
                return true;
            };
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(@"https://localhost:44302/WcfRestfulService/EmployeeRestfulService.svc/xml/1");

            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create(@"https://Dell-PC/WcfRestfulService/EmployeeRestfulService.svc/xml/1");
            //Add a header to the request that contains our credentials
            //DO NOT HARDCODE IN PRODUCTION!! Pull credentials real-time from database or other store.
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("admin@example.com" + ":" + "Admin@123456"));
            req.Headers.Add("Authorization", "Basic " + svcCredentials);
            //Just some example code to parse the JSON response using the JavaScriptSerializer
            using (HttpWebResponse svcResponse = (HttpWebResponse)req.GetResponse())
            {
                using (StreamReader sr = new StreamReader(svcResponse.GetResponseStream()))
                {
                    string jsonTxt = sr.ReadToEnd();
                }
                Assert.AreEqual(svcResponse.StatusCode, HttpStatusCode.OK);
            }
        }   
        [Test]
        [Order(3)]
        public void zGetList()
        {
            var client = GetEmployeeServiceProxy();
            
            var fname = "MinSoe" + DateTime.Now.Ticks;
            var e = new com.minsoehanwin.sample.Test.EmployeeService.Employee()
            {
                FirstName = fname,
                LastName = fname
            };
            var employeeList = client.GetList();
            var eCount = employeeList.Count();
            client.Save(e);
            employeeList = client.GetList();
            
            Assert.IsNotNull(employeeList.Where(x => x.FirstName == fname).First());
            
            com.minsoehanwin.sample.Test.EmployeeService.Employee savedEmployee = employeeList.Where(x => x.FirstName == e.FirstName).Select(x=>x).First<com.minsoehanwin.sample.Test.EmployeeService.Employee>();
            
            Assert.IsNotNull(savedEmployee);
            Assert.AreEqual(savedEmployee.FirstName, e.FirstName);
            Assert.AreEqual(savedEmployee.LastName, e.LastName);
            
            client.Delete(savedEmployee);

            Assert.AreEqual(eCount, client.GetList().Count());

            client.Close();
        }

        private static com.minsoehanwin.sample.Test.EmployeeService.EmployeeServiceClient GetEmployeeServiceProxy()
        {

            ////http://stackoverflow.com/questions/1742938/how-to-solve-could-not-establish-trust-relationship-for-the-ssl-tls-secure-chan
            //System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            //(se, cert, chain, sslerror) =>
            //{
            //    return true;
            //};

            var client = new com.minsoehanwin.sample.Test.EmployeeService.EmployeeServiceClient("BasicHttpBinding_IEmployeeService");
            // #Method 1
            client.ClientCredentials.UserName.UserName = "admin@example.com";
            client.ClientCredentials.UserName.Password = "Admin@123456";
            // #Method 2
            //ClientCredentials loginCredentials = new ClientCredentials();
            //loginCredentials.UserName.UserName = "test";
            //loginCredentials.UserName.Password = "abc";
            //var factory = client.ChannelFactory;
            //var defaultCredentials = factory.Endpoint.Behaviors.Find<ClientCredentials>();
            //factory.Endpoint.Behaviors.Remove(defaultCredentials); //remove default ones
            //factory.Endpoint.Behaviors.Add(loginCredentials); //add required ones
            return client;
        }

        [Test]
        [Order(2)]
        public void zAddWife()
        {
            var client = GetEmployeeServiceProxy();
            
            var fname="MinSoe"+DateTime.Now.Ticks;
            var e = new com.minsoehanwin.sample.Test.EmployeeService.Employee()
            {
                FirstName=fname,
                LastName=fname
            };
            var w = new com.minsoehanwin.sample.Test.EmployeeService.Wife()
            {
                FirstName = fname
                ,
                LastName = fname
            };
            client.AddWife(e,w);
            var list=client.GetList();
            var ee=list.Where(x => x.FirstName == fname).First();
            var ww=ee.Wifes.First();
            Assert.AreEqual(ww.FirstName, w.FirstName);
            
            var savedE=client.GetList().Where(x => x.FirstName == fname).FirstOrDefault();
            
            Assert.IsNotNull(savedE);
            Assert.IsNotNull(savedE.Wifes.First());
            Assert.AreEqual(savedE.Wifes.Count(),1);

            client.Delete(savedE);

            client.Close();
        }

        [Test]
        [Order(1)]
        public void zAddPassportInfo()
        {
            var fname = "MinSoe" + DateTime.Now.Ticks;
            var e = new com.minsoehanwin.sample.Test.EmployeeService.Employee()
            {
                FirstName = fname,
                LastName = fname
                
            };
            var pi = new com.minsoehanwin.sample.Test.EmployeeService.PassportInfo()
            {
                PassportNo = fname
                ,
                IssueDate = DateTime.Now//http://stackoverflow.com/questions/1331779/conversion-of-a-datetime2-data-type-to-a-datetime-data-type-results-out-of-range
                ,
                ExpiredDate=DateTime.Now
            };
            var client = GetEmployeeServiceProxy();
            client.AddPassportInfo(e,pi);
            var savedList = client.GetList();
            var savedE=savedList.Where(x => x.FirstName == fname).FirstOrDefault();
            Assert.IsNotNull(savedE);
            Assert.IsNotNull(savedE.PassportInfo);
            Assert.AreEqual(savedE.PassportInfo.PassportNo,fname);

            client.Delete(savedE);

            client.Close();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public com.minsoehanwin.sample.Core.Models.Employee GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}