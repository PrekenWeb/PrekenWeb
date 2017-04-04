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
    internal class LecturesRepository : ILecturesRepository
    {
        private readonly ILecturesService _lecturesService;
        private readonly IMapper _mapper;

        public LecturesRepository(ILecturesService lecturesService, IMapper mapper)
        {
            _lecturesService = lecturesService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LectureViewModel>> Get(LectureFilterModel filterModel)
        {
            var filter = _mapper.Map<LectureFilter>(filterModel);
            var lectures = await _lecturesService.Get(filter);
            return _mapper.Map<IEnumerable<LectureViewModel>>(lectures);
        }

        public async Task<LectureViewModel> GetSingle(int id)
        {
            var lecture = await _lecturesService.GetSingle(id);
            if (lecture == null) throw new ItemNotFoundException();
            return _mapper.Map<LectureViewModel>(lecture);
        }

        public async Task<int> Add(LectureViewModel lectureModel)
        {
            var lecture = _mapper.Map<Lecture>(lectureModel);
            return await _lecturesService.Add(lecture);
        }

        public async Task<bool> Update(LectureViewModel lectureModel)
        {
            var existing = _lecturesService.GetSingle(lectureModel.Id);
            if (existing == null) throw new ItemNotFoundException();

            var lecture = _mapper.Map<Lecture>(lectureModel);
            return await _lecturesService.Update(lecture);
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await _lecturesService.GetSingle(id);
            if (existing == null) throw new ItemNotFoundException();
            return await _lecturesService.Delete(existing);
        }
    }
}