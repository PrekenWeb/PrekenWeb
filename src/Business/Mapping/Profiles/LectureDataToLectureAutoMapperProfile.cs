using AutoMapper;
using Business.Models;
using Data.Database.Dapper.Models;

namespace Business.Mapping.Profiles
{
    public class LectureDataToLectureAutoMapperProfile : Profile
    {
        public override string ProfileName => nameof(LectureDataToLectureAutoMapperProfile);

        public LectureDataToLectureAutoMapperProfile()
        {
            // DB => DAL
            CreateMap<LectureData, Lecture>();
            CreateMap<ViewLectureData, ViewLecture>();

            // DAL => DB
            CreateMap<Lecture, LectureData>();
            CreateMap<ViewLecture, LectureData>();
            CreateMap<LectureFilter, LectureDataFilter>();
        }
    }
}