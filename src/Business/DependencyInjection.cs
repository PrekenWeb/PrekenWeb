using AutoMapper;
using Business.Mapping.Profiles;
using Business.Services;
using Business.Services.Interfaces;
using Ninject;

namespace Business
{
    public static class DependencyInjection
    {
        public static void RegisterDependencies(IKernel kernel)
        {
            kernel.Bind<ISermonsService>().To<SermonsService>().InSingletonScope();

            kernel.Bind<IBooksService>().To<BooksService>().InSingletonScope();
            kernel.Bind<IImagesService>().To<ImagesService>().InSingletonScope();
            kernel.Bind<ILanguagesService>().To<LanguagesService>().InSingletonScope();
            kernel.Bind<ILecturesService>().To<LecturesService>().InSingletonScope();
            kernel.Bind<ILectureTypesService>().To<LectureTypesService>().InSingletonScope();
            kernel.Bind<ISpeakersService>().To<SpeakersService>().InSingletonScope();

            // AutoMapper mapping profiles
            kernel.Bind<Profile>().To<DataModelToBusinessModelAutoMapperProfile>().InSingletonScope();
            kernel.Bind<Profile>().To<BackwardsCompatibilityAutoMapperProfile>().InSingletonScope();

            // Data
            Data.DependencyInjection.RegisterDependencies(kernel);
        }
    }
}
