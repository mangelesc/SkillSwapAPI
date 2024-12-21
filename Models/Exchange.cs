namespace SkillSwapAPI.Models.Enums
{
    public class Exchange
    {
        public int Id { get; set; }

        // Clave externa que referencia al usuario que ofrece la habilidad
        public int OfferedUserId { get; set; }

        // Clave externa que referencia al usuario que solicita la habilidad
        public int RequestedUserId { get; set; }

        // Clave externa que referencia a la habilidad que se está ofreciendo
        public int OfferedSkillId { get; set; }

        // Clave externa que referencia a la habilidad que se está solicitando
        public int RequestedSkillId { get; set; }

        // Estado actual del intercambio, representado como un enum
        public ExchangeStatus Status { get; set; }

        // Propiedad de navegación para acceder al usuario que ofrece la habilidad
        // Esto permite obtener detalles del usuario directamente desde el intercambio
        public User? OfferedUser { get; set; }

        // Propiedad de navegación para acceder al usuario que solicita la habilidad
        public User? RequestedUser { get; set; }

        // Propiedad de navegación para acceder a la habilidad ofrecida
        public Skill? OfferedSkill { get; set; }

        // Propiedad de navegación para acceder a la habilidad solicitada
        public Skill? RequestedSkill { get; set; }
    }
}
