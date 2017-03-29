using AutoMapper;
using PrekenWeb.Data.ViewModels;
using WebAPI.Models;

namespace WebAPI.Mapping.Profiles
{
    public class SermonApiModelAutoMapperProfile : Profile
    {
        public override string ProfileName => nameof(SermonApiModelAutoMapperProfile);

        public SermonApiModelAutoMapperProfile()
        {
            //
            // Data => API
            //
            CreateMap<Language, LanguageViewModel>();

            CreateMap<Speaker, SpeakerViewModel>();

            CreateMap<Sermon, SermonEditModel>();
            CreateMap<Sermon, SermonViewModel>();

            //
            // API => Data
            //
            CreateMap<LanguageFilterModel, LanguageFilter>();

            CreateMap<SpeakerFilterModel, SpeakerFilter>();

            CreateMap<SermonEditModel, Sermon>();
            CreateMap<SermonFilterModel, SermonFilter>();
        }
    }
}
