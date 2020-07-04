using System;
using System.Collections.Generic;
using AdvertApi.Models;

namespace AdvertApi.Services.Impl
{
    public class BannerAreaCalculatorService : IBannerAreaCalculatorService
    {
        public ICollection<Banner> CalculateBanners(Building from, Building to)
        {
            
            double distance = Math.Abs(from.StreetNumber - to.StreetNumber);

            double firstBannerArea;
            double secondBannerArea;

            if (from.Height < to.Height)
            {
                firstBannerArea = from.Height * distance;
                secondBannerArea = to.Height;
            } else if (from.Height > to.Height)
            {
                firstBannerArea = to.Height * distance;
                secondBannerArea = from.Height;
            }
            else
            {
                firstBannerArea = to.Height * ((distance + 1) / 2);
                secondBannerArea = firstBannerArea;
            }

            var firstBanner = new Banner
            {
                Area = firstBannerArea,
                Name = "First Banner"
            };
            var secondBanner = new Banner
            {
                Area = secondBannerArea,
                Name = "Second Banner"
            };
            
            return new List<Banner>{firstBanner, secondBanner};
        }
    }
}