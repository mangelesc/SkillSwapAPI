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
    }
}
