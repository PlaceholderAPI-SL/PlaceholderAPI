namespace PlaceholderAPI.Placeholders
{
    using Exiled.API.Features;
    using Exiled.API.Features.Roles;
    using PlaceholderAPI.API.Abstract;

    /// <summary>
    /// Implementation of the Player Placeholders.
    /// </summary>
    public class Scp173Placeholder : PlaceholderExpansion
    {
        /// <inheritdoc/>
        public override string Author { get; set; } = "NotZer0Two";

        /// <inheritdoc/>
        public override string Identifier { get; set; } = "scp173";

        /// <inheritdoc/>
        public override string RequiredPlugin { get; set; } = null;

        /// <inheritdoc/>
        public override string OnRequest(Player player, string param)
        {
            if (player.Role is not Scp173Role role)
            {
                return null;
            }

            switch (param.ToLower())
            {
                case "breakingneckcooldown":
                    return role.RemainingBreakneckCooldown.ToString();
                case "tantrumcooldown":
                    return role.RemainingTantrumCooldown.ToString();
                case "isobserved":
                    return role.IsObserved.ToString();
                case "blinkcooldown":
                    return role.BlinkCooldown.ToString();
                case "isbreakingneck":
                    return role.BreakneckActive.ToString();
            }

            return null;
        }
    }
}
