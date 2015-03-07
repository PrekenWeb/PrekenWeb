using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration; 

namespace Prekenweb.Models
{ 
    internal class AfbeeldingConfiguration : EntityTypeConfiguration<Afbeelding>
    {
        public AfbeeldingConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Afbeelding");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Omschrijving).HasColumnName("Omschrijving").IsRequired().HasMaxLength(255);
            Property(x => x.Bestandsnaam).HasColumnName("Bestandsnaam").IsRequired().HasMaxLength(255);
            Property(x => x.ContentType).HasColumnName("ContentType").IsRequired().HasMaxLength(255);
            
        }
        
    }

}
