using System;
using System.Linq;
using AdvertApi.Dto.Request;
using AdvertApi.Dto.Response;
using AdvertApi.Exceptions;
using AdvertApi.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdvertApi.Services.Impl
{
    public class CampaignDbService : ICampaignDbService
    {
        private readonly AdvertDbContext _advertDbContext;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IBannerAreaCalculatorService _bannerAreaCalculatorService;
        private readonly IMapper _mapper;

        public CampaignDbService(AdvertDbContext advertDbContext, IJwtTokenService jwtTokenService, 
            IBannerAreaCalculatorService bannerAreaCalculatorService, IMapper mapper)
        {
            _advertDbContext = advertDbContext;
            _jwtTokenService = jwtTokenService;
            _bannerAreaCalculatorService = bannerAreaCalculatorService;
            _mapper = mapper;
        }

        public NewCampaignResponseDto AddNewCampaign(NewCampaignRequestDto newCampaignRequestDto)
        {
            
            if (newCampaignRequestDto.FromIdBuilding.Equals(newCampaignRequestDto.ToIdBuilding))
            {
                throw new BadRequestException("Selected only one building");
            }

            var startDate = DateTime.Parse(newCampaignRequestDto.StartDate);
            var endDate = DateTime.Parse(newCampaignRequestDto.EndDate);
            
            var compareDate = DateTime.Compare(startDate, endDate);
            if (compareDate == 1 || compareDate == 0)
            {
                throw new BadRequestException("Bad date");
            }
            
            var client = _advertDbContext.Clients.FirstOrDefault(c =>
                c.IdClient.Equals(newCampaignRequestDto.IdClient));

            if (client == null)
            {
                throw new ResourceNotFoundException("Client not found");
            }

            var buildingFrom = _advertDbContext.Buildings.FirstOrDefault(b =>
                b.IdBuilding.Equals(newCampaignRequestDto.FromIdBuilding));
            
            if (buildingFrom == null)
            {
                throw new ResourceNotFoundException("Building not found");
            }
            
            var buildingTo = _advertDbContext.Buildings.FirstOrDefault(b =>
                b.IdBuilding.Equals(newCampaignRequestDto.ToIdBuilding));
            
            if (buildingTo == null)
            {
                throw new ResourceNotFoundException("Building not found");
            }
            
            if (!buildingFrom.City.Equals(buildingTo.City))
            {
                throw new BadRequestException("Building not located in same city");
            }

            if (!buildingFrom.Street.Equals(buildingTo.Street))
            {
                throw new BadRequestException("Building not located on same street");
            }

            var campaign = new Campaign
            {
                Client = client,
                BuildingFrom = buildingFrom,
                BuildingTo = buildingTo,
                StartDate = startDate,
                EndDate = endDate,
                PricePerSquareMeter = newCampaignRequestDto.PricePerSquareMeter,
                Banners = _bannerAreaCalculatorService.CalculateBanners(buildingFrom, buildingTo)
            };
            
            foreach (var campaignBanner in campaign.Banners)
            {
                campaignBanner.Price = campaignBanner.Area * campaign.PricePerSquareMeter;
            }

            _advertDbContext.Add(campaign);
            _advertDbContext.SaveChanges();

            return _mapper.Map<NewCampaignResponseDto>(campaign);
        }

        public ClientResponseDto GetClientCampaignList()
        {
            var login = _jwtTokenService.GetClaimByName("Login");

            var client = _advertDbContext.Clients.Where(c => login.Equals(c.Login))
                .Include(c => c.Campaigns)
                .ThenInclude(c => c.Banners)
                .FirstOrDefault();

            if (client == null)
            {
                throw new ResourceNotFoundException("No such client");
            }

            var clientResponseDto = new ClientResponseDto
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                Phone = client.Phone,
                Campaigns = client.Campaigns.Select(c =>
                    new CampaignResponseDto
                    {
                        StartDate = c.StartDate,
                        EndDate = c.EndDate,
                        PricePerSquareMeter = c.PricePerSquareMeter,
                        Banners = c.Banners.Select(b => 
                            new BannerResponseDto
                            {
                                Name = b.Name,
                                Area = b.Area,
                                Price = b.Price
                            }).ToList()
                    }).ToList()
            };

            clientResponseDto.Campaigns = clientResponseDto.Campaigns
                .OrderByDescending(c => c.StartDate).ToList();
            
            return clientResponseDto;
        }
    }
}