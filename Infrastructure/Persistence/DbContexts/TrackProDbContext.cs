using Microsoft.EntityFrameworkCore;
using TrackPro.Domain.Entities;

namespace TrackPro.Infrastructure.Persistence.DbContexts
{
    public class TrackProDbContext : DbContext
    {
        public TrackProDbContext(DbContextOptions<TrackProDbContext> options) : base(options)
        {
        }

        public DbSet<Part> Parts { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Movement> Movements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Part>()
                .HasKey(p => p.Code);
        }
    }
}