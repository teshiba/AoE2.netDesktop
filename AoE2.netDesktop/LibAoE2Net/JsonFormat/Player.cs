#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace AoE2NetDesktop.LibAoE2Net.JsonFormat;

using System.Runtime.Serialization;

/// <summary>
/// Players properties.
/// </summary>
[DataContract]

public class Player
{
    [DataMember(Name = "profile_id")]
    public int? ProfilId { get; set; }

    [DataMember(Name = "name")]
    public string Name { get; set; }

    [DataMember(Name = "clan")]
    public string Clan { get; set; }

    [DataMember(Name = "country")]
    public string Country { get; set; }

    [DataMember(Name = "slot")]
    public int? Slot { get; set; }

    [DataMember(Name = "slot_type")]
    public int? SlotType { get; set; }

    [DataMember(Name = "rating")]
    public int? Rating { get; set; }

    [DataMember(Name = "rating_change")]
    public string RatingChange { get; set; }

    [DataMember(Name = "color")]
    public int? Color { get; set; }

    [DataMember(Name = "team")]
    public int? Team { get; set; }

    [DataMember(Name = "civ")]
    public int? Civ { get; set; }

    [DataMember(Name = "won")]
    public bool? Won { get; set; }

    public override string ToString()
    {
        return $"[{Color}]{Name}(R:{Rating}) ID:{ProfilId}";
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented