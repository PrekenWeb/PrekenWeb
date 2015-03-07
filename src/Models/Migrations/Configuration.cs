using Prekenweb.Models.TestData;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Prekenweb.Models.Migrations
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

        }
    }
}
