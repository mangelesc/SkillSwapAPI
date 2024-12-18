using Microsoft.EntityFrameworkCore;
using SkillSwapAPI.Models;

namespace SkillSwapAPI.Data
{
    public class SkillSwapContext : DbContext
    {
        public SkillSwapContext(DbContextOptions<SkillSwapContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }


        public DbSet<Rating> Ratings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones de la relación de uno a muchos entre usuarios y puntuaciones
            modelBuilder.Entity<Rating>()
                .HasOne(r => r.User)
                .WithMany(u => u.RatingsReceived)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Rating>()
                .HasOne(r => r.RatedBy)
                .WithMany(u => u.RatingsGiven)
                .HasForeignKey(r => r.RatedById)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuraciones uno a muchos entre User y Skills
            modelBuilder.Entity<Skill>()
                .HasOne(s => s.User)
                .WithMany(u => u.Skills)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
