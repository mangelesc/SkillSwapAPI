using SkillSwapAPI.DTOs;
using SkillSwapAPI.Models;
using SkillSwapAPI.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillSwapAPI.Interfaces
{
    public interface IExchangeRepository
    {
        Task<IEnumerable<ExchangeDto>> GetAllExchangesAsync(ExchangeQueryParamsDto queryParams);
        Task<ExchangeDto> GetExchangeByIdAsync(int id);
        Task<Exchange> CreateExchangeAsync(CreateExchangeDto createExchangeDto);
        Task<Exchange> UpdateExchangeAsync(int id, UpdateExchangeDto updateExchangeDto);
        Task<bool> DeleteExchangeAsync(int id);
    }
}
