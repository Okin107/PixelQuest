using IdleHeroesDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace IdleHeroesDAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Profile> Profile { get; set; }
        public DbSet<Stage> Stage { get; set; }
        public DbSet<Companion> Companion { get; set; }
        public DbSet<OwnedCompanions> OwnedCompanions { get; set; }
        public DbSet<Tavern> Tavern { get; set; }
        public DbSet<TavernCompanion> TavernCompanion { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<TeamCompanion> TeamCompanion { get; set; }
    }
}
