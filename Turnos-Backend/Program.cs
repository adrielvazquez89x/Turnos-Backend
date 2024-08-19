using Microsoft.EntityFrameworkCore;
using Turnos_Backend.Models;
using Turnos_Backend.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

// Añadir la política CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder => builder
            .WithOrigins("http://localhost:5173") // La URL de tu aplicación React
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()); // Permite el uso de credenciales
});

// Add services to the container.

builder.Services.AddSingleton<ICounterService, CounterService>();
builder.Services.AddScoped<ITicketService, TicketService>();

builder.Services.AddDbContext<TicketContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TicketConnection"));
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");
app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

//app.UseWebSockets();
//app.UseMiddleware<TurnsWebSocketMiddleware>();

app.MapControllers();
app.MapHub<TurnsHub>("/turnsHub");

app.Run();
