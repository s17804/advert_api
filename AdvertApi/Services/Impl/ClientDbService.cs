using System.Linq;
using AdvertApi.Dto.Request;
using AdvertApi.Dto.Response;
using AdvertApi.Exceptions;
using AdvertApi.Models;
using AutoMapper;

namespace AdvertApi.Services.Impl
{
    public class ClientDbService : IClientDbService
    {
        private readonly AdvertDbContext _advertDbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        
        public ClientDbService(AdvertDbContext advertDbContext, IMapper mapper, IPasswordService passwordService)
        {
            _advertDbContext = advertDbContext;
            _mapper = mapper;
            _passwordService = passwordService;
        }
        
        public AddNewClientResponseDto AddNewClient(AddNewClientRequestDto addNewClientRequestDto)
        {
            if (_advertDbContext.Clients.Any(c => c.Login.Equals(addNewClientRequestDto.Login)))
            {
                throw new ObjectAlreadyInDatabaseException("Login already in database");
            }
            
            var client = _mapper.Map<Client>(addNewClientRequestDto);
            var salt = _passwordService.GenerateSalt();

            client.Salt = salt;
            client.Password = _passwordService.CreateSaltedPasswordHash(addNewClientRequestDto.Password, salt);

            _advertDbContext.Add((object) client);
            _advertDbContext.SaveChanges();

            return _mapper.Map<AddNewClientResponseDto>(client);
        }
    }
}