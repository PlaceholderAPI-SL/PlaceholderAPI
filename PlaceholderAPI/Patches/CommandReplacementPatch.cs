namespace PlaceholderAPI.Patches
{
    using System;
    using Exiled.API.Features;
    using HarmonyLib;
    using Hints;
    using PlaceholderAPI.API;
    using RemoteAdmin;

    // finally after 2 hour of debugging and seeing NW code, i got all the methods possible
    // I love NW <3

    /// <summary>
    /// Harmony patch to replace Ra Messages.
    /// </summary>
    [HarmonyPatch(typeof(PlayerCommandSender), nameof(PlayerCommandSender.RaReply))]
    public class CommandPlayerReplacementPatch
    {
        /// <summary>
        /// Prefix method for RaReply.
        /// </summary>
        /// <param name="__instance">Main Class.</param>
        /// <param name="hint">Hint.</param>
        /// <returns>instruction to harmony.</returns>
        public static bool Prefix(PlayerCommandSender __instance, ref string text, bool success, bool logToConsole, string overrideDisplay)
        {
            if (!PlaceholderAPIPlugin.Instance.Config.Harmony.Commands)
            {
                return true;
            }

            try
            {
                Player player = Player.Get(__instance);

                text = PlaceholderAPI.SetPlaceholders(player, text);
            }
            catch (Exception e)
            {
                Exiled.API.Features.Log.Error($"Error when trying to parse RaReply:\n{e}");
            }

            return true;
        }
    }

    /// <summary>
    /// Harmony patch to replace Messages.
    /// </summary>
    [HarmonyPatch(typeof(GameConsoleTransmission), nameof(GameConsoleTransmission.SendToClient))]
    public class CommandPlayerReplacementConsolePatch
    {
        /// <summary>
        /// Prefix method for RaReply.
        /// </summary>
        /// <param name="__instance">Main Class.</param>
        /// <param name="hint">Hint.</param>
        /// <returns>instruction to harmony.</returns>
        public static bool Prefix(GameConsoleTransmission __instance, ref string text, string color)
        {
            if (!PlaceholderAPIPlugin.Instance.Config.Harmony.Commands)
            {
                return true;
            }

            try
            {
                Player player = Player.Get(__instance._hub);

                text = PlaceholderAPI.SetPlaceholders(player, text);
            }
            catch (Exception e)
            {
                Exiled.API.Features.Log.Error($"Error when trying to parse RaReply:\n{e}");
            }

            return true;
        }
    }

    /// <summary>
    /// Harmony patch to replace Server RA (RA).
    /// </summary>
    [HarmonyPatch(typeof(ServerConsoleSender), nameof(ServerConsoleSender.RaReply))]
    public class CommandReplacementPatch
    {
        /// <summary>
        /// Prefix method for RaReply.
        /// </summary>
        /// <param name="__instance">Main Class.</param>
        /// <param name="hint">Hint.</param>
        /// <returns>instruction to harmony.</returns>
        public static bool Prefix(ServerConsoleSender __instance, ref string text, bool success, bool logToConsole, string overrideDisplay)
        {
            if (!PlaceholderAPIPlugin.Instance.Config.Harmony.Commands)
            {
                return true;
            }

            try
            {
                text = PlaceholderAPI.SetPlaceholders(text);
            }
            catch (Exception e)
            {
                Exiled.API.Features.Log.Error($"Error when trying to parse RaReply:\n{e}");
            }

            return true;
        }
    }

    /// <summary>
    /// Harmony patch to replace Server RA (Console).
    /// </summary>
    [HarmonyPatch(typeof(ServerConsoleSender), nameof(ServerConsoleSender.Print), typeof(string), typeof(ConsoleColor))]
    public class CommandReplacementPrintPatch
    {
        /// <summary>
        /// Prefix method for RaReply.
        /// </summary>
        /// <param name="__instance">Main Class.</param>
        /// <param name="hint">Hint.</param>
        /// <returns>instruction to harmony.</returns>
        public static bool Prefix(ServerConsoleSender __instance, ref string text, ConsoleColor c)
        {
            if (!PlaceholderAPIPlugin.Instance.Config.Harmony.Commands)
            {
                return true;
            }

            try
            {
                text = PlaceholderAPI.SetPlaceholders(text);
            }
            catch (Exception e)
            {
                Exiled.API.Features.Log.Error($"Error when trying to parse RaReply:\n{e}");
            }

            return true;
        }
    }

    /// <summary>
    /// Harmony patch to replace Messages.
    /// </summary>
    [HarmonyPatch(typeof(GameConsoleTransmission), nameof(GameConsoleTransmission.SendToServer))]
    public class CommandServerReplacementLocalPatch
    {
        /// <summary>
        /// Prefix method for RaReply.
        /// </summary>
        /// <param name="__instance">Main Class.</param>
        /// <param name="hint">Hint.</param>
        /// <returns>instruction to harmony.</returns>
        public static bool Prefix(GameConsoleTransmission __instance, ref string command)
        {
            if (!PlaceholderAPIPlugin.Instance.Config.Harmony.Commands)
            {
                return true;
            }

            try
            {
                Player player = Player.Get(__instance._hub);

                command = PlaceholderAPI.SetPlaceholders(player, command);
                Log.Info(command);
            }
            catch (Exception e)
            {
                Exiled.API.Features.Log.Error($"Error when trying to parse RaReply:\n{e}");
            }

            return true;
        }
    }
}
