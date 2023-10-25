using BusinessBuddyApp.Entities;
using Microsoft.AspNetCore.Mvc;
using BusinessBuddyApp.Migrations;
using BusinessBuddyApp.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BusinessBuddyApp.Services
{
    public interface IClientService
    {
        public ICollection<Client> GetAll();
        public Client Get(int id);
        public Task<Client> Update(Client client, int id);

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
            throw new ArgumentNullException("List of clients not found");
        }

        public Client Get(int id)
        {
            var client = _dbContext.Clients.FirstOrDefault(p => p.Id == id);
            if(client is not null)
            {
                return client;
            }
            throw new ArgumentNullException($"Client {id} not found");
        }

        public async Task<Client> Update(Client client, int id)
        {
            var clientToUpdate = await _dbContext.Clients.FirstOrDefaultAsync(p => p.Id == id);
            if (clientToUpdate is not null)
            {
                if (client.FirstName is not null) { clientToUpdate.FirstName = client.FirstName;}
                if (client.LastName is not null) { clientToUpdate.LastName = client.LastName; }
                if (client.TaxNumber is not null) { clientToUpdate.TaxNumber = client.TaxNumber; }
                if (client.PhoneNumber is not null) { clientToUpdate.Email = client.Email; }
                if (client.Email is not null) { clientToUpdate.Email = client.Email; }

                _dbContext.SaveChanges();
                return clientToUpdate;
            }
            throw new ArgumentNullException($"Client {id} not found");
     
        }
    }
}
