using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrekenWeb.Data.Tables
{
    internal class MailingConfiguration : EntityTypeConfiguration<Mailing>
    {
        public MailingConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Mailing");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Omschrijving).HasColumnName("Omschrijving").IsRequired().HasMaxLength(255);
            Property(x => x.MailChimpId).HasColumnName("MailChimpId").IsRequired().HasMaxLength(255);
            Property(x => x.TaalId).HasColumnName("TaalId").IsRequired();

            
            HasRequired(a => a.Taal).WithMany(b => b.Mailings).HasForeignKey(c => c.TaalId); 
        } 
    } 
}
