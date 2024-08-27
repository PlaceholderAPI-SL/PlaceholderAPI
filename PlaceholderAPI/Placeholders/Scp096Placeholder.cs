namespace PlaceholderAPI.Placeholders
{
    using Exiled.API.Features;
    using Exiled.API.Features.Roles;
    using PlaceholderAPI.API.Abstract;

    /// <summary>
    /// Implementation of the Player Placeholders.
    /// </summary>
    public class Scp096Placeholder : PlaceholderExpansion
    {
        /// <inheritdoc/>
        public override string Author { get; set; } = "NotZer0Two";

        /// <inheritdoc/>
        public override string Identifier { get; set; } = "scp096";

        /// <inheritdoc/>
        public override string RequiredPlugin { get; set; } = null;

        /// <inheritdoc/>
        public override string OnRequest(Player player, string param)
        {
            if (player.Role is not Scp096Role role)
            {
                return null;
            }

            switch (param.ToLower())
            {
                case "chargecooldown":
                    return role.ChargeCooldown.ToString();
                case "chargeduration":
                    return role.RemainingChargeDuration.ToString();
                case "enragecooldown":
                    return role.EnrageCooldown.ToString();
                case "enragedtime":
                    return role.EnragedTimeLeft.ToString();
                case "istryingnottocry":
                    return role.TryNotToCryActive.ToString();
            }

            return null;
        }
    }
}
