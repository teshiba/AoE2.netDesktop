namespace LibAoE2net
{
    /// <summary>
    /// Network status.
    /// </summary>
    public enum NetStatus
    {
        /// <summary>
        /// Connecting Now.
        /// </summary>
        Connecting,

        /// <summary>
        /// Network has connected.
        /// </summary>
        Connected,

        /// <summary>
        /// Network does not connected yet.
        /// </summary>
        Disconnected,

        /// <summary>
        /// server error.
        /// </summary>
        ServerError,

        /// <summary>
        /// communication timeout.
        /// </summary>
        ComTimeout,
    }
}
