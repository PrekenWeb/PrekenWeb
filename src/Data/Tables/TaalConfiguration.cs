using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Tables
{
    internal class TaalConfiguration : EntityTypeConfiguration<Taal>
    {
        public TaalConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Taal");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Code).HasColumnName("Code").IsRequired().HasMaxLength(10);
            Property(x => x.Omschrijving).HasColumnName("Omschrijving").IsRequired().HasMaxLength(255); 
        } 
    } 
}
