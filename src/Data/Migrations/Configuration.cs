using System.Data.Entity.Migrations;
using System.Linq;
using PrekenWeb.Data.TestData;

namespace PrekenWeb.Data.Migrations
{  
    public sealed class Configuration : DbMigrationsConfiguration<PrekenwebContext>
    {
        private readonly string _lastMigrationName;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            var migrator = new DbMigrator(this);
            if (migrator.GetPendingMigrations().Any())
            {
                _lastMigrationName = migrator.GetPendingMigrations().Last();
            }
        } 

        protected override void Seed(PrekenwebContext context)
        {
            if (_lastMigrationName != "201502051157493_InitialCreate") return;
            
            context.Configuration.ValidateOnSaveEnabled = false;
            var testDataProvicer = new TestDataProvider();
            testDataProvicer.Provision(context);
            context.SaveChanges();
            // reset all passwords to 'prekenweb'
            context.Database.ExecuteSqlCommand("UPDATE Gebruiker SET PasswordHash = 'AFO33v1wphsnVS+Kl0sGgmQT2dppYQj2USf4ybiBC8KpXmN1O5o8U29I1bcz/G0IgA==', SecurityStamp='3bc2837e-840d-4844-a245-4e07fbeca00a'");
            context.Database.ExecuteSqlCommand("insert into AspNetUserRoles select 1, id,1 from AspNetRoles");
            //
        
        }
    }
}
