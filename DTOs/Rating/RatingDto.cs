using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillSwapAPI.DTOs
{
    public class RatingDto
    {
        public int UserId { get; set; }
        public int RatedById { get; set; }
        public int Score { get; set; }
        public string? Comment { get; set; }
    }
}