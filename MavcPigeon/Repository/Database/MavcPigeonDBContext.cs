using DomainObject.AppServices;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<DefectSeverity>().HasKey(k => new { k.ClientDefectId, k.DefectClassificationId });
        }
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            //var auditEntries = OnBeforeSaveChanges();
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            //await OnAfterSaveChanges(auditEntries);
            return result;
        }

        public int SaveException()
        {
            var result = base.SaveChanges();
            return result;
        }

        public override int SaveChanges()
        {
            //var auditEntries = OnBeforeSaveChanges();
            var result = base.SaveChanges();
            //OnAfterSaveChanges(auditEntries);
            return result;
        }
    }
}
