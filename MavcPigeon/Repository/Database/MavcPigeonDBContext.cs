using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using DomainObject;

namespace Repository.Database
{
    public class MavcPigeonDBContext : DbContext
    {
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public MavcPigeonDBContext(DbContextOptions<MavcPigeonDBContext> options) : base(options)
        {
            //_httpContextAccessor = ServiceProviderProxy.Provider.GetService<IHttpContextAccessor>();
        }

        public DbSet<User> User { get; set; }

    }
}
