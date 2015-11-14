using System.Collections.Generic;
using System.Threading.Tasks;
using App.Shared.Db;

namespace App.Shared.Services
{
    public interface IPreekService
    {
        IEnumerable<PreekInLocalDb> GetNieuwePreken();
        Task UpdatePreken();
    }
}