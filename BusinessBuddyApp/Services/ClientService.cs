using BusinessBuddyApp.Entities;

namespace BusinessBuddyApp.Services
{
    public interface IClientService
    {
        public ICollection<Client> GetAll();
    }
    public class ClientService : IClientService
    {
        public BusinessBudyDbContext _dbContext;
        //public IClientService _clientService;

        public ICollection<Client> GetAll()
        {
            var clients = _dbContext.Clients.ToList();
            return clients;
        }
    }
}
