using System;
using System.Web.Hosting; 

namespace Prekenweb.Website.Hangfire
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