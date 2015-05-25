using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrekenWeb.Data.Tables
{
    internal class BoekHoofdstukTekstConfiguration : EntityTypeConfiguration<BoekHoofdstukTekst>
    {
        public BoekHoofdstukTekstConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".BoekHoofdstukTekst");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.BoekHoofdstukId).HasColumnName("BoekHoofdstukId").IsRequired();
            Property(x => x.Hoofdstuk).HasColumnName("Hoofdstuk").IsRequired();
            Property(x => x.Vers).HasColumnName("Vers").IsRequired();
            Property(x => x.Tekst).HasColumnName("Tekst").IsRequired();
            Property(x => x.Sortering).HasColumnName("Sortering").IsRequired();

            
            HasRequired(a => a.BoekHoofdstuk).WithMany(b => b.BoekHoofdstukTeksts).HasForeignKey(c => c.BoekHoofdstukId);  
        } 
    } 
}
