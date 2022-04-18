using com.minsoehanwin.sample.Core.Models;
using Mvc4AppTestWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mvc4AppTestWebAPI.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.CreateMap<Employee, StaffCreateViewModel>()
                .ForMember(dest => dest.PassportNo,
                           opts => opts.MapFrom(src => src.PassportInfo.PassportNo))
                .ForMember(dest => dest.IssueDate,
                            opts => opts.MapFrom(src => src.PassportInfo.IssueDate))
                .ForMember(dest => dest.ExpiredDate,
                            opts => opts.MapFrom(src => src.PassportInfo.ExpiredDate))
                .ForMember(dest => dest.WifeFirstName,
                            opts => opts.MapFrom(src => src.Wifes.FirstOrDefault().FirstName))
                .ForMember(dest => dest.WifeLastName,
                            opts => opts.MapFrom(src => src.Wifes.FirstOrDefault().LastName));

            AutoMapper.Mapper.CreateMap<StaffCreateViewModel, Employee>()
                .ForMember(dest=>dest.Id,opts=>opts.Ignore());

            AutoMapper.Mapper.CreateMap<StaffCreateViewModel, PassportInfo>();

            AutoMapper.Mapper.CreateMap<StaffCreateViewModel, Wife>()
                .ForMember(dest=>dest.Id,opts=>opts.Ignore())
                .ForMember(dest => dest.FirstName,
                           opts => opts.MapFrom(src => src.WifeFirstName))
                .ForMember(dest => dest.LastName,
                           opts => opts.MapFrom(src => src.WifeLastName));
        }
    }
}
