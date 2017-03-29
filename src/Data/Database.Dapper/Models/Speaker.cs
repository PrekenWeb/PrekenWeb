using DapperExtensions.Mapper;

namespace PrekenWeb.Data.DataModels
{
    public class SpeakerData
    {
        public int Id { get; set; }
        public string LastName { get; set; }
    }

    public sealed class SpeakerDataMapper : ClassMapper<SpeakerData>
    {
        public SpeakerDataMapper()
        {
            TableName = "Predikant";

            //have a custom primary key
            Map(x => x.Id).Key(KeyType.Assigned);

            // Map Dutch column names to English property names
            Map(x => x.LastName).Column("Achternaam");

            // auto map all other columns
            AutoMap();
        }
    }
}
