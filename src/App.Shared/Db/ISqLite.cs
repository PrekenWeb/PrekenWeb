using SQLite;

namespace App.Shared.Db
{
    public interface ISqLite
    {
        SQLiteConnection GetConnection();
    }
}