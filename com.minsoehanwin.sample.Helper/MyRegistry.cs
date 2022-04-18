using com.minsoehanwin.sample.Core;
using com.minsoehanwin.sample.Core.Repositories;
using com.minsoehanwin.sample.Core.Services;
using Microsoft.AspNet.Identity;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Helper
{
    public class MyRegistry : Registry
    {
        public MyRegistry()
            : base()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });

            For<IEmployeeService>().Use<com.minsoehanwin.sample.Services.EmployeeService>();
            For<IProductService>().Use<com.minsoehanwin.sample.Services.ProductService>();
            For<IStoreService>().Use<com.minsoehanwin.sample.Services.StoreService>();
            For<IPassportInfoService>().Use<com.minsoehanwin.sample.Services.PassportInfoService>();
            For<IWifeService>().Use<com.minsoehanwin.sample.Services.WifeService>();
            For<IEmailService>().Use<com.minsoehanwin.sample.Services.EmailService>();
            For<IIdentityMessageService>().Use<com.minsoehanwin.sample.Services.EmailService>();
            //For<IEmailClient>().Use<com.minsoehanwin.sample.Services.GmailSmtpClient>();
            For<IEmailClient>().Use<com.minsoehanwin.sample.Services.MockSmtpClient>();

            /////EF6
            For<IUnitOfWork>().Use<com.minsoehanwin.sample.Repositories.EF.UnitOfWorkImpl>().Named("EF6");
            For<IEmployeeRepository>().Use<com.minsoehanwin.sample.Repositories.EF.EmployeeRepository>().Named("EF6");
            For<IProductRepository>().Use<com.minsoehanwin.sample.Repositories.EF.ProductRepository>().Named("EF6");
            For<IStoreRepository>().Use<com.minsoehanwin.sample.Repositories.EF.StoreRepository>().Named("EF6");
            For<IWifeRepository>().Use<com.minsoehanwin.sample.Repositories.EF.WifeRepository>().Named("EF6");
            For<IPassportInfoRepository>().Use<com.minsoehanwin.sample.Repositories.EF.PassportInfoRepository>().Named("EF6");
            For<IEmailRepository>().Use<com.minsoehanwin.sample.Repositories.EF.EmailRepository>().Named("EF6");

            ////Nhibernate4
            //string connectionString = ConfigurationManager.ConnectionStrings["MyDataContext"].ToString();
            //For<IUnitOfWork>().Use<com.minsoehanwin.sample.Repositories.EF2.Provider.UnitOfWorkImpl>().Named("NHibernate4")
            //    .Ctor<string>("connectionString").Is(connectionString);
            //For<IEmployeeRepository>().Use<com.minsoehanwin.sample.Repositories.EF2.EmployeeRepository>().Named("NHibernate4");
            //For<IProductRepository>().Use<com.minsoehanwin.sample.Repositories.EF2.ProductRepository>().Named("NHibernate4");
            //For<IStoreRepository>().Use<com.minsoehanwin.sample.Repositories.EF2.StoreRepository>().Named("NHibernate4");
            //For<IWifeRepository>().Use<com.minsoehanwin.sample.Repositories.EF2.WifeRepository>().Named("NHibernate4");
            //For<IPassportInfoRepository>().Use<com.minsoehanwin.sample.Repositories.EF2.PassportInfoRepository>().Named("NHibernate4");
            //For<IEmailRepository>().Use<com.minsoehanwin.sample.Repositories.EF2.EmailRepository>().Named("NHibernate4");
        }
    }
}