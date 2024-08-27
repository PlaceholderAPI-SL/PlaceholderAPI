namespace PlaceholderAPI.API
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using Exiled.API.Features;
    using Exiled.API.Interfaces;
    using Exiled.Loader;
    using global::PlaceholderAPI.API.Abstract;
    using YamlDotNet.Core.Tokens;

    /// <summary>
    /// Main API for comunicating with PAPI.
    /// </summary>
    public static class PlaceholderAPI
    {
        /// <summary>
        /// The list of all Placeholders Registered.
        /// </summary>
        public static readonly Dictionary<string, PlaceholderExpansion> Placeholders = new Dictionary<string, PlaceholderExpansion>();

        /// <summary>
        /// Basic pattern used by the plugin to identify tags.
        /// </summary>
        private const string PlaceholderPattern = @"%(?<identifier>[a-zA-Z0-9]+)(?:_(?<params>[^%]*))?%";

        /// <summary>
        /// Basic pattern used for nested placeholders.
        /// </summary>
        private const string BracketPlaceholderPattern = @"\{(?<identifier>[a-zA-Z0-9]+)(?:_(?<params>[^{}]*))?\}";

        /// <summary>
        /// Pre-Compiled Regex that lets verify the Pattern.
        /// </summary>
        private static readonly Regex PlaceholderRegex = new Regex(PlaceholderPattern);

        /// <summary>
        /// Pre-Compiled Regex that lets verify the Pattern.
        /// </summary>
        private static readonly Regex BracketPlaceholderRegex = new Regex(BracketPlaceholderPattern);

        /// <summary>
        /// Sets the placeholder of that player.
        /// </summary>
        /// <param name="player">The player to pass for the placeholders.</param>
        /// <param name="text">The string that needs to be replaced the placeholders.</param>
        /// <returns>The string with the placeholder replaced.</returns>
        public static string SetPlaceholders(Player player, string text)
        {
            return PlaceholderRegex.Replace(text, match =>
            {
                string identifier = match.Groups["identifier"].Value;
                string parameters = match.Groups["params"].Value;

                if (Placeholders.TryGetValue(identifier, out PlaceholderExpansion replacement))
                {
                    bool hasPlayerBased = replacement.GetType().GetMethod("OnRequest", BindingFlags.Public | BindingFlags.Instance)?.DeclaringType != typeof(PlaceholderExpansion);
                    bool hasOfflineBased = replacement.GetType().GetMethod("OnOfflineRequest", BindingFlags.Public | BindingFlags.Instance)?.DeclaringType != typeof(PlaceholderExpansion);

                    if (hasPlayerBased)
                    {
                        string result = replacement.OnRequest(player, parameters);
                        if (result != null)
                        {
                            return result;
                        }
                    }

                    if (hasOfflineBased)
                    {
                        string result = replacement.OnOfflineRequest(parameters);
                        if (result != null)
                        {
                            return result;
                        }
                    }

                    if (hasPlayerBased || hasOfflineBased)
                    {
                        return "NaN";
                    }

                    Log.Warn($"The expansion {replacement.Identifier} has no valid methods for placeholders. Please contact {replacement.Author}.");
                    return match.Value;
                }
                else
                {
                    return match.Value;
                }
            });
        }

        /// <summary>
        /// Sets the placeholder without a player.
        /// </summary>
        /// <param name="text">The string that needs to be replaced the placeholders.</param>
        /// <returns>The string with the placeholder replaced.</returns>
        public static string SetPlaceholders(string text)
        {
            return PlaceholderRegex.Replace(text, match =>
            {
                string identifier = match.Groups["identifier"].Value;
                string parameters = match.Groups["params"].Value;
                if (Placeholders.TryGetValue(identifier, out PlaceholderExpansion replacement))
                {
                    bool isOfflineBased = replacement.GetType().GetMethod("OnOfflineRequest", BindingFlags.Public | BindingFlags.Instance)?.DeclaringType != typeof(PlaceholderExpansion);

                    if (isOfflineBased)
                    {
                        return replacement.OnOfflineRequest(parameters) ?? "NaN";
                    }
                    else
                    {
                        return match.Value;
                    }
                }
                else
                {
                    return match.Value;
                }
            });
        }

        /// <summary>
        /// Sets the brackets placeholder of that player.
        /// </summary>
        /// <param name="player">The player to pass for the placeholders.</param>
        /// <param name="text">The string that needs to be replaced the placeholders.</param>
        /// <returns>The string with the placeholder replaced.</returns>
        public static string SetBracketsPlaceholders(Player player, string text)
        {
            return BracketPlaceholderRegex.Replace(text, match =>
            {
                string identifier = match.Groups["identifier"].Value;
                string parameters = match.Groups["params"].Value;

                if (Placeholders.TryGetValue(identifier, out PlaceholderExpansion replacement))
                {
                    bool hasPlayerBased = replacement.GetType().GetMethod("OnRequest", BindingFlags.Public | BindingFlags.Instance)?.DeclaringType != typeof(PlaceholderExpansion);
                    bool hasOfflineBased = replacement.GetType().GetMethod("OnOfflineRequest", BindingFlags.Public | BindingFlags.Instance)?.DeclaringType != typeof(PlaceholderExpansion);

                    if (hasPlayerBased)
                    {
                        string result = replacement.OnRequest(player, parameters);
                        if (result != null)
                        {
                            return result;
                        }
                    }

                    if (hasOfflineBased)
                    {
                        string result = replacement.OnOfflineRequest(parameters);
                        if (result != null)
                        {
                            return result;
                        }
                    }

                    if (hasPlayerBased || hasOfflineBased)
                    {
                        return "NaN";
                    }

                    Log.Warn($"The expansion {replacement.Identifier} has no valid methods for placeholders. Please contact {replacement.Author}.");
                    return match.Value;
                }
                else
                {
                    return match.Value;
                }
            });
        }

        /// <summary>
        /// Sets the brackets placeholder without a player.
        /// </summary>
        /// <param name="text">The string that needs to be replaced the placeholders.</param>
        /// <returns>The string with the placeholder replaced.</returns>
        public static string SetBracketsPlaceholders(string text)
        {
            return BracketPlaceholderRegex.Replace(text, match =>
            {
                string identifier = match.Groups["identifier"].Value;
                string parameters = match.Groups["params"].Value;
                if (Placeholders.TryGetValue(identifier, out PlaceholderExpansion replacement))
                {
                    bool isOfflineBased = replacement.GetType().GetMethod("OnOfflineRequest", BindingFlags.Public | BindingFlags.Instance)?.DeclaringType != typeof(PlaceholderExpansion);

                    if (isOfflineBased)
                    {
                        return replacement.OnOfflineRequest(parameters) ?? "NaN";
                    }
                    else
                    {
                        return match.Value;
                    }
                }
                else
                {
                    return match.Value;
                }
            });
        }

        /// <summary>
        /// Registers the placeholders from other plugins.
        /// </summary>
        internal static void RegisterPlaceholdersFromPlugins()
        {
            foreach (IPlugin<IConfig> plugin in Loader.Plugins.Where(plugin => plugin.Name != PlaceholderAPIPlugin.Instance.Name))
            {
                Log.Debug($"[Importer Manager] Try Importing plugin {plugin.Name}");

                foreach (Type type in plugin.Assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(PlaceholderExpansion)) && !type.IsAbstract)
                    {
                        try
                        {
                            PlaceholderExpansion instance = (PlaceholderExpansion)Activator.CreateInstance(type);

                            instance.Register();
                        }
                        catch (Exception ex)
                        {
                            Log.Error($"[Importer SafeGuard] An error was found inside {plugin.Name}, the error is: {ex.Message}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Registers the placeholders from other plugins.
        /// </summary>
        internal static void RegisterPlaceholdersFromExpansions()
        {
            foreach (string dll in Directory.GetFiles(PlaceholderAPIPlugin.ExpansionPath, "*.dll"))
            {
                Assembly assembly = Assembly.LoadFrom(dll);
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(PlaceholderExpansion)) && !type.IsAbstract)
                    {
                        try
                        {
                            PlaceholderExpansion instance = (PlaceholderExpansion)Activator.CreateInstance(type);

                            instance.Register();
                        }
                        catch (Exception ex)
                        {
                            Log.Error($"[Importer SafeGuard] An error was found inside {assembly.FullName} (Expansion), the error is: {ex.Message}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Registers a specific Placeholder.
        /// </summary>
        /// <param name="dllPath">The path for the DLL.</param>
        internal static void RegisterPlaceholdersFromExpansion(string dllPath)
        {
            if (!File.Exists(dllPath))
            {
                Log.Error($"[Importer SafeGuard] The specified DLL file does not exist: {dllPath}");
                return;
            }

            Assembly assembly;
            try
            {
                assembly = Assembly.LoadFrom(dllPath);
            }
            catch (Exception ex)
            {
                Log.Error($"[Importer SafeGuard] Failed to load assembly from {dllPath}: {ex.Message}");
                return;
            }

            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(PlaceholderExpansion)) && !type.IsAbstract)
                {
                    try
                    {
                        PlaceholderExpansion instance = (PlaceholderExpansion)Activator.CreateInstance(type);

                        instance.Register();
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"[Importer SafeGuard] An error was found inside {assembly.FullName} (Expansion), the error is: {ex.Message}");
                    }
                }
            }
        }
    }
}
