using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Services.Interfaces
{
    public interface ISpeakersService
    {
        Task<Speaker> GetSingle(int id);
        Task<IEnumerable<Speaker>> Get(SpeakerFilter filter);
        Task<int> Add(Speaker speaker);
        Task<bool> Update(Speaker speaker);
        Task<bool> Delete(Speaker speaker);
    }
}