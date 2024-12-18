using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkillSwap.DTOs.Rating
{
    public class CreateRatingDto
    {
        [Range(0, 5, ErrorMessage = "Score must be between 0 and 5.")]
        public int Score { get; set; }

        [MinLength(5, ErrorMessage = "Name must be at least 5 characters long")]
        [MaxLength(250, ErrorMessage = "Name cannot exceed 250 characters")]
        public string? Comment { get; set; }
    }
}