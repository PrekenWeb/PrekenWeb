using Data.Database.Dapper.Common.Data;
using Data.Database.Dapper.Common.Filtering;
using Data.Database.Dapper.Gateways;
using Data.Database.Dapper.Interfaces.Gateways;
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

            kernel.Bind<IBooksGateway>().To<BooksGateway>().InSingletonScope();
            kernel.Bind<IImagesGateway>().To<ImagesGateway>().InSingletonScope();
            kernel.Bind<ILanguagesGateway>().To<LanguagesGateway>().InSingletonScope();
            kernel.Bind<ILecturesGateway>().To<LecturesGateway>().InSingletonScope();
            kernel.Bind<ILectureTypesGateway>().To<LectureTypesGateway>().InSingletonScope();
            kernel.Bind<ISpeakersGateway>().To<SpeakersGateway>().InSingletonScope();

            kernel.Bind<IFilterMetadataProvider>().To<BookFilterMetadataProvider>().InSingletonScope();
            kernel.Bind<IFilterMetadataProvider>().To<ImageFilterMetadataProvider>().InSingletonScope();
            kernel.Bind<IFilterMetadataProvider>().To<LanguageFilterMetadataProvider>().InSingletonScope();
            kernel.Bind<IFilterMetadataProvider>().To<LectureFilterMetadataProvider>().InSingletonScope();
            kernel.Bind<IFilterMetadataProvider>().To<LectureTypeFilterMetadataProvider>().InSingletonScope();
            kernel.Bind<IFilterMetadataProvider>().To<SpeakerFilterMetadataProvider>().InSingletonScope();
        }
    }
}
