namespace PlaceholderAPI.Cloud
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Exiled.API.Features;
    using PlaceholderAPI.Cloud.Beans;
    using PlaceholderAPI.Cloud.Helper;

    /// <summary>
    /// Basic for the ECloud.
    /// </summary>
    public class ECloudDatabase
    {
        private const string Url = "https://raw.githubusercontent.com/PlaceholderAPI-SL/ECloud/main/data.yml";
        private const string ETagCacheFileName = "ecloud_cache.txt";
        private const string DatabaseCacheFileName = "data.yml";
        private const int CacheTimeInMinutes = 5;

        static ECloudDatabase()
        {
            // If the cache directory doesn't exist, create it.
            if (!CacheDirectory.Exists)
            {
                CacheDirectory.Create();
                return;
            }

            // If the database cache file exists we process the data.
            if (!File.Exists(DatabaseCachePath))
            {
                return;
            }

            try
            {
                ProcessData(File.ReadAllText(DatabaseCachePath));

                // If the ETag cache file exists we read the data.
                if (File.Exists(ECachePath))
                {
                    ECache = File.ReadAllText(ECachePath);
                }
            }
            catch (Exception e)
            {
                Log.Error($"[ECloud Database] There was an error reading the cache files.");
                Log.Error(e);
            }
        }

        /// <summary>
        /// Gets the path to the cache directory.
        /// </summary>
        private static DirectoryInfo CacheDirectory { get; } = new (Path.Combine(Paths.Configs, "CacheEcloud"));

        /// <summary>
        /// Gets the path to the cache file.
        /// </summary>
        private static string ECachePath { get; } = Path.Combine(CacheDirectory.FullName, ETagCacheFileName);

        /// <summary>
        /// Gets the path to the database cache file.
        /// </summary>
        private static string DatabaseCachePath { get; } = Path.Combine(CacheDirectory.FullName, DatabaseCacheFileName);

        /// <summary>
        /// Gets a <see cref="Dictionary{TKey,TValue}"/> of recently cached userIds and their ranks.
        /// </summary>
        private static Dictionary<string, long> RankCache { get; } = new ();

        /// <summary>
        /// Gets or sets the ETag cache.
        /// </summary>
        private static string ECache { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the last time the database was updated.
        /// </summary>
        private static DateTime LastUpdate { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Tries to get the rank of a user from the cache.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <param name="url">The rank of the user.</param>
        /// <returns>Returns a value indicating whether the rank was found.</returns>
        public static bool TryGetUrl(string id, out long url)
        {
            return RankCache.TryGetValue(id, out url);
        }

        /// <summary>
        /// Updates the data from the database.
        /// </summary>
        public static void UpdateData()
        {
            if (DateTime.Now - LastUpdate < TimeSpan.FromMinutes(CacheTimeInMinutes))
            {
                return;
            }

            ThreadSafeRequest.Go(Url, ECache);
            LastUpdate = DateTime.Now;
        }

        /// <summary>
        /// Saves the ETag to the cache.
        /// </summary>
        /// <param name="etag">The ETag to save.</param>
        public static void SaveETag(string etag)
        {
            ECache = etag;
            File.WriteAllText(ECachePath, etag);
            Log.Debug($"{nameof(SaveETag)}: Successfully saved the ETag to the cache.");
        }

        /// <summary>
        /// Processes the data from the database.
        /// </summary>
        /// <param name="data">The data to process.</param>
        public static void ProcessData(string data)
        {
            try
            {
                ECloudExpansion[] items = ECloudExpansion.FromYaml(data);

                if (items is null || items.Length == 0)
                {
                    Log.Debug("No items found in the database.");
                    return;
                }

                foreach (ECloudExpansion item in items)
                {
                    Log.Debug($"Processing item: {item.Id} - {item.RepoId}");
                    RankCache[item.Id] = item.RepoId;
                }

                File.WriteAllText(DatabaseCachePath, data);
                Log.Debug($"{nameof(ProcessData)}: Successfully processed the data from the database.");
            }
            catch (Exception e)
            {
                Log.Error("There was an error processing the data from the database.");
                Log.Error(e);
            }
        }
    }
}
