
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace PrekenWeb.Security
{
    public class JwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        public const string AudienceIdPropertyKey = "audienceId";
        public const string AudienceSecretPropertyKey = "audienceSecret";

        private readonly string _issuer;

        public JwtFormat(string issuer)
        {
            _issuer = issuer;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)   throw new ArgumentNullException(nameof(data)); 

            string audienceId = data.Properties.Dictionary.ContainsKey(AudienceIdPropertyKey) ? data.Properties.Dictionary[AudienceIdPropertyKey] : null;
            if (string.IsNullOrWhiteSpace(audienceId)) throw new InvalidOperationException($"AuthenticationTicket.Properties does not include {AudienceIdPropertyKey}");

            string audienceSecret = data.Properties.Dictionary.ContainsKey(AudienceSecretPropertyKey) ? data.Properties.Dictionary[AudienceSecretPropertyKey] : null;
            if (string.IsNullOrWhiteSpace(audienceSecret)) throw new InvalidOperationException($"AuthenticationTicket.Properties does not include {AudienceSecretPropertyKey}");

            var keyByteArray = TextEncodings.Base64Url.Decode(audienceSecret);

            var issued = data.Properties.IssuedUtc ?? DateTime.Now;
            var expires = data.Properties.ExpiresUtc ?? DateTime.Now.AddHours(1);

            var securityKey = new SymmetricSecurityKey(keyByteArray);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued.UtcDateTime, expires.UtcDateTime, signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}