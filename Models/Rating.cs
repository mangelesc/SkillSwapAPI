namespace SkillSwapAPI.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RatedById { get; set; } 
        public int Score { get; set; }
        public string? Comment { get; set; } 
        public DateTime DateRated { get; set; } = DateTime.UtcNow; // Fecha de la puntuación

        // Navegación
        public User User { get; set; } = null!;
        public User RatedBy { get; set; } = null!;
    }
}
