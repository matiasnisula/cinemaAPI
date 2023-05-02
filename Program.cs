using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpLogging;
using cinemaAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.RequestProperties;
});

builder.Services.AddControllers();
builder.Services.AddDbContext<CinemaContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnectionLocal")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// TODO: Check whether the schema needs to be initialized

var options = new DbContextOptionsBuilder<CinemaContext>()
        .UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnectionLocal"))
        .Options;
    using (var context = new CinemaContext(options))
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

var app = builder.Build();

app.UseHttpLogging();

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
