#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace LibAoE2net
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Strings data.
    /// </summary>
    [DataContract]
    public class Strings
    {
        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "age")]
        public List<StringId> Age { get; set; }

        [DataMember(Name = "civ")]
        public List<StringId> Civ { get; set; }

        [DataMember(Name = "game_type")]
        public List<StringId> GameType { get; set; }

        [DataMember(Name = "leaderboard")]
        public List<StringId> Leaderboard { get; set; }

        [DataMember(Name = "map_size")]
        public List<StringId> MapSize { get; set; }

        [DataMember(Name = "map_type")]
        public List<StringId> MapType { get; set; }

        [DataMember(Name = "rating_type")]
        public List<StringId> RatingType { get; set; }

        [DataMember(Name = "resources")]
        public List<StringId> Resources { get; set; }

        [DataMember(Name = "speed")]
        public List<StringId> Speed { get; set; }

        [DataMember(Name = "victory")]
        public List<StringId> Victory { get; set; }

        [DataMember(Name = "visibility")]
        public List<StringId> Visibility { get; set; }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented
