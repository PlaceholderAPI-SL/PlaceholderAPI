namespace PlaceholderAPI.Cloud.Helper
{
    using System.Collections.Generic;
    using System.IO;
    using Exiled.API.Features;
    using MEC;
    using Newtonsoft.Json;
    using PlaceholderAPI.API;
    using UnityEngine.Networking;

    /// <summary>
    /// Main method to download Expansion.
    /// </summary>
    public class ECloudDownloader
    {
        /// <summary>
        /// The postfix that Files needs to have.
        /// </summary>
        public const string TargetFileName = "-Ecloud";

        /// <summary>
        /// Starts the download for a Expansion.
        /// </summary>
        /// <param name="repoId">Repository to get.</param>
        public static void Download(long repoId)
        {
            Timing.RunCoroutine(SearchAndDownloadFile($"https://api.github.com/repositories/{repoId}/releases"));
        }

        private static IEnumerator<float> SearchAndDownloadFile(string releaseUrl)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(releaseUrl))
            {
                request.SetRequestHeader("User-Agent", "ECloud.Downloader");
                yield return Timing.WaitUntilDone(request.SendWebRequest());

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Log.Error("[ECloud] Release fetch failed: " + request.error);
                    yield break;
                }

                string responseText = request.downloadHandler.text;
                string downloadUrl = ExtractDownloadUrlFromResponse(responseText);
                string assetsName = ExtractAssetsNameFromResponse(responseText);

                if (string.IsNullOrEmpty(downloadUrl))
                {
                    yield break;
                }

                using (UnityWebRequest downloadRequest = UnityWebRequest.Get(downloadUrl))
                {
                    downloadRequest.SetRequestHeader("User-Agent", "UnityApp");
                    yield return Timing.WaitUntilDone(downloadRequest.SendWebRequest());

                    if (downloadRequest.result != UnityWebRequest.Result.Success)
                    {
                        Log.Error("[ECloud] Download failed: " + downloadRequest.error);
                        yield break;
                    }

                    byte[] fileData = downloadRequest.downloadHandler.data;
                    string filePath = Path.Combine(PlaceholderAPIPlugin.ExpansionPath, assetsName);
                    File.WriteAllBytes(filePath, fileData);

                    API.PlaceholderAPI.RegisterPlaceholdersFromExpansion(filePath);

                    Log.Debug("[ECloud] File downloaded and saved to: " + filePath);
                }
            }
        }

        private static string ExtractAssetsNameFromResponse(string responseText)
        {
            var releases = JsonConvert.DeserializeObject<GitHubRelease[]>(responseText);

            foreach (var release in releases)
            {
                foreach (var asset in release.Assets)
                {
                    if (asset.Name.Contains(TargetFileName) && asset.Name.EndsWith(".dll"))
                    {
                        return asset.Name;
                    }
                }
            }

            return null;
        }

        private static string ExtractDownloadUrlFromResponse(string responseText)
        {
            var releases = JsonConvert.DeserializeObject<GitHubRelease[]>(responseText);

            foreach (var release in releases)
            {
                foreach (var asset in release.Assets)
                {
                    if (asset.Name.Contains(TargetFileName) && asset.Name.EndsWith(".dll"))
                    {
                        return asset.BrowserDownloadUrl;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Github Releases.
        /// </summary>
        public class GitHubRelease
        {
            /// <summary>
            /// Gets or sets files inside a release.
            /// </summary>
            [JsonProperty("assets")]
            public GitHubAsset[] Assets { get; set; }
        }

        /// <summary>
        /// Github Assets from Release.
        /// </summary>
        public class GitHubAsset
        {
            /// <summary>
            /// Gets or sets name of the File.
            /// </summary>
            [JsonProperty("name")]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets download for that file.
            /// </summary>
            [JsonProperty("browser_download_url")]
            public string BrowserDownloadUrl { get; set; }
        }
    }
}
