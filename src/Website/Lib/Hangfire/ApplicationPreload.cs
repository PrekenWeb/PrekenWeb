using System.Web.Hosting;

namespace Prekenweb.Website.Lib.Hangfire
{
    public class ApplicationPreload : IProcessHostPreloadClient
    {
        public void Preload(string[] parameters)
        {
            HangfireBootstrapper.Instance.Start(false); // do not throw exceptions here, if exception occurres here, application pool crashes...
        }
    }
}