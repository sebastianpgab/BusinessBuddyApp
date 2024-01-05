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
            CreateMap<InvoiceDto, Invoice>();
            CreateMap<Order, OrderDto>()
                .ForMember(p => p.FirstName, c => c.MapFrom(s => s.Client.FirstName))
                .ForMember(p => p.LastName, c => c.MapFrom(s => s.Client.LastName))
                .ForMember(p => p.TaxNumber, c => c.MapFrom(s => s.Client.TaxNumber))
                .ForMember(p => p.PhoneNumber, c => c.MapFrom(s => s.Client.PhoneNumber))
                .ForMember(p => p.Email, c => c.MapFrom(s => s.Client.Email))
                .ForMember(p => p.FirstName, c => c.MapFrom(s => s.Client.FirstName))
                .ForMember(p => p.FirstName, c => c.MapFrom(s => s.Client.FirstName))
                .ForMember(p => p.FirstName, c => c.MapFrom(s => s.Client.FirstName));


        }
    }
}
