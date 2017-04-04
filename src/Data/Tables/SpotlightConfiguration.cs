using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Tables
{
    internal class SpotlightConfiguration : EntityTypeConfiguration<Spotlight>
    {
        public SpotlightConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Spotlight");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Titel).HasColumnName("Titel").IsRequired().HasMaxLength(255);
            Property(x => x.Subtitel).HasColumnName("Subtitel").IsRequired().HasMaxLength(255);
            Property(x => x.LinkTitel).HasColumnName("LinkTitel").IsRequired().HasMaxLength(255);
            Property(x => x.Url).HasColumnName("Url").IsRequired();
            Property(x => x.AfbeeldingId).HasColumnName("AfbeeldingId").IsRequired();
            Property(x => x.Sortering).HasColumnName("Sortering").IsRequired();
            Property(x => x.TaalId).HasColumnName("TaalId").IsRequired();
            Property(x => x.NieuwVenster).HasColumnName("NieuwVenster").IsRequired();

            HasRequired(a => a.Afbeelding).WithMany(b => b.Spotlights).HasForeignKey(c => c.AfbeeldingId); 
            HasRequired(a => a.Taal).WithMany(b => b.Spotlights).HasForeignKey(c => c.TaalId);   
        } 
    } 
}
