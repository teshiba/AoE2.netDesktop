namespace LibAoE2net
{
    using System.Diagnostics;
    using System.IO;
    using System.Net.Http;
    using System.Runtime.Serialization.Json;
    using System.Text;
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
            => await base.GetStringAsync(requestUri);

        /// <summary>
        /// Sends a GET request to the specified Uri and returns the value
        /// that results from deserializing the response body as JSON in an asynchronous operation.
        /// </summary>
        /// <typeparam name="TValue">The target type to deserialize to.</typeparam>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task<TValue> GetFromJsonAsync<TValue>(string requestUri)
            where TValue : new()
        {
            TValue ret;
            try {
                Debug.Print($"Send Request {BaseAddress}{requestUri}");

                var jsonText = await GetStringAsync(requestUri);
                Debug.Print($"Get JSON {typeof(TValue)} {jsonText}");

                var serializer = new DataContractJsonSerializer(typeof(TValue));
                var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonText));
                ret = (TValue)serializer.ReadObject(stream);
            } catch (HttpRequestException e) {
                Debug.Print($"Request Error: {e.Message}");
                throw;
            } catch (TaskCanceledException e) {
                Debug.Print($"Timeout: {e.Message}");
                throw;
            }

            return ret;
        }
    }
}