using System.Linq;
using AdvertApi.Dto.Response;
using AdvertApi.Models;
using AutoMapper;

namespace AdvertApi.Mapper
{
    public class CampaignProfile : Profile
    {
        public CampaignProfile()
        {
            CreateMap<Campaign, NewCampaignResponseDto>()
                .ForMember(dest => dest.IdClient, 
                    opt => 
                        opt.MapFrom(campaign => campaign.Client.IdClient ))
                .ForMember(dest => dest.FromIdBuilding, 
                    opt => 
                        opt.MapFrom(campaign => campaign.BuildingFrom.IdBuilding))
                .ForMember(dest => dest.ToIdBuilding, 
                    opt => 
                        opt.MapFrom(campaign => campaign.BuildingTo.IdBuilding))
                .ForMember(dest => dest.TotalPrice, 
                    opt =>
                        opt.MapFrom(campaign => campaign.Banners.Sum(banner => banner.Price)));
        }
    }
}