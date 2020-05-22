using AutoMapper;
using Data.Models.Dtos;

namespace Data.Mapping.Profiles
{
    public class PreekDtoAutoMapperProfile : Profile
    {
        public override string ProfileName => nameof(PreekDtoAutoMapperProfile);

        public PreekDtoAutoMapperProfile()
        {
            CreateMap<Tables.Preek, Preek>()
                .ForMember(preekDto => preekDto.Id, options => options.MapFrom(preek => preek.Id))
                .ForMember(preekDto => preekDto.PreekTitel, options => options.MapFrom(preek => preek.PreekTitel))
                .ForMember(preekDto => preekDto.Informatie, options => options.MapFrom(preek => preek.Informatie))
                .ForMember(preekDto => preekDto.LezingCategorieNaam, options => options.Ignore()) // TODO
                .ForMember(preekDto => preekDto.GemeenteNaam, options => options.MapFrom(preek => preek.Gemeente.Omschrijving))
                .ForMember(preekDto => preekDto.PredikantNaam, options => options.MapFrom(preek => preek.Predikant.VolledigeNaam))
                .ForMember(preekDto => preekDto.PreekTypeNaam, options => options.MapFrom(preek => preek.PreekType.Omschrijving));

        }
    }
}