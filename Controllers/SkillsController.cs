using Microsoft.AspNetCore.Mvc;
using SkillSwapAPI.DTOs;
using SkillSwapAPI.Interfaces;
using SkillSwapAPI.Mappers;

namespace SkillSwapAPI.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillRepository _skillRepository;

        public SkillsController(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        // GET: api/skills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillDto>>> GetSkills([FromQuery] SkillQueryParamsDto queryParams)
        {
            var skills = await _skillRepository.GetAllSkillsAsync(queryParams);

            // Devolvemos directamente el SkillDto desde el repositorio
            return Ok(skills);
        }

        // GET: api/skills/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillDto>> GetSkill(int id)
        {
            var skill = await _skillRepository.GetSkillByIdAsync(id);

            if (skill == null)
                return NotFound();

            return Ok(skill);  // Retornamos el SkillDto directamente
        }

        // POST: api/skills
        [HttpPost]
        public async Task<ActionResult<SkillDto>> CreateSkill([FromBody] CreateSkillDto createSkillDto)
        {
            // Convertimos el DTO a modelo
            var skill = createSkillDto.ToSkill();

            var createdSkill = await _skillRepository.CreateSkillAsync(skill);

            if (createdSkill == null)
                return BadRequest("Failed to create skill");

            // Retornamos el SkillDto de la habilidad creada
            return CreatedAtAction(nameof(GetSkill), new { id = createdSkill.Id }, createdSkill);
        }

        // PUT: api/skills/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<SkillDto>> UpdateSkill(int id, [FromBody] UpdateSkillDto updateSkillDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Validamos si el DTO es correcto
            }

            // Usamos el repositorio para actualizar el Skill
            var updatedSkill = await _skillRepository.UpdateSkillAsync(id, updateSkillDto);
            
            if (updatedSkill == null)
            {
                return NotFound(); // Si no encontramos el Skill, devolvemos 404
            }

            return Ok(updatedSkill); // Devolvemos el DTO actualizado
        }
        // DELETE: api/skills/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSkill(int id)
        {
            var success = await _skillRepository.DeleteSkillAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
