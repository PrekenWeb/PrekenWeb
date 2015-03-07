using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using Prekenweb.Models;
using Prekenweb.Models.FakeDbSet;

namespace Website.UnitTests
{
    class TestPrekenwebContext<T> : IPrekenwebContext<T> where T : class
    {
        public TestPrekenwebContext()
        {
            Afbeeldings = new InMemoryDbSet<Afbeelding>();
            Boeks = new InMemoryDbSet<Boek>();
            BoekHoofdstuks = new InMemoryDbSet<BoekHoofdstuk>();
            BoekHoofdstukTeksts = new InMemoryDbSet<BoekHoofdstukTekst>();
            ElmahErrors = new InMemoryDbSet<ElmahError>();
            Gebeurtenis = new InMemoryDbSet<Gebeurtenis>();
            Gemeentes = new InMemoryDbSet<Gemeente>();
            Inboxes = new InMemoryDbSet<Inbox>();
            InboxOpvolgings = new InMemoryDbSet<InboxOpvolging>();
            InboxTypes = new InMemoryDbSet<InboxType>();
            LezingCategories = new InMemoryDbSet<LezingCategorie>();
            Mailings = new InMemoryDbSet<Mailing>();
            NieuwsbriefInschrijvings = new InMemoryDbSet<NieuwsbriefInschrijving>();
            Paginas = new InMemoryDbSet<Pagina>();
            Predikants = new InMemoryDbSet<Predikant>();
            Preeks = new InMemoryDbSet<Preek>();
            PreekCookies = new InMemoryDbSet<PreekCookie>();
            PreekLezenEnZingens = new InMemoryDbSet<PreekLezenEnZingen>();
            PreekTypes = new InMemoryDbSet<PreekType>();
            RefactorLogs = new InMemoryDbSet<RefactorLog>();
            Series = new InMemoryDbSet<Serie>();
            Spotlights = new InMemoryDbSet<Spotlight>();
            Taals = new InMemoryDbSet<Taal>();
            Teksts = new InMemoryDbSet<Tekst>();
            ZoekOpdrachten = new InMemoryDbSet<ZoekOpdracht>();
            Users = new InMemoryDbSet<T>();
        }

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
        public IDbSet<Preek> Preeks { get; set; }
        public IDbSet<PreekCookie> PreekCookies { get; set; }
        public IDbSet<PreekLezenEnZingen> PreekLezenEnZingens { get; set; }
        public IDbSet<PreekType> PreekTypes { get; set; }
        public IDbSet<RefactorLog> RefactorLogs { get; set; }
        public IDbSet<Serie> Series { get; set; }
        public IDbSet<Spotlight> Spotlights { get; set; }
        public IDbSet<Taal> Taals { get; set; }
        public IDbSet<Tekst> Teksts { get; set; }
        public IDbSet<ZoekOpdracht> ZoekOpdrachten { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public DbEntityEntry Entry(object entity)
        {
            throw new NotImplementedException();
        }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Database Database { get; private set; }
        public IDbSet<T> Users { get; set; }
    }
}