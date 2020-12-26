using DomainObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Contracts
{
    public interface IExceptionService
    {
        Task<ExceptionLog> UpsertException(Exception ex, string serviceName, string jsonObject);
    }
}
