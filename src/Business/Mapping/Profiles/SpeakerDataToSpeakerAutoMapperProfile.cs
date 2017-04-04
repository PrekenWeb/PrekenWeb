using AutoMapper;
using Business.Models;
using Data.Database.Dapper.Models;

namespace Business.Mapping.Profiles
{
    public class SpeakerDataToSpeakerAutoMapperProfile : Profile
    {
        public override string ProfileName => nameof(SpeakerDataToSpeakerAutoMapperProfile);

        public SpeakerDataToSpeakerAutoMapperProfile()
        {
            // DB => DAL
            CreateMap<SpeakerData, Speaker>();

            // DAL => DB
            CreateMap<Speaker, SpeakerData>();
            CreateMap<SpeakerFilter, SpeakerDataFilter>();

        }
    }
}