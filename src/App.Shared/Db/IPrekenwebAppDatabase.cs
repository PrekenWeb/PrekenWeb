using System.Collections.Generic;

namespace App.Shared.Db
{
    public interface IPrekenwebAppDatabase
    {
        IEnumerable<NieuwePreekInLocalDb> GetNieuwePreken(); 
        NieuwePreekInLocalDb GetPreek(int id);
        int SavePreek(NieuwePreekInLocalDb item);
        int DeletePreek(int id);
        void UpdateNieuwePreken(IEnumerable<NieuwePreekInLocalDb> nieuwePreken);
    }
}