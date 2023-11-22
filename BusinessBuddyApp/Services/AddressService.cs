using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BusinessBuddyApp.Services
{
    public interface IAddressService
    {
        public ICollection<Address> GetAll();
        public Task<Address> Get(int id);
        public Task<Address> Update(Address newAddress, int id);
        public bool Create(Address address, int clientId);

    }
    public class AddressService : IAddressService
    {
        private readonly BusinessBudyDbContext _dbContext;
        public AddressService(BusinessBudyDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public bool Create(Address address, int clientId)
        {
            if(address is not null)
            {
                address.ClientId = clientId;
                _dbContext.Add(address);
                _dbContext.SaveChanges();
                return true;
            }
            throw new NotFoundException("Address is null");
        }
    }
}
