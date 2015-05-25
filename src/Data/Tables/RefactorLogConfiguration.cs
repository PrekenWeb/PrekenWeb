using System.Data.Entity.ModelConfiguration;

namespace PrekenWeb.Data.Tables
{
    internal class RefactorLogConfiguration : EntityTypeConfiguration<RefactorLog>
    {
        public RefactorLogConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".__RefactorLog");
            HasKey(x => x.OperationKey);

            Property(x => x.OperationKey).HasColumnName("OperationKey").IsRequired(); 
        } 
    } 
}
