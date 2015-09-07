using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrekenWeb.Data.Tables
{
    internal class ElmahErrorConfiguration : EntityTypeConfiguration<ElmahError>
    {
        public ElmahErrorConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".ELMAH_Error");
            HasKey(x => x.ErrorId);

            Property(x => x.ErrorId).HasColumnName("ErrorId").IsRequired();
            Property(x => x.Application).HasColumnName("Application").IsRequired().HasMaxLength(60);
            Property(x => x.Host).HasColumnName("Host").IsRequired().HasMaxLength(50);
            Property(x => x.Type).HasColumnName("Type").IsRequired().HasMaxLength(100);
            Property(x => x.Source).HasColumnName("Source").IsRequired().HasMaxLength(60);
            Property(x => x.Message).HasColumnName("Message").IsRequired().HasMaxLength(500);
            Property(x => x.User).HasColumnName("User").IsRequired().HasMaxLength(50);
            Property(x => x.StatusCode).HasColumnName("StatusCode").IsRequired();
            Property(x => x.TimeUtc).HasColumnName("TimeUtc").IsRequired();
            Property(x => x.Sequence).HasColumnName("Sequence").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.AllXml).HasColumnName("AllXml").IsRequired().HasMaxLength(1073741823); 
        } 
    } 
}
