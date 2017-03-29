using AutoMapper;

namespace PrekenWeb.Data.Mapping
{
    public class IocInjectedMapperConfiguration : MapperConfiguration
    {
        public IocInjectedMapperConfiguration(Profile[] profiles)
            : base(cfg =>
            {
                if (profiles == null)
                    return;

                //// do not map properties automatically
                //cfg.ShouldMapProperty = p => false;

                //// do not map fields automatically
                //cfg.ShouldMapField = f => false;

                foreach (var profile in profiles)
                    cfg.AddProfile(profile);
            })
        {
        }
    }
}
