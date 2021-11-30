using Microsoft.EntityFrameworkCore;
using TVSeriesAPI.Models.Entities;

namespace TVSeriesAPI.DAL
{
    public class TVSeriesDbContext : DbContext
    {
        public DbSet<CastMember> CastMembers { get; set; } = null!;
        public DbSet<Episode> Episodes { get; set; } = null!;
        public DbSet<EpisodeCast> EpisodeCasts { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Season> Seasons { get; set; } = null!;
        public DbSet<Serie> Series { get; set; } = null!;

        public TVSeriesDbContext(DbContextOptions<TVSeriesDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedData();
        }
    }
}
