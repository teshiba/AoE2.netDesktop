namespace AoE2NetDesktop
{
    /// <summary>
    /// Match result.
    /// </summary>
    public enum MatchResult
    {
        /// <summary>
        /// match was victorious.
        /// </summary>
        Victorious,

        /// <summary>
        /// match was defeated.
        /// </summary>
        Defeated,

        /// <summary>
        /// Match is in progress.
        /// </summary>
        InProgress,

        /// <summary>
        /// Match is unknown status.
        /// </summary>
        Unknown,

        /// <summary>
        /// Not started.
        /// </summary>
        NotStarted,

        /// <summary>
        /// match was finished but unknown result.
        /// </summary>
        Finished,
    }
}