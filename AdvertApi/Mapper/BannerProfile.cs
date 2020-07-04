using AdvertApi.Dto.Response;
using AdvertApi.Models;
using AutoMapper;

namespace AdvertApi.Mapper
{
    public class BannerProfile : Profile
    {
        public BannerProfile()
        {
            CreateMap<Banner, BannerResponseDto>();
        }
    }
}