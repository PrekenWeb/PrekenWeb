using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using PrekenWeb.Security;

namespace Prekenweb.Website.Lib
{
    public interface IHuidigeGebruiker
    {
        int Id { get; }
    }

    public class HuidigeGebruiker : IHuidigeGebruiker
    {
        private int? _id;
        public int Id
        {
            get
            {
                if (_id.HasValue) return _id.Value;
                if (!HttpContext.Current.User.Identity.IsAuthenticated) return 0;

                var prekenWebUserManager = DependencyResolver.Current.GetService<IPrekenWebUserManager>() as PrekenWebUserManager;
                if (prekenWebUserManager == null) return 0;

                //var dbUser = prekenWebUserManager.FindByName(User.Identity.Name); // FindByName is an extension method which is almost not mock-able
                var dbUser = prekenWebUserManager.FindByNameAsync(HttpContext.Current.User.Identity.Name).Result;
                if (dbUser == null) return 0;

                _id = dbUser.Id;
                return _id.Value;
            }
        } 
    }
}