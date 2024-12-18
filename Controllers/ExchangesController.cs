using Microsoft.AspNetCore.Mvc;
using SkillSwapAPI.DTOs;
using SkillSwapAPI.Interfaces;
using SkillSwapAPI.Models;
using SkillSwapAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillSwapAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangesController : ControllerBase
    {
        private readonly IExchangeRepository _exchangeRepository;

        public ExchangesController(IExchangeRepository ExchangeRepository)
        {
            _exchangeRepository = ExchangeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExchangeDto>>> GetAllExchanges()
        {
            var exchanges = await _exchangeRepository.GetAllExchangesAsync();
            var exchangeDtos = exchanges.Select(e => new ExchangeDto
            {
                Id = e.Id,
                OfferedUserId = e.OfferedUserId,
                RequestedUserId = e.RequestedUserId,
                OfferedSkillId = e.OfferedSkillId,
                RequestedSkillId = e.RequestedSkillId,
                Status = e.Status,
                OfferedUserName = e.OfferedUser?.Name,
                RequestedUserName = e.RequestedUser?.Name,
                OfferedSkillName = e.OfferedSkill?.Name,
                RequestedSkillName = e.RequestedSkill?.Name
            }).ToList();

            return Ok(exchangeDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExchangeDto>> GetExchange(int id)
        {
            var exchange = await _exchangeRepository.GetExchangeByIdAsync(id);
            if (exchange == null)
            {
                return NotFound();
            }

            var exchangeDto = new ExchangeDto
            {
                Id = exchange.Id,
                OfferedUserId = exchange.OfferedUserId,
                RequestedUserId = exchange.RequestedUserId,
                OfferedSkillId = exchange.OfferedSkillId,
                RequestedSkillId = exchange.RequestedSkillId,
                Status = exchange.Status,
                OfferedUserName = exchange.OfferedUser?.Name,
                RequestedUserName = exchange.RequestedUser?.Name,
                OfferedSkillName = exchange.OfferedSkill?.Name,
                RequestedSkillName = exchange.RequestedSkill?.Name
            };

            return Ok(exchangeDto);
        }

        [HttpPost]
        public async Task<ActionResult<ExchangeDto>> CreateExchange(CreateExchangeDto createExchangeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdExchange = await _exchangeRepository.CreateExchangeAsync(createExchangeDto);

            var exchangeDto = new ExchangeDto
            {
                Id = createdExchange.Id,
                OfferedUserId = createdExchange.OfferedUserId,
                RequestedUserId = createdExchange.RequestedUserId,
                OfferedSkillId = createdExchange.OfferedSkillId,
                RequestedSkillId = createdExchange.RequestedSkillId,
                Status = createdExchange.Status
            };

            return CreatedAtAction(nameof(GetExchange), new { id = createdExchange.Id }, exchangeDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ExchangeDto>> UpdateExchange(int id, UpdateExchangeDto updateExchangeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedExchange = await _exchangeRepository.UpdateExchangeAsync(id, updateExchangeDto);
            if (updatedExchange == null)
            {
                return NotFound();
            }

            var exchangeDto = new ExchangeDto
            {
                Id = updatedExchange.Id,
                OfferedUserId = updatedExchange.OfferedUserId,
                RequestedUserId = updatedExchange.RequestedUserId,
                OfferedSkillId = updatedExchange.OfferedSkillId,
                RequestedSkillId = updatedExchange.RequestedSkillId,
                Status = updatedExchange.Status
            };

            return Ok(exchangeDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExchange(int id)
        {
            var result = await _exchangeRepository.DeleteExchangeAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
