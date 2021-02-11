#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace LibAoE2net
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Players properties.
    /// </summary>
    [DataContract]

    public class Players
    {
        [DataMember(Name = "profile_id")]
        public int ProfilId { get; set; }

        [DataMember(Name = "steam_id")]
        public string SteamId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "clan")]
        public string Clan { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "slot")]
        public int Slot { get; set; }

        [DataMember(Name = "slot_type")]
        public int SlotType { get; set; }

        [DataMember(Name = "rating")]
        public int? Rating { get; set; }

        [DataMember(Name = "rating_change")]
        public string RatingChange { get; set; }

        [DataMember(Name = "games")]
        public int? Games { get; set; }

        [DataMember(Name = "wins")]
        public int? Wins { get; set; }

        [DataMember(Name = "streak")]
        public int? Streak { get; set; }

        [DataMember(Name = "drops")]
        public int? Drops { get; set; }

        [DataMember(Name = "color")]
        public int Color { get; set; }

        [DataMember(Name = "team")]
        public int Team { get; set; }

        [DataMember(Name = "civ")]
        public int Civ { get; set; }

        [DataMember(Name = "won")]
        public bool? Won { get; set; }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore SA1600 // Elements should be documented