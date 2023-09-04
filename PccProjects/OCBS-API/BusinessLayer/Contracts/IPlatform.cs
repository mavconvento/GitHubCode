using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IPlatform
    {
        Task<TReturn> GetAsync<TReturn>(string relativeUri, string token, string platformuserid);
        Task<TReturn> PostAsync<TReturn, TParam>(string relativeUri, string token, string platformuserid, TParam param);
    }
}
