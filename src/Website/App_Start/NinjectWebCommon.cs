using System.Configuration;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Mvc.FilterBindingSyntax;
using PrekenWeb.Data;
using PrekenWeb.Data.Identity;
using PrekenWeb.Data.Repositories;
using Prekenweb.Models;
using PrekenWeb.Security;
using Microsoft.AspNet.Identity.Owin;
using Prekenweb.Website.Lib.Cache;
using Prekenweb.Website.Lib.Identity;

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
        }        
    }
}
