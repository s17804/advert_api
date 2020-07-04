using System;
using AdvertApi.Dto.Request;
using AdvertApi.Dto.Response;

namespace AdvertApi.Services
{
    public interface IClientDbService
    {
        AddNewClientResponseDto AddNewClient(AddNewClientRequestDto addNewClientRequestDto);

    }
}