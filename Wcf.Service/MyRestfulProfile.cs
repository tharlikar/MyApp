using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.minsoehanwin.sample.Wcf.Service
{
    public class MyRestfulProfile:Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Employee, com.minsoehanwin.sample.Core.Models.Employee>();
            Mapper.CreateMap<Wife, com.minsoehanwin.sample.Core.Models.Wife>();
            Mapper.CreateMap<PassportInfo, com.minsoehanwin.sample.Core.Models.PassportInfo>();
            Mapper.CreateMap<Product, com.minsoehanwin.sample.Core.Models.Product>();
            Mapper.CreateMap<Store, com.minsoehanwin.sample.Core.Models.Store>();
            Mapper.CreateMap<Car, com.minsoehanwin.sample.Core.Models.Car>();

            Mapper.CreateMap<com.minsoehanwin.sample.Core.Models.Employee, Employee>()
                .ForMember(dest => dest.Store, opts => opts.Ignore());
            Mapper.CreateMap<com.minsoehanwin.sample.Core.Models.Employee, Employee>()
                .ForMember(dest => dest.Car, opts => opts.Ignore());
            //WCF return error if there is un-ending references.
            //employee->wife->employee->wife....etc...
            Mapper.CreateMap<com.minsoehanwin.sample.Core.Models.Wife, Wife>()
                .ForMember(dest => dest.Employee, opts => opts.Ignore());

            //WCF return error if there is un-ending references.
            //employee->wife->employee->wife....etc...
            Mapper.CreateMap<com.minsoehanwin.sample.Core.Models.PassportInfo, PassportInfo>()
                .ForMember(dest => dest.Employee, opts => opts.Ignore());

            Mapper.CreateMap<com.minsoehanwin.sample.Core.Models.Product, Product>();
            Mapper.CreateMap<com.minsoehanwin.sample.Core.Models.Store, Store>();
        }
    }
}
