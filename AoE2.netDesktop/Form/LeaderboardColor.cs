namespace AoE2NetDesktop.Form
{
    using System.Drawing;

    /// <summary>
    /// LeaderboardColor.
    /// </summary>
    public struct LeaderboardColor
    {
        /// <summary>
        /// Gets or sets random Map 1v1.
        /// </summary>
        public Color RM1v1 { get; set; }

        /// <summary>
        /// Gets or sets random Map Team.
        /// </summary>
        public Color RMTeam { get; set; }

        /// <summary>
        /// Gets or sets death Match 1v1.
        /// </summary>
        public Color DM1v1 { get; set; }

        /// <summary>
        /// Gets or sets death Match Team.
        /// </summary>
        public Color DMTeam { get; set; }

        /// <summary>
        /// Gets or sets unranked.
        /// </summary>
        public Color Unranked { get; set; }

        /// <summary>
        /// Gets or sets empire Wars Team.
        /// </summary>
        public Color EWTeam { get; set; }

        /// <summary>
        /// Gets or sets empire Wars 1v1.
        /// </summary>
        public Color EW1v1 { get; set; }
    }
}
