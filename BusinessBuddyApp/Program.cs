using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Dodawanie us³ug
builder.Services.AddControllers();  // <- To jest kluczowe dla u¿ywania kontrolerów!

var app = builder.Build();

// Konfiguracja middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();  // <- To jest równie¿ wa¿ne!

app.Run();