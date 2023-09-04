using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public static class ServiceProviderProxy
    {
        public static IServiceProvider Provider { get; private set; }
        public static void Attach(IServiceProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            Provider = provider;
        }
    }
}
