using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("JLDB"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlateformGame>()
                .HasOne<Platform>(s => s.Platform)
                .WithMany(g => g.PlateformGames).HasForeignKey(key => key.PlatformId);
            modelBuilder.Entity<PlateformGame>()
               .HasOne<Game>(s => s.Game)
               .WithMany(g => g.PlateformGame).HasForeignKey(key => key.GameId);
            modelBuilder.Entity<UserGame>()
              .HasOne<User>(s => s.User)
              .WithMany(g => g.userGames).HasForeignKey(key => key.UserId);
            modelBuilder.Entity<UserGame>()
            .HasOne<Game>(s => s.Game)
            .WithMany(g => g.UserGames).HasForeignKey(key => key.GameId);



            base.OnModelCreating(modelBuilder);

        }

      

        public DbSet<WebApplication1.Models.Game> Game { get; set; }

        public DbSet<WebApplication1.Models.Platform> Platform { get; set; }

        public DbSet<WebApplication1.Models.User> User { get; set; }

        public DbSet<WebApplication1.Models.PlateformGame> PlateformGame { get; set; }

        public DbSet<WebApplication1.Models.UserGame> UserGame { get; set; }
    }
}
