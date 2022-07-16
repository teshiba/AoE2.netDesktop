#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace AoE2NetDesktop.LibAoE2Net.JsonFormat;

using AoE2NetDesktop.Utility.SysApi;

using System.Runtime.Serialization;

/// <summary>
/// Player last match data.
/// </summary>
[DataContract]
public class PlayerRating
{
    [DataMember(Name = "rating")]
    public int? Rating { get; set; }

    [DataMember(Name = "num_wins")]
    public int? NumWins { get; set; }

    [DataMember(Name = "num_losses")]
    public int? NumLosses { get; set; }

    [DataMember(Name = "streak")]
    public int? Streak { get; set; }

    [DataMember(Name = "drops")]
    public int? Drops { get; set; }

    [DataMember(Name = "timestamp")]
    public long? TimeStamp { get; set; }

    public override string ToString()
    {
        return $"R:{Rating} W:{NumWins} L:{NumLosses} Str:{Streak} Drp:{Drops} Time:{DateTimeExt.FromUnixTimeSeconds(TimeStamp)}";
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented