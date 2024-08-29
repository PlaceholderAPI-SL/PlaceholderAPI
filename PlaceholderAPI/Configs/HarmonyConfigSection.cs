using System.Collections.Generic;
using System.ComponentModel;

namespace PlaceholderAPI.Configs
{

    /// <summary>
    /// Config section for harmony.
    /// </summary>
    public class HarmonyConfigSection
    {
        /// <summary>
        /// Gets or sets harmony support.
        /// </summary>
        [Description("This allows the plugin to modify the game to apply tags everywhere")]
        public bool isHarmonyEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets harmony support for hints.
        /// </summary>
        [Description("This allows you to enable / disable the hint patch")]
        public bool Hints { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets harmony support for Broadcast.
        /// </summary>
        [Description("This allows you to enable / disable the Broadcast patch")]
        public bool Broadcast { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets harmony support for Command Interpolation.
        /// </summary>
        [Description("This allows you to enable / disable the Command Interpolation patch")]
        public bool CommandInterpolation { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets harmony support for Command.
        /// </summary>
        [Description("This allows you to enable / disable the Commands modification patch")]
        public bool Commands { get; set; } = true;
    }
}
