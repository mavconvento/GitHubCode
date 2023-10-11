using System;
using System.Threading.Tasks;

namespace Subscription
{
    public class Subscriptions : ISubscriptions
    {
        public Boolean GetApplicationExpired(DateTime loginDate)
        {
            bool isExpired = false;
            DateTime expirationDate = new DateTime(2022,12,31);
            if (DateTime.Now > expirationDate && DateTime.Now > loginDate)
            {
                isExpired = true;
            }

            return isExpired;
        }
    }
}
