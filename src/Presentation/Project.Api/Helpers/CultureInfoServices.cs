using System;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Api.Helpers
{
    public class CultureInfoServices
    {
        public CultureInfoServices()
        {
        }

        public string GetCurrentThreadCulture()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }

    }
}
