using Fundamental.Application.Repositories;
using Fundamental.Application.Services.EntityServices.CategoryServices;
using Fundamental.Application.Services.EntityServices.SettingServices;
using Fundamental.Application.UnitOfWorks;
using Fundamental.Domain.Entities;
using Fundamental.Persistence.Contexts;
using Fundamental.Persistence.Repositories;
using Fundamental.Persistence.Services.ConfigurationServices;
using Fundamental.Persistence.Services.EntityServices.CategorySevices;
using Fundamental.Persistence.Services.EntityServices.SettingServices;
using Fundamental.Persistence.Services.PresentationServices;
using Fundamental.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayerServices(this IServiceCollection services)
        {
            services.AddDbContext<MainContext>(options =>
            {
                //options.UseMySQL(ServiceConfiguration.MySQL);
                options.UseSqlServer(ServiceConfiguration.MSSQL);
            });
            services.AddScoped<LayoutService>();


            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredUniqueChars = 0;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireDigit = true;
                opt.User.RequireUniqueEmail = false;
            }).AddEntityFrameworkStores<MainContext>().AddDefaultTokenProviders();

            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ILanguageRepository,LanguageRepository>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.AddScoped<ISliderRepository, SliderRepository>();
            services.AddScoped<IMenuSliderRepository, MenuSliderRepository>();
            services.AddScoped<IStorePalacedTypeRepository, StorePalacedTypeRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<ISosialMediaRepository, SosialMediaRepository>();

            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<ICategoryService, CategoryService>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
