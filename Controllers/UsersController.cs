using Microsoft.AspNetCore.Mvc;
using SkillSwapAPI.Interfaces;
using SkillSwapAPI.DTOs;
using SkillSwapAPI.Helpers;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace SkillSwapAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserQueryParams queryParams)
        {
            // Verificar si el modelo es válido (validaciones en los DTOs)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _userRepository.GetAllAsync(queryParams);
            
            // Devolver los usuarios filtrados y ordenados
            return Ok(users);
        }

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Validación en el DTO
            }

            var user = await _userRepository.AddAsync(createUserDTO);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT: api/Users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO updateUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Validación en el DTO
            }

            var user = await _userRepository.UpdateAsync(id, updateUserDTO);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // DELETE: api/Users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var success = await _userRepository.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
