using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using Prekenweb.Models.Identity;

namespace Prekenweb.Models.Repository
{
    public interface IGebruikerRepository : IPrekenWebRepository
    {
        //Task<Gebruiker> GetGebruikerById(int id);
        //IEnumerable<Gebruiker> GetAlleGebruikers(bool alleenBeheerders, string zoekterm);
        IEnumerable<PreekCookie> GetBeluisterdePreken(int gebruikerId, int taalId);
        IEnumerable<PreekCookie> GetPrekenMetBladwijzer(int gebruikerId, int taalId);
        Task VerwijderGebruikerData(int gebruikerId);
        Task<IList<PreekCookie>> GetCookiesVoorGebruiker(int gebruikerId);
        //Task RemoveLoginAsync(int loggedInGebruikerId);
        Task AddToRolesAsync(int gebruikerId, string[] roleNames);
        Task RemoveAllRolesAsync(int gebruikerId);
    }

    public class GebruikerRepository : PrekenWebRepositoryBase, IGebruikerRepository
    {
        public GebruikerRepository(IPrekenwebContext<Gebruiker> prekenWebContext)
            : base(prekenWebContext)
        {
        }
        //public async Task<Gebruiker> GetGebruikerById(int id)
        //{
        //    return await Context.Users.Include(x => x.Roles).Include(x => x.Mailings).SingleAsync(g => g.Id == id);
        //}

        //public IEnumerable<Gebruiker> GetAlleGebruikers(bool alleenBeheerders, string zoekterm)
        //{  
        //    return PrekenWebUserManager.Users
        //        .Include(x => x.Roles)
        //        .Where(x =>
        //            (x.Roles.Any() == alleenBeheerders || !alleenBeheerders)
        //            && (x.Naam.Contains(zoekterm) || zoekterm == null || zoekterm == "")
        //            )
        //        .OrderByDescending(g => g.Roles.Count)
        //                .ThenBy(g => g.LaatstIngelogd)
        //                .ThenBy(g => g.Naam)
        //                .ToList();
        //}

        public IEnumerable<PreekCookie> GetBeluisterdePreken(int gebruikerId, int taalId)
        {
            return Context
                .PreekCookies
                .Include(x => x.Preek)
                .Where(x =>
                    x.GebruikerId == gebruikerId
                    && x.Preek.TaalId == taalId
                    && x.DateTime.HasValue
                )
                .OrderByDescending(x => x.DateTime)
                .ToList();
        }
        public IEnumerable<PreekCookie> GetPrekenMetBladwijzer(int gebruikerId, int taalId)
        {
            return Context
                .PreekCookies
                .Include(x => x.Preek)
                .Where(x =>
                    x.GebruikerId == gebruikerId
                    && x.Preek.TaalId == taalId
                    && x.BladwijzerGelegdOp.HasValue
                )
                .OrderByDescending(x => x.DateTime)
                .ToList();
        }

        public Task VerwijderGebruikerData(int gebruikerId)
        {
            throw new NotImplementedException();
        }

        #region Hooks
        public async Task RemoveLoginAsync(int gebruikerId)
        {
            await Context.Database.ExecuteSqlCommandAsync("delete from AspNetUserLogins where UserId = {0} or Gebruiker_Id = {0}", gebruikerId);
        }

        public async Task RemoveAllRolesAsync(int gebruikerId)
        {
            await Context.Database.ExecuteSqlCommandAsync("delete from AspNetUserRoles where UserId = {0} or Gebruiker_Id = {0}", gebruikerId);
        }


        public async Task<IList<PreekCookie>> GetCookiesVoorGebruiker(int gebruikerId)
        {
            return await Context
                .PreekCookies
                .Where(pc => pc.GebruikerId == gebruikerId)
                .ToListAsync();
        }

        public async Task AddToRolesAsync(int gebruikerId, string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                var dbContext = Context as PrekenwebContext;
                var role = await dbContext.Roles.SingleAsync(x => x.Name == roleName);
                await Context.Database.ExecuteSqlCommandAsync("insert into AspNetUserRoles values({0},{1},{2})", gebruikerId, role.Id, gebruikerId);
            } 
        }

        //public async Task RemoveLoginAsync(int loggedInGebruikerId)
        //{
        //    await Context.Database.ExecuteSqlCommandAsync("delete from AspNetUserLogins where UserId = {0} or Gebruiker_Id = {0}", loggedInGebruikerId);
        //}
        #endregion
    }
}
