using Microsoft.EntityFrameworkCore;
using SkillSwapAPI.Models;
using SkillSwapAPI.DTOs;
using SkillSwapAPI.Models.Enums;
using SkillSwapAPI.Interfaces;
using SkillSwapAPI.Data;
using SkillSwapAPI.Mappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillSwapAPI.Repository
{
    public class ExchangeRepository : IExchangeRepository
    {
        private readonly SkillSwapContext _context;

        public ExchangeRepository(SkillSwapContext context)
        {
            _context = context;
        }

        // Método auxiliar para cargar las relaciones
        private async Task LoadRelatedDataAsync(Exchange exchange)
        {
            await _context.Entry(exchange).Reference(e => e.OfferedUser).LoadAsync();
            await _context.Entry(exchange).Reference(e => e.RequestedUser).LoadAsync();
            await _context.Entry(exchange).Reference(e => e.OfferedSkill).LoadAsync();
            await _context.Entry(exchange).Reference(e => e.RequestedSkill).LoadAsync();
        }

        // Obtener todos los intercambios con filtros y paginación
        public async Task<IEnumerable<ExchangeDto>> GetAllExchangesAsync(ExchangeQueryParamsDto queryParams)
        {
            var query = _context.Exchanges
                .Include(e => e.OfferedUser)
                .Include(e => e.RequestedUser)
                .Include(e => e.OfferedSkill)
                .Include(e => e.RequestedSkill)
                .AsQueryable();

            // Aplicar filtros según los parámetros de consulta
            if (queryParams.OfferedSkillId.HasValue)
            {
                query = query.Where(e => e.OfferedSkillId == queryParams.OfferedSkillId.Value);
            }

            if (queryParams.RequestedSkillId.HasValue)
            {
                query = query.Where(e => e.RequestedSkillId == queryParams.RequestedSkillId.Value);
            }

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

            if (queryParams.UserId.HasValue)
            {
                query = query.Where(e => e.OfferedUserId == queryParams.UserId.Value || e.RequestedUserId == queryParams.UserId.Value);
            }

            // Paginación
            query = query.Skip((queryParams.Page - 1) * queryParams.PageSize)
                         .Take(queryParams.PageSize);

            // Obtener los intercambios y convertirlos a DTOs
            var exchanges = await query.ToListAsync();
            return exchanges.Select(e => e.ToExchangeDto()); // Usamos el mapper aquí
        }

        // Obtener un intercambio por su ID
        public async Task<ExchangeDto> GetExchangeByIdAsync(int id)
        {
            var exchange = await _context.Exchanges
                .Include(e => e.OfferedUser)
                .Include(e => e.RequestedUser)
                .Include(e => e.OfferedSkill)
                .Include(e => e.RequestedSkill)
                .FirstOrDefaultAsync(e => e.Id == id);

            return exchange?.ToExchangeDto(); // Usamos el mapper aquí
        }

        // Crear un intercambio
        public async Task<Exchange> CreateExchangeAsync(CreateExchangeDto createExchangeDto)
        {
            var exchange = createExchangeDto.ToExchange();
            _context.Exchanges.Add(exchange);
            await _context.SaveChangesAsync();

            // Cargar los datos relacionados (usuarios y habilidades)
            await LoadRelatedDataAsync(exchange);

            return exchange;
        }

        // Actualizar un intercambio
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

            exchange = updateExchangeDto.ToExchange(exchange); // Usamos el mapper aquí
            await _context.SaveChangesAsync();

            return exchange;
        }

        // Eliminar un intercambio
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
