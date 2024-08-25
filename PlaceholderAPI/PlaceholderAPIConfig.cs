namespace PlaceholderAPI
{
    using System.ComponentModel;
    using Exiled.API.Interfaces;

    /// <summary>
    /// Main Config.
    /// </summary>
    public class PlaceholderAPIConfig : IConfig
    {
        /// <inheritdoc/>
        public bool IsEnabled { get; set; } = true;

        /// <inheritdoc/>
        public bool Debug { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether if to connect to the ECloud.
        /// </summary>
        [Description("It connects you to the PlaceholderAPI Ecloud where you can download Expansions without reloading")]
        public bool ConnectToEcloud { get; set; } = true;
    }
}
