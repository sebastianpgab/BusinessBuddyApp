using AutoMapper;
using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Exceptions;
using BusinessBuddyApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessBuddyApp.Services
{
    public interface IAddressService
    {
        public ICollection<Address> GetAll();
        public Task<Address> Get(int id);
        public Task<Address> Update(Address newAddress, int id);
        public void Create(AddressDto addressDto, int clientId);

    }
    public class AddressService : IAddressService
    {
        private readonly BusinessBudyDbContext _dbContext;
        private readonly IMapper _mapper;
        public AddressService(BusinessBudyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }
        public ICollection<Address> GetAll() 
        {
            var addresses = _dbContext.Addresses.ToList();
            if (addresses is not null)
            {
                return addresses;
            }
            throw new NotFoundException("Addresses not found");
        }

        public async Task<Address> Get(int id)
        {
            var address = await _dbContext.Addresses.FirstOrDefaultAsync(p => p.Id == id);
            if (address is not null)
            {
                return address;
            }
            throw new NotFoundException("Address not found");
        }

        public async Task<Address> Update(Address newAddress, int id)
        {
            var address = await _dbContext.Addresses.FirstOrDefaultAsync(p => p.Id == id);
            if( address is not null)
            {
                if(address.Street != null) { address.Street = newAddress.Street; }
                if(address.BuildingNumber != null) { address.BuildingNumber = newAddress.BuildingNumber; }
                if (address.ApartmentNumber != null) { address.ApartmentNumber = newAddress.ApartmentNumber; }
                if (address.PostalCode != null) { address.PostalCode = newAddress.PostalCode; }
                if (address.City != null) { address.City = newAddress.City; }
                _dbContext.SaveChanges();
                return address;
            }
            throw new NotFoundException("Address not found");
        }

        public void Create(AddressDto addressDto, int clientId)
        {
            var client = _dbContext.Clients.FirstOrDefault(p => p.Id == clientId);
            if(client != null)
            {
                addressDto.ClientId = clientId;
                var addressMapped = _mapper.Map<Address>(addressDto);
                _dbContext.Add(addressMapped);
                _dbContext.SaveChanges();
            }
            throw new NotFoundException("Client not found");
        }
    }
}
