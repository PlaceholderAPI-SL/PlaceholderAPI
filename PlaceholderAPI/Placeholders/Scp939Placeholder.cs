namespace PlaceholderAPI.Placeholders
{
    using Exiled.API.Features;
    using Exiled.API.Features.Roles;
    using PlaceholderAPI.API.Abstract;

    /// <summary>
    /// Implementation of the Player Placeholders.
    /// </summary>
    public class Scp939Placeholder : PlaceholderExpansion
    {
        /// <inheritdoc/>
        public override string Author { get; set; } = "NotZer0Two";

        /// <inheritdoc/>
        public override string Identifier { get; set; } = "scp939";

        /// <inheritdoc/>
        public override string RequiredPlugin { get; set; } = null;

        /// <inheritdoc/>
        public override string OnRequest(Player player, string param)
        {
            if (player.Role is not Scp939Role role)
            {
                return null;
            }

            switch (param.ToLower())
            {
                case "attackcooldown":
                    return role.AttackCooldown.ToString();
                case "isfocused":
                    return role.IsFocused.ToString();
                case "amnesticcloudcooldown":
                    return role.AmnesticCloudCooldown.ToString();
                case "amnesticcloud":
                    return role.AmnesticCloudDuration.ToString();
                case "mimicrycooldown":
                    return role.MimicryCooldown.ToString();
                case "voices":
                    return role.SavedVoices.ToString();
                case "ismimicry":
                    return role.MimicryPointActive.ToString();
            }

            return null;
        }
    }
}
