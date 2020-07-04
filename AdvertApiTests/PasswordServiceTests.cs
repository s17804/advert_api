using System;
using AdvertApi.Services.Impl;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvertApiTests
{
    [TestClass]
    public class PasswordServiceTests
    {
        private const int KeyLength = 32;
        private const int Iterations = 10000;
        private PasswordService _service;
        
        [TestInitialize]
        public void InitializeTests()
        {
            _service = new PasswordService();
        }

        [TestMethod]
        public void CheckSaltLength()
        {
            // Arrange
            const int saltLength = 32;
            
            // Act
            var result = _service.GenerateSalt();
            
            // Assert
            Assert.AreEqual(saltLength, result.Length);
        }
        
        [TestMethod]
        public void CheckIfGeneratesRepeatingPasswordHash()
        {
            // Arrange
            var salt = new byte[]{97, 155, 86, 162, 177, 155, 41, 91, 174, 19};
            const string password = "AlaMaKoty2";
            const string hashedPassword = "KIOZA20iN8KpXQoDbBvBeUEqoiEmJR+4+7ZN24jbRn8=";
            
            // Act
            var result = _service.CreateSaltedPasswordHash(password, salt);
            
            // Assert
            Assert.AreEqual(hashedPassword, result);
        }
        
        [TestMethod]
        public void CheckIfGeneratedPasswordHashDoesntMatchProvidedHash()
        {
            // Arrange
            var salt = new byte[]{97, 155, 86, 162, 177, 155, 41, 91, 174, 20};
            const string password = "AlaMaKoty2";
            const string hashedPassword = "KIOZA20iN8KpXQoDbBvBeUEqoiEmJR+4+7ZN24jbRn8=";
            
            // Act
            var result = _service.CreateSaltedPasswordHash(password, salt);
            
            // Assert
            Assert.AreNotEqual(hashedPassword, result);
        }
        
        [TestMethod]
        public void CheckIfPasswordsAreIdentical()
        {
            // Arrange
            var salt = new byte[]{97, 155, 86, 162, 177, 155, 41, 91, 174, 19};
            const string password = "AlaMaKoty2";
            const string hashedPassword = "KIOZA20iN8KpXQoDbBvBeUEqoiEmJR+4+7ZN24jbRn8=";
            
            // Act
            var result = _service.ValidatePassword(password, hashedPassword, salt);
            
            // Assert
            Assert.IsTrue(result);
        }
    }
}