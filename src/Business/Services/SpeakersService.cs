using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Models;
using Business.Services.Interfaces;
using Data.Database.Dapper.Filters;
using Data.Database.Dapper.Interfaces.Gateways;
using Data.Models;

namespace Business.Services
{
    internal class SpeakersService : ISpeakersService
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

        public async Task<int> Add(Speaker speaker)
        {
            var speakerData = _mapper.Map<Speaker, SpeakerData>(speaker);
            return await _speakersGateway.Add(speakerData);
        }

        public async Task<bool> Update(Speaker speaker)
        {
            var speakerData = _mapper.Map<Speaker, SpeakerData>(speaker);
            return await _speakersGateway.Update(speakerData);
        }

        public async Task<bool> Delete(Speaker speaker)
        {
            var speakerData = _mapper.Map<Speaker, SpeakerData>(speaker);
            return await _speakersGateway.Delete(speakerData);
        }
    }
}