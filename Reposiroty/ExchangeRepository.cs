using Microsoft.EntityFrameworkCore;
using SkillSwapAPI.Models;
using SkillSwapAPI.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillSwapAPI.Interfaces;
using SkillSwapAPI.Data;

namespace SkillSwapAPI.Repositories
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly SkillSwapContext _context;

        public ExchangeRepository(SkillSwapContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Exchange>> GetAllExchangesAsync()
        {
            return await _context.Exchanges
                .Include(e => e.OfferedUser)
                .Include(e => e.RequestedUser)
                .Include(e => e.OfferedSkill)
                .Include(e => e.RequestedSkill)
                .ToListAsync();
        }

        public async Task<Exchange> GetExchangeByIdAsync(int id)
        {
            return await _context.Exchanges
                .Include(e => e.OfferedUser)
                .Include(e => e.RequestedUser)
                .Include(e => e.OfferedSkill)
                .Include(e => e.RequestedSkill)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Exchange> CreateExchangeAsync(CreateExchangeDto createExchangeDto)
        {
            var exchange = new Exchange
            {
                OfferedUserId = createExchangeDto.OfferedUserId,
                RequestedUserId = createExchangeDto.RequestedUserId,
                OfferedSkillId = createExchangeDto.OfferedSkillId,
                RequestedSkillId = createExchangeDto.RequestedSkillId,
                Status = ExchangeStatus.Pending // Por defecto, el estado es "Pending"
            };

            _context.Exchanges.Add(exchange);
            await _context.SaveChangesAsync();

            return exchange;
        }

        public async Task<Exchange> UpdateExchangeAsync(int id, UpdateExchangeDto updateExchangeDto)
        {
            var exchange = await _context.Exchanges.FindAsync(id);
            if (exchange == null)
            {
                return null;
            }

            exchange.Status = updateExchangeDto.Status;
            _context.Exchanges.Update(exchange);
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
