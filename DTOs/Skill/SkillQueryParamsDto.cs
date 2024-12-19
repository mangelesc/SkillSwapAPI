using SkillSwap.Models.Enums;

namespace SkillSwapAPI.DTOs
{
    public class SkillQueryParamsDto
    {
        public string? Name { get; set; }
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? UserId { get; set; }
        public SkillCategory? Category { get; set; } 
    }
}
