using BusinessBuddyApp.Entities;
using Microsoft.AspNetCore.Mvc;
using BusinessBuddyApp.Migrations;
using BusinessBuddyApp.Exceptions;

namespace BusinessBuddyApp.Services
{
    public interface IClientService
    {
        public ICollection<Client> GetAll();
        public Client Get(int id);
    }
    public class ClientService : IClientService
    {
        public readonly BusinessBudyDbContext _dbContext;
        public ClientService(BusinessBudyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<Client> GetAll()
        {
            var clients = _dbContext.Clients.ToList();
            if (clients.Any())
            {
                return clients;
            }
            throw new NotFoundException("List of clients not found");
        }

        public Client Get(int id)
        {
            var client = _dbContext.Clients.FirstOrDefault(p => p.Id == id);
            if(client is not null)
            {
                return client;
            }
            throw new NotFoundException($"Client {id} not found");
        }
    }
}
