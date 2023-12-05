using BusinessBuddyApp.Entities;
using Microsoft.AspNetCore.Mvc;
using BusinessBuddyApp.Migrations;
using BusinessBuddyApp.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.IIS.Core;
using BusinessBuddyApp.Models;
using AutoMapper;
using System.Linq;
using System.Diagnostics.Eventing.Reader;

namespace BusinessBuddyApp.Services
{
    public interface IClientService
    {
        public ICollection<Client> GetAll(string searchPhrase);
        public Client Get(int id);
        public Task<Client> Update(Client client, int id);
        public void Create(ClientDto clientDto);

    }
    public class ClientService : IClientService
    {
        private readonly BusinessBudyDbContext _dbContext;
        private readonly IMapper _mapper;
        public ClientService(BusinessBudyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public ICollection<Client> GetAll(string searchPhrase)
        {
            if (searchPhrase == null)
            {
                var clients = _dbContext.Clients.ToList();

                if (clients.Any())
                {
                    return clients;
                }
            }
            else
            {
                var foundByName = FindByName(searchPhrase);
                return foundByName;
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
            throw new NotFoundException($"Client {id} not found");
        }

        public void Create(ClientDto clientDto)
        {
            var clientMapped = _mapper.Map<Client>(clientDto);
            _dbContext.Add(clientMapped);
            _dbContext.SaveChanges();
        }

        public List<Client> FindByName(string searchPhrase)
        {
            var lowercasedAndTrimPhrase = searchPhrase.ToLower().Trim();
            var reversedLowercasePhrase = lowercasedAndTrimPhrase.Split(' ');
            var name = reversedLowercasePhrase.First();
            var surName = reversedLowercasePhrase.Last();

            var clients = _dbContext.Clients.Where(p => p.FirstName == name && p.LastName == surName).ToList();

            if(clients.Any())
            {
                return clients;
            }

            throw new NotFoundException($"Client with name '{name}' and surname '{surName}' was not found.");

        }
    }
}
