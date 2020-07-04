using System.Linq;
using AdvertApi.Models;
using AdvertApi.Services.Impl;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AdvertApiTests.Services
{
    public class BannerAreaCalculatorServiceTests
    {
        
        [Fact]
        public void ShouldReturnTwoBannersWithDifferentArea()
        {
            // Arrange
            var service = new BannerAreaCalculatorService();
            var buildingFrom = new Building
            {
                City = "Testowo",
                Street = "Karmnowa",
                StreetNumber = 1,
                Height = 10
            };
            
            var buildingTo = new Building
            {
                City = "Testowo",
                Street = "Karmnowa",
                StreetNumber = 4,
                Height = 15
            };

            const double firstBannerArea = 30.0;
            const double secondBannerArea = 15.0;
            
            // Act
            var result = service.CalculateBanners(buildingFrom, buildingTo);
            
            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(firstBannerArea, result.Single(banner => 
                banner.Name.Equals("First Banner")).Area);
            Assert.AreEqual(secondBannerArea, result.Single(banner => 
                banner.Name.Equals("Second Banner")).Area);
        }
        
        [Fact]
        public void ShouldReturnTwoBannersWitEqualArea()
        {
            // Arrange
            var service = new BannerAreaCalculatorService();
            var buildingFrom = new Building
            {
                City = "Testowo",
                Street = "Karmnowa",
                StreetNumber = 1,
                Height = 15
            };
            
            var buildingTo = new Building
            {
                City = "Testowo",
                Street = "Karmnowa",
                StreetNumber = 4,
                Height = 15
            };

            const double firstBannerArea = 30.0;
            const double secondBannerArea = 30.0;
            
            // Act
            var result = service.CalculateBanners(buildingFrom, buildingTo);
            
            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(firstBannerArea, result.Single(banner => 
                banner.Name.Equals("First Banner")).Area);
            Assert.AreEqual(secondBannerArea, result.Single(banner => 
                banner.Name.Equals("Second Banner")).Area);
        }
    }
}