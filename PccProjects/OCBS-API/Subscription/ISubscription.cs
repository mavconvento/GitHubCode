using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscription
{
    public interface ISubscriptions
    {
        Boolean GetApplicationExpired(DateTime loginDate);
    }
}
