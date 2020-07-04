using AdvertApi.Dto.Request;
using AdvertApi.Dto.Response;

namespace AdvertApi.Services
{
    public interface ICampaignDbService
    {
        NewCampaignResponseDto AddNewCampaign(NewCampaignRequestDto newCampaignRequestDto);

        ClientResponseDto GetClientCampaignList();
    }
}