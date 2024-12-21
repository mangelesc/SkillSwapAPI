using SkillSwapAPI.DTOs;
using SkillSwapAPI.Models;
using Microsoft.EntityFrameworkCore;
using SkillSwapAPI.Data;
using SkillSwapAPI.Interfaces;
using SkillSwapAPI.Mappers;

namespace SkillSwapAPI.Repository
{
    public class SkillRepository : ISkillRepository
    {
        private readonly SkillSwapContext _context;

        public SkillRepository(SkillSwapContext context)
        {
            _context = context;
        }

        // Obtener todas las habilidades con filtros y paginación
        public async Task<IEnumerable<SkillDto>> GetAllSkillsAsync(SkillQueryParamsDto queryParams)
        {
            var query = _context.Skills.AsQueryable();

            // Filtrar por nombre
            if (!string.IsNullOrEmpty(queryParams.Name))
                query = query.Where(s => s.Name.Contains(queryParams.Name));

            // Filtrar por UserId
            if (queryParams.UserId.HasValue)
                query = query.Where(s => s.UserId == queryParams.UserId.Value);

            // Filtrar por categoría
            if (queryParams.Category.HasValue)
                query = query.Where(s => s.SkillCategory == queryParams.Category.Value);

            // Ordenar
            query = ApplySorting(query, queryParams);

            // Paginación
            query = query.Skip((queryParams.Page - 1) * queryParams.PageSize)
                         .Take(queryParams.PageSize);

            // Obtener las habilidades
            var skills = await query.ToListAsync();

            // Mapeamos las habilidades al DTO
            return skills.Select(s => s.ToSkillDto()); // Usamos el mapper aquí
        }

        // Obtener una habilidad por ID
        public async Task<SkillDto?> GetSkillByIdAsync(int id)
        {
            var skill = await _context.Skills.FindAsync(id);

            return skill?.ToSkillDto(); // Usamos el mapeador para devolver el DTO
        }

        // Crear una nueva habilidad
        public async Task<SkillDto?> CreateSkillAsync(Skill skill)
        {
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return skill.ToSkillDto(); // Usamos el mapeador para devolver el DTO
        }

        // Actualizar una habilidad
        // public async Task<SkillDto?> UpdateSkillAsync(int id, Skill updatedSkill)
        // {
        //     var skill = await _context.Skills.FindAsync(id);

        //     if (skill == null)
        //         return null;

        //     // Actualizamos los valores del Skill
        //     skill.Name = updatedSkill.Name;
        //     skill.Description = updatedSkill.Description;
        //     skill.SkillCategory = updatedSkill.SkillCategory;

        //     await _context.SaveChangesAsync();

        //     return skill.ToSkillDto(); // Convertimos la entidad Skill a SkillDto
        // }

        public async Task<SkillDto?> UpdateSkillAsync(int id, UpdateSkillDto updateSkillDto)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
                return null;

            // Actualizamos los valores del Skill directamente desde el DTO
            skill.Name = updateSkillDto.Name;
            skill.Description = updateSkillDto.Description;
            skill.SkillCategory = updateSkillDto.SkillCategory;

            // Guardamos los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Convertimos la entidad Skill a SkillDto y lo retornamos
            return skill.ToSkillDto();
        }


        // Eliminar una habilidad
        public async Task<bool> DeleteSkillAsync(int id)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
                return false;

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return true;
        }

        // Método auxiliar para ordenar
        private IQueryable<Skill> ApplySorting(IQueryable<Skill> query, SkillQueryParamsDto queryParams)
        {
            if (!string.IsNullOrEmpty(queryParams.SortBy))
            {
                switch (queryParams.SortBy.ToLower())
                {
                    case "name":
                        query = queryParams.SortDescending
                            ? query.OrderByDescending(s => s.Name)
                            : query.OrderBy(s => s.Name);
                        break;
                    case "userid":
                        query = queryParams.SortDescending
                            ? query.OrderByDescending(s => s.UserId)
                            : query.OrderBy(s => s.UserId);
                        break;
                }
            }

            return query;
        }
    }
}
