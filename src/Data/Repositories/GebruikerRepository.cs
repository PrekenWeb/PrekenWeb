using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PrekenWeb.Data.Identity;
using PrekenWeb.Data.Tables;

namespace PrekenWeb.Data.Repositories
{
    public class GebruikerRepository : PrekenWebRepositoryBase, IGebruikerRepository
    {
        public GebruikerRepository(IPrekenwebContext<Gebruiker> prekenWebContext)
            : base(prekenWebContext)
        {
        } 

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

        public async Task<IEnumerable<PreekCookie>> GetPreekCookies(int gebruikerId, int[] preekIds)
        {
            return await Context
                .PreekCookies
                .Include(x => x.Preek)
                .Where(x =>
                    x.GebruikerId == gebruikerId
                    && preekIds.Contains(x.Preek.Id)
                )
                .OrderByDescending(x => x.DateTime)
                .ToListAsync();
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
