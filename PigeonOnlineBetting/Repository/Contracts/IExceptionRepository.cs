using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DomainObject;

namespace Repository.Contracts
{
    public interface IExceptionRepository
    {
        Task<ExceptionLog> UpsertException(Exception ex, string serviceName, string jsonObject, string userid);
    }
}
