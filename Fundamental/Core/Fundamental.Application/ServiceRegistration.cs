using AutoMapper;
using Fundamental.Application.Enums;
using Fundamental.Application.Profiles;
using Fundamental.Application.Storages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayerServices(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddHttpContextAccessor();
        }
    }
}
