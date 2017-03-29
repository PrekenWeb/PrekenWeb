using AutoMapper;
using Prekenweb.Models.Dtos;
using PrekenWeb.Data.Database.Dapper.Models;
using PrekenWeb.Data.DataModels;
using PrekenWeb.Data.ViewModels;

namespace PrekenWeb.Data.Mapping.Profiles
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