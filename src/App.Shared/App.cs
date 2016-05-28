using Xamarin.Forms;
using App.Shared.Db;

namespace App.Shared
{
    public class App : Application
    {

        static PrekenwebAppDatabase database;
         

        public static PrekenwebAppDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new PrekenwebAppDatabase();
                }
                return database;
            }
        }

        public App()
        { 
			MainPage = new RootPage ();
        } 
    }
}
