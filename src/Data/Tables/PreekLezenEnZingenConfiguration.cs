using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrekenWeb.Data.Tables
{
    internal class PreekLezenEnZingenConfiguration : EntityTypeConfiguration<PreekLezenEnZingen>
    {
        public PreekLezenEnZingenConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".PreekLezenEnZingen");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.PreekId).HasColumnName("PreekId").IsRequired();
            Property(x => x.Sortering).HasColumnName("Sortering").IsRequired();
            Property(x => x.Soort).HasColumnName("Soort").IsOptional().HasMaxLength(255);
            Property(x => x.Omschrijving).HasColumnName("Omschrijving").IsOptional().HasMaxLength(255);

            HasRequired(a => a.Preek).WithMany(b => b.PreekLezenEnZingens).HasForeignKey(c => c.PreekId); // FK_PreekLezenEnZingen_Preek
            
        }
        
    }

}
