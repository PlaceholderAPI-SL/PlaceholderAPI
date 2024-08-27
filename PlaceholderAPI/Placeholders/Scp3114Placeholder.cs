namespace PlaceholderAPI.Placeholders
{
    using Exiled.API.Features;
    using Exiled.API.Features.Roles;
    using PlaceholderAPI.API.Abstract;

    /// <summary>
    /// Implementation of the Player Placeholders.
    /// </summary>
    public class Scp3114Placeholder : PlaceholderExpansion
    {
        /// <inheritdoc/>
        public override string Author { get; set; } = "NotZer0Two";

        /// <inheritdoc/>
        public override string Identifier { get; set; } = "scp3114";

        /// <inheritdoc/>
        public override string RequiredPlugin { get; set; } = null;

        /// <inheritdoc/>
        public override string OnRequest(Player player, string param)
        {
            if (player.Role is not Scp3114Role role)
            {
                return null;
            }

            switch (param.ToLower())
            {
                case "stolenidentity":
                    return role.StolenRole.ToString();
                case "disguiseduration":
                    return role.DisguiseDuration.ToString();
                case "disguise":
                    return role.Disguise.ToString();
            }

            return null;
        }
    }
}
