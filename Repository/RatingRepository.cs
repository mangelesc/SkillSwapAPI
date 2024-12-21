using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkillSwapAPI.Data;
using SkillSwapAPI.DTOs;
using SkillSwapAPI.Interfaces;
using SkillSwapAPI.Mappers;
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

        public async Task<List<Rating>> GetRatingsByUserIdAsync(int userId)
        {
            return await _context.Ratings
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<Rating?> AddRatingAsync(int userId, int ratedById, CreateRatingDto createRatingDto)
        {
            // Mapeamos el DTO a la entidad Rating
            var rating = RatingMapper.ToRating(userId, ratedById, createRatingDto);

            try
            {
                // Agregar la calificación a la base de datos
                _context.Ratings.Add(rating);
                await _context.SaveChangesAsync();

                // Retornar la calificación que se ha creado
                return rating;
            }
            catch (Exception ex)
            {
                // Manejar el error si no se pudo guardar la calificación
                Console.WriteLine($"Error creating rating: {ex.Message}");
                return null;
            }
        }


        public async Task<double> GetAverageRatingByUserIdAsync(int userId)
        {
            var ratings = await _context.Ratings
                .Where(r => r.UserId == userId)
                .ToListAsync();

            if (ratings.Any())
            {
                return ratings.Average(r => r.Score);
            }
            return 0; // Return 0 if no ratings exist
        }
    }
}
