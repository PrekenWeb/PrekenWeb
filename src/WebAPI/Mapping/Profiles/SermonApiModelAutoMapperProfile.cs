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
            CreateMap<Lecture, SermonEditModel>();
            CreateMap<ViewLecture, SermonViewModel>();

            CreateMap<Book, BookViewModel>();
            CreateMap<Image, ImageViewModel>();
            CreateMap<Language, LanguageViewModel>();
            CreateMap<Lecture, LectureViewModel>();
            CreateMap<ViewLecture, LectureViewModel>();
            CreateMap<LectureType, LectureTypeViewModel>();
            CreateMap<Speaker, SpeakerViewModel>();

            //
            // API => Data
            //
            CreateMap<SermonEditModel, Lecture>();
            CreateMap<SermonFilterModel, LectureFilter>();

            CreateMap<BookFilterModel, BookFilter>();
            CreateMap<ImageFilterModel, ImageFilter>();
            CreateMap<LanguageFilterModel, LanguageFilter>();
            CreateMap<LectureFilterModel, LectureFilter>();
            CreateMap<LectureTypeFilterModel, LectureTypeFilter>();
            CreateMap<SpeakerFilterModel, SpeakerFilter>();
        }
    }
}
