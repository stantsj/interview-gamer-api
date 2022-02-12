using Microsoft.EntityFrameworkCore;

namespace GamerAPI.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user =>
            {
                user.OwnsMany(x => x.Games, game =>
                {
                    game.Property(e => e.Id).ValueGeneratedNever();
                });
            });
        }
    }
}
