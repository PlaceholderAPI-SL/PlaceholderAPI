namespace PlaceholderAPI.Cloud.Beans
{
    using Newtonsoft.Json;

    /// <summary>
    /// Bean for the Expansions.
    /// </summary>
    public class ECloudExpansion
    {
        /// <summary>
        /// Gets or sets the Id of the Expansion.
        /// </summary>
        [JsonProperty("_id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets whether the Expansion is verified.
        /// </summary>
        [JsonProperty("verified")]
        public bool Verified { get; set; }

        /// <summary>
        /// Gets or sets the GitHub URL of the Expansion.
        /// </summary>
        [JsonProperty("github")]
        public string GitHub { get; set; }

        /// <summary>
        /// Gets or sets the Discord ID of the creator.
        /// </summary>
        [JsonProperty("discordid")]
        public string DiscordId { get; set; }

        /// <summary>
        /// Gets or sets the description of the Expansion.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Repo ID of the Expansion.
        /// </summary>
        [JsonProperty("gitid")]
        public long RepoId { get; set; }

        /// <summary>
        /// Converts JSON string into an array of <see cref="ECloudExpansion"/>.
        /// </summary>
        /// <param name="json">The JSON string.</param>
        /// <returns>Returns an array of <see cref="ECloudExpansion"/>.</returns>
        public static ECloudExpansion[] FromJson(string json) => JsonConvert.DeserializeObject<ECloudExpansion[]>(json);
    }
}
