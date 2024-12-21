using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkillSwapAPI.DTOs
{
    public class CreateRatingDto
    {
        [Required]
        [Range(0, 5, ErrorMessage = "Score must be between 0 and 5.")]
        public int Score { get; set; }

        [MaxLength(250, ErrorMessage = "Name cannot exceed 250 characters")]
        public string? Comment { get; set; }

        [Required]
        public int RatedById { get; set; }
    }
}