using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkillSwapAPI.Data;
using SkillSwapAPI.Interfaces;
using SkillSwapAPI.Models;

namespace SkillSwapAPI.Repository
{
  public class RatingRepository : IRatingRepository
  {
    private readonly SkillSwapContext _context;

        public RatingRepository(SkillSwapContext context)
        {
            _context = context;
        }

        // Obtener todas las puntuaciones de un usuario
        public async Task<List<Rating>> GetRatingsByUserIdAsync(int userId)
        {
            return await _context.Ratings
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        // Agregar una nueva puntuación
        public async Task<Rating> AddRatingAsync(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        // Obtener la puntuación promedio de un usuario
        public async Task<double> GetAverageRatingByUserIdAsync(int userId)
        {
            var ratings = await _context.Ratings
                .Where(r => r.UserId == userId)
                .ToListAsync();

            if (ratings.Any())
            {
                return ratings.Average(r => r.Score);
            }
            return 0; // Si no hay puntuaciones, retornamos 0
        }
    }
}