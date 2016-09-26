using SQLite;

namespace App.Shared.Db
{ 
    public abstract class PreekInLocalDb
    {  
        [PrimaryKey]
        public int Id { get; set; }

        public string Titel { get; set; } 

        public string Filename { get; set; }

        public string LocalFilePath { get; set; } 
    }
}