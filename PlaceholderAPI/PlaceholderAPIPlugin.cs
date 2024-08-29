namespace PlaceholderAPI
{
    using System;
    using System.IO;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using HarmonyLib;
    using PlaceholderAPI.Cloud;
    using PlaceholderAPI.Placeholders;

    /// <summary>
    /// Main plugin loader.
    /// </summary>
    public class PlaceholderAPIPlugin : Plugin<PlaceholderAPIConfig>
    {
        /// <summary>
        /// Gets the Plugin Instance.
        /// </summary>
        public static PlaceholderAPIPlugin Instance { get; private set; }

        /// <summary>
        /// Gets the Harmony Instance.
        /// </summary>
        public static Harmony HarmonyPatch { get; private set; }

        /// <summary>
        /// The folder containing all the Expansions.
        /// </summary>
        public static readonly string ExpansionPath = Path.Combine(Paths.Plugins, "Expansions");

        private ECloudDatabase handler;

        /// <inheritdoc/>
        public override string Name => "PlaceholderAPI";

        /// <inheritdoc/>
        public override Version Version => new (1, 0, 2);

        /// <inheritdoc/>
        public override string Author => "NotZer0Two";

        /// <inheritdoc/>
        // Note for the people who are trying to understand why this decision of last but this allows to make all the plugin load
        // So in this way i know that if the Placeholders are searching for a plugin that one is loaded 100%
        // - Zer0Two
        public override PluginPriority Priority => PluginPriority.Last;

        /// <inheritdoc/>
        public override void OnEnabled()
        {
            Instance = this;

            if (!Directory.Exists(ExpansionPath))
            {
                Log.Info("I see its your first time opening this plugin.");
                Log.Info("I need to do some setups so if you see new folder is probably me creating them");

                Directory.CreateDirectory(ExpansionPath);

                Log.Info("Setup is completed now Enjoy the plugin.");
            }

            if (this.Config.Harmony.isHarmonyEnabled)
            {
                Log.Info("Harmony not loaded! Patching methods...");

                HarmonyPatch = new Harmony($"{this.Name}_v{this.Version}");

                HarmonyPatch.PatchAll();
            }

            new PlayerPlaceholders().Register();
            new ServerPlaceholders().Register();
            new Scp0492Placeholder().Register();
            new Scp049Placeholder().Register();
            new Scp079Placeholder().Register();
            new Scp096Placeholder().Register();
            new Scp106Placeholder().Register();
            new Scp173Placeholder().Register();
            new Scp939Placeholder().Register();
            new Scp3114Placeholder().Register();

            API.PlaceholderAPI.RegisterPlaceholdersFromExpansions();
            API.PlaceholderAPI.RegisterPlaceholdersFromPlugins();

            if (this.Config.ConnectToEcloud)
            {
                ECloudDatabase.UpdateData();
                this.handler = new ();

                Log.Info("[ECloud] Connected with success to the ECloud");
            }

            base.OnEnabled();
        }

        /// <inheritdoc/>
        public override void OnDisabled()
        {
            if (this.Config.Harmony.isHarmonyEnabled)
            {
                HarmonyPatch.UnpatchAll();
                HarmonyPatch = null;
            }

            Instance = null;
            base.OnDisabled();
        }
    }
}
