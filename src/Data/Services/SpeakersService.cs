using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PrekenWeb.Data.Database.Dapper.Models;
using PrekenWeb.Data.DataModels;
using PrekenWeb.Data.Gateways;
using PrekenWeb.Data.Services.Interfaces;
using PrekenWeb.Data.Tables;
using PrekenWeb.Data.ViewModels;

namespace PrekenWeb.Data.Services
{
    public class SpeakersService : ISpeakersService
    {
        private readonly IMapper _mapper;
        private readonly ISpeakersGateway _speakersGateway;

        public SpeakersService(IMapper mapper, ISpeakersGateway speakersGateway)
        {
            _mapper = mapper;
            _speakersGateway = speakersGateway;
        }

        public async Task<IEnumerable<Speaker>> Get(SpeakerFilter filter)
        {
            var dataFilter = _mapper.Map<SpeakerFilter, SpeakerDataFilter>(filter);
            var speakersData = await _speakersGateway.Get(dataFilter);
            var speakers = _mapper.Map<IEnumerable<SpeakerData>, IEnumerable<Speaker>>(speakersData);
            return speakers;
        }

        public async Task<Speaker> GetSingle(int id)
        {
            var speakerData = await _speakersGateway.GetSingle(id);
            var speaker = _mapper.Map<SpeakerData, Speaker>(speakerData);
            return speaker;
        }
    }
}