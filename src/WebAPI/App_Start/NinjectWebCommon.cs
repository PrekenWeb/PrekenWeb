using System.Web;
using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using Ninject;
using Ninject.Web.Common;
using PrekenWeb.Data;
using PrekenWeb.Data.Gateways;
using PrekenWeb.Data.Identity;
using PrekenWeb.Data.Mapping;
using PrekenWeb.Data.Mapping.Profiles;
using PrekenWeb.Data.Repositories;
using PrekenWeb.Data.Services;
using PrekenWeb.Data.Services.Interfaces;
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
            // Data
            DependencyInjection.RegisterDependencies(kernel);

            // Security
            kernel.Bind<IHuidigeGebruiker>().To<HuidigeGebruiker>();
            kernel.Bind<IPrekenWebUserManager>().ToMethod(c => HttpContext.Current.GetOwinContext().GetUserManager<PrekenWebUserManager>()).InRequestScope();

            // API
            kernel.Bind<Profile>().To<SermonApiModelAutoMapperProfile>().InSingletonScope();

            kernel.Bind<ISpeakersRepository>().To<SpeakersRepository>();

            kernel.Bind<IPreekRepository>().To<PreekRepository>();
            kernel.Bind<IGebruikerRepository>().To<GebruikerRepository>();

            // Register AutoMapper (needs Profile registrations, hence registered last)
            kernel.Bind<MapperConfiguration>().To<IocInjectedMapperConfiguration>().InSingletonScope();
            kernel.Bind<IMapper>().ToMethod(context => context.Kernel.Get<MapperConfiguration>().CreateMapper()).InSingletonScope();
        }
    }
}
