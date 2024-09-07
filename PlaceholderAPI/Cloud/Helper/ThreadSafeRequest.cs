namespace PlaceholderAPI.Cloud.Helper
{
    using System.Collections.Generic;

    using Exiled.API.Features;
    using MEC;
    using UnityEngine.Networking;

    /// <summary>
    /// Thread safe Request.
    /// </summary>
    internal sealed class ThreadSafeRequest
    {
        /// <summary>
        /// Handles the Safe Thread Request.
        /// </summary>
        private volatile bool done;

        /// <summary>
        /// Gets the result.
        /// </summary>
        public string Result { get; private set; }

        /// <summary>
        /// Gets a value indicating whether it was successful.
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Gets the HTTP Status Code.
        /// </summary>
        public long Code { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the request was successful.
        /// </summary>
        public bool Done => this.done;

        /// <summary>
        /// Gets the call to the website to obtain users to their roles.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="etag">The entity tag of the request.</param>
        public static void Go(string url, string etag)
        {
            Timing.RunCoroutine(MakeRequest(url, etag), Segment.LateUpdate);
        }

        private static IEnumerator<float> MakeRequest(string url, string etag)
        {
            ThreadSafeRequest request = new ();

            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            webRequest.SetRequestHeader("User-Agent", "Ecloud");
            webRequest.SetRequestHeader("If-None-Match", etag);

            yield return Timing.WaitUntilDone(webRequest.SendWebRequest());

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                request.Result = webRequest.downloadHandler.text;
                request.Success = true;
                request.Code = webRequest.responseCode;
            }
            else
            {
                request.Success = false;
                request.Code = webRequest.responseCode;
                Log.Debug($"{nameof(MakeRequest)}: Response: {request.Result} Code: {request.Code}");
            }

            request.done = true;
        }
    }
}
