using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNet.Identity;

namespace PrekenWeb.Security
{
    public class PrekenWebPasswordHasher : PasswordHasher
    {
        //public string HashPassword(string wachtwoord)
        //{
        //    var salt = createSalt512();
        //    return generateHMAC(wachtwoord, salt); 
        //}

        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword)) return PasswordVerificationResult.Failed;

            var hashedPasswordParts = hashedPassword.Split('|');
            if (hashedPasswordParts.Length != 2) return base.VerifyHashedPassword(hashedPassword, providedPassword);

            var gebruikerPasswordHash = hashedPasswordParts[0];
            var gebruikerSalt = hashedPasswordParts[1];

            var providedPasswordHash = generateHMAC(providedPassword, gebruikerSalt);

            return gebruikerPasswordHash.Equals(providedPasswordHash) ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }

        //private static string randomString(int size, bool lowerCase)
        //{
        //    var builder = new StringBuilder();
        //    var random = new Random();
        //    for (int i = 0; i < size; i++)
        //    {
        //        char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
        //        builder.Append(ch);
        //    }
        //    return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        //}

        //private static string createSalt512()
        //{
        //    var message = randomString(512, false);
        //    return BitConverter.ToString((new SHA512Managed()).ComputeHash(Encoding.ASCII.GetBytes(message))).Replace("-", "");
        //}

        private string generateHMAC(string clearMessage, string secretKeyString)
        {
            var encoder = new ASCIIEncoding();
            var messageBytes = encoder.GetBytes(clearMessage);
            var secretKeyBytes = new byte[secretKeyString.Length / 2];
            for (int index = 0; index < secretKeyBytes.Length; index++)
            {
                string byteValue = secretKeyString.Substring(index * 2, 2);
                secretKeyBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
            var hmacsha512 = new HMACSHA512(secretKeyBytes);

            byte[] hashValue = hmacsha512.ComputeHash(messageBytes);

            string hmac = "";
            foreach (byte x in hashValue)
            {
                hmac += String.Format("{0:x2}", x);
            }

            return hmac.ToUpper();
        }
    }
}