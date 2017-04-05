using AutoMapper;
using Business.Models;
using Data.Database.Dapper.Models;

namespace Business.Mapping.Profiles
{
    public class LanguageDataToLanguageAutoMapperProfile : Profile
    {
        public override string ProfileName => nameof(LanguageDataToLanguageAutoMapperProfile);

        public LanguageDataToLanguageAutoMapperProfile()
        {
            // DB => DAL
            CreateMap<LanguageData, Language>();

            // DAL => DB
            CreateMap<Language, LanguageData>();
            CreateMap<LanguageFilter, LanguageDataFilter>();
        }
    }
}