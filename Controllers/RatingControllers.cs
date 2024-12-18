using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillSwap.DTOs.Rating;
using SkillSwapAPI.Data;
using SkillSwapAPI.Interfaces;
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
            var ratingsDto = ratings.Select(r => new RatingDto
            {
                UserId = r.UserId,
                RatedById = r.RatedById,
                Score = r.Score,
                Comment = r.Comment
            }).ToList();

            return Ok(ratingsDto);
        }
        // POST: api/Ratings/rate/{userId}
        [HttpPost("rate/{userId}")]
        public async Task<ActionResult<RatingDto>> RateUser(int userId, CreateRatingDto createRatingDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // El usuario logueado es el que realiza la puntuación
            var ratedById = int.Parse(User.Identity.Name); // Obtén el ID del usuario logueado de la sesión

            var rating = new Rating
            {
                UserId = userId,  // Usuario que recibe la puntuación
                RatedById = ratedById,  // Usuario que da la puntuación
                Score = createRatingDTO.Score,
                Comment = createRatingDTO.Comment
            };

            // Guardar la puntuación en la base de datos
            var createdRating = await _ratingRepository.AddRatingAsync(rating);

            return Ok(new RatingDto
            {
                UserId = createdRating.UserId,
                RatedById = createdRating.RatedById,
                Score = createdRating.Score,
                Comment = createdRating.Comment
            });
        }

        
    }
}