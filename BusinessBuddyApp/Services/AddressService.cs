using BusinessBuddyApp.Entities;

namespace BusinessBuddyApp.Services
{
    public interface IAddressService
    {
        public ICollection<Address> Get();
    }
    public class AddressService : IAddressService
    {
        private readonly BusinessBudyDbContext _dbContext;
        public AddressService(BusinessBudyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ICollection<Address> Get() 
        {
            var addresses = _dbContext.Addresses.ToList();
            return addresses;
        }
    }
}
