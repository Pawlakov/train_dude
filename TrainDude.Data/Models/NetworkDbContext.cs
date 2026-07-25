namespace TrainDude.Data.Models;

using Microsoft.EntityFrameworkCore;

using TrainDude.Data.Models.Configuration;

public partial class NetworkDbContext(DbContextOptions<NetworkDbContext> options)
    : DbContext(options)
{
    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<Radius> Radii { get; set; }

    public virtual DbSet<RouteExtreme> RouteEnds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RadiusEntityTypeConfiguration).Assembly);
    }
}