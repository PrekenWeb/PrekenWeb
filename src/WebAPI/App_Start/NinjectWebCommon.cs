using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Ninject;
using Ninject.Web.Common;
using PrekenWeb.Data;
using PrekenWeb.Data.Identity;
using PrekenWeb.Data.Repositories;
using PrekenWeb.Data.Services;
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
            kernel.Bind<IHuidigeGebruiker>().To<HuidigeGebruiker>();
            kernel.Bind<PrekenwebContext>().ToSelf().WithConstructorArgument("proxyCreation", true);
            kernel.Bind<IPreekRepository>().To<PreekRepository>();
            kernel.Bind<IHomeService>().To<HomeService>();
            kernel.Bind<IGebruikerRepository>().To<GebruikerRepository>();
            kernel.Bind<IPrekenWebUserManager>().ToMethod(c => HttpContext.Current.GetOwinContext().GetUserManager<PrekenWebUserManager>()).InRequestScope(); 

        }
    }
}
