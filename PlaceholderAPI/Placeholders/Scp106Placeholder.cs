namespace PlaceholderAPI.Placeholders
{
    using Exiled.API.Features;
    using Exiled.API.Features.Roles;
    using PlaceholderAPI.API.Abstract;

    /// <summary>
    /// Implementation of the Player Placeholders.
    /// </summary>
    public class Scp106Placeholder : PlaceholderExpansion
    {
        /// <inheritdoc/>
        public override string Author { get; set; } = "NotZer0Two";

        /// <inheritdoc/>
        public override string Identifier { get; set; } = "scp106";

        /// <inheritdoc/>
        public override string RequiredPlugin { get; set; } = null;

        /// <inheritdoc/>
        public override string OnRequest(Player player, string param)
        {
            if (player.Role is not Scp106Role role)
            {
                return null;
            }

            switch (param.ToLower())
            {
                case "vigor":
                    return role.Vigor.ToString();
                case "issubmerged":
                    return role.IsSubmerged.ToString();
                case "sinkholetime":
                    return role.SinkholeCurrentTime.ToString();
                case "capturecooldown":
                    return role.CaptureCooldown.ToString();
                case "damage":
                    return role.AttackDamage.ToString();
                case "isstalking":
                    return role.IsStalking.ToString();
            }

            return null;
        }
    }
}
