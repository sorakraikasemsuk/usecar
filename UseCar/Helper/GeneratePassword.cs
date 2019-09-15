using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UseCar.Helper
{
    public static class GeneratePassword
    {
        private const int salt_length = 20;
        public static string PasswordCreate(string password,byte[] salt)
        {
            return Convert.ToBase64String(GenerateSaltedHash(Encoding.ASCII.GetBytes(password), salt));
        }
        public static bool PasswordCheck(string passwordInput,string salt,string password)
        {
            string passInputHash = Convert.ToBase64String(GenerateSaltedHash(Encoding.ASCII.GetBytes(passwordInput), Convert.FromBase64String(salt)));
            return passInputHash.Equals(password);
        }
        private static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }
        public static byte[] GetSalt()
        {
            var salt = new byte[salt_length];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }
    }
}
