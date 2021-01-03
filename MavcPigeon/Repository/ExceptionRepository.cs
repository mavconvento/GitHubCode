using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DomainObject;
using Repository.Contracts;
using Repository.Database;

namespace Repository
{
    public class ExceptionRepository : IExceptionRepository
    {
        private readonly MavcPigeonDBContext _dbcontext;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public ExceptionRepository(MavcPigeonDBContext dbcontext)
        {
            _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            //_httpContextAccessor = ServiceProviderProxy.Provider.GetService<IHttpContextAccessor>();
        }
        public async Task<ExceptionLog> UpsertException(Exception ex, string serviceName, string jsonObject, string userid)
        {
            string upn = string.Empty;

            try
            {
                var user = userid;

                ExceptionLog exception = new ExceptionLog
                {
                    Id = 0,
                    Name = ex.Message,
                    InnerException = ex.InnerException != null ? ex.InnerException.ToString() : "",
                    ServiceName = serviceName,
                    TimeStamp = DateTime.UtcNow,
                    JsonObject = jsonObject,
                    UserId = user
                };

                using (var transaction = await _dbcontext.Database.BeginTransactionAsync())
                {
                    _dbcontext.ExceptionLogs.UpdateRange(exception);
                    _dbcontext.SaveException();
                    transaction.Commit();
                }

                return exception;
            }
            catch (Exception e)
            {

                ExceptionLog exception = new ExceptionLog
                {
                    Id = 0,
                    Name = e.Message,
                    InnerException = e.InnerException != null ? e.InnerException.ToString() : "",
                    ServiceName = "Exception Service",
                    TimeStamp = DateTime.UtcNow,
                    JsonObject = jsonObject,
                    UserId = upn
                };

                return exception;
            }

        }
    }
}
