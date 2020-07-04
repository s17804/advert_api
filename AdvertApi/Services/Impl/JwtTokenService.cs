using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AdvertApi.Dto.Request;
using AdvertApi.Dto.Response;
using AdvertApi.Exceptions;
using AdvertApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AdvertApi.Services.Impl
{
    public class JwtTokenService : IJwtTokenService
    {
        private IConfiguration Configuration { get; }
        private readonly AdvertDbContext _advertDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPasswordService _passwordService;

        
        public JwtTokenService(IConfiguration configuration, AdvertDbContext advertDbContext, 
            IHttpContextAccessor httpContextAccessor, IPasswordService passwordService)
        {
            Configuration = configuration;
            _advertDbContext = advertDbContext;
            _httpContextAccessor = httpContextAccessor;
            _passwordService = passwordService;
        }

        public TokenResponseDto LogIn(LogInRequestDto logInRequestDto)
        {

            var client = _advertDbContext.Clients.FirstOrDefault(c => c.Login.Equals(logInRequestDto.Login));
            
            if (client == null)
            {
                throw new BadLoginOrPasswordException("Bad Login or Password");
            }
            
            var salt = client.Salt;
            var storedPassword = client.Password;
            
            if (!_passwordService.ValidatePassword(logInRequestDto.Password, storedPassword, salt))
            {
                throw new BadLoginOrPasswordException("Bad Login or Password");
            }

            var accessToken = CreateJwtToken(logInRequestDto.Login);
            var refreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            client.RefreshToken = refreshToken;

            _advertDbContext.SaveChanges();
            
            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public TokenResponseDto RefreshJwtToken(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var client = _advertDbContext.Clients.FirstOrDefault(c => c.RefreshToken.Equals(
                refreshTokenRequestDto.RefreshToken));

            if (client == null)
            {
                throw new ResourceNotFoundException("Refresh token doesn't exist");
            }
            
            var accessToken = CreateJwtToken(client.Login);
            var newRefreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            client.RefreshToken = newRefreshToken;
            _advertDbContext.SaveChanges();
                
            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken
            };
        }

        public string GetClaimByName(string claimName)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString()
                .Replace("Bearer ", string.Empty);
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
           return jwtToken.Claims.First(claim => claim.Type == claimName).Value;
        }

        private string CreateJwtToken(string login)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Login", login),
                    new Claim(ClaimTypes.Role, "client")
                }),
                Expires = DateTime.Now.AddHours(1),
                Issuer = "Advert",
                Audience = "Clients",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"])), 
                    SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}