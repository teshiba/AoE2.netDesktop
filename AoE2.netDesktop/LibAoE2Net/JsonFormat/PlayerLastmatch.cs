#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace AoE2NetDesktop.LibAoE2Net.JsonFormat;

using System.Runtime.Serialization;

/// <summary>
/// Player last match data.
/// </summary>
[DataContract]
public class PlayerLastmatch
{
    [DataMember(Name = "profile_id")]
    public int? ProfileId { get; set; }

    [DataMember(Name = "steam_id")]
    public string SteamId { get; set; }

    [DataMember(Name = "name")]
    public string Name { get; set; }

    [DataMember(Name = "country")]
    public string Country { get; set; }

    [DataMember(Name = "last_match")]
    public Match LastMatch { get; set; } = new ();
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented