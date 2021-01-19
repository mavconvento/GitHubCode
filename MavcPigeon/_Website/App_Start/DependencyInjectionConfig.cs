using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainObject.AppServices;
using BussinessLayer.Contracts;
using BussinessLayer;
using Repository.Contracts;
using Repository;
using Microsoft.Extensions.DependencyInjection;
using BussinessLayer.Helper;

namespace _Website.App_Start
{
    public class DependencyInjectionConfig
    {
        public static void AddScope(IServiceCollection services)
        {
            services.AddScoped<IAccount, AccountService>();
            services.AddScoped<IExceptionService, ExceptionService>();
            services.AddScoped<IFileDownloadUpload, FileDownloadUpload>();
            services.AddScoped<IImageService, ImageService>();


            //repository
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IExceptionRepository, ExceptionRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
        }
    }
}
