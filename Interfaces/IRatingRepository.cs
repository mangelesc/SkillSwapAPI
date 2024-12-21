using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillSwapAPI.DTOs;
using SkillSwapAPI.Models;

namespace SkillSwapAPI.Interfaces
{
    public interface IRatingRepository
    {
        Task<List<Rating>> GetRatingsByUserIdAsync(int userId); // Obtener todas las puntuaciones de un usuario
        Task<Rating> AddRatingAsync(int userId, int ratedById, CreateRatingDto createRatingDto); // Crear una nueva puntuación
        Task<double> GetAverageRatingByUserIdAsync(int userId); // Obtener la puntuación promedio de un usuario
    }
}
