#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace AoE2NetDesktop.LibAoE2Net.JsonFormat;

using System.Collections.Generic;
using System.Runtime.Serialization;

using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.Parameters;

/// <summary>
/// Match properties.
/// </summary>
[DataContract]
public class Match
{
    [DataMember(Name = "match_id")]
    public string MatchId { get; set; }

    [DataMember(Name = "match_uuid")]
    public string MatchUuid { get; set; }

    [DataMember(Name = "name")]
    public string Name { get; set; }

    [DataMember(Name = "num_players")]
    public int? NumPlayers { get; set; }

    [DataMember(Name = "num_slots")]
    public int? NumSlots { get; set; }

    [DataMember(Name = "cheats")]
    public bool? Cheats { get; set; }

    [DataMember(Name = "full_tech_tree")]
    public bool? FullTechTree { get; set; }

    [DataMember(Name = "ending_age")]
    public int? EndingAge { get; set; }

    [DataMember(Name = "game_type")]
    public int? GameType { get; set; }

    [DataMember(Name = "has_password")]
    public bool? HasPassword { get; set; }

    [DataMember(Name = "lock_speed")]
    public bool? LockSpeed { get; set; }

    [DataMember(Name = "lock_teams")]
    public bool? LockTeams { get; set; }

    [DataMember(Name = "map_size")]
    public int? MapSize { get; set; }

    [DataMember(Name = "map_type")]
    public int? MapType { get; set; }

    [DataMember(Name = "pop")]
    public int? Pop { get; set; }

    [DataMember(Name = "ranked")]
    public bool? Ranked { get; set; }

    [DataMember(Name = "leaderboard_id")]
    public LeaderboardId? LeaderboardId { get; set; }

    [DataMember(Name = "rating_type")]
    public int? RatingType { get; set; }

    [DataMember(Name = "resources")]
    public int? Resources { get; set; }

    [DataMember(Name = "rms")]
    public string Rms { get; set; }

    [DataMember(Name = "scenario")]
    public string Scenario { get; set; }

    [DataMember(Name = "server")]
    public string Server { get; set; }

    [DataMember(Name = "shared_exploration")]
    public bool? SharedExploration { get; set; }

    [DataMember(Name = "speed")]
    public int? Speed { get; set; }

    [DataMember(Name = "starting_age")]
    public int? StartingAge { get; set; }

    [DataMember(Name = "team_together")]
    public bool? TeamTogether { get; set; }

    [DataMember(Name = "team_positions")]
    public bool? TeamPositions { get; set; }

    [DataMember(Name = "treaty_length")]
    public long? TreatyLength { get; set; }

    [DataMember(Name = "turbo")]
    public bool? Turbo { get; set; }

    [DataMember(Name = "victory")]
    public long? Victory { get; set; }

    [DataMember(Name = "victory_time")]
    public long? VictoryTime { get; set; }

    [DataMember(Name = "started")]
    public long? Started { get; set; }

    [DataMember(Name = "finished")]
    public long? Finished { get; set; }

    [DataMember(Name = "players")]
    public List<Player> Players { get; set; } = new();

    public override string ToString()
        => $"{this.GetOpenedTime()} {Players.Count} Players Map:{this.GetMapName()}";
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented
