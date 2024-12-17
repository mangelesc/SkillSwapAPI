namespace SkillSwapAPI.Models
{
    public class Exchange
    {
        public int Id { get; set; }
        public int OfferedUserId { get; set; }  // Renombrado a 'OfferedUserId'
        public int RequestedUserId { get; set; }  // Renombrado a 'RequestedUserId'
        public int OfferedSkillId { get; set; }  // Renombrado a 'OfferedSkillId'
        public int RequestedSkillId { get; set; }  // Renombrado a 'RequestedSkillId'

        // Usamos el enum en lugar de un string
        public ExchangeStatus Status { get; set; }  // Renombrado 'Estado' a 'Status' y 'EstadoIntercambio' a 'ExchangeStatus'

        public User OfferedUser { get; set; }  // Renombrado a 'OfferedUser'
        public User RequestedUser { get; set; }  // Renombrado a 'RequestedUser'
        public Skill OfferedSkill { get; set; }  // Renombrado a 'OfferedSkill'
        public Skill RequestedSkill { get; set; }  // Renombrado a 'RequestedSkill'
    }
}
