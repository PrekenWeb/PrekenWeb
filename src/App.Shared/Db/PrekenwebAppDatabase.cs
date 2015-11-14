using System.Collections.Generic;
using System.Linq;
using SQLite;
using Xamarin.Forms;

namespace App.Shared.Db
{
    public class PrekenwebAppDatabase : IPrekenwebAppDatabase
    {
        static readonly object Locker = new object();

        readonly SQLiteConnection _database;

        public PrekenwebAppDatabase()
        {
            _database = DependencyService.Get<ISqLite>().GetConnection();
            _database.CreateTable<NieuwePreekInLocalDb>();
        }

        public IEnumerable<NieuwePreekInLocalDb> GetNieuwePreken()
        {
            lock (Locker)
            {
                var preken = _database.Table<NieuwePreekInLocalDb>().ToList();
                return preken; 
            }
        }

        public void UpdateNieuwePreken(IEnumerable<NieuwePreekInLocalDb> nieuwePreken)
        {
            lock (Locker)
            {
                _database.DeleteAll<NieuwePreekInLocalDb>();
                _database.InsertAll(nieuwePreken);
            }
            //lock (Locker)
            //{
            //    return _database.Query<NieuwePreekInLocalDb>("SELECT * FROM [Preek] WHERE [Id] = 1");
            //}
        }

        public NieuwePreekInLocalDb GetPreek(int id)
        {
            lock (Locker)
            {
                return _database.Table<NieuwePreekInLocalDb>().FirstOrDefault(x => x.Id == id);
            }
        }

        public int SavePreek(NieuwePreekInLocalDb item)
        {
            lock (Locker)
            {
                if (item.Id != 0)
                {
                    _database.Update(item);
                    return item.Id;
                }
                else
                {
                    return _database.Insert(item);
                }
            }
        }

        public int DeletePreek(int id)
        {
            lock (Locker)
            {
                return _database.Delete<NieuwePreekInLocalDb>(id);
            }
        }
    }
}