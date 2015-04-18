using System;
using System.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Thinktecture.IdentityModel.Tokens;

namespace PrekenWeb.Security
{
    public class JwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        public const string AudienceIdPropertyKey = "audienceId";
        public const string AudienceSecretPropertyKey = "audienceSecret";

        private readonly string _issuer = string.Empty;

        public JwtFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)   throw new ArgumentNullException("data"); 

            string audienceId = data.Properties.Dictionary.ContainsKey(AudienceIdPropertyKey) ? data.Properties.Dictionary[AudienceIdPropertyKey] : null;
            if (string.IsNullOrWhiteSpace(audienceId)) throw new InvalidOperationException(string.Format("AuthenticationTicket.Properties does not include {0}", AudienceIdPropertyKey));

            string audienceSecret = data.Properties.Dictionary.ContainsKey(AudienceSecretPropertyKey) ? data.Properties.Dictionary[AudienceSecretPropertyKey] : null;
            if (string.IsNullOrWhiteSpace(audienceSecret)) throw new InvalidOperationException(string.Format("AuthenticationTicket.Properties does not include {0}", AudienceSecretPropertyKey));

            var keyByteArray = TextEncodings.Base64Url.Decode(audienceSecret);
            var signingKey = new HmacSigningCredentials(keyByteArray);

            var issued = data.Properties.IssuedUtc ?? DateTime.Now;
            var expires = data.Properties.ExpiresUtc ?? DateTime.Now.AddHours(1);

            var token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued.UtcDateTime, expires.UtcDateTime, signingKey);

            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}