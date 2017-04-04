using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Tables
{
    internal class TekstConfiguration : EntityTypeConfiguration<Tekst>
    {
        public TekstConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Tekst");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Kop).HasColumnName("Kop").IsRequired().HasMaxLength(255);
            Property(x => x.Tekst_).HasColumnName("Tekst").IsRequired().HasMaxLength(2147483647);
            Property(x => x.TaalId).HasColumnName("TaalId").IsRequired();
            Property(x => x.PaginaId).HasColumnName("PaginaId").IsOptional();

            HasRequired(a => a.Taal).WithMany(b => b.Teksts).HasForeignKey(c => c.TaalId);  
            HasOptional(a => a.Pagina).WithMany(b => b.Teksts).HasForeignKey(c => c.PaginaId);   
        } 
    } 
}
