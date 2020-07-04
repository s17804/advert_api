using System.Linq;
using AdvertApi.Models;
using AdvertApi.Services.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdvertApiTests
{
    [TestClass]
    public class BannerAreaCalculatorServiceTests
    {
        private BannerAreaCalculatorService _service;

        [TestInitialize]
        public void InitializeTests()
        {
            _service = new BannerAreaCalculatorService();
        }
        
        [TestMethod]
        public void CheckIfMethodCalculatesCorrectBannerAreaWhenBuildingsSizeNotEqual()
        {
            // Arrange
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
            var banners = _service.CalculateBanners(buildingFrom, buildingTo);
            
            // Assert
            Assert.AreEqual(firstBannerArea, banners.Single(banner => 
                banner.Name.Equals("First Banner")).Area);
            Assert.AreEqual(secondBannerArea, banners.Single(banner => 
                banner.Name.Equals("Second Banner")).Area);
        }
        
        [TestMethod]
        public void CheckIfMethodCalculatesCorrectBannerAreaWhenBuildingsSizeIsEqual()
        {
            // Arrange
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
            var banners = _service.CalculateBanners(buildingFrom, buildingTo);
            
            // Assert
            Assert.AreEqual(firstBannerArea, banners.Single(banner => 
                banner.Name.Equals("First Banner")).Area);
            Assert.AreEqual(secondBannerArea, banners.Single(banner => 
                banner.Name.Equals("Second Banner")).Area);
        }
    }
}