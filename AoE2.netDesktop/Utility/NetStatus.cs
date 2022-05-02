namespace LibAoE2net
{
    /// <summary>
    /// Network status.
    /// </summary>
    public enum NetStatus
    {
        /// <summary>
        /// Communication timeout.
        /// </summary>
        ComTimeout,

        /// <summary>
        /// Network has connected.
        /// </summary>
        Connected,

        /// <summary>
        /// Connecting Now.
        /// </summary>
        Connecting,

        /// <summary>
        /// Network does not connected yet.
        /// </summary>
        Disconnected,

        /// <summary>
        /// Request with invalid parameter.
        /// </summary>
        InvalidRequest,

        /// <summary>
        /// Server error.
        /// </summary>
        ServerError,
    }
}
