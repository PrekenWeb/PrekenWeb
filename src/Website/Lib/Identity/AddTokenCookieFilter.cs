using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using PrekenWeb.Security;

namespace Prekenweb.Website.Lib.Identity
{
    public class AddTokenCookieAttribute : FilterAttribute
    {
        // This is the attribute used on action methods, since the actual filter (AddTokenCookieFilter) takes some dependencies its overriden by the IoC container
    }

    /// <summary>
    /// Use AddTokenCookieAttribute to mark action methods (constructor methods are resolved by IoC container)
    /// </summary>
    public class AddTokenCookieFilter : ActionFilterAttribute
    {
        private readonly string _audienceId;
        private readonly string _audienceSecret;
        private readonly IPrekenWebUserManager _prekenWebUserManager;
        private readonly IHuidigeGebruiker _huidigeGebruiker;
        private const string CookieKey = "Token";

        public AddTokenCookieFilter(string audienceId, string audienceSecret, IPrekenWebUserManager prekenWebUserManager, IHuidigeGebruiker huidigeGebruiker)
        {
            _audienceId = audienceId;
            _audienceSecret = audienceSecret;
            _prekenWebUserManager = prekenWebUserManager;
            _huidigeGebruiker = huidigeGebruiker;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (filterContext.HttpContext.Request.Cookies[CookieKey] == null) // cookie expired?
                {
                    InjectTokenCookie(filterContext.HttpContext.Response, filterContext.HttpContext.User);
                }
            }
            else
            {
                // unauthenticated, be sure no cookie is added
                filterContext.HttpContext.Response.Cookies.Remove(CookieKey);
            }

            base.OnActionExecuted(filterContext);
        }

        public void InjectTokenCookie(HttpResponseBase httpResponseBase, IPrincipal user)
        {
            var gebruikerIdentity = GetGebruikerIdentity(_prekenWebUserManager, _huidigeGebruiker, user);

            var ticket = GetAuthenticationTicket(gebruikerIdentity);

            var token = GetToken(ticket);

            var cookie = new HttpCookie(CookieKey, token);
            if (ticket.Properties.ExpiresUtc != null)
                cookie.Expires = DateTime.Now.AddMinutes(5);
            httpResponseBase.Cookies.Set(cookie);
        }

        private string GetToken(AuthenticationTicket ticket)
        {
            var jwtFormat = new JwtFormat("PrekenWeb.Website");
            return jwtFormat.Protect(ticket);
        }

        private AuthenticationTicket GetAuthenticationTicket(ClaimsIdentity gebruikerIdentity)
        {
            var authenticationProperties = new AuthenticationProperties(new Dictionary<string, string>
            {
                {JwtFormat.AudienceIdPropertyKey, _audienceId},
                {JwtFormat.AudienceSecretPropertyKey, _audienceSecret}
            });

            var ticket = new AuthenticationTicket(gebruikerIdentity, authenticationProperties);

            var currentUtc = new Microsoft.Owin.Infrastructure.SystemClock().UtcNow;
            ticket.Properties.IssuedUtc = currentUtc;
            ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(8));
            return ticket;
        }

        private ClaimsIdentity GetGebruikerIdentity(IPrekenWebUserManager prekenWebUserManager, IHuidigeGebruiker huidigeGebruiker, IPrincipal user)
        {
            var gebruikerId = AsyncHelper.RunSync(() => huidigeGebruiker.GetId(prekenWebUserManager, user));
            if (gebruikerId == 0) throw new Exception("Cannot create a token: invalid user");

            var gebruiker = AsyncHelper.RunSync(() => prekenWebUserManager.FindByIdAsync(gebruikerId));
            if (gebruiker == null) throw new InvalidOperationException("Cannot create a token: user not found!");

            var gebruikerIdentity = AsyncHelper.RunSync(() => prekenWebUserManager.CreateIdentityAsync(gebruiker, DefaultAuthenticationTypes.ExternalBearer));
            return gebruikerIdentity;
        }
    }
}