using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Tables
{
    internal class NieuwsbriefInschrijvingConfiguration : EntityTypeConfiguration<NieuwsbriefInschrijving>
    {
        public NieuwsbriefInschrijvingConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".NieuwsbriefInschrijving");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Naam).HasColumnName("Naam").IsRequired().HasMaxLength(255);
            Property(x => x.Email).HasColumnName("Email").IsRequired().HasMaxLength(255);
            Property(x => x.Aangemeld).HasColumnName("Aangemeld").IsRequired();
            Property(x => x.Afgemeld).HasColumnName("Afgemeld").IsOptional();
            Property(x => x.TaalId).HasColumnName("TaalId").IsRequired();

            HasRequired(a => a.Taal).WithMany(b => b.NieuwsbriefInschrijvings).HasForeignKey(c => c.TaalId); 
            
        }
        
    }

}
