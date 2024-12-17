namespace SkillSwapAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;  // Renombrado a 'Name'
        public string Email { get; set; } = string.Empty;
        public string? ProfilePicture { get; set; }  // Renombrado a 'ProfilePicture'
    }
}
