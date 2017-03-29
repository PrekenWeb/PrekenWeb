using Ninject;
using PrekenWeb.Data.Database.Dapper.Metadata;
using PrekenWeb.Data.DataModels;
using PrekenWeb.Data.Gateways;

namespace PrekenWeb.Data.Database.Dapper
{
    internal static class DependencyInjection
    {
        public static void RegisterDependencies(IKernel kernel)
        {
            kernel.Bind<IDbConnectionFactory>().To<SqlConnectionFactory>().InSingletonScope();
            kernel.Bind<IPredicateFactory>().To<PredicateFactory>().InSingletonScope();

            kernel.Bind<ISpeakersGateway>().To<SpeakersGateway>().InSingletonScope();

            kernel.Bind<IFilterMetadataProvider>().To<SpeakerFilterMetadataProvider>().InSingletonScope();

        }
    }
}
