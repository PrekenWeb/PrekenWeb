using AutoMapper;
using Business.Models;
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

            CreateMap<Lecture, SermonEditModel>();
            CreateMap<Lecture, SermonViewModel>();

            //
            // API => Data
            //
            CreateMap<LanguageFilterModel, LanguageFilter>();

            CreateMap<SpeakerFilterModel, SpeakerFilter>();

            CreateMap<SermonEditModel, Lecture>();
            CreateMap<SermonFilterModel, LectureFilter>();
        }
    }
}
