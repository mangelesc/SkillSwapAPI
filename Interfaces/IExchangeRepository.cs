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
        Task<IEnumerable<ExchangeDto>> GetAllExchangesAsync(ExchangeQueryParamsDto queryParams);  // Retorna DTOs con información de usuario y habilidad.
        Task<ExchangeDto> GetExchangeByIdAsync(int id);  // Retorna DTO con información de usuario y habilidad.
        Task<Exchange> CreateExchangeAsync(CreateExchangeDto createExchangeDto);  // Crea un intercambio.
        Task<Exchange> UpdateExchangeAsync(int id, UpdateExchangeDto updateExchangeDto);  // Actualiza el estado de un intercambio.
        Task<bool> DeleteExchangeAsync(int id);  // Elimina un intercambio.
    }
}