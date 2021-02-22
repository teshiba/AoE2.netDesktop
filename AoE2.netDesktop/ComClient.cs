namespace LibAoE2net
{
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Client of communication class.
    /// </summary>
    public class ComClient : HttpClient
    {
        /// <summary>
        /// Send a GET request to the specified Uri and return the response body as a string
        /// in an asynchronous operation.
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public new virtual async Task<string> GetStringAsync(string requestUri)
            => await new HttpClient().GetStringAsync(requestUri);
    }
}