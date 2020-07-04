using AdvertApi.Dto.Request;
using AdvertApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertApi.Controllers
{
    
    [ApiController]
    [Route("api/client")]
    public class ClientController : ControllerBase
    {

        private readonly IClientDbService _clientDbService;
        private readonly IJwtTokenService _jwtTokenService;

        public ClientController(IClientDbService clientDbService, IJwtTokenService jwtTokenService)
        {
            _clientDbService = clientDbService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        [Route("addNewClient")]
        [AllowAnonymous]
        public IActionResult AddNewClient(AddNewClientRequestDto addNewClientRequestDto)
        {
            return Created("",_clientDbService.AddNewClient(addNewClientRequestDto));
        }

        [HttpPost]
        [Route("refreshAccessToken")]
        [AllowAnonymous]
        public IActionResult RefreshAccessToken(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            return Ok(_jwtTokenService.RefreshJwtToken(refreshTokenRequestDto));
        }

        [HttpPost]
        [Route("logIn")]
        [AllowAnonymous]
        public IActionResult LogIn(LogInRequestDto logInRequestDto)
        {
            return Ok(_jwtTokenService.LogIn(logInRequestDto));
        }
    }
}