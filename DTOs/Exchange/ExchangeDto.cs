using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SkillSwapAPI.Models.Enums;

namespace SkillSwapAPI.DTOs
{
    public class ExchangeDto
    {
      public int Id { get; set; }

        [Required]
        public int OfferedUserId { get; set; }

        [Required]
        public int RequestedUserId { get; set; }

        [Required]
        public int OfferedSkillId { get; set; }

        [Required]
        public int RequestedSkillId { get; set; }

        [Required]
        public ExchangeStatus Status { get; set; }

        public string? OfferedUserName { get; set; }
        public string? RequestedUserName { get; set; }
        public string? OfferedSkillName { get; set; }
        public string? RequestedSkillName { get; set; }
    }
}