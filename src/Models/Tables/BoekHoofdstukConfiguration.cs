using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Prekenweb.Models
{
    internal class BoekHoofdstukConfiguration : EntityTypeConfiguration<BoekHoofdstuk>
    {
        public BoekHoofdstukConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".BoekHoofdstuk");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.BoekId).HasColumnName("BoekId").IsRequired();
            Property(x => x.Omschrijving).HasColumnName("Omschrijving").IsRequired().HasMaxLength(255);
            Property(x => x.Sortering).HasColumnName("Sortering").IsOptional();
            Property(x => x.OudId).HasColumnName("OudId").IsOptional(); 
            
            HasRequired(a => a.Boek).WithMany(b => b.BoekHoofdstuks).HasForeignKey(c => c.BoekId); 
            
        }
        
    }

}
