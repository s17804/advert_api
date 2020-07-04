using AdvertApi.Dto.Request;
using AdvertApi.Dto.Response;
using AdvertApi.Models;
using AutoMapper;

namespace AdvertApi.Mapper
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<AddNewClientRequestDto, Client>()
                .ForMember(dest => dest.Password, 
                    opt => opt.Ignore())
                .ForMember(dest => dest.IdClient, 
                    opt => opt.Ignore());
            CreateMap<Client, AddNewClientResponseDto>();
            CreateMap<Client, ClientResponseDto>();
        }
    }
}