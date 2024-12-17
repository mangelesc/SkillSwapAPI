using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSwapAPI.Data;
using SkillSwapAPI.Models;

namespace SkillSwapAPI.Controllers
{
    // Define el controlador para la entidad "Exchange" y la ruta base para este controlador
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangesController : ControllerBase
    {
        // Declaramos el contexto de la base de datos para interactuar con las entidades
        private readonly SkillSwapContext _context;

        // Constructor del controlador, recibe el contexto de la base de datos
        public ExchangesController(SkillSwapContext context)
        {
            _context = context;
        }

        // Acción GET: api/Exchanges
        // Devuelve una lista de todos los intercambios almacenados en la base de datos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exchange>>> GetExchanges()
        {
            // Incluye las entidades relacionadas (usuarios y habilidades) y devuelve todos los intercambios
            return await _context.Exchanges
                .Include(e => e.OfferedUser) // Incluye los datos del usuario que ofrece el intercambio
                .Include(e => e.RequestedUser) // Incluye los datos del usuario que solicita el intercambio
                .Include(e => e.OfferedSkill) // Incluye los datos de la habilidad ofrecida
                .Include(e => e.RequestedSkill) // Incluye los datos de la habilidad solicitada
                .ToListAsync();
        }

        // Acción POST: api/Exchanges
        // Crea un nuevo intercambio en la base de datos
        [HttpPost]
        public async Task<ActionResult<Exchange>> PostExchange(Exchange exchange)
        {
            // Añade el nuevo intercambio a la base de datos
            _context.Exchanges.Add(exchange);
            await _context.SaveChangesAsync(); // Guarda los cambios realizados en la base de datos

            // Devuelve el nuevo intercambio creado junto con su URL para acceder a él
            return CreatedAtAction(nameof(GetExchanges), new { id = exchange.Id }, exchange);
        }
    }
}
