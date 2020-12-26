using BussinessLayer.Contracts;
using DomainObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class ExceptionService : IExceptionService
    {
        public Task<ExceptionLog> UpsertException(Exception ex, string serviceName, string jsonObject)
        {
            throw new NotImplementedException();
        }
    }
}
