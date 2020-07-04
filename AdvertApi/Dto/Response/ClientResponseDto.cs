using System.Collections.Generic;

namespace AdvertApi.Dto.Response
{
    public class ClientResponseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public ICollection<CampaignResponseDto> Campaigns { get; set; }
        
        
    }
}