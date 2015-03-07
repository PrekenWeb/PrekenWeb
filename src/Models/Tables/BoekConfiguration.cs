using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Prekenweb.Models
{
    internal class BoekConfiguration : EntityTypeConfiguration<Boek>
    {
        public BoekConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Boek");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Boeknaam).HasColumnName("Boeknaam").IsRequired().HasMaxLength(255);
            Property(x => x.Sortering).HasColumnName("Sortering").IsRequired();
            Property(x => x.OudId).HasColumnName("OudId").IsOptional();
            Property(x => x.Afkorting).HasColumnName("Afkorting").IsOptional().HasMaxLength(50);
            Property(x => x.ToonHoofdstukNummer).HasColumnName("ToonHoofdstukNummer").IsRequired();
            Property(x => x.TaalId).HasColumnName("TaalId").IsRequired();
             
            HasRequired(a => a.Taal).WithMany(b => b.Boeks).HasForeignKey(c => c.TaalId); 
            
        }
        
    }

}
