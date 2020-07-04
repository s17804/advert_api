using AdvertApi.Dto.Request;
using AdvertApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertApi.Controllers
{
    [ApiController]
    [Route("api/campaign")]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignDbService _campaignDbService;

        public CampaignController(ICampaignDbService campaignDbService)
        {
            _campaignDbService = campaignDbService;
        }

        [HttpGet]
        [Route("getClientCampaignList")]
        [Authorize(Roles = "client")]
        public IActionResult GetClientCampaignList()
        {
            return Ok(_campaignDbService.GetClientCampaignList());
        }

        [HttpPost]
        [Route("addNewCampaign")]
        [AllowAnonymous]
        public IActionResult AddNewCampaign(NewCampaignRequestDto newCampaignRequestDto)
        {
            return Created("", _campaignDbService.AddNewCampaign(newCampaignRequestDto));
        }
        
    }
}