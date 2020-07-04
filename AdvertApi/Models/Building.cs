using System.Collections;
using System.Collections.Generic;

namespace AdvertApi.Models
{
    public class Building
    {
        public int IdBuilding { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public string City { get; set; }
        public double Height { get; set; }

        public ICollection<Campaign> CampaignsFrom { get; set; }
        public ICollection<Campaign> CampaignsTo { get; set; }
    }
}