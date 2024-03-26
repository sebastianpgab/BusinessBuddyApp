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
        public Task<Address> Get(int clientId);
        public Task<Address> Update(Address newAddress, int id);
        public Task Create(AddressDto addressDto, int clientId);

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
            if (!addresses.Any())
            {
                throw new NotFoundException("Addresses not found");
            }
            return addresses;
        }

        public async Task<Address> Get(int clientId)
        {
            var addresses = await _dbContext.Addresses.ToListAsync();
            var address = await _dbContext.Addresses.FirstOrDefaultAsync(p => p.ClientId == clientId);
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

        public async Task Create(AddressDto addressDto, int clientId)
        {
            var client = _dbContext.Clients.FirstOrDefault(p => p.Id == clientId);
            if(client != null)
            {
                addressDto.ClientId = clientId;
                var addressMapped = _mapper.Map<Address>(addressDto);
                _dbContext.Add(addressMapped);
                await _dbContext.SaveChangesAsync();
            }
            throw new NotFoundException("Client not found");
        }
    }
}
