using AutoMapper;

using Data.Identity;
using Data.Mapping.Profiles;

using Ninject;
using Ninject.Web.Common;

namespace Data
{
    public static class DependencyInjection
    {
        public static void RegisterDependencies(IKernel kernel)
        {
            // Entity Framework
            kernel.Bind<IPrekenwebContext<Gebruiker>>().To<PrekenwebContext>().InRequestScope();
            kernel.Bind<PrekenwebContext>().ToSelf().WithConstructorArgument("proxyCreation", true);

            // AutoMapper mapping profiles
            kernel.Bind<Profile>().To<PreekDtoAutoMapperProfile>().InSingletonScope();
        }
    }
}
