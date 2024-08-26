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
        /// Gets or sets harmony support.
        /// </summary>
        [Description("This allows you to enable / disable the patches you don't want the plugin to do")]
        public Dictionary<string, bool> TagsAllowed { get; set; } = new ()
        {
            ["Hints"] = true,
            ["Broadcast"] = true,
            ["CommandInterpolation"] = true,
        };

    }
}
