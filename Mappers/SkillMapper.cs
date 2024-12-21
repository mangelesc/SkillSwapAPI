using SkillSwapAPI.Models;
using SkillSwapAPI.DTOs;

namespace SkillSwapAPI.Mappers
{
    public static class SkillMapper
    {
        // Mapper para convertir de Skill a SkillDto
        public static SkillDto ToSkillDto(this Skill skill)
        {
            return new SkillDto
            {
                Id = skill.Id,
                Name = skill.Name,
                Description = skill.Description,
                UserId = skill.UserId,
                UserName = skill.User?.Name ?? string.Empty, // Obtiene el nombre del usuario si existe
                SkillCategory = skill.SkillCategory
            };
        }

        // Mapper para convertir de CreateSkillDto a Skill
        public static Skill ToSkill(this UpdateSkillDto updateSkillDto, Skill skill)
        {
            skill.Name = updateSkillDto.Name;
            skill.Description = updateSkillDto.Description;
            skill.SkillCategory = updateSkillDto.SkillCategory;  // Asumiendo que también actualizas la categoría de la habilidad
            return skill;
        }

        public static Skill ToSkill(this CreateSkillDto createSkillDto)
        {
            return new Skill
            {
                Name = createSkillDto.Name,
                Description = createSkillDto.Description,
                UserId = createSkillDto.UserId,
                SkillCategory = createSkillDto.SkillCategory
            };
        }


    }
}
