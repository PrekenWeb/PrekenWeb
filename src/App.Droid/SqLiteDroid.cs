using System;
using System.IO;
using App.Droid;
using App.Shared.Db;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SqLiteDroid))]

namespace App.Droid
{
    public class SqLiteDroid : ISqLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "PrekenWeb.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);

            var conn = new SQLiteConnection(path); 
            return conn;
        }
    }
}
