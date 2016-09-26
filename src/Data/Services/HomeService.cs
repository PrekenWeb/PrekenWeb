using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PrekenWeb.Data.Repositories;
using PrekenWeb.Data.Tables;

namespace PrekenWeb.Data.Services
{
    public class HomeService : IHomeService
    {
        private readonly IPreekRepository _preekRepository;

        public HomeService(IPreekRepository preekRepository)
        {
            _preekRepository = preekRepository;
        }

        public async Task<IEnumerable<Prekenweb.Models.Dtos.Preek>> NieuwePreken(int taalId)
        {
            var preken = await _preekRepository.GetAllePreken(taalId);

            Mapper.CreateMap<Preek, Prekenweb.Models.Dtos.Preek>()
                .ForMember(x => x.LezingCategorieNaam, opt => opt.Ignore()) //todo
				.ForMember(x => x.GemeenteNaam, opt => opt.MapFrom(y => y.Gemeente.Omschrijving))
				.ForMember(x => x.PredikantNaam, opt => opt.MapFrom(y => y.Predikant.VolledigeNaam))
				.ForMember(x => x.PreekTypeNaam, opt => opt.MapFrom(y => y.PreekType.Omschrijving));

            var prekenDtos = Mapper.Map<IEnumerable<Preek>, IEnumerable<Prekenweb.Models.Dtos.Preek>>(preken);
            Mapper.AssertConfigurationIsValid();
            return prekenDtos;
        }
    }
}