
using System;
using AdvertApi.Dto.Request;
using AdvertApi.Dto.Response;

namespace AdvertApi.Services
{
    public interface IJwtTokenService
    {
        TokenResponseDto LogIn(LogInRequestDto loginRequestDto);
        TokenResponseDto RefreshJwtToken(RefreshTokenRequestDto refreshTokenRequestDto);

        string GetClaimByName(string claimName);

    }
}