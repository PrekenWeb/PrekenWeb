using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Prekenweb.Models
{
    public interface IPrekenwebContext<T> : IDisposable where T : class
    {
        IDbSet<Afbeelding> Afbeeldings { get; set; }
        IDbSet<Boek> Boeks { get; set; }
        IDbSet<BoekHoofdstuk> BoekHoofdstuks { get; set; }
        IDbSet<BoekHoofdstukTekst> BoekHoofdstukTeksts { get; set; }
        IDbSet<ElmahError> ElmahErrors { get; set; }
        IDbSet<Gebeurtenis> Gebeurtenis { get; set; }
        IDbSet<Gemeente> Gemeentes { get; set; }
        IDbSet<Inbox> Inboxes { get; set; }
        IDbSet<InboxOpvolging> InboxOpvolgings { get; set; }
        IDbSet<InboxType> InboxTypes { get; set; }
        IDbSet<LezingCategorie> LezingCategories { get; set; }
        IDbSet<Mailing> Mailings { get; set; }
        IDbSet<NieuwsbriefInschrijving> NieuwsbriefInschrijvings { get; set; }
        IDbSet<Pagina> Paginas { get; set; }
        IDbSet<Predikant> Predikants { get; set; }
        IDbSet<Preek> Preeks { get; set; }
        IDbSet<PreekCookie> PreekCookies { get; set; }
        IDbSet<PreekLezenEnZingen> PreekLezenEnZingens { get; set; }
        IDbSet<PreekType> PreekTypes { get; set; }
        IDbSet<RefactorLog> RefactorLogs { get; set; }
        IDbSet<Serie> Series { get; set; }
        IDbSet<Spotlight> Spotlights { get; set; }
        IDbSet<Taal> Taals { get; set; }
        IDbSet<Tekst> Teksts { get; set; }
        IDbSet<ZoekOpdracht> ZoekOpdrachten { get; set; }

        // Implemented by DbContext
        DbEntityEntry Entry(object entity); 
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(); 
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Database Database { get; }
       
        // Implemented by IdentityDbContext 
         IDbSet<T> Users { get; set; }

    }
}
