using Microsoft.EntityFrameworkCore;

namespace cinemaAPI.Models;

public class CinemaContext : DbContext
{
    public CinemaContext(DbContextOptions<CinemaContext> options)
        : base(options)
    {
    }

    public DbSet<Cinema> Cinemas { get; set; } = null!;
}