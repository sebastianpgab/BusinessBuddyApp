using BusinessBuddyApp.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BusinessBuddyApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Inicjowanie konfiguracji
            var configuration = builder.Configuration;

            // Dodawanie us³ug
            builder.Services.AddControllers();
            builder.Services.AddDbContext<BusinessBudyDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BusinessBudyDbConnection")));

            var app = builder.Build();

            // Konfiguracja middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}