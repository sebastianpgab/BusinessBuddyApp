using AutoMapper;
using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Exceptions;
using BusinessBuddyApp.Models;
using BusinessBuddyApp.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessBuddyApp.Services
{
    public interface IAccountService
    {
        public void RegisterUser(RegisterUserDto userDto);
        public string GenerateJwt(LoginUserDto loginUserDto);

    }
    public class AccountService : IAccountService
    {
        private readonly BusinessBudyDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;
        private readonly AuthenticationSettings _authenticationSettings;
        public AccountService(BusinessBudyDbContext dbContext, IPasswordHasher<User> passwordHasher, IMapper mapper, 
            AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _authenticationSettings = authenticationSettings;
        }
        public void RegisterUser(RegisterUserDto userDto) 
        {
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
            };

            var hashedPassword = _passwordHasher.HashPassword(user, userDto.Password);
            var userMapped = _mapper.Map<User>(userDto);
            userMapped.PasswordHash = hashedPassword;

            _dbContext.Users.Add(userMapped);
            _dbContext.SaveChanges();
        }

        public string GenerateJwt(LoginUserDto loginUserDto)
        {
            var user = _dbContext.Users.Include(c => c.Role).FirstOrDefault(p => p.Email == loginUserDto.Email);

            if(user == null)
            {
                throw new BadRequestException("The provided email address or password is incorrect.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginUserDto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("The provided email address or password is incorrect.");
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim("CompanyId", user.IdCompany.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
