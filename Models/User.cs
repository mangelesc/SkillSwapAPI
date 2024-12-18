namespace SkillSwapAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;  
        public string Email { get; set; } = string.Empty;
        public string? ProfilePicture { get; set; }  

        // Relación con Ratings
        public ICollection<Rating> RatingsGiven { get; set; } = new List<Rating>(); // Las puntuaciones dadas por este usuario
        public ICollection<Rating> RatingsReceived { get; set; } = new List<Rating>(); // Las puntuaciones recibidas por este usuario
    }
}
