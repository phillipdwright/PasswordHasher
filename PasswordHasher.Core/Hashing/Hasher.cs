using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordHasher.Core.Hashing
{
    public class Hasher : IHasher
    {
        public string ToBase64EncodedSha512(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash;
            using (var sha512 = new SHA512Managed())
            {
                hash = sha512.ComputeHash(bytes);
            }
            return Convert.ToBase64String(hash);
        }
    }
}