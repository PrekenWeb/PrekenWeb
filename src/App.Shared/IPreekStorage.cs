using System.Threading.Tasks;

namespace App.Shared
{
    public interface IPreekStorage
    {
        Task<string> DownloadPreek(int id, string fileName);
    }
}