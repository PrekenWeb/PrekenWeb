using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Prekenweb.Models
{
    internal class PredikantConfiguration : EntityTypeConfiguration<Predikant>
    {
        public PredikantConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Predikant");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Titels).HasColumnName("Titels").IsOptional().HasMaxLength(50);
            Property(x => x.Voorletters).HasColumnName("Voorletters").IsOptional().HasMaxLength(50);
            Property(x => x.Achternaam).HasColumnName("Achternaam").IsRequired().HasMaxLength(255);
            Property(x => x.Gemeente).HasColumnName("Gemeente").IsOptional().HasMaxLength(255);
            Property(x => x.LevensPeriode).HasColumnName("LevensPeriode").IsOptional().HasMaxLength(255);
            Property(x => x.OudId).HasColumnName("OudID").IsOptional();
            Property(x => x.GemeenteId).HasColumnName("GemeenteId").IsOptional();
            Property(x => x.Tussenvoegsels).HasColumnName("Tussenvoegsels").IsOptional().HasMaxLength(50);
            Property(x => x.Opmerking).HasColumnName("Opmerking").IsOptional();
            Property(x => x.TaalId).HasColumnName("TaalId").IsRequired();

            HasOptional(a => a.Gemeente_GemeenteId).WithMany(b => b.Predikants).HasForeignKey(c => c.GemeenteId); // FK_Predikant_To_Gemeente
            HasRequired(a => a.Taal).WithMany(b => b.Predikants).HasForeignKey(c => c.TaalId); // FK_Predikant_To_Taal
            
        }
        
    }

}
