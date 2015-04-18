using System.Data.SqlClient;
using System.Web.Hosting;
using Hangfire;
using Hangfire.SqlServer;

namespace Prekenweb.Website.Lib.Hangfire
{
    //public static class HangfireBootstrapper
    //{
    //    private static BackgroundJobServer _backgroundJobServer;
    //    private static bool _starting;
    //    private static bool _started;

    //    public static void Start()
    //    {
    //        if (_started || _starting) return;
    //        _starting = true; 

    //        JobStorage.Current = new SqlServerStorage("hangfire-sqlserver");

    //        _backgroundJobServer = new BackgroundJobServer();

    //        _backgroundJobServer.Start();

    //        _started = true; 
    //        _starting = false;
    //    }

    //    public static void Stop()
    //    {
    //        if (_backgroundJobServer != null) _backgroundJobServer.Stop();
    //    }
    //}
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

                _backgroundJobServer = new BackgroundJobServer();
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