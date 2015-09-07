using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrekenWeb.Data.Tables
{
    internal class InboxOpvolgingConfiguration : EntityTypeConfiguration<InboxOpvolging>
    {
        public InboxOpvolgingConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".InboxOpvolging");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.InboxId).HasColumnName("InboxId").IsRequired();
            Property(x => x.GebruikerId).HasColumnName("GebruikerId").IsOptional();
            Property(x => x.Aangemaakt).HasColumnName("Aangemaakt").IsRequired();
            Property(x => x.Onderwerp).HasColumnName("Onderwerp").IsOptional();
            Property(x => x.Tekst).HasColumnName("Tekst").IsOptional().HasMaxLength(2147483647);
            Property(x => x.VerstuurAlsMail).HasColumnName("VerstuurAlsMail").IsRequired();
            Property(x => x.Verstuurd).HasColumnName("Verstuurd").IsOptional();

            HasRequired(a => a.Inbox).WithMany(b => b.InboxOpvolgings).HasForeignKey(c => c.InboxId);  
            HasOptional(a => a.Gebruiker).WithMany(b => b.InboxOpvolgings).HasForeignKey(c => c.GebruikerId);  
            
        }
        
    }

}
