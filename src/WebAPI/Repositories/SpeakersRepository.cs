using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Models;
using Business.Services.Interfaces;
using WebAPI.Common;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositories
{
    internal class SpeakersRepository : ISpeakersRepository
    {
        private readonly ISpeakersService _speakersService;
        private readonly IMapper _mapper;

        public SpeakersRepository(ISpeakersService speakersService, IMapper mapper)
        {
            _speakersService = speakersService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SpeakerViewModel>> Get(SpeakerFilterModel filterModel)
        {
            var filter = _mapper.Map<SpeakerFilter>(filterModel);
            var speakers = await _speakersService.Get(filter);
            return _mapper.Map<IEnumerable<SpeakerViewModel>>(speakers);
        }

        public async Task<SpeakerViewModel> GetSingle(int id)
        {
            var speaker = await _speakersService.GetSingle(id);
            if (speaker == null) throw new ItemNotFoundException();
            return _mapper.Map<SpeakerViewModel>(speaker);
        }

        public async Task<int> Add(SpeakerViewModel speakerModel)
        {
            var speaker = _mapper.Map<Speaker>(speakerModel);
            return await _speakersService.Add(speaker);
        }

        public async Task<bool> Update(SpeakerViewModel speakerModel)
        {
            var existing = _speakersService.GetSingle(speakerModel.Id);
            if (existing == null) throw new ItemNotFoundException();

            var speaker = _mapper.Map<Speaker>(speakerModel);
            return await _speakersService.Update(speaker);
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await _speakersService.GetSingle(id);
            if (existing == null) throw new ItemNotFoundException();
            return await _speakersService.Delete(existing);
        }
    }
}