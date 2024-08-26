namespace PlaceholderAPI.Patches
{
    using System;
    using Exiled.API.Features;
    using HarmonyLib;
    using Hints;
    using Mirror;
    using PlaceholderAPI.API;
    using Utils.CommandInterpolation;
    using static Broadcast;

    /// <summary>
    /// Harmony patch to replace Interpolated Message.
    /// </summary>
    [HarmonyPatch(typeof(InterpolatedCommandFormatter), nameof(InterpolatedCommandFormatter.ProcessInterpolation))]
    public static class CommandInterpolationReplacementPatch
    {
        /// <summary>
        /// Prefix method for Command Interpolation.
        /// </summary>
        /// <param name="__instance">Main Class.</param>
        /// <returns>instruction to harmony.</returns>
        public static bool Prefix(Broadcast __instance, ref string raw)
        {
            if (PlaceholderAPIPlugin.Instance.Config.Harmony.TagsAllowed.TryGetValue("CommandInterpolation", out bool value) && !value)
            {
                return true;
            }

            try
            {
                raw = PlaceholderAPI.SetPlaceholders(raw);
                return true;
            }
            catch (Exception e)
            {
                Exiled.API.Features.Log.Error($"Error when trying to parse Interpolation:\n{e}");
                return true;
            }
        }
    }
}
