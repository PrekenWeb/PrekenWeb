using System.Data.SqlClient;
using System.Web.Hosting;
using Hangfire;
using Hangfire.SqlServer;

namespace Prekenweb.Website.Lib.Hangfire
{ 
    public class HangfireBootstrapper : IRegisteredObject
    {
        public static readonly HangfireBootstrapper Instance = new HangfireBootstrapper();

        private readonly object _lockObject = new object();
        private bool _started;

        private BackgroundJobServer _backgroundJobServer;

        public void Start()
        {
            lock (_lockObject)
            {
                if (_started) return;
                _started = true;

                HostingEnvironment.RegisterObject(this);
                try
                {
                    JobStorage.Current = new SqlServerStorage("hangfire-sqlserver");
                }
                catch (SqlException)
                {
                    // probably wrong db-connection or non-existing db, let DbContext handle this
                    _started = false;
                    return;
                }

                _backgroundJobServer = new BackgroundJobServer(OwinStartup.BackgroundJobServerOptions);
                _backgroundJobServer.Start();
            }
        }

        public void Stop()
        {
            lock (_lockObject)
            {
                if (_backgroundJobServer != null)
                {
                    _backgroundJobServer.Dispose();
                }

                HostingEnvironment.UnregisterObject(this);
            }
        }

        void IRegisteredObject.Stop(bool immediate)
        {
            Stop();
        }
    }
}