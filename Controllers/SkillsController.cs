using Microsoft.AspNetCore.Mvc;
using SkillSwapAPI.DTOs;
using SkillSwapAPI.Interfaces;
using SkillSwapAPI.Models;

namespace SkillSwapAPI.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IUserRepository _userRepository;

        public SkillsController(ISkillRepository skillRepository, IUserRepository userRepository)
        {
            _skillRepository = skillRepository;
            _userRepository = userRepository;
        }

        // GET: api/skills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SkillDto>>> GetSkills([FromQuery] SkillQueryParamsDto queryParams)
        {
            var skills = await _skillRepository.GetAllSkillsAsync(queryParams);

            // Para cada habilidad, obtener el UserName
            foreach (var skill in skills)
            {
                var user = await _userRepository.GetByIdAsync(skill.UserId);
                if (user != null)
                {
                    skill.UserName = user.Name;
                }
            }

            return Ok(skills);
        }

        // GET: api/skills/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillDto>> GetSkill(int id)
        {
            var skill = await _skillRepository.GetSkillByIdAsync(id);

            if (skill == null)
            {
                return NotFound();
            }

            // Obtener el nombre del usuario para la habilidad
            var user = await _userRepository.GetByIdAsync(skill.UserId);
            if (user != null)
            {
                skill.UserName = user.Name;
            }

            return Ok(skill);
        }

        // POST: api/skills
        [HttpPost]
        public async Task<ActionResult<SkillDto>> CreateSkill(CreateSkillDto createSkillDto)
        {
            // Comprobar si el UserId existe
            var userExists = await _userRepository.UserExistsAsync(createSkillDto.UserId);
            if (!userExists)
            {
                return BadRequest("User not found");
            }

            var createdSkill = await _skillRepository.CreateSkillAsync(createSkillDto);

            // Obtener el nombre del usuario para la habilidad recién creada
            var user = await _userRepository.GetByIdAsync(createdSkill.UserId);
            if (user != null)
            {
                createdSkill.UserName = user.Name;
            }

            return CreatedAtAction(nameof(GetSkill), new { id = createdSkill.Id }, createdSkill);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SkillDto>> UpdateSkill(int id, UpdateSkillDto updateSkillDto)
        {
            // Buscar la habilidad por ID en la base de datos
            var existingSkill = await _skillRepository.GetSkillByIdAsync(id);

            if (existingSkill == null)
            {
                return NotFound();  // Si no se encuentra la habilidad, retornamos un error 404
            }

            // Actualizar solo los campos permitidos (Name y Description)
            existingSkill.Name = updateSkillDto.Name;
            existingSkill.Description = updateSkillDto.Description;

            // Actualizar la habilidad en la base de datos, ahora pasando el id y el dto
            var updatedSkill = await _skillRepository.UpdateSkillAsync(id, updateSkillDto);

            if (updatedSkill == null)
            {
                return NotFound();  // Si la actualización falla, retornar 404
            }

            // Obtener el nombre del usuario asociado a la habilidad actualizada
            var user = await _userRepository.GetByIdAsync(updatedSkill.UserId);
            if (user != null)
            {
                updatedSkill.UserName = user.Name;
            }

            // Retornar el DTO actualizado
            return Ok(updatedSkill);
        }


        // DELETE: api/skills/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSkill(int id)
        {
            var success = await _skillRepository.DeleteSkillAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
