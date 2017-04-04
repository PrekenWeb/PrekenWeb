using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Tables
{
    internal class SerieConfiguration : EntityTypeConfiguration<Serie>
    {
        public SerieConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Serie");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Omschrijving).HasColumnName("Omschrijving").IsRequired().HasMaxLength(255);
            Property(x => x.OudId).HasColumnName("OudId").IsOptional();
            Property(x => x.TaalId).HasColumnName("TaalId").IsRequired();

            
            HasRequired(a => a.Taal).WithMany(b => b.Series).HasForeignKey(c => c.TaalId); 
            
        } 
    } 
}
