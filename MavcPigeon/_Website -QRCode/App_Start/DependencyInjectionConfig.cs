using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainObject.AppServices;
using BussinessLayer.Contracts;
using BussinessLayer;
using Repository.Contracts;
using Repository.Helper;
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
            services.AddScoped<IRaceService, RaceService>();
            services.AddScoped<IMemberService, MemberService>();

            //repository
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IExceptionRepository, ExceptionRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IOTPCodeRepository, OtpRepository>();
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<IRaceRepository, RaceRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
        }
    }
}
