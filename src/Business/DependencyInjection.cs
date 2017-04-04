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

            kernel.Bind<ILanguagesService>().To<LanguagesService>().InSingletonScope();
            kernel.Bind<ISpeakersService>().To<SpeakersService>().InSingletonScope();
            kernel.Bind<ILecturesService>().To<LecturesService>().InSingletonScope();

            // AutoMapper mapping profiles
            kernel.Bind<Profile>().To<SermonModelAutoMapperProfile>().InSingletonScope();

            kernel.Bind<Profile>().To<LanguageDataToLanguageAutoMapperProfile>().InSingletonScope();
            kernel.Bind<Profile>().To<SpeakerDataToSpeakerAutoMapperProfile>().InSingletonScope();
            kernel.Bind<Profile>().To<LectureDataToLectureAutoMapperProfile>().InSingletonScope();

            // Data
            Data.DependencyInjection.RegisterDependencies(kernel);
        }
    }
}
