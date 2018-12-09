using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrustMyCar.Utils
{
    public static class UtilMetods
    {
        public static IMapper GetMapper()
        {
            return new MapperConfiguration(cfg => cfg.AddProfile(new TrustMyCarMapperProfile())).CreateMapper();
        }
    }
}
