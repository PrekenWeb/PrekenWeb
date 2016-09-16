using App.Droid;
using App.Shared.Db;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqLiteDroid))]

namespace App.Droid
{ 
    public class SqLiteDroid : ISqLite
    {
        public  SQLiteConnection GetConnection()
        {
           

            return new SQLiteConnection("");
        }
    }
}
