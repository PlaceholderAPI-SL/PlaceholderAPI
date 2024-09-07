namespace PlaceholderAPI.Cloud
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Exiled.API.Features;
    using Newtonsoft.Json;
    using PlaceholderAPI.Cloud.Beans;

    /// <summary>
    /// Handles the caching and processing of the ECloud database.
    /// </summary>
    public class ECloudDatabase
    {
        private const string Url = "https://papi-website.vercel.app//api/user";
        private const string DatabaseCacheFileName = "data.json";
        private const int CacheTimeInMinutes = 5;

        static ECloudDatabase()
        {
            // If the cache directory doesn't exist, create it.
            if (!CacheDirectory.Exists)
            {
                CacheDirectory.Create();
                return;
            }

            // If the database cache file exists, process the data.
            if (File.Exists(DatabaseCachePath))
            {
                try
                {
                    ProcessData(File.ReadAllText(DatabaseCachePath));
                }
                catch (Exception e)
                {
                    Log.Error("[ECloud Database] There was an error reading the cache files.");
                    Log.Error(e);
                }
            }
        }

        /// <summary>
        /// Gets the path to the cache directory.
        /// </summary>
        private static DirectoryInfo CacheDirectory { get; } = new(Path.Combine(Paths.Configs, "CacheEcloud"));

        /// <summary>
        /// Gets the path to the database cache file.
        /// </summary>
        private static string DatabaseCachePath { get; } = Path.Combine(CacheDirectory.FullName, DatabaseCacheFileName);

        /// <summary>
        /// Stores expansions with their IDs as keys.
        /// </summary>
        private static Dictionary<string, ECloudExpansion> ExpansionsCache { get; } = new();

        /// <summary>
        /// Gets or sets the last time the database was updated.
        /// </summary>
        private static DateTime LastUpdate { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Tries to get the expansion by its name.
        /// </summary>
        /// <param name="name">The name of the expansion.</param>
        /// <param name="expansion">The expansion, if found.</param>
        /// <returns>Returns true if the expansion was found, otherwise false.</returns>
        public static bool TryGetExpansion(string name, out ECloudExpansion expansion)
        {
            expansion = ExpansionsCache.Values.FirstOrDefault(e => e.Id.Equals(name, StringComparison.OrdinalIgnoreCase));
            return expansion != null;
        }

        /// <summary>
        /// Updates the data from the database.
        /// </summary>
        public static async void UpdateData()
        {
            if (DateTime.Now - LastUpdate < TimeSpan.FromMinutes(CacheTimeInMinutes))
            {
                return;
            }

            try
            {
                using HttpClient client = new();
                string response = await client.GetStringAsync(Url);

                if (!string.IsNullOrEmpty(response))
                {
                    ProcessData(response);
                    LastUpdate = DateTime.Now;
                }
            }
            catch (Exception e)
            {
                Log.Error($"[ECloud Database] Failed to update data from {Url}.");
                Log.Error(e);
            }
        }

        /// <summary>
        /// Processes the data from the API.
        /// </summary>
        /// <param name="data">The data to process.</param>
        public static void ProcessData(string data)
        {
            try
            {
                // Deserialize JSON data to an array of expansions using Newtonsoft.Json
                ECloudExpansion[] items = JsonConvert.DeserializeObject<ECloudExpansion[]>(data);

                if (items == null || items.Length == 0)
                {
                    Log.Debug("No items found in the database.");
                    return;
                }

                // Clear the existing cache and populate it with new data
                ExpansionsCache.Clear();
                foreach (ECloudExpansion item in items)
                {
                    Log.Debug($"Processing item: {item.Id} - {item.RepoId}");
                    ExpansionsCache[item.Id] = item;
                }

                // Save the processed data to the cache
                File.WriteAllText(DatabaseCachePath, data);
                Log.Debug($"{nameof(ProcessData)}: Successfully processed the data from the database.");
            }
            catch (Exception e)
            {
                Log.Error("[ECloud Database] There was an error processing the data from the database.");
                Log.Error(e);
            }
        }
    }
}
