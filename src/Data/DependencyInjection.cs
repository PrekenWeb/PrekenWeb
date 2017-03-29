using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ninject;
using Ninject.Web.Common;
using PrekenWeb.Data.Gateways;
using PrekenWeb.Data.Identity;
using PrekenWeb.Data.Mapping.Profiles;
using PrekenWeb.Data.Services;
using PrekenWeb.Data.Services.Interfaces;

namespace PrekenWeb.Data
{
    public static class DependencyInjection
    {
        public static void RegisterDependencies(IKernel kernel)
        {
            // Entity Framework
            kernel.Bind<IPrekenwebContext<Gebruiker>>().To<PrekenwebContext>().InRequestScope();
            kernel.Bind<PrekenwebContext>().ToSelf().WithConstructorArgument("proxyCreation", true);

            // Database => Dapper
            Database.Dapper.DependencyInjection.RegisterDependencies(kernel);

            // AutoMapper mapping profiles
            kernel.Bind<Profile>().To<SpeakerDataToSpeakerAutoMapperProfile>().InSingletonScope();
            kernel.Bind<Profile>().To<PreekDtoAutoMapperProfile>().InSingletonScope();
            kernel.Bind<Profile>().To<SermonModelAutoMapperProfile>().InSingletonScope();

            kernel.Bind<ILanguagesService>().To<LanguagesService>().InSingletonScope();
            kernel.Bind<ISpeakersService>().To<SpeakersService>().InSingletonScope();
            kernel.Bind<ISermonsService>().To<SermonsService>().InSingletonScope();
        }
    }
}
