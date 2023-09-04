using Microsoft.Extensions.DependencyInjection;
using BusinessLayer.Contracts;
using BusinessLayer;
using Repository.Contracts;
using Repository;
using BusinessLayer.ApiHelper;

namespace OCBS_API.App_Start
{
    public class DependencyInjectionConfig
    {
        public static void AddScope(IServiceCollection services)
        {
            //business layer
            services.AddScoped<IBettings, Bettings>();
            services.AddScoped<IEvents, Events>();
            services.AddScoped<IUsers, Users>();
            services.AddScoped<IPlatform, Platform>();
            services.AddScoped<IUsers, Users>();
            //services.AddScoped<IMemberService, MemberService>();

            //repository
            services.AddScoped<IDatabaseConnection, DatabaseConnection>();
            services.AddScoped<IBettingRepository, BettingRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            //services.AddScoped<IOTPCodeRepository, OtpRepository>();
            //services.AddScoped<ICommonRepository, CommonRepository>();
            //services.AddScoped<IRaceRepository, RaceRepository>();
            //services.AddScoped<IMemberRepository, MemberRepository>();
        }
    }
}
