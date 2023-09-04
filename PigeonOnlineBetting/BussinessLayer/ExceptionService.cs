using BussinessLayer.Contracts;
using DomainObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Repository.Contracts;
using Repository;

namespace BussinessLayer
{
    public class ExceptionService : IExceptionService
    {
        private readonly IExceptionRepository _exceptionRepository;

        public ExceptionService(IExceptionRepository exceptionRepository)
        {
            _exceptionRepository = exceptionRepository ?? throw new ArgumentNullException(nameof(exceptionRepository));
        }
        public async Task<ExceptionLog> UpsertException(Exception ex, string serviceName, string jsonObject, string userid)
        {
            try
            {
                return await _exceptionRepository.UpsertException(ex, serviceName, jsonObject, userid);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
