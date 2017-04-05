using AutoMapper;
using Business.Models;
using Data.Database.Dapper.Models;

namespace Business.Mapping.Profiles
{
    public class LectureTypeDataToLectureTypeAutoMapperProfile : Profile
    {
        public override string ProfileName => nameof(LectureTypeDataToLectureTypeAutoMapperProfile);

        public LectureTypeDataToLectureTypeAutoMapperProfile()
        {
            // DB => DAL
            CreateMap<LectureTypeData, LectureType>();

            // DAL => DB
            CreateMap<LectureType, LectureTypeData>();
            CreateMap<LectureTypeFilter, LectureTypeDataFilter>();

        }
    }
}