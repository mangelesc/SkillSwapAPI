using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkillSwapAPI.DTOs
{
    public class CreateExchangeDto
    {
        [Required]
        public int OfferedUserId { get; set; }

        [Required]
        public int RequestedUserId { get; set; }

        [Required]
        public int OfferedSkillId { get; set; }

        [Required]
        public int RequestedSkillId { get; set; }
    }
}