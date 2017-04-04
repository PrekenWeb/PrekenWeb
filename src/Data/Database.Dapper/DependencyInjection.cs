using Data.Database.Dapper.Gateways;
using Data.Database.Dapper.Metadata;
using Ninject;

namespace Data.Database.Dapper
{
    internal static class DependencyInjection
    {
        public static void RegisterDependencies(IKernel kernel)
        {
            kernel.Bind<IDbConnectionFactory>().To<SqlConnectionFactory>().InSingletonScope();
            kernel.Bind<IPredicateFactory>().To<PredicateFactory>().InSingletonScope();

            kernel.Bind<ILanguagesGateway>().To<LanguagesGateway>().InSingletonScope();
            kernel.Bind<ISpeakersGateway>().To<SpeakersGateway>().InSingletonScope();
            kernel.Bind<ILecturesGateway>().To<LecturesGateway>().InSingletonScope();

            kernel.Bind<IFilterMetadataProvider>().To<LanguageFilterMetadataProvider>().InSingletonScope();
            kernel.Bind<IFilterMetadataProvider>().To<SpeakerFilterMetadataProvider>().InSingletonScope();
            kernel.Bind<IFilterMetadataProvider>().To<LectureFilterMetadataProvider>().InSingletonScope();

        }
    }
}
