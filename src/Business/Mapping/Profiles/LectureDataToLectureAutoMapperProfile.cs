using AutoMapper;
using Business.Models;
using Data.Database.Dapper.Models;

namespace Business.Mapping.Profiles
{
    public class LectureDataToLectureAutoMapperProfile : Profile
    {
        public override string ProfileName => nameof(SpeakerDataToSpeakerAutoMapperProfile);

        public LectureDataToLectureAutoMapperProfile()
        {
            // DB => DAL
            CreateMap<LectureData, Lecture>();

            // DAL => DB
            CreateMap<Lecture, LectureData>();
            CreateMap<LectureFilter, LectureDataFilter>();

        }
    }
}