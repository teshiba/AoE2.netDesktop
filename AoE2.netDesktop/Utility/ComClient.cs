﻿namespace AoE2NetDesktop.Utility;

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

using AoE2NetDesktop.Utility.SysApi;

/// <summary>
/// Client of communication class.
/// </summary>
public class ComClient : HttpClient
{
    /// <summary>
    /// Gets or sets system API.
    /// </summary>
    public ISystemApi SystemApi { get; set; } = new SystemApi(new User32Api());

    /// <summary>
    /// Gets or sets the base address of CivImage Resource URI of the AoE2net.
    /// </summary>
    public Uri CivImageBaseAddress { get; set; }

    /// <summary>
    /// Send a GET request to the specified Uri and return the response body as a string
    /// in an asynchronous operation.
    /// </summary>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    public new virtual async Task<string> GetStringAsync(string requestUri)
        => await base.GetStringAsync(requestUri).ConfigureAwait(false);

    /// <summary>
    /// Sends a GET request to the specified Uri and returns the value
    /// that results from deserializing the response body as JSON in an asynchronous operation.
    /// </summary>
    /// <typeparam name="TValue">The target type to deserialize to.</typeparam>
    /// <param name="requestUri">The Uri the request is sent to.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    /// <exception cref="ComClientException">ComClient Exception.</exception>
    public async Task<TValue> GetFromJsonAsync<TValue>(string requestUri)
        where TValue : new()
    {
        TValue ret = default;

        if(requestUri != null) {
            try {
                Debug.Print($"Send Request {BaseAddress}{requestUri}");

                var jsonText = await GetStringAsync(requestUri).ConfigureAwait(false);
                Log.Info($"Get JSON {typeof(TValue)} {jsonText}");

                using var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonText));
                var serializer = new DataContractJsonSerializer(typeof(TValue));
                ret = (TValue)serializer.ReadObject(stream);
            } catch(HttpRequestException e) {
                NetStatus netStatus;
                if(e.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    netStatus = NetStatus.InvalidRequest;
                } else {
                    netStatus = NetStatus.ServerError;
                }

                throw new ComClientException("Request Error", netStatus, e);
            } catch(TaskCanceledException e) {
                throw new ComClientException("Timeout", NetStatus.ComTimeout, e);
            }
        }

        return ret;
    }

    /// <summary>
    /// Open specified URI.
    /// </summary>
    /// <param name="requestUri">URI string.</param>
    /// <returns>browser process.</returns>
    public virtual Process OpenBrowser(string requestUri)
    {
        Process ret;
        try {
            ret = SystemApi.Start(requestUri);
        } catch(Win32Exception noBrowser) {
            Debug.Print(noBrowser.Message);
            throw;
        } catch(Exception other) {
            Debug.Print(other.Message);
            throw;
        }

        return ret;
    }

    /// <summary>
    /// Gets Image file location on AoE2.net.
    /// </summary>
    /// <param name="civName">civilization name in English.</param>
    /// <returns>Image file location.</returns>
    public virtual string GetCivImageLocation(string civName)
        => $"{CivImageBaseAddress}{civName.ToLower()}.png";
}
