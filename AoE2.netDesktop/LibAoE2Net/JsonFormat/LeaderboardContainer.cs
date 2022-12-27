#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace AoE2NetDesktop.LibAoE2Net.JsonFormat;

using System.Collections.Generic;
using System.Runtime.Serialization;

using AoE2NetDesktop.LibAoE2Net.Parameters;

[DataContract]
public class LeaderboardContainer
{
    [DataMember(Name = "total")]
    public int Total { get; set; }

    [DataMember(Name = "leaderboard_id")]
    public LeaderboardId LeaderBoardId { get; set; }

    [DataMember(Name = "start")]
    public int Start { get; set; }

    [DataMember(Name = "count")]
    public int Count { get; set; }

    [DataMember(Name = "leaderboard")]
    public List<Leaderboard> Leaderboards { get; set; }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented
