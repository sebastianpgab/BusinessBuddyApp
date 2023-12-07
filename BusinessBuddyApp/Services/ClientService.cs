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
using System.Xml.Linq;
using System.Linq.Expressions;

namespace BusinessBuddyApp.Services
{
    public interface IClientService
    {
        public PagedResult<Client> GetAll(ClientQuery clientQuery);
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

        public PagedResult<Client> GetAll(ClientQuery clientQuery)
        {
            if (clientQuery != null)
            {              
                var foundByName = FindByName(clientQuery);
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

        public PagedResult<Client> FindByName(ClientQuery clientQuery)
        {
            IQueryable<Client> baseQuery;

            if (clientQuery.SearchPhrase == null)
            {
                baseQuery = _dbContext.Clients;
            }
            else
            {
                var lowercasedAndTrimPhrase = clientQuery.SearchPhrase.ToLower().Trim();
                var reversedLowercasePhrase = lowercasedAndTrimPhrase.Split(' ');
                var name = reversedLowercasePhrase.First();
                var surName = reversedLowercasePhrase.Last();

                baseQuery = _dbContext.Clients.Where(p => p.FirstName.ToLower() == name && p.LastName.ToLower() == surName);
            }

            if (!string.IsNullOrEmpty(clientQuery.SortBy))
            {
                var columnSelectors = new Dictionary<string, Expression<Func<Client, object>>>
                {
                    { nameof(Client.FirstName), p => p.FirstName},
                    { nameof(Client.LastName), p => p.LastName},
                    { nameof(Client.Email), p => p.Email},
                };
                var selectedColumn = columnSelectors[clientQuery.SortBy];

                baseQuery = clientQuery.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);

            }

            var clients = baseQuery.Skip(clientQuery.PageSize * (clientQuery.PageNumber - 1)).Take(clientQuery.PageSize).ToList();
            var result = new PagedResult<Client>(clients, baseQuery.Count(), clientQuery.PageSize, clientQuery.PageNumber);

            if (clients.Any())
            {
                return result;
            }

            throw new NotFoundException($"Client with was not found.");

        }
    }
}
