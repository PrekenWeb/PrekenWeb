using System;
using System.IO;
using App.iOS;
using App.Shared.Db;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqLiteiOS))]

namespace App.iOS
{
    // ReSharper disable once InconsistentNaming
    public class SqLiteiOS : ISqLite
    {
        public SQLite.SQLiteConnection GetConnection()
        {
           var sqliteFilename = "PrekenWebSQLite.db3";

            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); 
            var libraryPath = Path.Combine(documentsPath, "PW"); 
            var path = Path.Combine(libraryPath, sqliteFilename);
 
            Directory.CreateDirectory(libraryPath);

            return new SQLiteConnection(path);
        }
    }
}
