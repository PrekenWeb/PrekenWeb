using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Prekenweb.Models
{
    internal class LezingCategorieConfiguration : EntityTypeConfiguration<LezingCategorie>
    {
        public LezingCategorieConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".LezingCategorie");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Omschrijving).HasColumnName("Omschrijving").IsRequired().HasMaxLength(255);
            Property(x => x.OudId).HasColumnName("OudId").IsOptional(); 
        } 
    } 
}
