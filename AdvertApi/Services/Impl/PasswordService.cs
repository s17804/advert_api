using System;
using System.Security.Cryptography;

namespace AdvertApi.Services.Impl
{
    public class PasswordService : IPasswordService
    {
        private const int SaltLength = 32;
        private const int KeyLength = 32;
        private const int Iterations = 10000;

        public string CreateSaltedPasswordHash(string password, byte[] salt)
        {
            var hashValue = GenerateHashValue(password, salt);
            return Convert.ToBase64String(hashValue);
        }
        
        public byte[] GenerateSalt()
        {
            var randomNumberGenerator = new RNGCryptoServiceProvider();
            var salt = new byte[SaltLength];
            randomNumberGenerator.GetBytes(salt);
            return salt;
        }
        
        public bool ValidatePassword(string receivedPassword, string storesPassword, byte[] salt)
        {
            var receivedPasswordHash = CreateSaltedPasswordHash(receivedPassword, salt);
            return storesPassword.Equals(receivedPasswordHash);
        } 
        
        private static byte[] GenerateHashValue(string password, byte[] salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            return pbkdf2.GetBytes(KeyLength);
        }
    }
}