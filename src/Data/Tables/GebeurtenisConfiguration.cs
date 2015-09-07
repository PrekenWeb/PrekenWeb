using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrekenWeb.Data.Tables
{
    internal class GebeurtenisConfiguration : EntityTypeConfiguration<Gebeurtenis>
    {
        public GebeurtenisConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Gebeurtenis");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Omschrijving).HasColumnName("Omschrijving").IsRequired().HasMaxLength(255);
            Property(x => x.OudId).HasColumnName("OudId").IsOptional();
            Property(x => x.Sortering).HasColumnName("Sortering").IsRequired();
            Property(x => x.TaalId).HasColumnName("TaalId").IsRequired(); 
            HasRequired(a => a.Taal).WithMany(b => b.Gebeurtenis).HasForeignKey(c => c.TaalId); 
        }
        
    }

}
