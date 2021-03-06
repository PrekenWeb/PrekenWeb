﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Data.Identity;
using Data.Tables;

namespace Data.Repositories
{
    public class MailingRepository : PrekenWebRepositoryBase, IMailingRepository
    {
        public MailingRepository(IPrekenwebContext<Gebruiker> prekenWebContext)
            : base(prekenWebContext)
        {
        }

        public async Task NieuwsbrievenOverschrijvenBijGebruiker(int gebruikerId, int[] nieuwsBriefIds)
        {
            var gebruiker = Context.Users.Single(x => x.Id == gebruikerId);
            gebruiker.Mailings.ToList().ForEach(m => gebruiker.Mailings.Remove(m));
            await Context.SaveChangesAsync();

            if (!nieuwsBriefIds.Any()) return;

            Context.Mailings
               .Where(m => nieuwsBriefIds.Contains(m.Id))
               .ToList()
               .ForEach(m => gebruiker.Mailings.Add(m));
            await Context.SaveChangesAsync();

        }

        public async Task NieuwsbriefToevoegenAanGebruiker(int gebruikerId, int nieuwsBriefId)
        {
            var gebruiker = Context.Users.Single(x => x.Id == gebruikerId);
            var mailing = await Context.Mailings.SingleAsync(x => x.Id == nieuwsBriefId); 
            gebruiker.Mailings.Add(mailing);

            await Context.SaveChangesAsync();
        }

        public async Task NieuwsbriefOntkoppelen(int gebruikerId, int nieuwsBriefId)
        {
            var gebruiker = Context.Users.Single(x => x.Id == gebruikerId);

            var mailing = await Context.Mailings.SingleAsync(x => x.Id == nieuwsBriefId);
            gebruiker.Mailings.Remove(mailing);

            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Mailing>> GetAlleMailings()
        {
            return await Context.Mailings.ToListAsync();
        } 
    }
}
