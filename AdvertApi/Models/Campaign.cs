using System;
using System.Collections.Generic;

namespace AdvertApi.Models
{
    public class Campaign
    {
        public int IdCampaign { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double PricePerSquareMeter { get; set; }

        public Client Client { get; set; }
        public Building BuildingFrom { get; set; }
        public Building BuildingTo { get; set; }

        public ICollection<Banner> Banners { get; set; }
    }
}