using System.Web;
using AutoMapper;
using Business.Mapping;
using Data.Mapping;
using Data.Repositories;
using Microsoft.AspNet.Identity.Owin;
using Ninject;
using Ninject.Web.Common;
using PrekenWeb.Security;
using WebAPI.Interfaces;
using WebAPI.Mapping.Profiles;
using WebAPI.Repositories;

namespace WebAPI
{
    public static class NinjectWebCommon
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            // Business
            Business.DependencyInjection.RegisterDependencies(kernel);

            // Security
            kernel.Bind<IHuidigeGebruiker>().To<HuidigeGebruiker>();
            kernel.Bind<IPrekenWebUserManager>().ToMethod(c => HttpContext.Current.GetOwinContext().GetUserManager<PrekenWebUserManager>()).InRequestScope();

            // API
            kernel.Bind<Profile>().To<SermonApiModelAutoMapperProfile>().InSingletonScope();

            kernel.Bind<IBooksRepository>().To<BooksRepository>();
            kernel.Bind<IImagesRepository>().To<ImagesRepository>();
            kernel.Bind<ILanguagesRepository>().To<LanguagesRepository>();
            kernel.Bind<ILecturesRepository>().To<LecturesRepository>();
            kernel.Bind<ILectureTypesRepository>().To<LectureTypesRepository>();
            kernel.Bind<ISpeakersRepository>().To<SpeakersRepository>();

            kernel.Bind<IPreekRepository>().To<PreekRepository>();
            kernel.Bind<IGebruikerRepository>().To<GebruikerRepository>();

            // Register AutoMapper (needs Profile registrations, hence registered last)
            kernel.Bind<MapperConfiguration>().To<IocInjectedMapperConfiguration>().InSingletonScope();
            kernel.Bind<IMapper>().ToMethod(context => context.Kernel.Get<MapperConfiguration>().CreateMapper()).InSingletonScope();
        }
    }
}
