using AutoMapper;
using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Models;

namespace BusinessBuddyApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<SourceType, DestinationType>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<AddressDto, Address>();
            CreateMap<ClientDto, Client>();
        }
    }
}
