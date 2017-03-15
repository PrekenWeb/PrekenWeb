using System.Web;
using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using Ninject;
using Ninject.Web.Common;
using PrekenWeb.Data;
using PrekenWeb.Data.Identity;
using PrekenWeb.Data.Mapping;
using PrekenWeb.Data.Mapping.Profiles;
using PrekenWeb.Data.Repositories;
using PrekenWeb.Data.Services;
using PrekenWeb.Data.Services.Interfaces;
using PrekenWeb.Security;

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
            kernel.Bind<IPrekenwebContext<Gebruiker>>().To<PrekenwebContext>().InRequestScope();
            kernel.Bind<PrekenwebContext>().ToSelf().WithConstructorArgument("proxyCreation", true);
            kernel.Bind<IHuidigeGebruiker>().To<HuidigeGebruiker>();
            kernel.Bind<IPrekenWebUserManager>().ToMethod(c => HttpContext.Current.GetOwinContext().GetUserManager<PrekenWebUserManager>()).InRequestScope();

            // Register mapping profiles
            kernel.Bind<Profile>().To<PreekDtoAutoMapperProfile>().InSingletonScope();
            kernel.Bind<Profile>().To<SermonModelAutoMapperProfile>().InSingletonScope();

            kernel.Bind<IPreekRepository>().To<PreekRepository>();
            kernel.Bind<IGebruikerRepository>().To<GebruikerRepository>();

            kernel.Bind<ISermonsService>().To<SermonsService>().InSingletonScope();
            kernel.Bind<ILanguagesService>().To<LanguagesService>().InSingletonScope();

            // Register AutoMapper (needs Profile registrations, hence registered last)
            kernel.Bind<MapperConfiguration>().To<IocInjectedMapperConfiguration>().InSingletonScope();
            kernel.Bind<IMapper>().ToMethod(context => context.Kernel.Get<MapperConfiguration>().CreateMapper()).InSingletonScope();
        }
    }
}
