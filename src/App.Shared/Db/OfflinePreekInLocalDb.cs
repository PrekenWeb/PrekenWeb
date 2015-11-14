using SQLite;

namespace App.Shared.Db
{
    [Table("OfflinePreek")]
    public class OfflinePreekInLocalDb : PreekInLocalDb
    {
        public int SecondenBeluisterd { get; set; }
    }
}