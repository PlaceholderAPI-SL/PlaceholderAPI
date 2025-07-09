namespace PlaceholderAPI.Patches
{
    using System;
    using Exiled.API.Features;
    using HarmonyLib;
    using PlaceholderAPI.API;

    /// <summary>
    /// Harmony patch to replace CustomNickname.
    /// </summary>
    [HarmonyPatch(typeof(NicknameSync), nameof(NicknameSync.DisplayName), MethodType.Setter)]
    public class PlayerReplacementPatch
    {
        /// <summary>
        /// Prefix method for RaReply.
        /// </summary>
        /// <param name="__instance">Main Class.</param>
        /// <param name="hint">Hint.</param>
        /// <returns>instruction to harmony.</returns>
        public static bool Prefix(NicknameSync __instance, ref string value)
        {
            if (!PlaceholderAPIPlugin.Instance.Config.Harmony.Commands)
            {
                return true;
            }

            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            try
            {
                Player player = Player.Get(__instance._hub);

                value = PlaceholderAPI.SetPlaceholders(player, value);
            }
            catch (Exception e)
            {
                Exiled.API.Features.Log.Error($"Error when trying to parse RaReply:\n{e}");
            }

            return true;
        }
    }


    /// <summary>
    /// Harmony patch to replace CustomNickname.
    /// </summary>
    [HarmonyPatch(typeof(NicknameSync), nameof(NicknameSync.Network_customPlayerInfoString), MethodType.Setter)]
    public class PlayerReplacementCustomInfoPatch
    {
        /// <summary>
        /// Prefix method for RaReply.
        /// </summary>
        /// <param name="__instance">Main Class.</param>
        /// <param name="hint">Hint.</param>
        /// <returns>instruction to harmony.</returns>
        public static bool Prefix(NicknameSync __instance, ref string value)
        {
            if (!PlaceholderAPIPlugin.Instance.Config.Harmony.Commands)
            {
                return true;
            }

            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            try
            {
                Player player = Player.Get(__instance._hub);

                value = PlaceholderAPI.SetPlaceholders(player, value);
            }
            catch (Exception e)
            {
                Exiled.API.Features.Log.Error($"Error when trying to parse RaReply:\n{e}");
            }

            return true;
        }
    }

    /// <summary>
    /// Harmony patch to replace CustomNickname.
    /// </summary>
    [HarmonyPatch(typeof(ServerRoles), nameof(ServerRoles.SetText))]
    public class PlayerRankReplacementPatch
    {
        /// <summary>
        /// Prefix method for RaReply.
        /// </summary>
        /// <param name="__instance">Main Class.</param>
        /// <param name="hint">Hint.</param>
        /// <returns>instruction to harmony.</returns>
        public static bool Prefix(ServerRoles __instance, ref string i)
        {
            if (!PlaceholderAPIPlugin.Instance.Config.Harmony.Commands)
            {
                return true;
            }

            if (string.IsNullOrEmpty(i))
            {
                return true;
            }

            try
            {
                Player player = Player.Get(__instance._hub);

                i = PlaceholderAPI.SetPlaceholders(player, i);
            }
            catch (Exception e)
            {
                Exiled.API.Features.Log.Error($"Error when trying to parse RaReply:\n{e}");
            }

            return true;
        }
    }
}
