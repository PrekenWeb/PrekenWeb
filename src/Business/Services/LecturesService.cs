using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Business.Models;
using Business.Services.Interfaces;
using Data.Database.Dapper.Gateways;
using Data.Database.Dapper.Models;

namespace Business.Services
{
    public class LecturesService : ILecturesService
    {
        private readonly IMapper _mapper;
        private readonly ILecturesGateway _lecturesGateway;

        public LecturesService(IMapper mapper, ILecturesGateway lecturesGateway)
        {
            _mapper = mapper;
            _lecturesGateway = lecturesGateway;
        }

        public async Task<IEnumerable<Lecture>> Get(LectureFilter filter)
        {
            var dataFilter = _mapper.Map<LectureFilter, LectureDataFilter>(filter);
            var lecturesData = await _lecturesGateway.Get(dataFilter);
            var lectures = _mapper.Map<IEnumerable<LectureData>, IEnumerable<Lecture>>(lecturesData);
            return lectures;
        }

        public async Task<Lecture> GetSingle(int id)
        {
            var lectureData = await _lecturesGateway.GetSingle(id);
            var lecture = _mapper.Map<LectureData, Lecture>(lectureData);
            return lecture;
        }

        public async Task<int> Add(Lecture lecture)
        {
            var lectureData = _mapper.Map<Lecture, LectureData>(lecture);
            return await _lecturesGateway.Add(lectureData);
        }

        public async Task<bool> Update(Lecture lecture)
        {
            var lectureData = _mapper.Map<Lecture, LectureData>(lecture);
            return await _lecturesGateway.Update(lectureData);
        }

        public async Task<bool> Delete(Lecture lecture)
        {
            var lectureData = _mapper.Map<Lecture, LectureData>(lecture);
            return await _lecturesGateway.Delete(lectureData);
        }
    }
}