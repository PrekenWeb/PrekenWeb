namespace Data.Tables
{
    public class PreekLezenEnZingen
    {
        public int Id { get; set; } // Id (Primary key)
        public int PreekId { get; set; } // PreekId
        public byte Sortering { get; set; } // Sortering
        public string Soort { get; set; } // Soort
        public string Omschrijving { get; set; } // Omschrijving

        public virtual Preek Preek { get; set; } //  FK_PreekLezenEnZingen_Preek
    }

}
