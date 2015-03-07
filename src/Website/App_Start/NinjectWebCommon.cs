using System.Web;
using Ninject;
using Ninject.Web.Common;
using Prekenweb.Models;
using Prekenweb.Models.Identity;
using Prekenweb.Models.Repository;
using PrekenWeb.Security;
using Prekenweb.Website.Lib;
using Microsoft.AspNet.Identity.Owin;

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
            kernel.Bind<IPrekenwebContext<Gebruiker>>().To<PrekenwebContext>().InRequestScope();
            kernel.Bind<IHuidigeGebruiker>().To<HuidigeGebruiker>();
            kernel.Bind<IPrekenwebCookie>().To<PrekenwebCookie>();
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
