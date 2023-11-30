using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Middleware;
using BusinessBuddyApp.Models;
using BusinessBuddyApp.Models.Validators;
using BusinessBuddyApp.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using System.Reflection;

namespace BusinessBuddyApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseNLog();

            // Inicjowanie konfiguracji
            var configuration = builder.Configuration;

            // Dodawanie us³ug
            builder.Services.AddControllers().AddFluentValidation();
            builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
            builder.Services.AddScoped<Seeder>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IAddressService, AddressService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IOrderProductService, OrderProductService>();
            builder.Services.AddScoped<IOrderDetailService,  OrderDetailService>();
            builder.Services.AddScoped<IInvoiceService, InvoiceService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IPasswordHasher<RegisterUserDto>, PasswordHasher<RegisterUserDto>>();
            builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));
            //Fluent Validations
            builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            builder.Services.AddScoped<IValidator<AddressDto>, AddressDtoValidator>();
            builder.Services.AddScoped<IValidator<ClientDto>, ClientDtoValidator>();
            //Middlewares
            builder.Services.AddScoped<RequestTimeMiddleware>();
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            //Db Connection
            builder.Services.AddDbContext<BusinessBudyDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BusinessBudyDbConnection")));

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();

            seeder.Seed();
            // Konfiguracja middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestTimeMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}