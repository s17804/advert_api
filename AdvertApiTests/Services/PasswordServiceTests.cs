using AdvertApi.Services.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AdvertApiTests.Services
{
    public class PasswordServiceTests
    {
        
        [Fact]
        public void ShouldReturnFixedSizeSalt()
        {
            // Arrange
            var service = new PasswordService();
            const int saltLength = 32;
            
            // Act
            var result = service.GenerateSalt();
            
            // Assert
            Assert.AreEqual(saltLength, result.Length);
        }
        
        [Fact]
        public void ShouldReturnIdenticalHashedPassword()
        {
            // Arrange
            var service = new PasswordService();
            var salt = new byte[]{97, 155, 86, 162, 177, 155, 41, 91, 174, 19};
            const string password = "AlaMaKoty2";
            const string hashedPassword = "KIOZA20iN8KpXQoDbBvBeUEqoiEmJR+4+7ZN24jbRn8=";
            
            // Act
            var result = service.CreateSaltedPasswordHash(password, salt);
            
            // Assert
            Assert.AreEqual(hashedPassword, result);
        }
        
        [Fact]
        public void ShoudlReturnDifferentHashedPassword()
        {
            // Arrange
            var service = new PasswordService();
            var salt = new byte[]{97, 155, 86, 162, 177, 155, 41, 91, 174, 20};
            const string password = "AlaMaKoty2";
            const string hashedPassword = "KIOZA20iN8KpXQoDbBvBeUEqoiEmJR+4+7ZN24jbRn8=";
            
            // Act
            var result = service.CreateSaltedPasswordHash(password, salt);
            
            // Assert
            Assert.AreNotEqual(hashedPassword, result);
        }
        
        [Fact]
        public void ShouldReturnTrueWhenTwoHashedPasswordAreIdentical()
        {
            // Arrange
            var service = new PasswordService();
            var salt = new byte[]{97, 155, 86, 162, 177, 155, 41, 91, 174, 19};
            const string password = "AlaMaKoty2";
            const string hashedPassword = "KIOZA20iN8KpXQoDbBvBeUEqoiEmJR+4+7ZN24jbRn8=";
            
            // Act
            var result = service.ValidatePassword(password, hashedPassword, salt);
            
            // Assert
            Assert.IsTrue(result);
        }
        
        [Fact]
        public void ShouldReturnFalseWhenTwoHashedPasswordAreNotIdentical()
        {
            // Arrange
            var service = new PasswordService();
            var salt = new byte[]{97, 155, 86, 162, 177, 155, 41, 91, 174, 19};
            const string password = "AlaMaKoty2";
            const string hashedPassword = "KIOZA20iN8KpXQoDbBvBeUEqoiEmJR+4+7ZN24jbRn8+";
            
            // Act
            var result = service.ValidatePassword(password, hashedPassword, salt);
            
            // Assert
            Assert.IsFalse(result);
        }
    }
}