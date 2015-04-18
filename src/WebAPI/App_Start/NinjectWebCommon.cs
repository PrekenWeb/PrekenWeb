using Ninject;
using Prekenweb.Models;
using Prekenweb.Models.Repository;

namespace WebAPI
{
    public static class NinjectWebCommon
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                //kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                //kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

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
            kernel.Bind<PrekenwebContext>().ToSelf().WithConstructorArgument("proxyCreation", true);
            kernel.Bind<IPreekRepository>().To<PreekRepository>();
        }
    }
}
