using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SkillSwapAPI.Models.Enums
{
    public enum SkillCategory
    {
        [EnumMember(Value = "Software Development")]
        SoftwareDevelopment = 0,

        [EnumMember(Value = "Languages")]
        Languages = 1,

        [EnumMember(Value = "Manual Skills")]
        ManualSkills = 2,

        [EnumMember(Value = "Creative Arts")]
        CreativeArts = 3,

        [EnumMember(Value = "Others")]
        Others = 4
    }
}