using Microsoft.EntityFrameworkCore;
using cinemaAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = "Server=localhost;Port=5432;Database=database;User Id=admin;Password=admin;";

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<CinemaContext>(opt =>
    opt.UseNpgsql(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// TODO: Check whether the schema needs to be initialized

var options = new DbContextOptionsBuilder<CinemaContext>()
        .UseNpgsql(connectionString)
        .Options;
    using (var context = new CinemaContext(options))
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
