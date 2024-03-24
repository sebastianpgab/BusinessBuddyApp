using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Models;

namespace BusinessBuddyApp.Services
{
    public interface IPurchaseService
    {
        public Task<bool> CreatePurchase(PurchaseDto purchaseDto);
    }
    public class PurchaseService : IPurchaseService
    {
        private readonly BusinessBudyDbContext _dbContext;
        private readonly ClientService _clientService;
        private readonly AddressService _addressService;
        public PurchaseService(BusinessBudyDbContext dbContext, ClientService clientService,
            AddressService addressService)
        {
            _dbContext = dbContext;
            _clientService = clientService;
            _addressService = addressService;
        }
        public async Task<bool> CreatePurchase(PurchaseDto purchase)
        {
           using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _clientService.Create(purchase.ClientDto);
                    _addressService.Create(purchase.AddressDto, purchase.ClientDto.Id);



                }
            }
            
        }
    }
}
