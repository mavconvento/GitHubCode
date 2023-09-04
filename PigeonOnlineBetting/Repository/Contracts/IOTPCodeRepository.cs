using DomainObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Contracts
{
    public interface IOTPCodeRepository
    {
        Task<string> ValidateOTPCode(LinkMobile linkMobile);
        Task<string> GetOtpCode(LinkMobile linkMobile);
    }
}
