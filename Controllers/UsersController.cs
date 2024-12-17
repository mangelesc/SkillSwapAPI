using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSwapAPI.Data;
using SkillSwapAPI.Models;

namespace SkillSwapAPI.Controllers
{
    // Define el controlador para la entidad "User" y la ruta base para este controlador
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // Declaramos el contexto de la base de datos para interactuar con las entidades
        private readonly SkillSwapContext _context;

        // Constructor del controlador, recibe el contexto de la base de datos
        public UsersController(SkillSwapContext context)
        {
            _context = context;
        }

        // Acción GET: api/Users
        // Devuelve una lista de todos los usuarios almacenados en la base de datos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            // Devuelve todos los usuarios almacenados en la base de datos
            return await _context.Users.ToListAsync();
        }

        // Acción POST: api/Users
        // Crea un nuevo usuario en la base de datos
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            // Añade el nuevo usuario a la base de datos
            _context.Users.Add(user);
            await _context.SaveChangesAsync(); // Guarda los cambios realizados en la base de datos

            // Devuelve el usuario recién creado junto con su URL para acceder a él
            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }
    }
}
