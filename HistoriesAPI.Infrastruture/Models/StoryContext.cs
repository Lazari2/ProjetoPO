using Microsoft.EntityFrameworkCore;
using StoriesAPI.Infrastruture.Configuration;

namespace StoriesAPI.Infrastruture.Models
{
    public class StoryContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "Stories";

        public StoryContext(DbContextOptions<StoryContext> options) : base(options)
        {
        }

        public DbSet<Story> Stories { get; set; } = null!;
        public DbSet<Vote> Votes { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new StoryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new VoteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfigurations());

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.User)
                .WithMany(u => u.Votes)
                .HasForeignKey(v => v.UserId);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Story)
                .WithMany(s => s.Votes)
                .HasForeignKey(v => v.StoryId);
        }
    }
}
