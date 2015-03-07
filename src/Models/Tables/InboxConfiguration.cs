using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Prekenweb.Models
{
    internal class InboxConfiguration : EntityTypeConfiguration<Inbox>
    {
        public InboxConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Inbox");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.InboxTypeId).HasColumnName("InboxTypeId").IsRequired();
            Property(x => x.Omschrijving).HasColumnName("Omschrijving").IsRequired().HasMaxLength(255);
            Property(x => x.Inhoud).HasColumnName("Inhoud").IsRequired();
            Property(x => x.GebruikerId).HasColumnName("GebruikerId").IsOptional();
            Property(x => x.Afgehandeld).HasColumnName("Afgehandeld").IsRequired();
            Property(x => x.Aangemaakt).HasColumnName("Aangemaakt").IsRequired();
            Property(x => x.VanNaam).HasColumnName("VanNaam").IsRequired().HasMaxLength(255);
            Property(x => x.VanEmail).HasColumnName("VanEmail").IsRequired().HasMaxLength(255);
            Property(x => x.PreekId).HasColumnName("PreekId").IsOptional();
            Property(x => x.AanNaam).HasColumnName("AanNaam").IsRequired().HasMaxLength(255);
            Property(x => x.AanEmail).HasColumnName("AanEmail").IsRequired().HasMaxLength(255);

            HasRequired(a => a.InboxType).WithMany(b => b.Inboxes).HasForeignKey(c => c.InboxTypeId);  
            HasOptional(a => a.Gebruiker).WithMany(b => b.Inboxes).HasForeignKey(c => c.GebruikerId); 
            HasOptional(a => a.Preek).WithMany(b => b.Inboxes).HasForeignKey(c => c.PreekId);  
            
        }
        
    }

}
