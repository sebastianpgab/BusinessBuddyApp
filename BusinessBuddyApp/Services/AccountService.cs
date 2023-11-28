using AutoMapper;
using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Models;
using Microsoft.AspNetCore.Identity;

namespace BusinessBuddyApp.Services
{
    public interface IAccountService
    {
        public void RegisterUser(RegisterUserDto userDto);
    }
    public class AccountService : IAccountService
    {
        private readonly BusinessBudyDbContext _dbContext;
        private readonly IPasswordHasher<RegisterUserDto> _passwordHasher;
        private readonly IMapper _mapper;
        public AccountService(BusinessBudyDbContext dbContext, IPasswordHasher<RegisterUserDto> passwordHasher,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }
        public void RegisterUser(RegisterUserDto userDto) 
        {
            var hashedPassword = _passwordHasher.HashPassword(userDto, userDto.Password);
            var userMapped = _mapper.Map<User>(userDto);
            userMapped.PasswordHash = hashedPassword;

            _dbContext.Users.Add(userMapped);
            _dbContext.SaveChanges();
        }
    }
}
