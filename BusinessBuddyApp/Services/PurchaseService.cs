using AutoMapper;
using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Models;

namespace BusinessBuddyApp.Services
{
    public interface IPurchaseService
    {
        public Task CreatePurchase(PurchaseDto purchaseDto);
    }
    public class PurchaseService : IPurchaseService
    {
        private readonly BusinessBudyDbContext _dbContext;
        private readonly IClientService _clientService;
        private readonly IAddressService _addressService;
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderProductService _orderProductService;
        private readonly IMapper _mapper;
        public PurchaseService(BusinessBudyDbContext dbContext, IClientService clientService,
            IAddressService addressService, IOrderService orderService, IOrderDetailService orderDetailService,
            IOrderProductService orderProductService, 
             IMapper mapper)
        {
            _dbContext = dbContext;
            _clientService = clientService;
            _addressService = addressService;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
            _orderProductService = orderProductService;
            _mapper = mapper;
        }
        public async Task CreatePurchase(PurchaseDto purchase)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var client = await _clientService.Create(purchase.ClientDto);
                var address = await _addressService.Create(purchase.AddressDto, client.Id);

                if (client == null || address == null)
                {
                    throw new Exception("Failed to create client or address.");
                }

                var orderServiceMapped = _mapper.Map<Order>(purchase.OrderDto);
                if (orderServiceMapped == null)
                {
                    throw new Exception("Failed to map order DTO.");
                }
                orderServiceMapped.ClientId = client.Id;
                var order = await _orderService.Create(orderServiceMapped);

                var orderDetailMapped = _mapper.Map<OrderDetail>(purchase.OrderDetailDto);
                if (orderDetailMapped == null)
                {
                    throw new Exception("Failed to map order detail DTO.");
                }
                orderDetailMapped.DeliveryId = address.Id;
                var orderDetail = await _orderDetailService.Create(orderDetailMapped, order.Id);

                var orderProductMapped = _mapper.Map<OrderProduct>(purchase.OrderProductDto);
                if (orderProductMapped == null)
                {
                    throw new Exception("Failed to map order product DTO.");
                }
                await _orderProductService.Create(orderProductMapped, orderDetail.Id);

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
