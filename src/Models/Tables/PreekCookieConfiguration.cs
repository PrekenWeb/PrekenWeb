using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Prekenweb.Models
{
    internal class PreekCookieConfiguration : EntityTypeConfiguration<PreekCookie>
    {
        public PreekCookieConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".PreekCookie");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.PreekId).HasColumnName("PreekId").IsRequired();
            Property(x => x.DateTime).HasColumnName("DateTime").IsOptional();
            Property(x => x.Opmerking).HasColumnName("Opmerking").IsOptional();
            Property(x => x.AfgespeeldTot).HasColumnName("AfgespeeldTot").IsOptional();
            Property(x => x.GebruikerId).HasColumnName("GebruikerId").IsRequired();
            Property(x => x.BladwijzerGelegdOp).HasColumnName("BladwijzerGelegdOp").IsOptional();

            HasRequired(a => a.Preek).WithMany(b => b.PreekCookies).HasForeignKey(c => c.PreekId); 
            HasRequired(a => a.Gebruiker).WithMany(b => b.PreekCookies).HasForeignKey(c => c.GebruikerId);  
            
        }
        
    }

}
