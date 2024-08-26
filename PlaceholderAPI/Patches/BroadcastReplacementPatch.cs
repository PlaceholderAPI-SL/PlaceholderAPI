namespace PlaceholderAPI.Patches
{
    using System;
    using HarmonyLib;
    using Hints;
    using Mirror;
    using PlaceholderAPI.API;
    using static Broadcast;

    /// <summary>
    /// Harmony patch to replace Broadcast (Player Specific).
    /// </summary>
    [HarmonyPatch(typeof(Broadcast), nameof(Broadcast.TargetAddElement))]
    public static class BroadcastPlayerReplacementPatch
    {
        /// <summary>
        /// Prefix method for Broadcast (Player Specific).
        /// </summary>
        /// <param name="__instance">Main Class.</param>
        /// <param name="hint">Hint.</param>
        /// <returns>instruction to harmony.</returns>
        public static bool Prefix(Broadcast __instance, NetworkConnection conn, ref string data, ushort time, BroadcastFlags flags)
        {
            if (PlaceholderAPIPlugin.Instance.Config.Harmony.TagsAllowed.TryGetValue("Broadcast", out bool value) && !value)
            {
                return true;
            }

            try
            {
                Exiled.API.Features.Player player = Exiled.API.Features.Player.Get(conn);

                if (player is null)
                {
                    // How?
                    return true;
                }

                data = PlaceholderAPI.SetPlaceholders(player, data);

                return true;
            }
            catch (Exception e)
            {
                Exiled.API.Features.Log.Error($"Error when trying to parse Broadcast:\n{e}");
                return true;
            }
        }
    }

    /// <summary>
    /// Harmony patch to replace Broadcast (Player Specific).
    /// </summary>
    [HarmonyPatch(typeof(Broadcast), nameof(Broadcast.RpcAddElement))]
    public static class BroadcastReplacementPatch
    {
        /// <summary>
        /// Prefix method for Broadcast (Player Specific).
        /// </summary>
        /// <param name="__instance">Main Class.</param>
        /// <param name="hint">Hint.</param>
        /// <returns>instruction to harmony.</returns>
        public static bool Prefix(Broadcast __instance, ref string data, ushort time, BroadcastFlags flags)
        {
            if (PlaceholderAPIPlugin.Instance.Config.Harmony.TagsAllowed.TryGetValue("Broadcast", out bool value) && !value)
            {
                return true;
            }

            try
            {
                data = PlaceholderAPI.SetPlaceholders(data);

                return true;
            }
            catch (Exception e)
            {
                Exiled.API.Features.Log.Error($"Error when trying to parse Broadcast:\n{e}");
                return true;
            }
        }
    }
}
