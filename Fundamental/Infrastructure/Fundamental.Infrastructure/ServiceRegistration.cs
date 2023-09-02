using Fundamental.Application.Enums;
using Fundamental.Application.Helper.FileCheckers;
using Fundamental.Application.Storages;
using Fundamental.Application.Storages.CloudinaryStorages;
using Fundamental.Infrastructure.Helper.FileCheckers;
using Fundamental.Infrastructure.Services.StorageServices.CloudinaryStorageServices;
using Fundamental.Infrastructure.Services.StorageServices.LocalStorageServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Infrastructure
{
    public static class ServiceRegistration
    {

        public static void AddInfrastructureLayerServices(this IServiceCollection services)
        {
            services.AddSingleton<IFileChecker, FileChecker>();
        }

        public static void AddInfrastructureLayerServices(this IServiceCollection services, StorageEnum storageEnum)
        {
            switch (storageEnum)
            {
                case StorageEnum.LocalStorage:
                    services.AddSingleton<IStorage, LocalStorage>();
                    break;
                case StorageEnum.CloudinaryStorage:
                    services.AddSingleton<IStorage, CloudinaryStorage>();
                    break;
                default:
                    services.AddSingleton<IStorage, LocalStorage>();
                    break;
            }
        }

    }
}
