using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrekenWeb.Data.Identity
{
    internal partial class GebruikerConfiguration : EntityTypeConfiguration<Gebruiker>
    {
        public GebruikerConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Gebruiker");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Naam).HasColumnName("Naam").IsRequired().HasMaxLength(255); 
            Property(x => x.LaatstIngelogd).HasColumnName("LaatstIngelogd").IsOptional(); 
            HasMany(t => t.Mailings).WithMany(t => t.Gebruikers).Map(m => 
            {
                m.ToTable("GebruikerMailing");
                m.MapLeftKey("GebruikerId");
                m.MapRightKey("MailingId");
            });
            
        }
        
    }

}
