namespace PlaceholderAPI.Cloud.Beans
{
    using Serialization;

    /// <summary>
    /// Bean for the Expansions.
    /// </summary>
    public class ECloudExpansion
    {
        /// <summary>
        /// Gets or sets the Id of the Expansion.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Repoid.
        /// </summary>
        public long repoid { get; set; }

        /// <summary>
        /// Gets all the tag items from a yaml string.
        /// </summary>
        /// <param name="yaml">The yaml string.</param>
        /// <returns>Returns an array of <see cref="TagItem"/>.</returns>
        public static ECloudExpansion[] FromYaml(string yaml) => YamlParser.Deserializer.Deserialize<ECloudExpansion[]>(yaml);
    }
}
