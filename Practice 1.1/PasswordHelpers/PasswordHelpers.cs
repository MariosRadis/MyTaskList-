using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Practice_1._1.PasswordHelpers
{
    public class PasswordHelpers
    {
        public static class Passhash
        {
            private static int _saltLengthLimit = 32;
            public static byte[] GetSalt()
            {
                return GetSalt(_saltLengthLimit);
            }
            public static byte[] GetSalt(int maximumSaltLength)
            {
                var salt = new byte[maximumSaltLength];
                using (var random = new RNGCryptoServiceProvider())
                {
                    random.GetNonZeroBytes(salt);
                }

                return salt;
            }
            public static byte[] Hash(string value, byte[] salt)
            {
                return Hash(Encoding.UTF8.GetBytes(value), salt);
            }
            public static byte[] Hash(byte[] value, byte[] salt)
            {
                byte[] saltedValue = value.Concat(salt).ToArray();
                // Alternatively use CopyTo.
                //var saltedValue = new byte[value.Length + salt.Length];
                //value.CopyTo(saltedValue, 0);
                //salt.CopyTo(saltedValue, value.Length);

                return new SHA256Managed().ComputeHash(saltedValue);
            }
        }
    }
}