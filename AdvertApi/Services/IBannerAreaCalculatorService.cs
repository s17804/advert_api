using System.Collections.Generic;
using AdvertApi.Models;

namespace AdvertApi.Services
{
    public interface IBannerAreaCalculatorService
    {
        ICollection<Banner> CalculateBanners(Building from, Building to);
    }
}