using PrekenWeb.Data.Identity;

namespace PrekenWeb.Data.Repositories
{
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
