using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Prekenweb.Models
{
    internal class GemeenteConfiguration : EntityTypeConfiguration<Gemeente>
    {
        public GemeenteConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Gemeente");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Omschrijving).HasColumnName("Omschrijving").IsOptional().HasMaxLength(255); 
        } 
    }

}
