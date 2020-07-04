using System;
using System.Collections.Generic;

namespace AdvertApi.Dto.Response
{
    public class CampaignResponseDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double PricePerSquareMeter { get; set; }
        public ICollection<BannerResponseDto> Banners { get; set; }
        
    }
}