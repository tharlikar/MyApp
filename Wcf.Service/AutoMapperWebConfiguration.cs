using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.minsoehanwin.sample.Wcf.Service
{
    public static class AutoMapperWebConfiguration
    {
        public static void Configure(Profile profile)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(profile);
                //cfg.AddProfile(new PostProfile());
            });
        }
    }
}
