using System;

namespace AdvertApi.Dto.Request
{
    public class NewCampaignRequestDto
    {
        public int IdClient { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public double PricePerSquareMeter { get; set; }
        public int FromIdBuilding { get; set; }
        public int ToIdBuilding { get; set; }
    }
}