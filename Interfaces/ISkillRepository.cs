using SkillSwapAPI.DTOs;
using SkillSwapAPI.Models;

namespace SkillSwapAPI.Interfaces
{
    public interface ISkillRepository
    {
        Task<IEnumerable<SkillDto>> GetAllSkillsAsync(SkillQueryParamsDto queryParams);
        Task<SkillDto?> GetSkillByIdAsync(int id);

        Task<SkillDto?> CreateSkillAsync(Skill skill); 
        Task<SkillDto?> UpdateSkillAsync(int id, UpdateSkillDto updateSkillDto);
        Task<bool> DeleteSkillAsync(int id);
    }
}
