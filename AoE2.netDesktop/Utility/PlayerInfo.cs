namespace AoE2NetDesktop
{
    using System;

    /// <summary>
    /// Player Informations.
    /// </summary>
    public class PlayerInfo
    {
        /// <summary>
        /// Gets or sets country Name.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets profile ID.
        /// </summary>
        public int? ProfileId { get; set; }

        /// <summary>
        /// Gets or sets 1v1 Random map rate.
        /// </summary>
        public int? Rate1v1RM { get; set; }

        /// <summary>
        /// Gets or sets team Random map rate.
        /// </summary>
        public int? RateTeamRM { get; set; }

        /// <summary>
        /// Gets or sets game count that player is ally.
        /// </summary>
        public int GamesAlly { get; set; }

        /// <summary>
        /// Gets or sets game count that player is enemy.
        /// </summary>
        public int GamesEnemy { get; set; }

        /// <summary>
        /// Gets or sets game count of 1v1 random map.
        /// </summary>
        public int Games1v1 { get; set; }

        /// <summary>
        /// Gets or sets game count of team random map.
        /// </summary>
        public int GamesTeam { get; set; }

        /// <summary>
        /// Gets or sets last match date.
        /// </summary>
        public DateTime LastDate { get; set; }
    }
}
