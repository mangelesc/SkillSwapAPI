using System.Runtime.Serialization;

namespace SkillSwapAPI.Models.Enums
{
    public enum ExchangeStatus
    {
        [EnumMember(Value = "Pending")]
        Pending = 0,

        [EnumMember(Value = "Accepted")]
        Accepted = 1,

        [EnumMember(Value = "Completed")]
        Completed = 2,

        [EnumMember(Value = "Rejected")]
        Rejected = 3
    }
}
