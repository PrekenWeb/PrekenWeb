using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface ISpeakersRepository : IRepository<SpeakerViewModel, SpeakerFilterModel>
    {
    }
}