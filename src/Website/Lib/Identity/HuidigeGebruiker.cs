using System.Security.Principal;
using System.Threading.Tasks;
using PrekenWeb.Security;

namespace Prekenweb.Website.Lib.Identity
{
    public interface IHuidigeGebruiker
    {
        Task<int> GetId(IPrekenWebUserManager prekenWebUserManager, IPrincipal user);
    }

    public class HuidigeGebruiker : IHuidigeGebruiker
    {
        private int? _id;
        public async Task<int> GetId(IPrekenWebUserManager prekenWebUserManager, IPrincipal user)
        {
            if (_id.HasValue) return _id.Value;
            if (user == null) return 0;
            if (!user.Identity.IsAuthenticated) return 0;
            if (prekenWebUserManager == null) return 0;

            var dbUser = await prekenWebUserManager.FindByNameAsync(user.Identity.Name);
            if (dbUser == null) return 0;

            _id = dbUser.Id;
            return _id.Value;
        }
    }
}