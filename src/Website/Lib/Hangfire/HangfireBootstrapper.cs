using System;
using System.Data.SqlClient;
using System.Web.Hosting;
using Hangfire;

namespace Prekenweb.Website.Lib.Hangfire
{ 
    public class HangfireBootstrapper : IRegisteredObject
    {
        private readonly BackgroundJobServerOptions _backgroundJobServerOptions = new BackgroundJobServerOptions
        {
            SchedulePollingInterval = TimeSpan.FromMinutes(1),
            WorkerCount = 2 // two concurrent workers is more than enough!
        };


        public static readonly HangfireBootstrapper Instance = new HangfireBootstrapper();

        private readonly object _lockObject = new object();
        private bool _started;

        private BackgroundJobServer _backgroundJobServer;

        public void Start(bool throwOnException)
        {
            lock (_lockObject)
            {
                if (_started) return;
                _started = true;

                HostingEnvironment.RegisterObject(this);
                try
                {
                    GlobalConfiguration.Configuration.UseSqlServerStorage("hangfire-sqlserver"); 
                }
                catch (SqlException)
                {
                    // probably wrong db-connection or non-existing db, let DbContext handle this
                    _started = false;
                    if (throwOnException) throw;
                    return;
                }

                _backgroundJobServer = new BackgroundJobServer(_backgroundJobServerOptions); 
            }
        }

        public void Stop()
        {
            lock (_lockObject)
            {
                _backgroundJobServer?.Dispose();

                HostingEnvironment.UnregisterObject(this);
            }
        }

        void IRegisteredObject.Stop(bool immediate)
        {
            Stop();
        }
    }
}