//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Http;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security;
//using Microsoft.Owin.Security.Cookies;
//using Microsoft.Owin.Security.OAuth;
//using Prekenweb.Models;
//using Prekenweb.Models.Identity;
//using Prekenweb.Models.Repository;
//using PrekenWeb.Security;

//namespace WebAPI.Controllers
//{
//    public class GebruikerController : ApiController
//    {
//        #region UserManager
//        private PrekenWebUserManager _prekenWebUserManager;

//        public PrekenWebUserManager PrekenWebUserManager
//        {
//            get
//            {
//                return _prekenWebUserManager ?? Request.GetOwinContext().GetUserManager<PrekenWebUserManager>(); 
//            }
//            private set
//            {
//                _prekenWebUserManager = value;
//            }
//        }
//        #endregion

//        // GET api/Account/ExternalLogin
//        [OverrideAuthentication]
//        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
//        [AllowAnonymous]
//        [Route("ExternalLogin", Name = "ExternalLogin")]
//        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
//        {
//            if (error != null)
//            {
//                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
//            }

//            if (!User.Identity.IsAuthenticated)
//            {
//                return new ChallengeResult(provider, this);
//            }

//            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

//            if (externalLogin == null)
//            {
//                return InternalServerError();
//            }

//            if (externalLogin.LoginProvider != provider)
//            {
//                Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
//                return new ChallengeResult(provider, this);
//            }

//            Gebruiker user = await PrekenWebUserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,  externalLogin.ProviderKey));

//            bool hasRegistered = user != null;

//            if (hasRegistered)
//            {
//                Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

//                ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(PrekenWebUserManager, OAuthDefaults.AuthenticationType);
//                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(PrekenWebUserManager, CookieAuthenticationDefaults.AuthenticationType);

//                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
//                Request.GetOwinContext().Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
//            }
//            else
//            {
//                IEnumerable<Claim> claims = externalLogin.GetClaims();
//                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
//                Request.GetOwinContext().Authentication.SignIn(identity);
//            }

//            return Ok();
//        }
//    }
//    public class ChallengeResult : IHttpActionResult
//    {
//        public ChallengeResult(string loginProvider, ApiController controller)
//        {
//            LoginProvider = loginProvider;
//            Request = controller.Request;
//        }

//        public string LoginProvider { get; set; }
//        public HttpRequestMessage Request { get; set; }

//        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
//        {
//            Request.GetOwinContext().Authentication.Challenge(LoginProvider);

//            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
//            response.RequestMessage = Request;
//            return Task.FromResult(response);
//        }
//    }

//    public class ExternalLoginData
//    {
//        public string LoginProvider { get; set; }
//        public string ProviderKey { get; set; }
//        public string UserName { get; set; }

//        public IList<Claim> GetClaims()
//        {
//            IList<Claim> claims = new List<Claim>();
//            claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

//            if (UserName != null)
//            {
//                claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
//            }

//            return claims;
//        }

//        public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
//        {
//            if (identity == null)
//            {
//                return null;
//            }

//            Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

//            if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
//                || String.IsNullOrEmpty(providerKeyClaim.Value))
//            {
//                return null;
//            }

//            if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
//            {
//                return null;
//            }

//            return new ExternalLoginData
//            {
//                LoginProvider = providerKeyClaim.Issuer,
//                ProviderKey = providerKeyClaim.Value,
//                UserName = identity.FindFirstValue(ClaimTypes.Name)
//            };
//        }
//    }

//    public static class RandomOAuthStateGenerator
//    {
//        private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

//        public static string Generate(int strengthInBits)
//        {
//            const int bitsPerByte = 8;

//            if (strengthInBits % bitsPerByte != 0)
//            {
//                throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
//            }

//            int strengthInBytes = strengthInBits / bitsPerByte;

//            byte[] data = new byte[strengthInBytes];
//            _random.GetBytes(data);
//            return HttpServerUtility.UrlTokenEncode(data);
//        }
//    }
//}
