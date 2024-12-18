using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SkillSwapAPI.Models;

namespace SkillSwapAPI.DTOs
{
    public class UpdateExchangeDto
    {
        [Required]
        public ExchangeStatus Status { get; set; }
    }
}