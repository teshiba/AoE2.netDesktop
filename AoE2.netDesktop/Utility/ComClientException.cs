namespace AoE2NetDesktop.Utility
{
    using System;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    using static System.Windows.Forms.VisualStyles.VisualStyleElement;

    /// <summary>
    /// ComClient exception.
    /// </summary>
    [Serializable]
    public class ComClientException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComClientException"/> class.
        /// </summary>
        public ComClientException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComClientException"/> class.
        /// </summary>
        /// <param name="message">exception messages as we discover problems.</param>
        public ComClientException(string message)
            : this(message, NetStatus.ServerError, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComClientException"/> class.
        /// </summary>
        /// <param name="message">exception messages as we discover problems.</param>
        /// <param name="status">network status.</param>
        public ComClientException(string message, NetStatus status)
            : this(message, status, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComClientException"/> class.
        /// </summary>
        /// <param name="message">exception messages as we discover problems.</param>
        /// <param name="status">network status.</param>
        /// <param name="innerException">innner exception.</param>
        public ComClientException(string message, NetStatus status, Exception innerException)
            : base(message, innerException)
        {
            Status = status;
            Debug.Print($"Request Error: {message} : {innerException?.Message}");
        }

        /// <summary>
        /// Gets network status.
        /// </summary>
        public NetStatus Status { get; init; }
    }
}