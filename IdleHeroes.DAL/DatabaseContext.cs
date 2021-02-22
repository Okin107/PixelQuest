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
        public DbSet<Tavern> Tavern { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<KeyStore> KeyStore { get; set; }
    }
}
