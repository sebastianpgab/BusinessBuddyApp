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
                //Client
                .ForMember(p => p.FirstName, c => c.MapFrom(s => s.Client.FirstName))
                .ForMember(p => p.LastName, c => c.MapFrom(s => s.Client.LastName))
                .ForMember(p => p.TaxNumber, c => c.MapFrom(s => s.Client.TaxNumber))
                .ForMember(p => p.PhoneNumber, c => c.MapFrom(s => s.Client.PhoneNumber))
                .ForMember(p => p.Email, c => c.MapFrom(s => s.Client.Email))
                 //Address
                .ForMember(p => p.Street, c => c.MapFrom(s => s.OrderDetail.DeliveryAddress.Street))
                .ForMember(p => p.BuildingNumber, c => c.MapFrom(s => s.OrderDetail.DeliveryAddress.BuildingNumber))
                .ForMember(p => p.ApartmentNumber, c => c.MapFrom(s => s.OrderDetail.DeliveryAddress.ApartmentNumber))
                .ForMember(p => p.PostalCode, c => c.MapFrom(s => s.OrderDetail.DeliveryAddress.PostalCode))
                .ForMember(p => p.City, c => c.MapFrom(s => s.OrderDetail.DeliveryAddress.City))
                //OrderDetail
                .ForMember(p => p.OrderDate, c => c.MapFrom(s => s.OrderDetail.OrderDate))
                .ForMember(p => p.CompletionDate, c => c.MapFrom(s => s.OrderDetail.CompletionDate))
                .ForMember(p => p.Status, c => c.MapFrom(s => s.OrderDetail.Status))
                .ForMember(p => p.Notes, c => c.MapFrom(s => s.OrderDetail.Notes))
                .ForMember(p => p.PaymentMethod, c => c.MapFrom(s => s.OrderDetail.PaymentMethod))
                .ForMember(p => p.FinalAmount, c => c.MapFrom(s => s.OrderDetail.FinalAmount))
                .ForMember(p => p.DeliveryId, c => c.MapFrom(s => s.OrderDetail.DeliveryId))
                // Order Product
                .ForMember(p => p.TotalAmount, c => c.MapFrom(s => s.OrderDetail.OrderProducts.Select(p => p.TotalAmount)))
                .ForMember(p => p.Quantity, c => c.MapFrom(s => s.OrderDetail.OrderProducts.Select(p => p.Quantity)))
                .ForMember(p => p.ProductId, c => c.MapFrom(s => s.OrderDetail.OrderProducts.Select(p => p.ProductId)))
                //Product
                .ForMember(p => p.ProductType, c => c.MapFrom(s => s.OrderDetail.OrderProducts.Select(p => p.Product.ProductType)))
                .ForMember(p => p.Price, c => c.MapFrom(s => s.OrderDetail.OrderProducts.Select(p => p.Product.Price)))
                .ForMember(p => p.Color, c => c.MapFrom(s => s.OrderDetail.OrderProducts.Select(p => p.Product.Color)))
                //Invoice
                .ForMember(p => p.InvoiceNumber, c => c.MapFrom(s => s.Invoice.InvoiceNumber))
                .ForMember(p => p.InvoiceDate, c => c.MapFrom(s => s.Invoice.InvoiceDate))
                .ForMember(p => p.DueDate, c => c.MapFrom(s => s.Invoice.DueDate))
                .ForMember(p => p.IsPaid, c => c.MapFrom(s => s.Invoice.IsPaid));
        }
    }
}
