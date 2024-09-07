namespace PlaceholderAPI
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Exiled.API.Interfaces;
    using PlaceholderAPI.Configs;

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

        /// <summary>
        /// Gets or sets a value indicating whether if to download unsafe expansions from the ECloud.
        /// </summary>
        [Description("It allows you to download unverified expansions from the ECloud")]
        public bool DownloadUnsafeFromEcloud { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether if Harmony config section.
        /// </summary>
        [Description("Config section releated to modify in-game aspects")]
        public HarmonyConfigSection Harmony { get; set; } = new ();
    }
}
