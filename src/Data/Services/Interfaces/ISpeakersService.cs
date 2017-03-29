using System.Collections.Generic;
using System.Threading.Tasks;
using PrekenWeb.Data.ViewModels;

namespace PrekenWeb.Data.Services.Interfaces
{
    public interface ISpeakersService
    {
        Task<Speaker> GetSingle(int id);
        Task<IEnumerable<Speaker>> Get(SpeakerFilter filter);
        //Task<int> Add(Speaker speaker);
        //Task<int> Update(Speaker speaker);
        //Task<bool> Delete(Speaker speaker);
    }
}