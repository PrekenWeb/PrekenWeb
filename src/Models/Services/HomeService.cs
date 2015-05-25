using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Prekenweb.Models.Repository;

namespace Prekenweb.Models.Services
{
    public class HomeService : IHomeService
    {
        private readonly IPreekRepository _preekRepository;

        public HomeService(IPreekRepository preekRepository)
        {
            _preekRepository = preekRepository;
        }

        public async Task<IEnumerable<Dtos.Preek>> NieuwePreken(int taalId)
        {
            var preken = await _preekRepository.GetAllePreken(taalId);

            Mapper.CreateMap<Preek, Dtos.Preek>()
                .ForMember(x => x.LezingCategorieNaam, opt => opt.Ignore())
                .ForMember(x => x.GemeenteNaam, opt => opt.Ignore())
                .ForMember(x => x.PredikantNaam, opt => opt.Ignore())
                .ForMember(x => x.PreekTypeNaam, opt => opt.Ignore());

            var prekenDtos = Mapper.Map<IEnumerable<Preek>, IEnumerable<Dtos.Preek>>(preken);
            Mapper.AssertConfigurationIsValid();
            return prekenDtos;
        }
    }
}