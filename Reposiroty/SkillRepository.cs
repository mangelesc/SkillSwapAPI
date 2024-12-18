using SkillSwapAPI.DTOs;
using SkillSwapAPI.Models;
using Microsoft.EntityFrameworkCore;
using SkillSwapAPI.Data;
using SkillSwapAPI.Interfaces;

namespace SkillSwapAPI.Repositories
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

            // Filtrado por nombre
            if (!string.IsNullOrEmpty(queryParams.Name))
            {
                query = query.Where(s => s.Name.Contains(queryParams.Name));
            }

            // Filtrado por UserId
            if (queryParams.UserId.HasValue)
            {
                query = query.Where(s => s.UserId == queryParams.UserId.Value);
            }

            // Ordenar
            if (!string.IsNullOrEmpty(queryParams.SortBy))
            {
                if (queryParams.SortBy.ToLower() == "name")
                {
                    query = queryParams.SortDescending 
                        ? query.OrderByDescending(s => s.Name) 
                        : query.OrderBy(s => s.Name);
                }
                else if (queryParams.SortBy.ToLower() == "userid")
                {
                    query = queryParams.SortDescending 
                        ? query.OrderByDescending(s => s.UserId) 
                        : query.OrderBy(s => s.UserId);
                }
            }

            // Paginación
            query = query.Skip((queryParams.Page - 1) * queryParams.PageSize)
                         .Take(queryParams.PageSize);

            var skills = await query
                .Select(s => new SkillDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    UserId = s.UserId
                })
                .ToListAsync();

            return skills;
        }

        // Obtener una habilidad por ID
        public async Task<SkillDto> GetSkillByIdAsync(int id)
        {
            var skill = await _context.Skills
                .Where(s => s.Id == id)
                .Select(s => new SkillDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    UserId = s.UserId
                })
                .FirstOrDefaultAsync();

            return skill;
        }

        // Crear una nueva habilidad
        public async Task<SkillDto> CreateSkillAsync(CreateSkillDto createSkillDto)
        {
            var skill = new Skill
            {
                Name = createSkillDto.Name,
                Description = createSkillDto.Description,
                UserId = createSkillDto.UserId
            };

            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return new SkillDto
            {
                Id = skill.Id,
                Name = skill.Name,
                Description = skill.Description,
                UserId = skill.UserId
            };
        }

        // Actualizar una habilidad
        public async Task<SkillDto> UpdateSkillAsync(int id, UpdateSkillDto updateSkillDto)
        {
            // Buscar la habilidad en la base de datos por ID
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
            {
                return null;  // Si no se encuentra la habilidad, devolver null
            }

            // Actualizar los campos de la habilidad con los datos del DTO
            skill.Name = updateSkillDto.Name;
            skill.Description = updateSkillDto.Description;

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Crear y devolver el DTO actualizado
            return new SkillDto
            {
                Id = skill.Id,
                Name = skill.Name,
                Description = skill.Description,
                UserId = skill.UserId
            };
        }

        // Eliminar una habilidad
        public async Task<bool> DeleteSkillAsync(int id)
        {
            var skill = await _context.Skills.FindAsync(id);

            if (skill == null)
            {
                return false;
            }

            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
