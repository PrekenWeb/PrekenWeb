using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrekenWeb.Data.Tables
{
    internal class InboxTypeConfiguration : EntityTypeConfiguration<InboxType>
    {
        public InboxTypeConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".InboxType");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Omschrijving).HasColumnName("Omschrijving").IsRequired().HasMaxLength(255);
            
        }
        
    }

}
