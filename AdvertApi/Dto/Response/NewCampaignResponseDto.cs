using System;
using System.Collections.Generic;

namespace AdvertApi.Dto.Response
{
    public class NewCampaignResponseDto
    {
        public int IdClient { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PricePerSquareMeter { get; set; }
        public int FromIdBuilding { get; set; }
        public int ToIdBuilding { get; set; }
        public double TotalPrice { get; set; }

        public ICollection<BannerResponseDto> Banners { get; set; }
    }
}