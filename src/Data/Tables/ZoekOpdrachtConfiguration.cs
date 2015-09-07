using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrekenWeb.Data.Tables
{
    internal class ZoekOpdrachtConfiguration : EntityTypeConfiguration<ZoekOpdracht>
    {
        public ZoekOpdrachtConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".ZoekOpdracht");
            HasKey(x => x.Id); 

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(a => a.Gebruiker).WithMany(b => b.ZoekOpdrachten).HasForeignKey(c => c.GebruikerId);   
        } 
    } 
}
