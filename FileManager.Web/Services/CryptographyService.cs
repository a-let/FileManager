using FileManager.Web.Services.Interfaces;

using System;
using System.Security.Cryptography;
using System.Text;

namespace FileManager.Web.Services
{
    public class CryptographyService : ICryptographyService
    {
        public void CreateHash(string value, out byte[] hash, out byte[] salt)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("Value cannot be empty.");

            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(value));
            }
        }

        public bool VerifyHash(string value, byte[] hash, byte[] salt)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("Value cannot be empty.");

            using (var hmac = new HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(value));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != hash[i])
                        return false;
                }
            }

            return true;
        }
    }
}