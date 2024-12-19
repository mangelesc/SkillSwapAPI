namespace SkillSwapAPI.DTOs
{
    public class ExchangeQueryParamsDto
    {
        public int? OfferedSkillId { get; set; }
        public int? RequestedSkillId { get; set; }
        public string? Status { get; set; }
        public int? UserId { get; set; } // Para filtrar por usuario (puede ser OfferedUserId o RequestedUserId)
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
