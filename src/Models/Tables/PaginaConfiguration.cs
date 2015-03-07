using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Prekenweb.Models
{
    internal class PaginaConfiguration : EntityTypeConfiguration<Pagina>
    {
        public PaginaConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Pagina");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Gepubliceerd).HasColumnName("Gepubliceerd").IsRequired();
            Property(x => x.Identifier).HasColumnName("Identifier").IsRequired().HasMaxLength(255);
            Property(x => x.Aangemaakt).HasColumnName("Aangemaakt").IsRequired();
            Property(x => x.Bijgewerkt).HasColumnName("Bijgewerkt").IsRequired();
            Property(x => x.AangemaaktDoor).HasColumnName("AangemaaktDoor").IsRequired();
            Property(x => x.BijgewerktDoor).HasColumnName("BijgewerktDoor").IsRequired();
            Property(x => x.TonenOpHomepage).HasColumnName("TonenOpHomepage").IsRequired(); 
            
            HasRequired(a => a.Gebruiker_AangemaaktDoor).WithMany(b => b.Paginas_AangemaaktDoor).HasForeignKey(c => c.AangemaaktDoor);  
            HasRequired(a => a.Gebruiker_BijgewerktDoor).WithMany(b => b.Paginas_BijgewerktDoor).HasForeignKey(c => c.BijgewerktDoor);  
            
        }
        
    }

}
