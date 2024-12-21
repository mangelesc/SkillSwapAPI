using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkillSwapAPI.DTOs;
using SkillSwapAPI.Models;

namespace SkillSwapAPI.Mappers
{
    public static class RatingMapper
    {
        public static RatingDto ToRatingDto(Rating rating)
        {
            return new RatingDto
            {
                UserId = rating.UserId,
                RatedById = rating.RatedById,
                Score = rating.Score,
                Comment = rating.Comment
            };
        }

        public static Rating ToRating(int userId, int ratedById, CreateRatingDto createRatingDto)
        {
            return new Rating
            {
                UserId = userId,
                RatedById = ratedById,
                Score = createRatingDto.Score,
                Comment = createRatingDto.Comment,
                DateRated = DateTime.UtcNow
            };
        }
    }
}