using Microsoft.AspNetCore.Mvc;

namespace AdvertApi.Models
{
    public class Banner
    {
        public int IdAdvertisement { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Area { get; set; }

        public Campaign Campaign { get; set; }
    }
}