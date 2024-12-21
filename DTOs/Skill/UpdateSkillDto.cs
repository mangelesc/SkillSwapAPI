using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SkillSwapAPI.Models.Enums;

namespace SkillSwapAPI.DTOs
{
    public class UpdateSkillDto
    {

        [Required(ErrorMessage = "Skill name is required.")]
        [StringLength(50, ErrorMessage = "Skill name must not exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Skill description is required.")]
        [StringLength(250, ErrorMessage = "Description must not exceed 250 characters.")]
        public string Description { get; set; } = string.Empty;
        public SkillCategory SkillCategory { get; set; }
    }
}