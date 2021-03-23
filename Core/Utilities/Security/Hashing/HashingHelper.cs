using System;
using System.Linq;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public static class HashingHelper
    {
        public static bool CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac512 = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac512.Key;
            passwordHash = hmac512.ComputeHash(Encoding.UTF8.GetBytes(password));
            return passwordHash != null;
        }

        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac512 = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            byte[] computedHash = hmac512.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i])
                {
                    return false;
                }
            }

            return true;
            //return passwordHash.Equals(computedHash);
            //return passwordHash.SequenceEqual(computedHash);
        }
    }
}