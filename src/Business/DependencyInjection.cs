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
            kernel.Bind<Profile>().To<SermonModelAutoMapperProfile>().InSingletonScope();

            kernel.Bind<Profile>().To<BookDataToBookAutoMapperProfile>().InSingletonScope();
            kernel.Bind<Profile>().To<ImageDataToImageAutoMapperProfile>().InSingletonScope();
            kernel.Bind<Profile>().To<LanguageDataToLanguageAutoMapperProfile>().InSingletonScope();
            kernel.Bind<Profile>().To<LectureDataToLectureAutoMapperProfile>().InSingletonScope();
            kernel.Bind<Profile>().To<LectureTypeDataToLectureTypeAutoMapperProfile>().InSingletonScope();
            kernel.Bind<Profile>().To<SpeakerDataToSpeakerAutoMapperProfile>().InSingletonScope();

            // Data
            Data.DependencyInjection.RegisterDependencies(kernel);
        }
    }
}
