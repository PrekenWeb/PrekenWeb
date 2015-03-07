using Prekenweb.Models.Identity;

namespace Prekenweb.Models.Repository
{
    public interface IPrekenWebRepository
    {
        void SaveChanges();
    }

    public abstract class PrekenWebRepositoryBase : IPrekenWebRepository
    {
        protected IPrekenwebContext<Gebruiker> Context;

        protected PrekenWebRepositoryBase(IPrekenwebContext<Gebruiker> context)
        {
            Context = context;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
