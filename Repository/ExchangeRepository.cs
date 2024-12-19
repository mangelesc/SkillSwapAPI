using Microsoft.EntityFrameworkCore;
using SkillSwapAPI.Models;
using SkillSwapAPI.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillSwapAPI.Interfaces;
using SkillSwapAPI.Data;

namespace SkillSwapAPI.Repository
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly SkillSwapContext _context;

        public ExchangeRepository(SkillSwapContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExchangeDto>> GetAllExchangesAsync(ExchangeQueryParamsDto queryParams)
        {
            var query = _context.Exchanges
                .Include(e => e.OfferedUser)
                .Include(e => e.RequestedUser)
                .Include(e => e.OfferedSkill)
                .Include(e => e.RequestedSkill)
                .AsQueryable();

            // Filtros por OfferedSkillId
            if (queryParams.OfferedSkillId.HasValue)
            {
                query = query.Where(e => e.OfferedSkillId == queryParams.OfferedSkillId.Value);
            }

            // Filtros por RequestedSkillId
            if (queryParams.RequestedSkillId.HasValue)
            {
                query = query.Where(e => e.RequestedSkillId == queryParams.RequestedSkillId.Value);
            }

            // Filtro por status
            if (!string.IsNullOrEmpty(queryParams.Status))
            {
                if (Enum.TryParse<ExchangeStatus>(queryParams.Status, out var status))
                {
                    query = query.Where(e => e.Status == status);
                }
                else
                {
                    throw new ArgumentException("Invalid status value.");
                }
            }

            // Filtro por UserId (puede ser OfferedUserId o RequestedUserId)
            if (queryParams.UserId.HasValue)
            {
                query = query.Where(e => e.OfferedUserId == queryParams.UserId.Value || e.RequestedUserId == queryParams.UserId.Value);
            }

            // Paginación
            query = query.Skip((queryParams.Page - 1) * queryParams.PageSize)
                        .Take(queryParams.PageSize);

            var exchanges = await query
                .Select(e => new ExchangeDto
                {
                    Id = e.Id,
                    OfferedUserId = e.OfferedUserId,
                    RequestedUserId = e.RequestedUserId,
                    OfferedSkillId = e.OfferedSkillId,
                    RequestedSkillId = e.RequestedSkillId,
                    Status = e.Status,
                    OfferedUserName = e.OfferedUser.Name,
                    RequestedUserName = e.RequestedUser.Name,
                    OfferedSkillName = e.OfferedSkill.Name,
                    RequestedSkillName = e.RequestedSkill.Name
                })
                .ToListAsync();

            return exchanges;
        }


        public async Task<ExchangeDto> GetExchangeByIdAsync(int id)
        {
            var exchange = await _context.Exchanges
                .Include(e => e.OfferedUser)
                .Include(e => e.RequestedUser)
                .Include(e => e.OfferedSkill)
                .Include(e => e.RequestedSkill)
                .Where(e => e.Id == id)
                .Select(e => new ExchangeDto
                {
                    Id = e.Id,
                    OfferedUserId = e.OfferedUserId,
                    RequestedUserId = e.RequestedUserId,
                    OfferedSkillId = e.OfferedSkillId,
                    RequestedSkillId = e.RequestedSkillId,
                    Status = e.Status,
                    OfferedUserName = e.OfferedUser.Name,
                    RequestedUserName = e.RequestedUser.Name,
                    OfferedSkillName = e.OfferedSkill.Name,
                    RequestedSkillName = e.RequestedSkill.Name
                })
                .FirstOrDefaultAsync();

            return exchange;
        }

        public async Task<Exchange> CreateExchangeAsync(CreateExchangeDto createExchangeDto)
        {
            var exchange = new Exchange
            {
                OfferedUserId = createExchangeDto.OfferedUserId,
                RequestedUserId = createExchangeDto.RequestedUserId,
                OfferedSkillId = createExchangeDto.OfferedSkillId,
                RequestedSkillId = createExchangeDto.RequestedSkillId,
                Status = ExchangeStatus.Pending
            };

            _context.Exchanges.Add(exchange);
            await _context.SaveChangesAsync();

            // Cargar los datos relacionados
            await _context.Entry(exchange).Reference(e => e.OfferedUser).LoadAsync();
            await _context.Entry(exchange).Reference(e => e.RequestedUser).LoadAsync();
            await _context.Entry(exchange).Reference(e => e.OfferedSkill).LoadAsync();
            await _context.Entry(exchange).Reference(e => e.RequestedSkill).LoadAsync();

            return exchange;
        }

        public async Task<Exchange> UpdateExchangeAsync(int id, UpdateExchangeDto updateExchangeDto)
        {
            var exchange = await _context.Exchanges
                .Include(e => e.OfferedUser)
                .Include(e => e.RequestedUser)
                .Include(e => e.OfferedSkill)
                .Include(e => e.RequestedSkill)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (exchange == null)
            {
                return null;
            }

            exchange.Status = updateExchangeDto.Status;
            await _context.SaveChangesAsync();

            return exchange;
        }

        public async Task<bool> DeleteExchangeAsync(int id)
        {
            var exchange = await _context.Exchanges.FindAsync(id);
            if (exchange == null)
            {
                return false;
            }

            _context.Exchanges.Remove(exchange);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
