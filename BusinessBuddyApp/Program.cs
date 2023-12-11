using BusinessBuddyApp.Entities;
using BusinessBuddyApp.Middleware;
using BusinessBuddyApp.Models;
using BusinessBuddyApp.Models.Validators;
using BusinessBuddyApp.Services;
using BusinessBuddyApp.Settings;
using DinkToPdf.Contracts;
using DinkToPdf;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using System.Reflection;
using System.Text;

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

            var authenticationSettings = new AuthenticationSettings();
            builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
            builder.Services.AddSingleton(authenticationSettings);

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),

                };
            });

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
            builder.Services.AddScoped<IInvoiceGenerator, InvoiceGenerator>();
            builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));
            //Fluent Validations
            builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            builder.Services.AddScoped<IValidator<AddressDto>, AddressDtoValidator>();
            builder.Services.AddScoped<IValidator<ClientDto>, ClientDtoValidator>();
            builder.Services.AddScoped<IValidator<ClientQuery>, ClientQueryValidator>();
            //Middlewares
            builder.Services.AddScoped<RequestTimeMiddleware>();
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            //Db Connection
            builder.Services.AddDbContext<BusinessBudyDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BusinessBudyDbConnection")));
            //cors
            builder.Services.AddCors(options => options.AddPolicy("FrontedClient", builder => 
            builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();

            seeder.Seed();
            app.UseCors("FrontedClient");
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