using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSwapAPI.Data;
using SkillSwapAPI.DTOs;
using SkillSwapAPI.Interfaces;
using SkillSwapAPI.Mappers;
using SkillSwapAPI.Models;

namespace SkillSwap.Controllers
{
    [Route("api/ratings")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingsController(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        // GET: api/Ratings/{userId}/average
        [HttpGet("{userId}/average")]
        public async Task<ActionResult<double>> GetAverageRating(int userId)
        {
            var averageRating = await _ratingRepository.GetAverageRatingByUserIdAsync(userId);
            return Ok(averageRating);
        }

        // GET: api/Ratings/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<RatingDto>>> GetRatings(int userId)
        {
            var ratings = await _ratingRepository.GetRatingsByUserIdAsync(userId);

            // Use the mapper to convert to DTOs
            var ratingsDto = ratings.Select(RatingMapper.ToRatingDto).ToList();

            return Ok(ratingsDto);
        }

        // POST: api/Ratings/rate/{userId}
        [HttpPost("rate/{userId}")]
        public async Task<ActionResult<RatingDto>> RateUser(int userId, [FromBody] CreateRatingDto createRatingDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validamos que RatedById sea válido
            if (createRatingDTO.RatedById <= 0)
            {
                return BadRequest("Invalid RatedById.");
            }

            // Llamamos al repositorio para agregar la calificación
            var rating = await _ratingRepository.AddRatingAsync(userId, createRatingDTO.RatedById, createRatingDTO);

            // Si la calificación no fue creada, retornamos un error
            if (rating == null)
            {
                return BadRequest("Failed to create rating.");
            }

            // Retornamos el RatingDto con la información creada
            return Ok(RatingMapper.ToRatingDto(rating));
        }

    }
}