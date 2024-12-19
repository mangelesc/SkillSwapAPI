using Microsoft.AspNetCore.Mvc;
using SkillSwapAPI.DTOs;
using SkillSwapAPI.Interfaces;
using SkillSwapAPI.Models;

namespace SkillSwapAPI.Controllers
{
    [Route("api/exchanges")]
    [ApiController]
    public class ExchangesController : ControllerBase
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISkillRepository _skillRepository;

        public ExchangesController(
            IExchangeRepository exchangeRepository, 
            IUserRepository userRepository, 
            ISkillRepository skillRepository)
        {
            _exchangeRepository = exchangeRepository;
            _userRepository = userRepository;
            _skillRepository = skillRepository;
        }

        // GET: api/exchanges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExchangeDto>>> GetExchanges([FromQuery] ExchangeQueryParamsDto queryParams)
        {
            var exchanges = await _exchangeRepository.GetAllExchangesAsync(queryParams);
            return Ok(exchanges);
        }

        // GET: api/exchanges/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ExchangeDto>> GetExchange(int id)
        {
            var exchange = await _exchangeRepository.GetExchangeByIdAsync(id);
            if (exchange == null)
            {
                return NotFound();
            }

            return Ok(exchange);
        }

        // POST: api/exchanges
        [HttpPost]
        public async Task<ActionResult<ExchangeDto>> CreateExchange(CreateExchangeDto createExchangeDto)
        {
            // Validar que los usuarios existen
            var offeredUserExists = await _userRepository.UserExistsAsync(createExchangeDto.OfferedUserId);
            var requestedUserExists = await _userRepository.UserExistsAsync(createExchangeDto.RequestedUserId);
            if (!offeredUserExists || !requestedUserExists)
            {
                return BadRequest("One or both users do not exist.");
            }

            // Validar que las habilidades existen
            var offeredSkill = await _skillRepository.GetSkillByIdAsync(createExchangeDto.OfferedSkillId);
            var requestedSkill = await _skillRepository.GetSkillByIdAsync(createExchangeDto.RequestedSkillId);
            if (offeredSkill == null || requestedSkill == null)
            {
                return BadRequest("One or both skills do not exist.");
            }

            // Crear el intercambio
            var createdExchange = await _exchangeRepository.CreateExchangeAsync(createExchangeDto);

            // Retornar resultado
            return CreatedAtAction(nameof(GetExchange), new { id = createdExchange.Id }, createdExchange);
        }

        // PUT: api/exchanges/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ExchangeDto>> UpdateExchange(int id, UpdateExchangeDto updateExchangeDto)
        {
            var exchange = await _exchangeRepository.GetExchangeByIdAsync(id);
            if (exchange == null)
            {
                return NotFound();
            }

            // Actualizar el estado del intercambio
            var updatedExchange = await _exchangeRepository.UpdateExchangeAsync(id, updateExchangeDto);

            if (updatedExchange == null)
            {
                return NotFound();
            }

            return Ok(updatedExchange);
        }

        // DELETE: api/exchanges/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExchange(int id)
        {
            var success = await _exchangeRepository.DeleteExchangeAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
