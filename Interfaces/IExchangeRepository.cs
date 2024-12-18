using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillSwapAPI.DTOs;
using SkillSwapAPI.Models;

namespace SkillSwapAPI.Interfaces
{
    public interface IExchangeRepository
    {
        Task<IEnumerable<Exchange>> GetAllExchangesAsync();
        Task<Exchange> GetExchangeByIdAsync(int id);
        Task<Exchange> CreateExchangeAsync(CreateExchangeDto createExchangeDto);
        Task<Exchange> UpdateExchangeAsync(int id, UpdateExchangeDto updateExchangeDto);
        Task<bool> DeleteExchangeAsync(int id);
    }
}