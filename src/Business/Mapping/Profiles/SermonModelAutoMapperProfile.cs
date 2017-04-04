using System;
using AutoMapper;
using Business.Models;
using Data.Tables;

namespace Business.Mapping.Profiles
{
    public class SermonModelAutoMapperProfile : Profile
    {
        public override string ProfileName => nameof(SermonModelAutoMapperProfile);

        public SermonModelAutoMapperProfile()
        {
            CreateMap<Preek, Lecture>()
                .ForMember(sermonModel => sermonModel.Id, options => options.MapFrom(preek => preek.Id))
                .ForMember(sermonModel => sermonModel.Title, options => options.MapFrom(preek => preek.PreekTitel))
                .ForMember(sermonModel => sermonModel.Information, options => options.MapFrom(preek => preek.Informatie))
                ;

            CreateMap<LectureFilter, SermonDataFilter>()
                .ForMember(sermonFilter => sermonFilter.LanguageId, options => options.MapFrom(filter => filter.LanguageId))
                .ForMember(sermonFilter => sermonFilter.PageSize, options => options.MapFrom(filter => Math.Max(filter.PageSize ?? 25, 1))) // >= 1
                .ForMember(sermonFilter => sermonFilter.Page, options => options.MapFrom(filter => Math.Max(filter.Page ?? 0, 0))) // >= 0
                ;
        }
    }
}
