using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using Prekenweb.Models.Identity;
using Prekenweb.Models.Migrations;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Prekenweb.Models
{
    public partial class PrekenwebContext : IdentityDbContext<Gebruiker, PrekenWebRole, int, PrekenWebUserLogin, PrekenWebUserRole, PrekenWebUserClaim>, IPrekenwebContext<Gebruiker>
    {
        public IDbSet<Afbeelding> Afbeeldings { get; set; }
        public IDbSet<Boek> Boeks { get; set; }
        public IDbSet<BoekHoofdstuk> BoekHoofdstuks { get; set; }
        public IDbSet<BoekHoofdstukTekst> BoekHoofdstukTeksts { get; set; }
        public IDbSet<ElmahError> ElmahErrors { get; set; }
        public IDbSet<Gebeurtenis> Gebeurtenis { get; set; }
        public IDbSet<Gemeente> Gemeentes { get; set; }
        public IDbSet<Inbox> Inboxes { get; set; }
        public IDbSet<InboxOpvolging> InboxOpvolgings { get; set; }
        public IDbSet<InboxType> InboxTypes { get; set; }
        public IDbSet<LezingCategorie> LezingCategories { get; set; }
        public IDbSet<Mailing> Mailings { get; set; }
        public IDbSet<NieuwsbriefInschrijving> NieuwsbriefInschrijvings { get; set; }
        public IDbSet<Pagina> Paginas { get; set; }
        public IDbSet<Predikant> Predikants { get; set; }
        public virtual IDbSet<Preek> Preeks { get; set; }
        public IDbSet<PreekCookie> PreekCookies { get; set; }
        public IDbSet<PreekLezenEnZingen> PreekLezenEnZingens { get; set; }
        public IDbSet<PreekType> PreekTypes { get; set; }
        public IDbSet<RefactorLog> RefactorLogs { get; set; }
        public IDbSet<Serie> Series { get; set; }
        public virtual IDbSet<Spotlight> Spotlights { get; set; }
        public IDbSet<Taal> Taals { get; set; }
        public virtual IDbSet<Tekst> Teksts { get; set; }
        public IDbSet<ZoekOpdracht> ZoekOpdrachten { get; set; }

        static PrekenwebContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PrekenwebContext, Configuration>());
        }

        public PrekenwebContext()
            : base("Name=PrekenwebContext")
        {
            try
            {
                var objectContext = (this as IObjectContextAdapter).ObjectContext;
                if (objectContext != null) // needed since Moq (unit test mocking) does not mock the IObjectContextAdapter interface.
                { 
                    objectContext.CommandTimeout = 3;
                }
            }
            catch (DataException de)
            {
                var ie = de.InnerException as DbEntityValidationException;
                if (ie != null)
                {
                    var fouten = string.Join(Environment.NewLine, ie.EntityValidationErrors.Select(x => string.Format("{0}:{1}", x.ValidationErrors.First().PropertyName, x.ValidationErrors.First().ErrorMessage)));
                    throw new Exception("Fout bij initialiseren database:" + Environment.NewLine + fouten); 
                }
            }
        } 

        public static PrekenwebContext Create()
        {
            return new PrekenwebContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AfbeeldingConfiguration());
            modelBuilder.Configurations.Add(new BoekConfiguration());
            modelBuilder.Configurations.Add(new BoekHoofdstukConfiguration());
            modelBuilder.Configurations.Add(new BoekHoofdstukTekstConfiguration());
            modelBuilder.Configurations.Add(new ElmahErrorConfiguration());
            modelBuilder.Configurations.Add(new GebeurtenisConfiguration());
            modelBuilder.Configurations.Add(new GebruikerConfiguration());
            modelBuilder.Configurations.Add(new GemeenteConfiguration());
            modelBuilder.Configurations.Add(new InboxConfiguration());
            modelBuilder.Configurations.Add(new InboxOpvolgingConfiguration());
            modelBuilder.Configurations.Add(new InboxTypeConfiguration());
            modelBuilder.Configurations.Add(new LezingCategorieConfiguration());
            modelBuilder.Configurations.Add(new MailingConfiguration());
            modelBuilder.Configurations.Add(new NieuwsbriefInschrijvingConfiguration());
            modelBuilder.Configurations.Add(new PaginaConfiguration());
            modelBuilder.Configurations.Add(new PredikantConfiguration());
            modelBuilder.Configurations.Add(new PreekConfiguration());
            modelBuilder.Configurations.Add(new PreekCookieConfiguration());
            modelBuilder.Configurations.Add(new PreekLezenEnZingenConfiguration());
            modelBuilder.Configurations.Add(new PreekTypeConfiguration());
            modelBuilder.Configurations.Add(new RefactorLogConfiguration());
            modelBuilder.Configurations.Add(new SerieConfiguration());
            modelBuilder.Configurations.Add(new SpotlightConfiguration());
            modelBuilder.Configurations.Add(new TaalConfiguration());
            modelBuilder.Configurations.Add(new TekstConfiguration());
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new AfbeeldingConfiguration(schema));
            modelBuilder.Configurations.Add(new BoekConfiguration(schema));
            modelBuilder.Configurations.Add(new BoekHoofdstukConfiguration(schema));
            modelBuilder.Configurations.Add(new BoekHoofdstukTekstConfiguration(schema));
            modelBuilder.Configurations.Add(new ElmahErrorConfiguration(schema));
            modelBuilder.Configurations.Add(new GebeurtenisConfiguration(schema));
            modelBuilder.Configurations.Add(new GebruikerConfiguration(schema));
            modelBuilder.Configurations.Add(new GemeenteConfiguration(schema));
            modelBuilder.Configurations.Add(new InboxConfiguration(schema));
            modelBuilder.Configurations.Add(new InboxOpvolgingConfiguration(schema));
            modelBuilder.Configurations.Add(new InboxTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new LezingCategorieConfiguration(schema));
            modelBuilder.Configurations.Add(new MailingConfiguration(schema));
            modelBuilder.Configurations.Add(new NieuwsbriefInschrijvingConfiguration(schema));
            modelBuilder.Configurations.Add(new PaginaConfiguration(schema));
            modelBuilder.Configurations.Add(new PredikantConfiguration(schema));
            modelBuilder.Configurations.Add(new PreekConfiguration(schema));
            modelBuilder.Configurations.Add(new PreekCookieConfiguration(schema));
            modelBuilder.Configurations.Add(new PreekLezenEnZingenConfiguration(schema));
            modelBuilder.Configurations.Add(new PreekTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new RefactorLogConfiguration(schema));
            modelBuilder.Configurations.Add(new SerieConfiguration(schema));
            modelBuilder.Configurations.Add(new SpotlightConfiguration(schema));
            modelBuilder.Configurations.Add(new TaalConfiguration(schema));
            modelBuilder.Configurations.Add(new TekstConfiguration(schema));
            return modelBuilder;
        }
    }
}
