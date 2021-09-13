#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace LibAoE2net
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class LeaderboardContainer
    {
        [DataMember(Name = "total")]
        public int Total { get; set; }

        [DataMember(Name = "leaderboard_id")]
        public LeaderBoardId LeaderBoardId { get; set; }

        [DataMember(Name = "start")]
        public int Start { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "leaderboard")]
        public List<Leaderboard> Leaderboards { get; set; }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented
