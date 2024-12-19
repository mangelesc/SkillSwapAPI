using SkillSwap.Models.Enums;

namespace SkillSwapAPI.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; 
        public int UserId { get; set; }
        public SkillCategory SkillCategory { get; set; }

        public User User { get; set; } = null!; 
    }
}
