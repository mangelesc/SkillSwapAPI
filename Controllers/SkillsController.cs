using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSwapAPI.Data;
using SkillSwapAPI.Models;

namespace SkillSwapAPI.Controllers
{
    // Define el controlador para la entidad "Skill" y la ruta base para este controlador
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        // Declaramos el contexto de la base de datos para interactuar con las entidades
        private readonly SkillSwapContext _context;

        // Constructor del controlador, recibe el contexto de la base de datos
        public SkillsController(SkillSwapContext context)
        {
            _context = context;
        }

        // Acción GET: api/Skills
        // Devuelve una lista de todas las habilidades almacenadas en la base de datos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkills()
        {
            // Devuelve todas las habilidades almacenadas en la base de datos
            return await _context.Skills.ToListAsync();
        }

        // Acción POST: api/Skills
        // Crea una nueva habilidad en la base de datos
        [HttpPost]
        public async Task<ActionResult<Skill>> PostSkill(Skill skill)
        {
            // Añade la nueva habilidad a la base de datos
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync(); // Guarda los cambios realizados en la base de datos

            // Devuelve la habilidad recién creada junto con su URL para acceder a ella
            return CreatedAtAction(nameof(GetSkills), new { id = skill.Id }, skill);
        }
    }
}
