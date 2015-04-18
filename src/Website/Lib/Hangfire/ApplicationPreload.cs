using System;
using System.Web.Hosting;

namespace Prekenweb.Website.Lib.Hangfire
{
    public class ApplicationPreload : IProcessHostPreloadClient
    {
        public void Preload(string[] parameters)
        {
            // catch all, if exception occurres here, application pool crashes...
            try
            { 
                HangfireBootstrapper.Instance.Start();
            }
            catch (Exception)
            {

            }
        }
    }
}