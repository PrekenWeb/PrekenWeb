using System.Configuration;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Identity;
using Data.Repositories;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Mvc.FilterBindingSyntax;
using PrekenWeb.Security;
using Microsoft.AspNet.Identity.Owin;
using Prekenweb.Website.Lib.Cache;
using Prekenweb.Website.Lib.Identity;
using Prekenweb.Website.Mapping.Profiles;
using AutoMapper;
using Business.Mapping;

namespace Prekenweb.Website
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
            kernel.Bind<IPrekenwebContext<Gebruiker>>().To<PrekenwebContext>();//.InRequestScope();
            kernel.Bind<IHuidigeGebruiker>().To<HuidigeGebruiker>().InRequestScope();

            //kernel
            //    .BindFilter<AddTokenCookieFilter>(FilterScope.Controller, 0)
            //    .WhenControllerHas<AddTokenCookieAttribute>()
            //    .WithConstructorArgument("audienceId", ConfigurationManager.AppSettings["AudienceId"])
            //    .WithConstructorArgument("audienceSecret", ConfigurationManager.AppSettings["AudienceSecret"]);

            kernel
                .BindFilter<AddTokenCookieFilter>(FilterScope.Action, 0)
                .WhenActionMethodHas<AddTokenCookieAttribute>()
                .WithConstructorArgument("audienceId", ConfigurationManager.AppSettings["AudienceId"])
                .WithConstructorArgument("audienceSecret", ConfigurationManager.AppSettings["AudienceSecret"]);

            kernel.Bind<IGebruikerRepository>().To<GebruikerRepository>();
            kernel.Bind<IMailingRepository>().To<MailingRepository>();
            kernel.Bind<ITekstRepository>().To<TekstRepository>();
            kernel.Bind<IPrekenRepository>().To<PrekenRepository>();
            kernel.Bind<ISpotlightRepository>().To<SpotlightRepository>();
            kernel.Bind<IZoekenRepository>().To<ZoekenRepository>();
            kernel.Bind<IPrekenwebCache>().To<PrekenwebHttpCache>().InRequestScope();
            kernel.Bind<IPrekenWebUserManager>().ToMethod(c => HttpContext.Current.GetOwinContext().GetUserManager<PrekenWebUserManager>()).InRequestScope();

            // AutoMapper mapping profiles
            kernel.Bind<Profile>().To<WebsiteAutoMapperProfile>().InSingletonScope();

            // Register AutoMapper (needs Profile registrations, hence registered last)
            kernel.Bind<MapperConfiguration>().To<IocInjectedMapperConfiguration>().InSingletonScope();
            kernel.Bind<IMapper>().ToMethod(context => context.Kernel.Get<MapperConfiguration>().CreateMapper()).InSingletonScope();
        }
    }
}
