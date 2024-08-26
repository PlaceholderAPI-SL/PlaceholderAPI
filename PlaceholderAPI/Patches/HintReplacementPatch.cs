namespace PlaceholderAPI.Patches
{
    using System;
    using Exiled.API.Features;
    using HarmonyLib;
    using Hints;
    using PlaceholderAPI.API;

    /// <summary>
    /// Harmony patch to replace Hints.
    /// </summary>
    [HarmonyPatch(typeof(HintDisplay), nameof(HintDisplay.Show))]
    public static class HintReplacementPatch
    {
        /// <summary>
        /// Prefix method for HintDisplay.
        /// </summary>
        /// <param name="__instance">Main Class.</param>
        /// <param name="hint">Hint.</param>
        /// <returns>instruction to harmony.</returns>
        public static bool Prefix(HintDisplay __instance, Hints.Hint hint)
        {
            if (PlaceholderAPIPlugin.Instance.Config.Harmony.TagsAllowed.TryGetValue("Hints", out bool value) && !value)
            {
                return true;
            }

            try
            {
                Player player = Player.Get(__instance.gameObject);

                if (player is null)
                {
                    // How?
                    return true;
                }

                if (hint.GetType() != typeof(TextHint))
                {
                    return true;
                }

                TextHint t = hint as TextHint;

                t.Text = PlaceholderAPI.SetPlaceholders(player, t.Text);
                return true;
            }
            catch (Exception e)
            {
                Log.Error($"Error when trying to parse hint:\n{e}");
                return true;
            }
        }
    }
}
